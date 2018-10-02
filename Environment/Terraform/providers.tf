provider "azurerm" {
  version         = "~> 1.16"
}

provider "random" {
  version = "~> 1.3"
}

provider "kubernetes" {
  version                = "~> 1.1"
  host                   = "${azurerm_kubernetes_cluster.demo.kube_config.0.host}"
  client_certificate     = "${base64decode(azurerm_kubernetes_cluster.demo.kube_config.0.client_certificate)}"
  client_key             = "${base64decode(azurerm_kubernetes_cluster.demo.kube_config.0.client_key)}"
  cluster_ca_certificate = "${base64decode(azurerm_kubernetes_cluster.demo.kube_config.0.cluster_ca_certificate)}"
  insecure               = false
  load_config_file       = false
}
