using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKSWebsite.Models
{
    public class HomePageModel
    {
        public IConfiguration Configuration { get; set; }
        public string APIResponse { get; set; }
        public string EnvAPILocation { get; set; }
        public string DNSAPILocation { get; set; }
        public string APINameFormmatted { get; set; }

        public string APIName { get; set; }
    }
}
