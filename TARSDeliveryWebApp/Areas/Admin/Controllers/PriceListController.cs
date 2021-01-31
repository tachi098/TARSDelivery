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

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Index()
        {
            var res = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(
                                                        httpClient.GetStringAsync(uri).Result);
            return View(res);
        }



        public IActionResult Edit(string Id)
        {
            var model = JsonConvert.DeserializeObject<PriceList>(
                                       httpClient.GetStringAsync(uri + Id).Result);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PriceList priceList)
        {
            try
            {
                var modelOld = JsonConvert.DeserializeObject<PriceList>(
                            httpClient.GetStringAsync(uri + priceList.Id).Result);
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


        public IActionResult Delete(int id)
        {
            try
            {
                var priceLists = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(httpClient.GetStringAsync(uri).Result);
                var priceList = priceLists.SingleOrDefault(a => a.Id == id);
                if (priceList != null)
                {
                    if (priceList.Delete_at == null)
                    {
                        priceList.Delete_at = DateTime.Now;
                        var model = httpClient.PutAsJsonAsync(uri, priceList).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "PriceList");
                        }
                    }
                    else
                    {
                        priceList.Delete_at = null;
                        var model = httpClient.PutAsJsonAsync(uri, priceList).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "PriceList");
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



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PriceList priceList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = httpClient.PostAsJsonAsync<PriceList>(uri, priceList).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "PriceList");
                    }
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
