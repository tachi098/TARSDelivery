using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class ServicesController : Controller
    {
        private readonly string uriServices = "http://localhost:50354/api/Services";
        private readonly HttpClient httpClient = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }
    }
}
