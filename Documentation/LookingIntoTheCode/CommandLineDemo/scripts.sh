
# Connecting to the cluster and setting the correct path
Use the Visual studio code extension to connect to the cluster, then right click this scripts.sh file and choose open in terminal

# Create a new namesapce for these demo's
kubectl create namespace demo

# Label the Kube System Namespace
kubectl label namespace/kube-system name=kube-system

# Apply Default Limit Ranges
kubectl apply -f ./limit-range-memory.yaml -n demo

# Deploy the API
kubectl apply -f ./deployment-api.yaml -n demo

# Deploy the website
kubectl apply -f ./deployment-website.yaml -n demo

# Deploy the Ingress and Policies
kubectl apply -f ./ingress-rules.yaml -n demo

# Apply Network Policies
kubectl apply -f ./network-policy.yaml -n demo
---------------------------------------------------------------------------------------------------------------------------

# Delete these deployments
kubectl delete -f ./ingress-rules.yaml -n demo
kubectl delete -f ./deployment-api.yaml -n demo
kubectl delete -f ./deployment-website.yaml -n demo
kubectl delete -f ./network-policy.yaml -n demo


---------------------------------------------------------------------------------------------------------------------------

# No need run this as the delete deployments will remove everything, but here is example of deleting everything from a namespace
kubectl delete daemonsets,replicasets,services,deployments,pods,secret,ingress,configmaps,rc,networkpolicy --namespace=demo --all





