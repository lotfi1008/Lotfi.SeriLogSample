using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lotfi.SeriLogSample.MVC.Models;
using Microsoft.Extensions.Logging;
using Lotfi.SeriLogSample.MVC.Infrastractures.Middlewares;

namespace Lotfi.SeriLogSample.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }
        public IActionResult Index()
        {
            throw new NullReferenceException("Nolllllllllllllll"); 
            return View();
        }

        public IActionResult Index2()
        {

            throw new NotFoundCustomException("Message","Description",400); 
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
