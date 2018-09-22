[![banner](../images/banner-lab.png)](../../README.md)

# Setting up your environment using the Azure CLI

## Create resource group

```
az group create --name aksDemo --location westeurope
```

## Create cluster with application routing and role based access control

```
az aks create --resource-group aksDemo --name aksCluster --node-count 3 --enable-addons http_application_routing --generate-ssh-keys -r
```

## Create an Azure Container Registry

It may be worthwhile putting the registry in a different resource group to the cluster
```
az group create --name acrDemo --location westeurope
az acr create --resource-group acrDemo --name azureDemosACR --sku Basic
```

## Authenticate with Azure Container Registry from Azure Kubernetes Service

When you're using Azure Container Registry (ACR) with Azure Kubernetes Service (AKS), an authentication mechanism needs to be established. 

Run this script in the Azure Portal cloud shell to create a new service principal with read only access to your registry. Make sure to replace ```<YOUR_ACR_NAME>``` with the name of your registry.


![Authenticate ACR](images/opencloudshell.png)

```
#!/bin/bash

ACR_NAME=<YOUR_ACR_NAME>
SERVICE_PRINCIPAL_NAME=<YOUR_ACR_NAME>-acr-service-principal

# Populate the ACR login server and resource id.
ACR_LOGIN_SERVER=$(az acr show --name $ACR_NAME --query loginServer --output tsv)
ACR_REGISTRY_ID=$(az acr show --name $ACR_NAME --query id --output tsv)

# Create a 'Reader' role assignment with a scope of the ACR resource.
SP_PASSWD=$(az ad sp create-for-rbac --name $SERVICE_PRINCIPAL_NAME --role Reader --scopes $ACR_REGISTRY_ID --query password --output tsv)

# Get the service principal client id.
CLIENT_ID=$(az ad sp show --id http://$SERVICE_PRINCIPAL_NAME --query appId --output tsv)

# Output used when creating Kubernetes secret.
echo "Service principal ID: $CLIENT_ID"
echo "Service principal password: $SP_PASSWD"
```
> Make sure to take note of the Service principal ID and Service principal password from the output

### Create a Kubernetes Secret

Now we've created a service principal that has read access to our registry, we can create an image pull secret in Kubernetes that the deployments will use later.

#### First connect to your cluster

First you will need to connect to your cluster. Open the portal, navigate to your AKS cluster, then click on the "View Kubernetes Dashboard" link and follow the instructions.

![Authenticate ACR](images/getclustercreds.png)

Once your command window is connected to your cluster we can run a command to install a secret into the cluster. The email address can be anything, its not really used.

```
kubectl create secret docker-registry acr-auth --docker-server <acr-login-server> --docker-username <service-principal-ID> --docker-password <service-principal-password> --docker-email <email-address>
```


#### Thoubleshooting 

If you have difficulties creating the Service Principal, you can enable the admin user role for your Azure Container Registry and use the UserName and Password in place of the ```<service-principal-ID>``` and ```<service-principal-password>```

This will work, the reason we don't recommend doing this, is because the service principal only has read accesss, where as the admin user role will have full access to the registry. 

![Authenticate ACR](images/acrenableadmin.png)

More info on this subject can be found here at  https://docs.microsoft.com/en-us/azure/container-registry/container-registry-auth-aks

# Next Steps 
### [Setup Azure DevOps project](../DevOpsSetup)