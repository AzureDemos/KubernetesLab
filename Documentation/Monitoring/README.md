[![banner](../images/banner-lab.png)](../../README.md)

# Monitoring AKS

## AKS Container Insights (preview)

To set up and use AKS Container Insights, use the following command to install the the monitoring agent daemonset on our Kubernetes cluster:

```PowerShell
az aks enable-addons -a monitoring --name <AKSname> --resource-group <resourcegroup>
```

![](https://raw.githubusercontent.com/markharrisonuk/Lab_AKS/master/Images/EnableAddon.png)

- In the Azure portal - select the AKS Monitoring blade

![](https://raw.githubusercontent.com/markharrisonuk/Lab_AKS/master/Images/MonitorCluster.png)

- Switch to examine Nodes | Controllers | Container views 

![](https://raw.githubusercontent.com/markharrisonuk/Lab_AKS/master/Images/MonitorNodes.png)

- Select a container, on the right hand side select `View Container logs`

![](https://raw.githubusercontent.com/markharrisonuk/Lab_AKS/master/Images/ImagesMonitorLogs.png)

## Tidy Up

We can use the following command to remove the monitoring agent daemonset

```text
az aks disable-addons -a monitoring --name <AKSname> --resource-group <resourcegroup>
```

## More monitoring options

- [More options on monitoring AKS](https://github.com/markharrisonuk/Lab_AKS/blob/master/aks-3.md)



