using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class PackageController : Controller
    {
        private const string uriPackages = "http://localhost:50354/api/Packages/";
        private const string uriPackage = "http://localhost:50354/api/Packages/GetPackage/";
        private readonly HttpClient httpClient = new HttpClient();
        public IActionResult Index()
        {
            var saccount = HttpContext.Session.GetString("sAccount");
            Account account = JsonConvert.DeserializeObject<Account>(saccount);
            var model = JsonConvert.DeserializeObject<IEnumerable<Package>>(httpClient.GetStringAsync(uriPackages).Result);
            var packageOwn = model.Where(p => p.Email.Equals(account.Email)).ToList();
            return View(packageOwn);
        }
        public IActionResult Search(int? search)
        {
            if(search == null)
            {
                return RedirectToAction("index", "Home");
            }
            var model = JsonConvert.DeserializeObject<Package>(httpClient.GetStringAsync(uriPackage + search).Result);
            if(model == null)
            {
                return RedirectToAction("index", "Home");

            }
            return View(model);
        }
    }
}
