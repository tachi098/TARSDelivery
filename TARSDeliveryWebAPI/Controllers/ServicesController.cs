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
    public class ServicesController : ControllerBase
    {
        private readonly IPriceListServices priceListServices;

        public ServicesController(IPriceListServices priceListServices)
        {
            this.priceListServices = priceListServices;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPriceList([FromRoute] string name)
        {
            return Ok(await priceListServices.GetPriceList(name));
        }
    }
}
