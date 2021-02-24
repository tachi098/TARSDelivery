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
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;

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
                        string path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Admin/Account/Login";
                        Help.SendEmail.EmployeeLogin(account.Email, tempPassword, path).Wait();
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
                    acc.FullName = acc.Email;
                    acc.Birthday = DateTime.Now;
                    var model = httpClient.PostAsJsonAsync(uriAccount, acc).Result;
                    if (model.IsSuccessStatusCode)
                    {
                        accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
                        var id = accounts.Max(a => a.Id);
                        Role role = new Role() { AccountId = id, Position = 2 };
                        var modelRole = httpClient.PostAsJsonAsync(uriRole, role).Result;
                        string path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Admin/Account/Login";
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
        public IActionResult Login()
        {
            if (User.Identity.Name != null)
            {
                return RedirectToAction("Index", "Home");
            }
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
                            new Claim("AccountId", account.Id.ToString()),
                            new Claim(ClaimTypes.Name,account.Email),
                            new Claim(ClaimTypes.Role,"Admin"),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                        isAuthenticate = true;
                    }
                    if (role.Position == 2)
                    {
                        identity = new ClaimsIdentity(new[]
                        {
                            new Claim("AccountId", account.Id.ToString()),
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

        //Employee Profile
        [Authorize(Roles = "Manager")]
        public IActionResult Profile()
        {
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(httpClient.GetStringAsync(uriAccount).Result);
            Account account = accounts.SingleOrDefault(a => a.Email.Equals(User.Identity.Name));
            return View(account);
        }

        [HttpPost]
        public IActionResult Profile(Account acc, IFormFile file, string confirmpassword, string password)
        {
            try
            {
                var modelOld = JsonConvert.DeserializeObject<Account>(httpClient.GetStringAsync($"{uriAccount}{acc.Id}").Result);
                var pathImageOld = modelOld?.Avartar;
                var passwordold = modelOld.Password;
                acc.Create_at = modelOld.Create_at;
                acc.Update_at = DateTime.Now;
                if (acc.Birthday == null)
                {
                    acc.Birthday = modelOld.Birthday;
                }
                if (file == null)
                {
                    if (password != null && confirmpassword != null)
                    {
                        if (password == confirmpassword)
                        {
                            acc.Password = BCrypt.Net.BCrypt.HashPassword(password);
                            acc.Avartar = modelOld.Avartar;
                            var model = httpClient.PutAsJsonAsync(uriAccount, acc).Result;
                            if (model.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            acc.Avartar = modelOld.Avartar;
                            ViewBag.Msg = "Password and password confirm not match";
                            return View(acc);
                        }
                    }
                    else
                    {
                        acc.Password = passwordold;
                        acc.Avartar = modelOld.Avartar;
                        var model = httpClient.PutAsJsonAsync(uriAccount, acc).Result;
                        if (model.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    if (file.Length > 0)
                    {
                        string name = file.FileName;
                        if (name.Contains(".jpg") || name.Contains(".png") || name.Contains(".gif"))
                        {
                            if (password != null && confirmpassword != null)
                            {
                                if (password == confirmpassword)
                                {
                                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                    var rename = Convert.ToString(Guid.NewGuid()) + "." + fileName.Split('.').Last();
                                    var path = Path.Combine("wwwroot/images", rename);
                                    var stream = new FileStream(path, FileMode.Create);
                                    file.CopyToAsync(stream);
                                    acc.Avartar = "images/" + rename;
                                    acc.Password = BCrypt.Net.BCrypt.HashPassword(password);
                                    var model = httpClient.PutAsJsonAsync(uriAccount, acc).Result;
                                    if (model.IsSuccessStatusCode)
                                    {
                                        if (!string.IsNullOrEmpty(pathImageOld))
                                        {
                                            var pathOld = Path.Combine("wwwroot", pathImageOld);
                                            if (System.IO.File.Exists(pathOld) && pathImageOld != "images/p1.png")
                                            {
                                                System.GC.Collect();
                                                System.GC.WaitForPendingFinalizers();
                                                System.IO.File.Delete(pathOld);
                                            }
                                        }
                                        return RedirectToAction("Index", "Home");
                                    }
                                }
                                else
                                {
                                    acc.Avartar = modelOld.Avartar;
                                    ViewBag.Msg = "Password and password confirm not match";
                                    return View(acc);
                                }
                            }
                            else
                            {
                                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                var rename = Convert.ToString(Guid.NewGuid()) + "." + fileName.Split('.').Last();
                                var path = Path.Combine("wwwroot/images", rename);
                                var stream = new FileStream(path, FileMode.Create);
                                file.CopyToAsync(stream);
                                acc.Avartar = "images/" + rename;
                                acc.Password = passwordold;
                                var model = httpClient.PutAsJsonAsync(uriAccount, acc).Result;
                                if (model.IsSuccessStatusCode)
                                {
                                    if (!string.IsNullOrEmpty(pathImageOld))
                                    {
                                        var pathOld = Path.Combine("wwwroot", pathImageOld);
                                        if (System.IO.File.Exists(pathOld) && pathImageOld != "images/p1.png")
                                        {
                                            System.GC.Collect();
                                            System.GC.WaitForPendingFinalizers();
                                            System.IO.File.Delete(pathOld);
                                        }
                                    }
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                        else
                        {
                            acc.Avartar = modelOld.Avartar;
                            ViewBag.Msg = "File is invalid";
                            return View(acc);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Msg = e.Message;
            }
            return View(acc);
        }

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
    }
}
