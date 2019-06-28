[![banner](../images/banner-lab.png)](../../README.md)

# Ingress Controller

In this section we will look at ingress controllers and their uses. For this specific lab we will focus on using Nginx, but there are many other 3rd party options that all work against the same Kubernetes interface.

Ingress implementations through products like Nginx have many benefits, including ssl termination and route re-writing, allowing for a single entry point to services within the cluster, almost like an api gateway or reverse proxy. 

At the moment we have been exposing both our website and our middle api outside of the cluster, while there isn't anything wrong with this approach you may decide that you would rather use a single hostname and handle routing based on paths or ports. Ingress is a great option for this and also means that you are then only exposing one service outside of the cluster.

## Restrict exposure to existing services

![Image showing ingress controller](/Documentation/images/architecture.png)

Now we want to implement ingress, lets first modify our services so that they are set to be ClusterIP rather than LoadBalancer.

Go to '/Source/YAML/Website/deployment-website.yaml' and modify it like so:

```
apiVersion: v1
kind: Service
metadata:
  name: website
spec:
  selector:
    app: website
  ports:
  - protocol: TCP
    port: 80
    targetPort: 80
  type: ClusterIP
```

Now commit and push the changes into git so that they are pushed through your CI / CD pipeline.

Try to navitgate to the website again and you will notice that the website is no longer available. 

## Adding the ingress rule

Luckily our cluster already has nginx installed, so let's fix access to our website by adding an ingress rule.

Go to '/Source/YAML/Website/deployment-website.yaml' and add the following:

```
apiVersion: extensions/v1beta1
kind: Ingress
metadata:
  name: website
  annotations:
    kubernetes.io/ingress.class: nginx
spec:
  rules:
  - host:
    http:
      paths:
      - path: /
        backend:
          serviceName: website
          servicePort: 80
```

Again commit and push this change in and wait for our CI / CD pipeline to complete. Once it has grab the public IP address of our ingress controller and navigate to it.

One way to do this is by running the following command.

```
kubectl get services -n dev
```
