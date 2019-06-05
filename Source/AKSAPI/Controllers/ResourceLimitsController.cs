using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using AKSAPI.Services;
using Microsoft.AspNetCore.Hosting;
using AKSAPI.Content;
using System.Reflection;
using System.IO;
using System.Net;

namespace AKSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResourceLimitsController : ControllerBase
    {
        public IMemoryCache Cache { get; }
        public TestResourceLimitsController(IMemoryCache _cache)
        {
            Cache = _cache;
        }

        // GET api/TestResourceLimits
        [HttpGet]
        public ActionResult<string> Get()
        {
            int cacheItems = 20;
            var assembly = typeof(ContentPointer).GetTypeInfo().Assembly;
            string content = "";
            using (var stream = assembly.GetManifestResourceStream($"AKSAPI.Content.LargeTextFile.txt"))
            using (var reader = new StreamReader(stream))
                content = reader.ReadToEnd();

            var cacheEntryOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
            for (int i = 0; i < cacheItems; i++) 
                Cache.Set(Guid.NewGuid().ToString(), Guid.NewGuid().ToString() + content + Guid.NewGuid().ToString(), cacheEntryOptions);

            
            int? cacheCount = (int?)Cache.Get("ItemsCount");
            if (cacheCount.HasValue) 
                cacheCount += cacheItems;
            else
                cacheCount = cacheItems;

            Cache.Set("ItemsCount", cacheCount, cacheEntryOptions);
            var byteCountPerItem = System.Text.Encoding.ASCII.GetByteCount(Guid.NewGuid().ToString() + content + Guid.NewGuid().ToString());
            Cache.Set("MegaBytesCount", (cacheCount.Value * byteCountPerItem) / 1000000, cacheEntryOptions);

            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            return Ok($"{cacheItems} large items added to {ipHostInfo.HostName} Cache. Now contains cacheCount items");
        }

     
    }
}


