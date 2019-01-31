using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKSWebsite.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private IConfiguration Config;
        public ServiceLocator(IConfiguration config)
        {
            Config = config;
        }

        /// <summary>
        /// Gets serivce URI from Environment Variables from Kubernets naming convention
        /// e.g. Host variable for a service named 'demo-aks-api' will be 'DEMO_AKS_API_SERVICE_HOST'
        /// </summary>
        public string GetServiceUri(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                return "";

            var serviceNameFormatted = serviceName.ToUpper().Replace("-", "_");
            var host = Config[serviceNameFormatted + "_SERVICE_HOST"]; 
            var port = Config[serviceNameFormatted + "_SERVICE_PORT"];
            var uri = $"http://{host}:{port}";

            return uri;
        }
    }
}
