
# Connecting to the cluster and setting the correct path
# Use the Visual studio code extension to connect to the cluster, then right click this scripts.sh file and choose open in terminal
# If running inside a remote window you may need to select View > Terminal in the top window and then nagigate to the scripts location
# run > cd Documentation\LookingIntoTheCode\CommandLineDemo

-----------------------------------------------------------------------------------------------------------------------

# Create a new namesapce for these demo's - Only run this once
kubectl create namespace demo

# Label the Kube System Namespace - Only run this once
kubectl label namespace/kube-system name=kube-system

# Apply Default Limit Ranges - Only run this once
kubectl apply -f ./limit-range-memory.yaml -n demo

-------------------------------------------------------------------------------------------------------------------------
# Copy and past all of these terminal

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
kubectl delete daemonsets,replicasets,services,deployments,pods,secret,ingress,configmaps,rc,networkpolicy -n demo --all





