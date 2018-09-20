
## Setting up our environment using the Azure CLI

### Create resource group

```
az group create --name aksDemo --location westeurope
```

### Create cluster with application routing

```
az aks create --resource-group aksDemo --name aksCluster --node-count 3 --enable-addons http_application_routing --generate-ssh-keys
```

### Create an Azure Container Registry

It may be worthwhile putting the registry in a different resource group to the cluster
```
az group create --name acrDemo --location westeurope
az acr create --resource-group acrDemo --name stevesACR --sku Basic
```



## Create Azure DevOps Project
![Create DevOps Project](images/createproject.png)


