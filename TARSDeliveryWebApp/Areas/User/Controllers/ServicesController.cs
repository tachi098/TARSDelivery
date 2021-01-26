using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
