using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Services.Interfaces;

namespace TARSDeliveryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IPriceListServices priceListServices;
        private readonly IBillServices billServices;
        private readonly IPackageServices packageServices;

        public ServicesController(IPriceListServices priceListServices, IBillServices billServices, IPackageServices packageServices)
        {
            this.priceListServices = priceListServices;
            this.billServices = billServices;
            this.packageServices = packageServices;
        }

        [HttpGet("GetPriceList/{name}")]
        public async Task<IActionResult> GetPriceList([FromRoute] string name)
        {
            return Ok(await priceListServices.GetPriceList(name));
        }

        [HttpGet("GetNewPackage")]
        public async Task<IActionResult> GetNewPackage()
        {
            return Ok(await packageServices.GetNewPackage());
        }

        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill([FromBody] Bill bill)
        {
            return Ok(await billServices.CreateBill(bill));
        }

        [HttpPost("CreatePackage")]
        public async Task<IActionResult> CreatePackage([FromBody] Package package)
        {
            return Ok(await packageServices.CreatePackage(package));
        }
    }
}
