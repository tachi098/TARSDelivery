using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TARSDeliveryWebApp.Helper;
using TARSDeliveryWebApp.Models;

namespace TARSDeliveryWebApp.Areas.User.Controllers
{
    [Area("User")]
    public class AccountController : Controller
    {
        private const string uriAccount = "http://localhost:50354/api/Account/";
        private const string uriPackage = "http://localhost:50354/api/Packages/GetPackage/";
        private const string uriBillPackage = "http://localhost:50354/api/Bills/GetBillPackages/";
        private const string uriRole = "http://localhost:50354/api/Role/";
        private readonly HttpClient httpClient = new HttpClient();
        private readonly Random rnd = new Random();
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
        public IActionResult Login(Account acc)
        {
            try
            {
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                var account = accounts.SingleOrDefault(a => a.Email.Equals(acc.Email) && BCrypt.Net.BCrypt.Verify(acc.Password, a.Password));
                if (account != null)
                {
                    Role role = JsonConvert.DeserializeObject<Role>(httpClient.GetStringAsync(uriRole + account.Id).Result);
                    if (role.Position == 1 || role.Position == 3 && account.Delete_at == null)
                    {
                        HttpContext.Session.SetString("sRole", role.Position.ToString());
                        HttpContext.Session.SetString("sAccount", JsonConvert.SerializeObject(account));
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.Msg = "No authorize to connect.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Msg = "Email or password is wrong! Please try again.";
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("sAccount");
            HttpContext.Session.Remove("sRole");
            return RedirectToAction("Index", "Home");
        }

        // User Regeister
        public IActionResult Register()
        {
            var saccount = HttpContext.Session.GetString("sAccount");
            if (saccount != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Register(Account acc, string confirmpassword)
        {
            try
            {
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                var account = accounts.SingleOrDefault(a => a.Email.Equals(acc.Email));
                if (account == null)
                {
                    if (acc.Password == confirmpassword)
                    {
                        acc.Password = BCrypt.Net.BCrypt.HashPassword(acc.Password);
                        var model = httpClient.PostAsJsonAsync(uriAccount, acc).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                            var id = accounts.Max(a => a.Id);
                            Role role = new Role() { AccountId = id, Position = 3 };
                            var modelRole = httpClient.PostAsJsonAsync(uriRole, role).Result;
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            ViewBag.Msg = "Create account fail";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "Password and confirm password not match";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Msg = "Email exists";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        // User ForgotPassword
        public IActionResult ForgotPassword()
        {
            var saccount = HttpContext.Session.GetString("sAccount");
            if (saccount != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult ForgotPassword(Account acc)
        {
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
            var account = accounts.SingleOrDefault(a => a.Email.Equals(acc.Email));
            try
            {
                if (acc.Email != null)
                {
                    if (account != null)
                    {
                        Role role = JsonConvert.DeserializeObject<Role>(httpClient.GetStringAsync(uriRole + account.Id).Result);
                        if (role.Position == 3)
                        {
                            int random = rnd.Next(1000, 9999);
                            string code = BCrypt.Net.BCrypt.HashPassword(random.ToString());
                            account.Code = code;
                            var model = httpClient.PutAsJsonAsync(uriAccount, account).Result;
                            if (model.IsSuccessStatusCode)
                            {
                                string path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/User/Account/ResetPassword/?Code={account.Code}&Email={account.Email}";
                                Help.SendEmail.ResetPassword(account.Email, path).Wait();
                                return RedirectToAction("ForgotPasswordConfirm", "Account");
                            }
                        }
                        else
                        {
                            ViewBag.Msg = "Admin and employee cannot reset";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "Email wrong! Please check again!";
                        return View();
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }


        public IActionResult ForgotPasswordConfirm()
        {
            var saccount = HttpContext.Session.GetString("sAccount");
            if (saccount != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult ResetPassword([FromQuery(Name = "Code")] string code, [FromQuery(Name = "Email")] string email)
        {
            var saccount = HttpContext.Session.GetString("sAccount");
            if (saccount != null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (TempData["shortMessage"] != null)
            {
                ViewBag.Msg = "Password and confirm password not match";
                TempData.Remove("shortMessage");
            }
            ViewBag.code = code;
            ViewBag.email = email;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(Account acc, string confirmpassword)
        {
            try
            {
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                var account = accounts.SingleOrDefault(a => a.Code == acc.Code && a.Email.Equals(acc.Email));
                if (account != null)
                {
                    if (acc.Password == confirmpassword)
                    {
                        account.Password = BCrypt.Net.BCrypt.HashPassword(acc.Password);
                        account.Code = null;
                        var model = httpClient.PutAsJsonAsync(uriAccount, account).Result;
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        TempData["shortMessage"] = "Password and confirm password not match";
                        return RedirectToAction("ResetPassword", "Account", new { code = acc.Code, email = acc.Email });
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

        public IActionResult LoginGoogle(string email, string name)
        {
            if (email != null)
            {
                try
                {
                    var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                    var account = accounts.SingleOrDefault(a => a.Email.Equals(email));
                    if (account != null)
                    {
                        Role role = JsonConvert.DeserializeObject<Role>(httpClient.GetStringAsync(uriRole + account.Id).Result);
                        if (role.Position == 1 || role.Position == 3 && account.Delete_at == null)
                        {
                            HttpContext.Session.SetString("sRole", role.Position.ToString());
                            HttpContext.Session.SetString("sAccount", JsonConvert.SerializeObject(account));
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Login", "Account");
                        }
                    }
                    if (account == null)
                    {
                        var password = "12345678";
                        Account acc = new Account();
                        acc.Password = BCrypt.Net.BCrypt.HashPassword(password);
                        acc.FullName = name;
                        acc.Email = email;
                        var model = httpClient.PostAsJsonAsync(uriAccount, acc).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                            var id = accounts.Max(a => a.Id);
                            Role role = new Role() { AccountId = id, Position = 3 };
                            var modelRole = httpClient.PostAsJsonAsync(uriRole, role).Result;
                            var accnew = accounts.SingleOrDefault(a => a.Email.Equals(email));
                            HttpContext.Session.SetString("sRole", role.Position.ToString());
                            HttpContext.Session.SetString("sAccount", JsonConvert.SerializeObject(accnew));
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.Msg = "Email or password is wrong! Please try again.";
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Msg = e.Message;
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
