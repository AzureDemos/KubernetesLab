using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AKSAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AKSAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IConfiguration Config;
        private IHttpContextAccessor ContextAccessor;
        public IMemoryCache Cache { get; }

        public StatusController(IConfiguration config, IHttpContextAccessor accessor, IMemoryCache cache)
        {
            Config = config;
            ContextAccessor = accessor;
            Cache = cache;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {
            var nameofThisService = this.Config["NameofThisService"] ?? "api";
            var downStreamAPIName = this.Config["DownStreamAPIName"];
            var response = new APIResponse();

            if (!string.IsNullOrWhiteSpace(downStreamAPIName))
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    using (HttpResponseMessage res = await client.GetAsync("http://" + downStreamAPIName + "/api/status"))
                    using (HttpContent content = res.Content)
                    {
                        var data = await content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<APIResponse>(data);
                    }
                }
                catch (Exception ex)
                {
                    response.Responses.Add( new APIResponseDetails()
                    {
                        Response = $"An Error Occurred calling {downStreamAPIName}"
                    });
                }
            }

            var thisResponse = new APIResponseDetails() { Response = $"This response has come from {nameofThisService} at {DateTime.Now.ToString("HH:mm:ss.fff")}" };
            int? cacheItems = (int?)Cache.Get("ItemsCount");
            if (cacheItems.HasValue)
                thisResponse.CacheData = $"{cacheItems.Value} Items in Cache, totalling {Cache.Get("MegaBytesCount")}mb";
            else
                thisResponse.CacheData = "Cache is empty";

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            thisResponse.HostName = $"{ipHostInfo.HostName} - {String.Join(',', ipHostInfo.Aliases)}";
    

            foreach (var x in this.ContextAccessor.HttpContext.Request.Headers)
                thisResponse.Headers.Add(x.Key, x.Value);

            response.Responses.Add(thisResponse);

            return response;
        }
    }
}
