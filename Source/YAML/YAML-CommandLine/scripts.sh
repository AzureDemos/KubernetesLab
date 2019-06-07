
#************************ Connecting to the Cluster *************************
#****************************************************************************

# Get Credentials for the cluster in Azure

az aks get-credentials --name <CLUSTER_NAME> --resource-group <RESOURCE_GROUP_NAME>

# Browse the Dashboard
az aks browse --name <CLUSTER_NAME> --resource-group <RESOURCE_GROUP_NAME>

# Get connection details from the portal

# Default Limit Range
kubectl apply -f ./limit-range-memory.yaml --namespace=dev

#************************ Deploying the API *********************************
#****************************************************************************

# Create / update the API
kubectl apply -f ./API/deployment-api.yaml --namespace=dev


#************************ Deploying the Website *****************************
#****************************************************************************

# Create / update website
kubectl apply -f ./Website/deployment-website.yaml --namespace=dev




# Scale 
kubectl scale deployment website-deployment --replicas=2 --namespace=dev


------------------------------------- Dont Run this --------------------------------------
kubectl delete -f ./API/deployment-api.yaml --namespace=dev

kubectl delete -f ./Website/deployment-website.yaml --namespace=dev

# Clear everything in the dev namespace except the secrets
kubectl delete daemonsets,replicasets,services,deployments,pods,configmaps,rc --namespace=dev --all

# Clear everything in the dev namespace
kubectl delete daemonsets,replicasets,services,deployments,pods,secret,configmaps,rc --namespace=dev --all





