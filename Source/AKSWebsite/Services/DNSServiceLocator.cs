using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKSWebsite.Services
{
    public class DNSServiceLocator : IServiceLocator
    {

        /// <summary>
        /// Uses Kubernetes CoreDNS
        /// </summary>
        public string GetServiceUri(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                return "";
            var uri = $"http://{serviceName}";
            return uri;
        }
    }
}
