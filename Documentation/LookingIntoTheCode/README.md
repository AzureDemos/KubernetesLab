[![banner](../images/banner-lab.png)](../../README.md)

# Looking into the code

> Need to fill this out a little more...

## Configs when running locally

Within the Website there is a folder called Configs and sub folder called development. 
This contains the Json and XML Configs and a secrets config that are read upon application start up and loaded into the apps configuration. 

![AKS Config](images/configs-dev.png)

## Configs when running in AKS

When the the website is deployed into AKS we will use Config Maps and Secrets to mount the same files (with different values) under a folder called Kubernetes. 

![AKS Config](images/configs-aks.png)

## Loading the Config

The application start up uses the environment name to determine the location of the configs. See below

![Start Up](images/startup.png)

When running in debug this location is Development, but within our YAML files we will set the environment name to match the location of the configs folder as shown below. 

![YAML](images/yamlconfigs.png)

> Note, our release pipelines will replace the value ```#{EnvironmentName}#``` with values defined for each environment 


## IOC / Dependency Injection

Two Services have been created: 

1. Service Locator - This returns the URI of any other service in the cluster if you pass its name
2. APIService - A class that will make a HTTP Get to our API and return the result

![Start Up](images/services.png)

Within the CongigureServices method we have registered these services and the configuration into the IOC container, so they can be injected into our HomeController.cs

![Start Up](images/ioc.png)


Home Controller

The HomeController takes the Configuration, ServiceLocator and APIService in its constructor.
It then call thes API and builds a Model to display the config to the user and the response from the API

![Start Up](images/homectrl.png)


# Next Steps

### [Create Build Pipelines](../BuildPipelines)
