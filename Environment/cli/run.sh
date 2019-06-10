# Sets a random resource and resource group name
RG_NAME=aks$(date +%s%N | md5sum | cut -c1-6)
 
#Create Resource Group
az group create --name $RG_NAME --location westeurope
 
# Create Container Registry (ACR)
ACR_Name="${RG_NAME}ACR"
az acr create --resource-group $RG_NAME --name $ACR_Name --sku Standard --admin-enabled
 
# Create Cluster
AKS_Name="${RG_NAME}-akscluster"
az aks create \
    --resource-group $RG_NAME \
    --name $AKS_Name \
    --node-count 3 \
    --enable-addons http_application_routing \
    --generate-ssh-keys \
    --network-plugin azure \
    --network-policy azure 
 
# Connect to cluster
az aks get-credentials --resource-group $RG_NAME --name $AKS_Name
 
# Create Namespaces
kubectl create namespace dev
kubectl create namespace prod

# Create ClusterRoleBinding for Dashboard
kubectl create clusterrolebinding kubernetes-dashboard -n kube-system --clusterrole=cluster-admin --serviceaccount=kube-system:kubernetes-dashboard

# Install Nginx
helm init
kubectl create clusterrolebinding kubernetes-default -n kube-system --clusterrole=cluster-admin --serviceaccount=kube-system:default
sleep 60s # hack needed to give tiller to sort its self out.
helm install stable/nginx-ingress