using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillsController : Controller
    {
        private const string uri = "http://localhost:50354/api/Bills/GetBillPackages";
        private readonly HttpClient httpClient = new HttpClient();


        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Index()
        {
            var models = JsonConvert.DeserializeObject<IEnumerable<BillPackage>>(httpClient.GetStringAsync(uri).Result);
            return View(models);
        }
    }
}
