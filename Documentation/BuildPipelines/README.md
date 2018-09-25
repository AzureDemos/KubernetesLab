[![banner](../images/banner-lab.png)](../../README.md)

# New API build pipeline

Within your Azure DevOps project, select Builds from the Pipelines menu and create a new Build Pipeline. 

## 1. Select the source for the build

Select Azure Repos Git as the source and choose your imported repository. 

![Create new build](images/newbuildpipeline.png)

## 2. Create empty build pipeline

Under the templates selection, choose Empty Pipeline.

![Empty Template](images/emptybuild.png)

## 3. Select Linux Agent Pool

Choose Hosted Linux as the agent pool and rename your Pipeline to ```API-Build-Pipeline```

![Linux Agent](images/linuxagentbuild.png)

## 4. Add Docker Build Step (API Build)

Click the plus icon to the far right of "Agent Job 1" to bring up the new build step selection window. 

Search for "Docker" and add the Docker build step. 

![Add Docker Build Step](images/adddockerbuildstep.png)

### 4.1 Edit Docker Build Step (API Build)

In the edit window for the Docker build step, set the following properties:

1. Name - Build API Image
2. Container Registry Type - Azure Container Registry
3. Azure Subscription - Chose the subscription your ACR and AKS reside
4. Azure Container Registry - Select your ACR from the drop down
5. Command - Build
6. Dockerfile - Click on the ... to open a pop up window and chose the file ```Source/AKSAPI/Dockerfile.ci```
7. Image name - ```api:$(Build.BuildId)```
8. Check the box "Include Latest Tag"

![Edit Build Docker](images/apibuild.png)

## 5. Add another Docker Build Step (API Push)

Follow the same process as Step 4. Add Docker Build Step (API Build)

### 5.1 Edit Docker Build Step (API Push)

In the edit window for the Docker build step, set the following properties:

1. Name - Push API Image
2. Container Registry Type - Azure Container Registry
3. Azure Subscription - Chose the subscription your ACR and AKS reside
4. Azure Container Registry - Select your ACR from the drop down
5. Command - Push
6. Image name - ```api:$(Build.BuildId)```
7. Check the box "Include Latest Tag"

![Edit Push Docker](images/pushapibuild.png)


## 6. Publish Build Artificats

Add a third build step and search for "publish" then add the ```Publish Build Artifacts``` task.

![Publish Artifacts](images/publishbuildartifacts.png)

### 6.1. Choose the folder to publish

Edit the Publish Build Artifacts task, and lcik on the ... next to "Path to Publish"

In the pop up window select the ```deployment-api.yaml``` file as shown in the image below. 

![Path to Publish](images/publishapibuild.png)


## 7. Enable Continous Integration

Click on the "Tiggers" tab and check the box "Enable continous integration"

![Enable CI](images/enablebuildci.png)

### 8. Trigger a new build

To test everything is working, click the Save and Queue new build button to trigger a new build. 

# New Website build pipeline

Follow the same steps as the API build pipeline line with the following changes: 

1. Set the title as ```Website Build Pipeline```
2. Set the Docker File in the docker build steps as ```Source/AKSWebsite/Dockerfile.ci```
3. Set the Image Name in the docker build steps to ```website:$(Build.BuildId)```
4. Set the Path to Publish in the Publish Artifacts step to ```Source/YAML/Website```

# Review

After following this you should now have two build pipelines; one for the API and one for the website which both push Docker images into your ACR with their build number and the latest tag and then publish the YAML files required to deploy the applications, ready for the release pipelines that we will build next to pickup. 

# Next Steps 
### [Create Release Pipelines](../ReleasePipelines)