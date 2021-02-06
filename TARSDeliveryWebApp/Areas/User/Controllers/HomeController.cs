using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private string uriComment = "http://localhost:50354/api/Comment/";
        private HttpClient httpClient = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(Comment comment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = httpClient.PostAsJsonAsync(uriComment, comment).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Contact");
                    }
                    else
                    {
                        ViewBag.Msg = "Error";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch(Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);

            }
            return View();
            }

        public IActionResult Team()
        {
            return View();
        }
    }
}
