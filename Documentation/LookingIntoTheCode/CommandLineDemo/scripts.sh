
# Connecting to the cluster and setting the correct path
Use the Visual studio code extension to connect to the cluster, then right click this scripts.sh file and choose open in terminal

# Create a new namesapce for these demo's
kubectl create namespace demo

# Apply Default Limit Ranges
kubectl apply -f ./limit-range-memory.yaml --namespace=demo

# Deploy the API
kubectl apply -f ./deployment-api.yaml --namespace=demo

# Deploy the website
kubectl apply -f ./deployment-website.yaml --namespace=demo


---------------------------------------------------------------------------------------------------------------------------

# Delete these deployments
kubectl delete -f ./deployment-api.yaml --namespace=demo
kubectl delete -f ./deployment-website.yaml --namespace=demo


---------------------------------------------------------------------------------------------------------------------------

# No need run this as the delete deployments will remove everything, but here is example of deleting everything from a namespace
kubectl delete daemonsets,replicasets,services,deployments,pods,secret,ingress,configmaps,rc --namespace=demo --all





