using AKSWebsite.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        private string GetURI(string resourceRoot)
        {
            var serviceName = this.config["API:Name"];
            var baseURI = this.serviceLocator.GetServiceUri(serviceName);
            var root = baseURI + resourceRoot;
            return root;
        }

        public async Task<APIResponse> CallAPI()
        {   try
            {
                var uri = this.GetURI("/api/status");
                using (HttpClient client = new HttpClient() {  Timeout = TimeSpan.FromSeconds(10) })  //set timeout, as requests will fail when network policies are enforced and we want our website to show the error, instead of also timing out
                using (HttpResponseMessage res = await client.GetAsync(uri))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<APIResponse>(data);
                }
            }
            catch (Exception ex)
            {
                APIResponse response = new APIResponse();
                response.Responses.Add(new APIResponseDetails()
                {
                    Response = $"An error occurred calling the api - {ex.Message}",
                    IsError = true
                });
                return response;
            }
        }

        public async Task<string> RequestAPIMemorySpike()
        {
            try
            {
                var uri = this.GetURI("/api/TestResourceLimits");
                using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) })  //set timeout, as requests will fail when network policies are enforced and we want our website to show the error, instead of also timing out
                using (HttpResponseMessage res = await client.GetAsync(uri))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();
                    return data;
                }
            }
            catch (Exception ex)
            {
                return $"Error Occurred Calling API - {ex.Message}";
            }
        }
    }
}
