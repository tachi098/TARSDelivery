using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TARSDeliveryWebAPI.Models;
using TARSDeliveryWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TARSDeliveryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private readonly IPriceListServices priceListServices;
        public PriceListController(IPriceListServices priceListServices)
        {
            this.priceListServices = priceListServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetPriceLists()
        {
            return Ok(await priceListServices.priceLists());
        }
        [HttpPut]
        public async Task<IActionResult> EditPriceList([FromBody] PriceList priceList)
        {
            return Ok(await priceListServices.Edit(priceList));
        }

        [HttpPut("DeletePriceList/{id}")]
        public async Task<IActionResult> DeletePriceList([FromRoute] int id) => Ok(await priceListServices.DeletePriceList(id));

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPriceList([FromRoute] string name) => Ok(await priceListServices.GetPriceList(name));


        [HttpPost]
        public async Task<IActionResult> CreatePriceList([FromBody] PriceList priceList)
        {
            return Ok(await priceListServices.Create(priceList));
        }
    }
}
