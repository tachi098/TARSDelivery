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
    public class CommentController : Controller
    {

        private string uri = "http://localhost:50354/api/Comment/";
        private HttpClient httpClient = new HttpClient();

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var res = JsonConvert.DeserializeObject<IEnumerable<Comment>>(
                                                        httpClient.GetStringAsync(uri).Result);
            return View(res);
        }

        public IActionResult Details(int id)
        {
            var comment = JsonConvert.DeserializeObject<IEnumerable<Comment>>(httpClient.GetStringAsync(uri).Result);
            Comment comments = comment.SingleOrDefault(a => a.Id.Equals(id));
            return View(comments);
        }

    }


}
