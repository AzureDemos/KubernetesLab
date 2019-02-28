using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AKSWebsite.Services
{
    public class APIService : IAPIService
    {
        private IConfiguration config;
        private IServiceLocator serviceLocator;

        public APIService(IConfiguration config, IServiceLocator serviceLocator)
        {
            this.config = config;
            this.serviceLocator = serviceLocator;
        }

        public async Task<string> CallAPI()
        {   try
            {
                var serviceName = this.config["API:Name"];
                var baseURI = this.serviceLocator.GetServiceUri(serviceName);
                var resourceRoot = "/api/values";
           
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage res = await client.GetAsync(baseURI + resourceRoot))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return $"An error occurred calling the backend api {ex.Message}";
            }
        }
    }
}
