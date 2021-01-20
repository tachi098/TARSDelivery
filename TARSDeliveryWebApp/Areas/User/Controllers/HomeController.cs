using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        [Area("User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
