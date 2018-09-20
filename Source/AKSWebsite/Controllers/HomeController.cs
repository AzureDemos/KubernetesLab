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
            var apiResponse = await this.APIService.CallAPI();
            var model = new HomePageModel()
            {
                Configuration = this.Config,
                APILocation = this.ServiceLocator.GetServiceUri(Config["API:Name"]),
                APIResponse = apiResponse
            };
            return View(model);
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
