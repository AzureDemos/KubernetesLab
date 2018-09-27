resource "kubernetes_namespace" "dev" {
  metadata {
    name = "dev"
  }
}

resource "kubernetes_namespace" "prod" {
  metadata {
    name = "prod"
  }
}

locals {
  dockercfg = {
    "${azurerm_container_registry.demo.login_server}" = {
      email    = "notneeded@notneeded.com"
      username = "${azurerm_container_registry.demo.admin_username}"
      password = "${azurerm_container_registry.demo.admin_password}"
    }
  }
}

resource "kubernetes_secret" "devacr" {
  metadata {
    name      = "acr-auth"
    namespace = "dev"
  }

  # terraform states this is a map of the variables here, it actual wants a structured json object
  # https://github.com/terraform-providers/terraform-provider-kubernetes/issues/81
  data {
    ".dockercfg" = "${ jsonencode(local.dockercfg) }"
  }

  type = "kubernetes.io/dockercfg"
}

resource "kubernetes_secret" "prodacr" {
  metadata {
    name      = "acr-auth"
    namespace = "prod"
  }

  # terraform states this is a map of the variables here, it actual wants a structured json object
  # https://github.com/terraform-providers/terraform-provider-kubernetes/issues/81
  data {
    ".dockercfg" = "${ jsonencode(local.dockercfg) }"
  }

  type = "kubernetes.io/dockercfg"
}
