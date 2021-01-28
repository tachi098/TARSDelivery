using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        private const string uriBranch = "http://localhost:50354/api/Branch/";
        private const string uriRole = "http://localhost:50354/api/Role/";
        private HttpClient httpClient = new HttpClient();

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Branch>>(httpClient.GetStringAsync(uriBranch).Result);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Branch branch)
        {
            var model = httpClient.PostAsJsonAsync(uriBranch, branch).Result;
            if (model.IsSuccessStatusCode)
            {
                return RedirectToAction("index", "Branch");
            }
            return View();
        }
    }
}
