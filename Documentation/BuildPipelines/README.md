[![banner](../images/banner-lab.png)](../../README.md)

# New API build pipeline

Within your Azure DevOps project, select Builds from the Pipelines menu and create a new Build Pipeline. 

Select Azure Repos Git as the source and choose your imported repository. 

![Create new build](images/newbuildpipeline.png)

## Create empty build pipeline

Under the templates selection, choose Empty Pipeline.

![Empty Template](images/emptybuild.png)

### Select Linux Agent Pool

Choose Hosted Linux as the agent pool and rename your Pipeline to ```API-Build-Pipeline```

![](images/linuxagentbuild.png)

### Add Docker Build Step

Click the plus icon to the far right of "Agent Job 1" to bring up the new build step selection window. 

Search for "Docker" and add the Docker build step. 

![](images/adddockerbuildstep.png)

### Edit Docker Build Step

In the edit window for the Docker build step, set the following properties:

1. Name - Build API Image
2. Container Registry Type - Azure Container Registry
3. Azure Subscription - Chose the subscription your ACR and AKS reside
4. Azure Container Registry - Select your ACR from the drop down
Command - Build
Dockerfile - Click on the ... to open a pop out window and chose the file ```Source/AKSAPI/Dockerfile.ci```
5. Image name - api:$(Build.BuildId)
6. Check the box Include Latest Tag

![](images/apibuild.png)

### Push API Build

![](images/pushapibuild.png)


### Publish Build Artificats

![](images/publishbuildartifacts.png)

#### Choose the folder to publish

![](images/publishapibuild.png)


### Enable CI

![](images/enablebuildci.png)

### Trigger new build
Click save and queue

# New Website build pipeline

Follow the same steps as the API build pipeline line with the following changes: 

1. Set the title as ```Website Build Pipeline```
2. Set the Docker File in the docker build steps as ```Source/AKSWebsite/Dockerfile.ci```
3. Set the Image Name in the docker build steps to ```website:$(Build.BuildId)```
4. Set the Path to Publish in the Publish Artifacts step to ```Source/YAML/Website```


# Next Steps 
### [Create Release Pipelines](../ReleasePipelines)