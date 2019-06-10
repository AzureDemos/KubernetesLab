# Sets a random resource and resource group name
RG_NAME=aks$(date +%s%N | md5sum | cut -c1-6)
 
#Create Resource Group
az group create --name $RG_NAME --location westeurope
 
# Create Container Registry (ACR)
ACR_Name="${RG_NAME}ACR"
az acr create --resource-group $RG_NAME --name $ACR_Name --sku Basic
 
# Create Service Principal (SP)
SERVICE_PRINCIPAL_NAME="${ACR_Name}-acr-service-principal"
 
# Get the ACR login server URI and resource id.
ACR_LOGIN_SERVER=$(az acr show --name $ACR_Name --query loginServer --output tsv)
ACR_REGISTRY_ID=$(az acr show --name $ACR_Name --query id --output tsv)
 
# Create a 'Reader' role assignment with a scope of the ACR resource.
SP_PASSWD=$(az ad sp create-for-rbac --name $SERVICE_PRINCIPAL_NAME --role Reader --scopes $ACR_REGISTRY_ID --query password --output tsv)
 
# Get the service principal client id.
CLIENT_ID=$(az ad sp show --id http://$SERVICE_PRINCIPAL_NAME --query appId --output tsv)
 
# Output used when creating Kubernetes secret.
echo "Service principal ID: $CLIENT_ID"
echo "Service principal password: $SP_PASSWD"
 
# Create Cluster
AKS_Name="${RG_NAME}-akscluster"
az aks create \
    --resource-group $RG_NAME \
    --name $AKS_Name \
    --node-count 1 \
    --enable-addons http_application_routing \
    --generate-ssh-keys \
    --network-plugin azure \
    --network-policy azure 
 
# Connect to cluster
az aks get-credentials --resource-group $RG_NAME --name $AKS_Name
 
# Create Namespaces
kubectl create namespace dev
kubectl create namespace prod

# Create secret to connect to ACR for each namespace and the default
kubectl create secret docker-registry acr-auth --docker-server $ACR_LOGIN_SERVER --docker-username $CLIENT_ID --docker-password $SP_PASSWD --docker-email k8slab@azuredemos.com
kubectl create secret docker-registry acr-auth --docker-server $ACR_LOGIN_SERVER --docker-username $CLIENT_ID --docker-password $SP_PASSWD --docker-email k8slab@azuredemos.com --namespace=dev
kubectl create secret docker-registry acr-auth --docker-server $ACR_LOGIN_SERVER --docker-username $CLIENT_ID --docker-password $SP_PASSWD --docker-email k8slab@azuredemos.com --namespace=prod

# Create ClusterRoleBinding for Dashboard
kubectl create clusterrolebinding kubernetes-dashboard -n kube-system --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

# Install Nginx
helm init
kubectl create clusterrolebinding kubernetes-default -n kube-system --clusterrole=cluster-admin --serviceaccount=kube-system:default
sleep 60s # hack needed to give tiller to sort its self out.
helm install stable/nginx-ingress