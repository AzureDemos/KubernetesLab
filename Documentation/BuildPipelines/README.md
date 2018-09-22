[![banner](../images/banner-lab.png)](../../README.md)

# New API build pipeline

![Create DevOps Project](images/newbuildpipeline.png)

## Create empty piple line

![](images/emptybuild.png)

### linux agent - name it

![](images/linuxagentbuild.png)

### add docker build step

![](images/adddockerbuildstep.png)


### API build step

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