[![banner](../../images/banner-lab.png)](../../../README.md)

# Setting up your environment using the Terraform

Before starting you should have already installed the Azure CLI and logged in as well as having [Terraform](https://www.terraform.io/) installed. 

Unlike the Azure CLI, Terraform is a 3rd party product, while this has a negative impact that means it is always behind in Azure resource compatiability compared to the Azure CLI it has several postives that make it a great tool to work with. The key ones we will utilise in this lab will be:

- Ability to mix and match providers, meaning we can use the Azure and the Kubernetes native providers.
- State management. Terraform tracks state of the environment, allowing it to run a diff when adding new resources as well as fixing the environment if things are removed.

## setup Azure DevOps (Optional)

To ensure you only have one copy of the code on your local machine, we recommended importing the GitHub repo into Azure DevOps first, then clone from there.

[DevOps Setup](../../DevOpsSetup)


## Switch Directory

From the root directory switch to "/Environment/Terraform"

## Create RSA SSH Keys (Optional)

The Linux VMs require SSH Keys to be created, while the Azure CLI allows you to pass in "--generate-ssh-keys" as an optional parameter to create the SSH Keys, this is actually a custom feature to the CLI and not part of the Azure RM API. Because Terraform is based off the Azure GO SDK which is generated with AutoRest From the Azure RM API we need to generate the keys manually. 

In a new terminal, run the following replacing the comment "-C" and the passphrase "-N"

```
ssh-keygen -t rsa -C "email@email.com" -N "somepassphrase" -f id_rsa
```

## Initalize Terraform

This will download the required providers ready for use.

```
terraform init
```

## Build a plan (Optional)

This step will allow Terraform to build its plan and highlight what it plans to create, update and delete against your environment.

```
terraform plan -var "resource_name={replace}"
```

### Execute terraform

Finally we need to run terraform with specific variables to provision our environment. The resource name should be lowercase, not contain any special characters and be 10 or less characeters long.

```
terraform apply -var "resource_name={replace}"
```

# Next Steps

### [Setup Azure DevOps project](../../DevOpsSetup)