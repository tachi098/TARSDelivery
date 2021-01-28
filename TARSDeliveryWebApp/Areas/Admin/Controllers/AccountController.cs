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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private const string uriAccount = "http://localhost:50354/api/Account/";
        private const string uriRole = "http://localhost:50354/api/Role/";
        private const string uriBranch = "http://localhost:50354/api/Branch/";
        private HttpClient httpClient = new HttpClient();
        private Random rnd = new Random();

        // Admin List
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
            var accounts = model.Where(a => !a.Email.Equals(User.Identity.Name)).ToList();
            var roles = JsonConvert.DeserializeObject<IEnumerable<Role>>(httpClient.GetStringAsync(uriRole).Result);
            ViewBag.Role = roles;
            return View(accounts);
        }

        // Admin Delete
        public IActionResult Delete(int id)
        {
            try
            {
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                var account = accounts.SingleOrDefault(a => a.Id == id);
                if (account != null)
                {
                    if (account.Delete_at == null)
                    {
                        account.Delete_at = DateTime.Now;
                        var model = httpClient.PutAsJsonAsync(uriAccount, account).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Account");
                        }
                    }
                    else
                    {
                        account.Delete_at = null;
                        var model = httpClient.PutAsJsonAsync(uriAccount, account).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Account");
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

        // Admin Detail
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
            var account = model.SingleOrDefault(a => a.Id == id);
            var roles = JsonConvert.DeserializeObject<IEnumerable<Role>>(httpClient.GetStringAsync(uriRole).Result);
            var role = roles.SingleOrDefault(r => r.AccountId.Equals(account.Id));
            ViewBag.Role = role;
            var branches = JsonConvert.DeserializeObject<IEnumerable<Branch>>(httpClient.GetStringAsync(uriBranch).Result);
            var branch = branches.SingleOrDefault(r => r.Id.Equals(account.BranchId));
            ViewBag.Branch = branch;
            return View(account);
        }

        // Admin ResetPassword for employee
        public IActionResult ResetPassword(int id)
        {
            try
            {
                string tempPassword = "12345678";
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                Account account = accounts.SingleOrDefault(a => a.Id == id);
                if (account != null)
                {
                    account.Password = BCrypt.Net.BCrypt.HashPassword(tempPassword);
                    var model = httpClient.PutAsJsonAsync(uriAccount, account).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        string path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Admin/Account/Login/?Password={account.Password}&Email={account.Email}";
                        Help.SendEmail.ResetPassword(account.Email, path).Wait();
                        return RedirectToAction("Index", "Account");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        // Admin Create account for employee
        public IActionResult Create()
        {
            var branches = JsonConvert.DeserializeObject<IEnumerable<Branch>>(httpClient.GetStringAsync(uriBranch).Result);
            ViewBag.List = new SelectList(branches, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Account acc)
        {
            try
            {
                var branches = JsonConvert.DeserializeObject<IEnumerable<Branch>>(httpClient.GetStringAsync(uriBranch).Result);
                ViewBag.List = new SelectList(branches, "Id", "Name");
                string tempPassword = "12345678";
                var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                var account = accounts.SingleOrDefault(a => a.Email.Equals(acc.Email));
                if (account == null)
                {
                    acc.Password = BCrypt.Net.BCrypt.HashPassword(tempPassword);
                    var model = httpClient.PostAsJsonAsync(uriAccount, acc).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                        var id = accounts.Max(a => a.Id);
                        Role role = new Role() { AccountId = id, Position = 2 };
                        var modelRole = httpClient.PostAsJsonAsync(uriRole, role).Result;
                        string path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Admin/Account/Login/?Password={acc.Password}&Email={acc.Email}";
                        Help.SendEmail.EmployeeLogin(acc.Email, tempPassword, path).Wait();
                        return RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    ViewBag.Msg = "Email is exists";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View();
        }

        // Admin Login
        public IActionResult Login([FromQuery(Name = "Password")] string password, [FromQuery(Name = "Email")] string email)
        {
            if (User.Identity.Name != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Password = password;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public IActionResult Login(Account acc)
        {
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
            if (!string.IsNullOrEmpty(acc.Email) && !string.IsNullOrEmpty(acc.Password))
            {
                var account = accounts.SingleOrDefault(a => a.Email.Equals(acc.Email) && BCrypt.Net.BCrypt.Verify(acc.Password, a.Password) || acc.Password == a.Password);
                if (account != null && account.Delete_at == null)
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
                    else
                    {
                        ViewBag.Msg = "User cannot login";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Msg = "Email or password wrong! Please try again";
                    return View();
                }
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
