using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKSWebsite.Services
{
    public interface IAPIService
    {
        Task<string> CallAPI();
    }
}
