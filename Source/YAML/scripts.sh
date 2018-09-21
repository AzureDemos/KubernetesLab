
GET KUBE CONFIG
az aks get-credentials -g aks -n slaks -f - > .kube\config
Then open the editor in the cloud shell by clickuing the icon - opens as VS CODE editor 

kubectl create secret docker-registry regsecret --docker-server=stevescontainers.azurecr.io --docker-username=stevescontainers --docker-password=J2G7qvJdcidy6UhWWROH=pPU1Tjb6UQj --docker-email=stleonar@micrososft.com



http://default-frontend-service2.55474723f3b7466899bc.westeurope.aksapp.io

http://dev-sampleapp.4f02b96afa9f44b084dc.northeurope.aksapp.io/



App Settings
--------------------------------------------------------------------------------------
Type        FileName                    Values                      Location
--------------------------------------------------------------------------------------
Env Vars    - NA                        - EnvironmentName           NA
Internal    - appsettings.json          - App:CodeVersion           AppRoot
ConfigMap   - configmap-frontend.xml    - App:Theme                 AppRoot/configs
ConfigMap   - configmap-frontend.json   - API:URI                   AppRoot/configs
Secret      - secret-frontend.json      - API:Key, API:Secret      AppRoot/secrets

#Environment variable for backend api
BACKEND_SERVICE_PORT

*****************************************************************************

#get creds
az aks get-credentials --resource-group aksDemo --name aksCluster

#browse dashboard
az aks browse --resource-group aksDemo --name aksCluster

#go to correct folder
cd C:\Users\stleonar\Documents\GitHub\KubernetesLab\Source\YAML

#create namespace 
kubectl create -f namespace-dev.yaml
kubectl create -f namespace-prod.yaml


#delete existing secret
kubectl delete secret website-secret --namespace=dev
#VSTS make sure outputformat is empty

#create secret from file
kubectl create secret generic website-secret --from-file=./Website/secret-website.json --namespace=dev

#create / update config map - inline json
kubectl apply -f ./Website/configmap-website.yaml --namespace=dev

#delete existing config map external xml
kubectl delete configmap config-xml-website --namespace=dev
#VSTS make sure outputformat is empty

#create config map external xml
kubectl create configmap config-xml-website --from-file=./Website/configmap-website.xml --namespace=dev

#create / update application
kubectl apply -f ./Website/deployment-website.yaml --namespace=dev

#scale 
kubectl scale deployment website-deployment --replicas=2

#view service
IPAddress
my-svc.my-namespace.svc.cluster.local
e.g. http://frontend-service.dev.55474723f3b7466899bc.westeurope.aksapp.io
http://dev-frontend-service.55474723f3b7466899bc.westeurope.aksapp.io

#clear namespace
kubectl delete daemonsets,replicasets,services,deployments,pods,secret,configmaps,rc --namespace=dev --all


