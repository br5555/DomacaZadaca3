using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Domaca_Zadaca_3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Domaca zadaca 3 c#";

            return View();
        }

       

        public IActionResult Error()
        {
            return View();
        }
    }
}
