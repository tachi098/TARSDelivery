using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private const string uriBills = "http://localhost:50354/api/Bills";
        private const string uriAccount = "http://localhost:50354/api/Account/";

        private readonly HttpClient httpClient = new HttpClient();

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Index()
        {
            var models = JsonConvert.DeserializeObject<IEnumerable<BillPackage>>(httpClient.GetStringAsync($"{uriBills}/GetBillPackages").Result);
            if (User.IsInRole("Admin"))
            {
                ViewBag.Storing = models.Where(m => m.GetPackage.Status == 1 && m.GetPackage.Delete_at == null).Count();
                ViewBag.Doing = models.Where(m => m.GetPackage.Status == 2 && m.GetPackage.Delete_at == null).Count();
                ViewBag.Finished = models.Where(m => m.GetPackage.Status == 3 && m.GetPackage.Delete_at == null).Count();
                ViewBag.Error = models.Where(m => m.GetPackage.Status == 4 && m.GetPackage.Delete_at == null).Count();

                double[] totalMonth = new double[12];
                for (int i = 0; i < totalMonth.Length; i++)
                {
                    var month = models.Where(e => e.GetPackage.Create_at.Month == i + 1 && e.GetPackage.Delete_at == null).Sum(e => e.GetPackage.TotalPrice);
                    totalMonth[i] += Math.Round(month, 2, MidpointRounding.AwayFromZero);
                }
                ViewBag.Data = JsonConvert.SerializeObject(totalMonth);
            }
            if (User.IsInRole("Manager"))
            {
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                var account = accounts.SingleOrDefault(a => a.Email.Equals(User.Identity.Name));
                ViewBag.Storing = models.Where(m => m.GetPackage.Status == 1 && m.GetPackage.Delete_at == null && m.GetPackage.BranchId == account.BranchId).Count();
                ViewBag.Doing = models.Where(m => m.GetPackage.Status == 2 && m.GetPackage.Delete_at == null && m.GetPackage.BranchId == account.BranchId).Count();
                ViewBag.Finished = models.Where(m => m.GetPackage.Status == 3 && m.GetPackage.Delete_at == null && m.GetPackage.BranchId == account.BranchId).Count();
                ViewBag.Error = models.Where(m => m.GetPackage.Status == 4 && m.GetPackage.Delete_at == null && m.GetPackage.BranchId == account.BranchId).Count();

                double[] totalMonth = new double[12];
                for (int i = 0; i < totalMonth.Length; i++)
                {
                    var month = models.Where(e => e.GetPackage.Create_at.Month == i + 1 && e.GetPackage.Delete_at == null && e.GetPackage.BranchId == account.BranchId).Sum(e => e.GetPackage.TotalPrice);
                    totalMonth[i] += month;
                }
                ViewBag.Data = JsonConvert.SerializeObject(totalMonth);
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
