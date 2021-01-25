using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private const string uriAccount = "http://localhost:50354/api/Account/";
        private const string uriRole = "http://localhost:50354/api/Role/";
        private HttpClient httpClient = new HttpClient();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            var saccount = HttpContext.Session.GetString("sAccount");
            if (saccount != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
            var account = accounts.SingleOrDefault(a => a.Email.Equals(email) && BCrypt.Net.BCrypt.Verify(password, a.Password));
            if (account != null)
            {
                Role role = JsonConvert.DeserializeObject<Role>(httpClient.GetStringAsync(uriRole + account.Id).Result);
                if (role.Position == 1 || role.Position == 3)
                {
                    HttpContext.Session.SetString("sAccount", JsonConvert.SerializeObject(account));
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Msg = "No authorize to connect";
                    return View();
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("sAccount");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            var saccount = HttpContext.Session.GetString("sAccount");
            if (saccount != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
