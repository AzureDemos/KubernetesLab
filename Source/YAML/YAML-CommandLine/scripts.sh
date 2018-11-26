
#************************ Connecting to the Cluster *************************
#****************************************************************************

# Get Credentials for the cluster in Azure
az aks get-credentials --resource-group aksDemo --name aksCluster

# Browse the Kubernetes Dashboard
az aks browse --resource-group aksDemo --name aksCluster

# Go to correct folder
cd C:\Users\stleonar\Documents\GitHub\KubernetesLab\Source\YAML



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

# Create / update config map (inline json)
kubectl apply -f ./Website/configmap-website.yaml --namespace=dev

# Delete existing config map external xml
kubectl delete configmap config-xml-website --namespace=dev

# Create config map (external xml file)
kubectl create configmap config-xml-website --from-file=./Website/configmap-website.xml --namespace=dev

# Create / update website
kubectl apply -f ./Website/deployment-website.yaml --namespace=dev

# Scale 
kubectl scale deployment website-deployment --replicas=2 --namespace=dev

# Clear everything in the dev namespace
kubectl delete daemonsets,replicasets,services,deployments,pods,secret,configmaps,rc --namespace=dev --all


