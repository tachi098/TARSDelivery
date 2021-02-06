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
using System.Dynamic;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillsController : Controller
    {
        private const string uriBills = "http://localhost:50354/api/Bills";
        private const string uriPackages = "http://localhost:50354/api/Packages";
        private const string uriBranchs = "http://localhost:50354/api/Branch";
        private const string uriAccount = "http://localhost:50354/api/Account";

        private readonly HttpClient httpClient = new HttpClient();


        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Index()
        {
            var models = JsonConvert.DeserializeObject<IEnumerable<BillPackage>>(httpClient.GetStringAsync($"{uriBills}/GetBillPackages").Result);  

            var modelsWorking = models.Where(m => m.GetBill.Delete_at == null && m.GetPackage.Delete_at == null).ToList();
            var modelsStopWorking = models.Where(m => m.GetBill.Delete_at != null && m.GetPackage.Delete_at != null).ToList();

            if (User.IsInRole("Manager"))
            {
                var userId = int.Parse(User.FindFirst("AccountId").Value);
                var account = JsonConvert.DeserializeObject<Account>(httpClient.GetStringAsync($"{uriAccount}/{userId}").Result);

                modelsWorking = models.Where(m => m.GetBill.Delete_at == null && m.GetPackage.Delete_at == null && m.GetPackage.BranchId == account.BranchId && m.GetPackage.Status != 4).ToList();
                modelsStopWorking = models.Where(m => m.GetBill.Delete_at != null && m.GetPackage.Delete_at != null && m.GetPackage.BranchId == account.BranchId && m.GetPackage.Status != 4).ToList();
            }

            ViewBag.ModelsDeleted = modelsStopWorking;

            return View(modelsWorking);
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Update(int packageId)
        {
            var priceList = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(httpClient.GetStringAsync($"{uriBills}/GetPriceLists").Result);
            ViewBag.PriceLists = new SelectList(priceList.Where(m => !m.Name.Equals("VPP") && m.Delete_at == null), "Name", "Name");
            
            var models = JsonConvert.DeserializeObject<IEnumerable<Branch>>(httpClient.GetStringAsync($"{uriBranchs}").Result);
            ViewBag.Branchs = new SelectList(models, "Id", "Name");

            var model = JsonConvert.DeserializeObject<Package>(httpClient.GetStringAsync($"{uriPackages}/GetPackage/{packageId}").Result);
            ViewBag.Distance = model.Distance;
            ViewBag.TotalPrice = model.TotalPrice;

            ViewBag.BranchId = "";

            if (User.IsInRole("Manager"))
            {
                ViewBag.BranchId = "Manager";
            }
            else if (User.IsInRole("Admin"))
            {
                ViewBag.BranchId = "Admin";
            }

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

            var priceList = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(httpClient.GetStringAsync($"{uriBills}/GetPriceLists").Result);
            ViewBag.PriceLists = new SelectList(priceList.Where(m => !m.Name.Equals("VPP") && m.Delete_at == null), "Name", "Name");

            ViewBag.BranchId = "";

            if (User.IsInRole("Manager"))
            {
                ViewBag.BranchId = "Manager";
            } 
            else if(User.IsInRole("Admin"))
            {
                ViewBag.BranchId = "Admin";
            }

            return View();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package)
        {
            try
            {
                var accountId = int.Parse(User.FindFirst("AccountId").Value);
                var account = JsonConvert.DeserializeObject<Account>(httpClient.GetStringAsync($"{uriAccount}/{accountId}").Result);

                if (User.IsInRole("Manager"))
                {
                    package.BranchId = account.BranchId;
                }
                
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

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Undo(int billId, int packageId)
        {
            try
            {
                var modelPackage = JsonConvert.DeserializeObject<Package>(httpClient.GetStringAsync($"{uriPackages}/GetPackage/{packageId}").Result);
                var modelBill = JsonConvert.DeserializeObject<Bill>(httpClient.GetStringAsync($"{uriBills}/GetBill/{billId}").Result);

                modelPackage.Delete_at = null;
                modelBill.Delete_at = null;

                var updatePackage = httpClient.PutAsJsonAsync($"{uriPackages}/UpdatePackage", modelPackage).Result;
                var updateBill = httpClient.PutAsJsonAsync($"{uriBills}/UpdateBill", modelBill).Result;

                if(updatePackage.IsSuccessStatusCode && updateBill.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }


            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Details(int billId)
        {
            var models = JsonConvert.DeserializeObject<IEnumerable<BillPackage>>(httpClient.GetStringAsync($"{uriBills}/GetBillPackages").Result);
            var modelBill = models.SingleOrDefault(m => m.GetBill.Id == billId);

            return View(modelBill);
        }
    }
}
