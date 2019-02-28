using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using AKSAPI.Services;

namespace AKSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceLimitsController : ControllerBase
    {
        public IMemoryCache _Cache { get; }

        public ResourceLimitsController(IMemoryCache _cache)
        {
            _Cache = _cache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This response has come from the backend API";
        }

        // GET api/ResourceLimits/SpikeCPU
        [HttpGet]
        public ActionResult<string> SpikeCPU(int cpuUsage = 40, int minutes = 1)
        {
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(SharedServices.SpikeCPU));
                t.Start(cpuUsage);
                threads.Add(t);
            }
            Thread.Sleep(minutes * 60000);
            foreach (var t in threads)
            {
                t.Abort();
            }
            return "ss";
        }

       //[HttpGet]
       //public ActionResult SpikeMemory()
       //{
       //    var txt = System.IO.File.ReadAllText(@"..\configs\largetextfile.txt");
       //    IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
       //    for (int i = 0; i < 2000000; i++)
       //    {
       //        object result = cache.Set(Guid.NewGuid().ToString(), txt);
       //    }
       //    return Ok("Cached large amount of data");
       //}

    }
}


