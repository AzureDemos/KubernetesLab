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

        public string GetServiceUri(string serviceName)
        {
            var serviceEnvironmentName = serviceName.ToUpper().Replace("-", "_");
            var host = Config[serviceEnvironmentName + "_SERVICE_HOST"];
            var port = Config[serviceEnvironmentName + "_SERVICE_PORT"];
            var uri = $"http://{host}:{port}";
            return uri;
        }
    }
}
