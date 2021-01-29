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

        public BillsController(IBillServices billServices)
        {
            this.billServices = billServices;
        }

        [HttpGet("GetBillPackages")]
        public async Task<IActionResult> GetBills() => Ok(await billServices.GetBillPackages());

        [HttpPut("DeleteBill/{id}")]
        public async Task<IActionResult> DeleteBill([FromRoute] int id) => Ok(await billServices.DeleteBill(id));

        [HttpPut("UpdateBill")]
        public async Task<IActionResult> UpdateBill([FromBody] Bill bill) => Ok(await billServices.UpdateBill(bill));

        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill([FromBody] Bill bill) => Ok(await billServices.CreateBill(bill));
    }
}
