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

        public ServicesController(IPriceListServices priceListServices, IBillServices billServices)
        {
            this.priceListServices = priceListServices;
            this.billServices = billServices;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPriceList([FromRoute] string name)
        {
            return Ok(await priceListServices.GetPriceList(name));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill([FromBody] Bill bill)
        {
            return Ok(await billServices.CreateBill(bill));
        }


    }
}
