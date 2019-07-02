using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKSWebsite.Models
{
    public class APIResponseDetails
    {
        public string Response { get; set; }
        public bool IsError { get; set; }
        public string CacheData { get; set; }
        public string HostName { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }

    public class APIResponse
    {
        public List<APIResponseDetails> Responses { get; set; } = new List<APIResponseDetails>();
    }
}
