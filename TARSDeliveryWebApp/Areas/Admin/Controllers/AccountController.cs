using Microsoft.AspNetCore.Authentication;
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
        private HttpClient httpClient = new HttpClient();
        private Random rnd = new Random();

        [Authorize(Roles = "Admin")]

        //Admin List
        public IActionResult Index()
        {
            return View();
        }

        //Admin Login
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
        //End Admin Login

        //Admin Logout
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        //Admin Password
        public IActionResult ForgotPassword(int id)
        {
            if (id == 1)
            {
                ViewBag.Area = "User";
                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(string email, int id)
        {
            int area = id == 1 ? 1 : 2;
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
            var account = accounts.SingleOrDefault(a => a.Email.Equals(email));
            try
            {
                if (account != null)
                {
                    Role role = JsonConvert.DeserializeObject<Role>(httpClient.GetStringAsync(uriRole + account.Id).Result);
                    int random = rnd.Next(1000, 9999);
                    string code = BCrypt.Net.BCrypt.HashPassword(random.ToString());
                    account.Code = code;
                    var model = httpClient.PutAsJsonAsync(uriAccount, account).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        string path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Admin/Account/ResetPassword/?Code={account.Code}&Email={account.Email}&Area={area}";
                        Help.SendEmail.ResetPassword(account.Email, path).Wait();
                        return RedirectToAction("ForgotPasswordConfirm", "Account", new { position = role.Position});
                    }
                }
                else
                {
                    ViewBag.Msg = "Email wrong! Please check again!";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult ForgotPasswordConfirm(int position)
        {
            ViewBag.Position = position;
            return View();
        }

        public IActionResult ResetPassword([FromQuery(Name = "Code")] string code, [FromQuery(Name = "Email")] string email, [FromQuery(Name = "Area")] string area)
        {
            ViewBag.code = code;
            ViewBag.email = email;
            ViewBag.area = area;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(string code, string email, string password, string confirmpassword, int area)
        {
            try
            {
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                var account = accounts.SingleOrDefault(a => a.Code == code && a.Email.Equals(email));
                if (account != null)
                {
                    Role role = JsonConvert.DeserializeObject<Role>(httpClient.GetStringAsync(uriRole + account.Id).Result);
                    if (password == confirmpassword)
                    {
                        account.Password = BCrypt.Net.BCrypt.HashPassword(password);
                        account.Code = null;
                        var model = httpClient.PutAsJsonAsync(uriAccount, account).Result;
                        if (role.Position == 3)
                        {
                            return RedirectToAction("Login", "Account", new { area = "User" });
                        }
                        else if (area != 1 && role.Position == 1)
                        {
                            return RedirectToAction("Login", "Account", new { area = "User" });
                        }
                        else
                        {
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "Password and confirm password not match";
                        return RedirectToAction("ResetPassword", "Account",new { code = code, email = email});
                    }
                }
                else
                {
                    ViewBag.Msg = "Url wrong! please check your email again!";
                    return View();
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
