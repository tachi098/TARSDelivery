﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security.Claims;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Models;
using TARSDeliveryWebApp.Helper;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private const string uriAccount = "http://localhost:50354/api/Account/";
        private const string uriRole = "http://localhost:50354/api/Role/";
        private readonly HttpClient httpClient = new HttpClient();
        private readonly Random rnd = new Random();

        [Authorize(Roles = "Admin")]

        // Admin List
        public IActionResult Index()
        {
            return View();
        }

        // Admin Login
        public IActionResult Login()
        {
            if (User.Identity.Name != null)
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
                ClaimsIdentity identity = null;
                bool isAuthenticate = false;
                if (role.Position == 1)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name,account.Email),
                        new Claim(ClaimTypes.Role,"Admin"),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                if (role.Position == 2)
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name,account.Email),
                        new Claim(ClaimTypes.Role,"Manager"),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    isAuthenticate = true;
                }
                if (isAuthenticate)
                {
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                        IsPersistent = true,
                    };
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            else
            {
                ViewBag.Msg = "Email or password wrong! Please try again";
                return View();
            }
            return View();
        }

        // Admin Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
