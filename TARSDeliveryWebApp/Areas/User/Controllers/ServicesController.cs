using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("sAccount") == null)
            {
                ViewBag.Account = "";
            }
            else
            {
                ViewBag.Account = JsonConvert.DeserializeObject<Account>(HttpContext.Session.GetString("sAccount")).Id;
            }
            
            return View();
        }
    }
}
