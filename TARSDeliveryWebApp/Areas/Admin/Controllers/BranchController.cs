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
            var branches = model.ToList();
            return View(branches);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                var model = httpClient.PostAsJsonAsync(uriBranch, branch).Result;
                if (model.IsSuccessStatusCode)
                {
                    return RedirectToAction("index", "Branch");
                }
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = JsonConvert.DeserializeObject<Branch>(httpClient.GetStringAsync($"{uriBranch}GetBranch/{id}").Result);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Branch branch)
        {
            try
            {
                var model = httpClient.PutAsJsonAsync($"{uriBranch}", branch).Result;
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
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var branchs = JsonConvert.DeserializeObject<IEnumerable<Branch>>(httpClient.GetStringAsync(uriBranch).Result);
                var branch = branchs.SingleOrDefault(a => a.Id == id);
                if (branch != null)
                {
                    if (branch.Delete_at == null)
                    {
                        branch.Delete_at = DateTime.Now;
                        var model = httpClient.PutAsJsonAsync(uriBranch, branch).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Branch");
                        }
                    }
                    else
                    {
                        branch.Delete_at = null;
                        var model = httpClient.PutAsJsonAsync(uriBranch, branch).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Branch");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

    }
}
