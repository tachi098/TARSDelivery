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
            var res = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(httpClient.GetStringAsync(uri).Result);
            var model = res.SingleOrDefault(e => e.Name.Equals(name));
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(PriceList priceList)
        {
            try
            {
                var res = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(httpClient.GetStringAsync(uri).Result);
                var modelOld = res.SingleOrDefault(e => e.Id == priceList.Id);
                var exist = res.SingleOrDefault(e => e.Name.Equals(priceList.Name) && e.Id != priceList.Id);
                if (exist == null)
                {
                    priceList.Create_at = modelOld.Create_at;
                    if (ModelState.IsValid)
                    {
                        var model = httpClient.PutAsJsonAsync<PriceList>(uri, priceList).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "PriceList");
                        }
                    }
                    else
                    {
                        return View(modelOld);
                    }
                }
                else if ((modelOld.PriceDistance != priceList.PriceDistance && exist == null) || (modelOld.PriceWeight != priceList.PriceWeight && exist == null))
                {
                    priceList.Create_at = modelOld.Create_at;
                    if (ModelState.IsValid)
                    {
                        var model = httpClient.PutAsJsonAsync<PriceList>(uri, priceList).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "PriceList");
                        }
                    }
                    else
                    {
                        return View(modelOld);
                    }
                }
                else
                {
                    ViewBag.Msg = "Name is exists";
                    return View(modelOld);
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


        [HttpGet]
        public IActionResult DeletePriceList(int id)
        {
            try
            {
                var pricelists = JsonConvert.DeserializeObject<IEnumerable<PriceList>>(httpClient.GetStringAsync(uri).Result);
                var pricelist = pricelists.SingleOrDefault(a => a.Id == id);
                if (pricelist != null)
                {
                    if (pricelist.Delete_at == null)
                    {
                        pricelist.Delete_at = DateTime.Now;
                        var model = httpClient.PutAsJsonAsync(uri, pricelist).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "PriceList");
                        }
                    }
                    else
                    {
                        pricelist.Delete_at = null;
                        var model = httpClient.PutAsJsonAsync(uri, pricelist).Result;
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
    }
}
