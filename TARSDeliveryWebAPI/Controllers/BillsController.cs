using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Services.Interfaces;

namespace TARSDeliveryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillServices billServices;

        public BillsController(IBillServices billServices)
        {
            this.billServices = billServices;
        }

        [HttpGet("GetBillPackages")]
        public async Task<IActionResult> GetBills() => Ok(await billServices.GetBillPackages());
    }
}
