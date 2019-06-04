using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AKSWebsite.Models;
using Microsoft.Extensions.Configuration;
using AKSWebsite.Services;

namespace AKSWebsite.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Config;
        private IServiceLocator ServiceLocator;
        private IAPIService APIService;
        public HomeController(IConfiguration config, IServiceLocator serviceLocator, IAPIService apiService)
        {
            Config = config;
            ServiceLocator = serviceLocator;
            APIService = apiService;
        }

        public async Task <IActionResult> Index()
        {
            var theme = this.Config["App:Theme"];
            if (theme != null && theme.ToLower() != "default")
                ViewData["Theme"] = theme.ToLower();

            var apiResponse = await this.APIService.CallAPI();
            var model = new HomePageModel()
            {
                Configuration = this.Config,
                APILocation = ServiceLocator.GetServiceUri(Config["API:Name"]),
                APIResponse = apiResponse,
                APIName = Config["API:Name"]
            };

            return View(model);
        }

        //api/ResourceLimits/SpikeCPU
        public async Task<IActionResult> Spike()
        {
            var response = await this.APIService.RequestAPIMemorySpike();//dont wait for response
            TempData["SpikeResponse"] = response;
            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
