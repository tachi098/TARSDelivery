using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TARSDeliveryWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BillController : Controller
    {

        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
