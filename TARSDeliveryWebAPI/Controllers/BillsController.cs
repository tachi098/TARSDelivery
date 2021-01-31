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
    public class BillsController : ControllerBase
    {
        private readonly IBillServices billServices;
        private readonly IPriceListServices priceListServices;

        public BillsController(IBillServices billServices, IPriceListServices priceListServices)
        {
            this.billServices = billServices;
            this.priceListServices = priceListServices;
        }

        [HttpGet("GetBillPackages")]
        public async Task<IActionResult> GetBills() => Ok(await billServices.GetBillPackages());

        [HttpGet("GetPriceLists")]
        public async Task<IActionResult> GetPriceLists() => Ok(await priceListServices.GetPriceLists());

        [HttpPut("DeleteBill/{id}")]
        public async Task<IActionResult> DeleteBill([FromRoute] int id) => Ok(await billServices.DeleteBill(id));

        [HttpPut("UpdateBill")]
        public async Task<IActionResult> UpdateBill([FromBody] Bill bill) => Ok(await billServices.UpdateBill(bill));

        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill([FromBody] Bill bill) => Ok(await billServices.CreateBill(bill));

        [HttpPut("UndoBill/{id}")]
        public async Task<IActionResult> UndoPackage([FromRoute] int id) => Ok(await billServices.UndoBill(id));

        [HttpGet("GetBill/{id}")]
        public async Task<IActionResult> GetBill([FromRoute] int id) => Ok(await billServices.GetBill(id));
    }
}
