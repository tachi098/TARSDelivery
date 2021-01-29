using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using TARSDeliveryWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillsController : Controller
    {
        private const string uriBills = "http://localhost:50354/api/Bills";
        private const string uriPackages = "http://localhost:50354/api/Packages";
        private const string uriBranchs = "http://localhost:50354/api/Branch";
        private readonly HttpClient httpClient = new HttpClient();


        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Index()
        {
            var models = JsonConvert.DeserializeObject<IEnumerable<BillPackage>>(httpClient.GetStringAsync($"{uriBills}/GetBillPackages").Result);
            return View(models);
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Update(int packageId)
        {
            var model = JsonConvert.DeserializeObject<Package>(httpClient.GetStringAsync($"{uriPackages}/GetPackage/{packageId}").Result);
            ViewBag.Distance = model.Distance;
            ViewBag.TotalPrice = model.TotalPrice;
            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Package package)
        {
            try
            {
                var model = httpClient.PutAsJsonAsync($"{uriPackages}/UpdatePackage", package).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Msg = "Error Update!";
            }
            catch (Exception)
            {
                ViewBag.Msg = "Error!";
            }
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            var models = JsonConvert.DeserializeObject<IEnumerable<Branch>>(httpClient.GetStringAsync($"{uriBranchs}").Result);
            ViewBag.Branchs = new SelectList(models, "Id", "Name");
            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package)
        {
            try
            {
                var modelPackage = httpClient.PostAsJsonAsync($"{uriPackages}/CreatePackage", package).Result;
                var modelNewPackage = JsonConvert.DeserializeObject<Package>(httpClient.GetStringAsync($"{uriPackages}/GetNewPackage").Result);

                var bill = new Bill();
                bill.AccountId = int.Parse(User.FindFirst("AccountId").Value);
                bill.PackageId = modelNewPackage.Id;

                var modelBill = httpClient.PostAsJsonAsync($"{uriBills}/CreateBill", bill).Result;

                if (modelPackage.IsSuccessStatusCode && modelBill.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ViewBag.Msg = "Error Create!";
            }
            catch (Exception)
            {
                ViewBag.Msg = "Error!";
            }
            return View();
        }
    }
}
