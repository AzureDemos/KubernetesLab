
#************************ Connecting to the Cluster *************************
#****************************************************************************

# Get Credentials for the cluster in Azure

az aks get-credentials --name <CLUSTER_NAME> --resource-group <RESOURCE_GROUP_NAME>

# Browse the Dashboard
az aks browse --name <CLUSTER_NAME> --resource-group <RESOURCE_GROUP_NAME>

# Get connection details from the portal

# Limit Range
kubectl apply -f ./limit-range-memory.yaml --namespace=dev

#************************ Deploying the API *********************************
#****************************************************************************

# Create / update the API
kubectl apply -f ./API/deployment-api.yaml --namespace=dev


#************************ Deploying the Website *****************************
#****************************************************************************

# Delete existing secret
kubectl delete secret website-secret --namespace=dev

# Create secret from file
kubectl create secret generic website-secret --from-file=./Website/secret-website.json --namespace=dev


kubectl create secret generic sss --from-file=./Website/secret-website.json --dry-run -o json | kubectl apply -f - --namespace=dev

# Create / update config map (inline json)
kubectl apply -f ./Website/configmap-website.yaml --namespace=dev


# Create / update website
kubectl apply -f ./Website/deployment-website.yaml --namespace=dev

# Scale 
kubectl scale deployment website-deployment --replicas=2 --namespace=dev



------------------------------------- Dont Run this --------------------------------------



# Clear everything in the dev namespace except the secrets
kubectl delete daemonsets,replicasets,services,deployments,pods,configmaps,rc --namespace=dev --all

# Clear everything in the dev namespace
kubectl delete daemonsets,replicasets,services,deployments,pods,secret,configmaps,rc --namespace=dev --all






