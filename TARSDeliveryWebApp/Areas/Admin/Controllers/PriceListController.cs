using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PriceListController : Controller
    {

        private string uri = "http://localhost:50354/api/PriceList/";
        private HttpClient httpClient = new HttpClient();

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var res = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(
                                                        httpClient.GetStringAsync(uri).Result);
            return View(res);
        }



        public IActionResult Edit(string name)
        {
            var model = JsonConvert.DeserializeObject<PriceList>(httpClient.GetStringAsync(uri + name).Result);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PriceList priceList)
        {
            try
            {
                var modelOld = JsonConvert.DeserializeObject<PriceList>(httpClient.GetStringAsync(uri + priceList.Name).Result);
                priceList.Create_at = modelOld.Create_at;
                if (ModelState.IsValid)
                {
                    var model = httpClient.PutAsJsonAsync<PriceList>(uri, priceList).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index","PriceList");
                    }
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PriceList priceList)
        {
            try
            {
                var modelOld = JsonConvert.DeserializeObject<PriceList>(httpClient.GetStringAsync(uri + priceList.Name).Result);
                if (modelOld == null)
                {
                    var model = httpClient.PostAsJsonAsync(uri, priceList).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "PriceList");
                    }
                }
                else
                {
                    ViewBag.Msg = "Name is exists";
                    return View();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
            }
            return View();
        }

    }
}
