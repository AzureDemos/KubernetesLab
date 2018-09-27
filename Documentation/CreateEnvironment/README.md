[![banner](../images/banner-lab.png)](../../README.md)

# Setting up your environment

## Environment provisioning options

There are several approaches you can use to provision your environment, including ARM templates, PowerShell, Terraform and Azure CLI. They all have their advantages and disadvantages which is a whole sperate topic. 

For the sake of this lab with have included Azure CLI and Terraform

## Login with the Azure CLI

Before running through either of the options below you first need to login using the Azure CLI

```
az login
```

You then have the optional step of setting which subscription you would like to use in the current context.

```
az account set --subscription {subscription_guid}
```

### [1. Using the Azure CLI](AzureCLI)

### [2. Using Terraform](Terraform)

# Review

Now you should have successfully created a Kubernetes cluster (AKS) and an Azure Container Registry (ACR) to store your Docker images. You have also given AKS the authenication required to pull images from your private ACR. 