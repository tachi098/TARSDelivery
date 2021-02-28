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
    public class PackagesController : ControllerBase
    {
        private readonly IPackageServices packageServices;

        public PackagesController(IPackageServices packageServices)
        {
            this.packageServices = packageServices;
        }

        [HttpPut("DeletePackage/{id}")]
        public async Task<IActionResult> DeletePackage([FromRoute] int id) => Ok(await packageServices.DeletePackage(id));

        [HttpGet("GetPackage/{id}")]
        public async Task<IActionResult> GetPackage([FromRoute] int id) => Ok(await packageServices.GetPackage(id));
        
        [HttpPut("UpdatePackage")]
        public async Task<IActionResult> UpdatePackage([FromBody] Package package) => Ok(await packageServices.UpdatePackage(package));

        [HttpPost("CreatePackage")]
        public async Task<IActionResult> CreatePackage([FromBody] Package package) => Ok(await packageServices.CreatePackage(package));

        [HttpGet("GetNewPackage")]
        public async Task<IActionResult> GetNewPackage() => Ok(await packageServices.GetNewPackage());

        [HttpPut("UndoPackage/{id}")]
        public async Task<IActionResult> UndoPackage([FromRoute] int id) => Ok(await packageServices.UndoPackage(id));

        [HttpGet]
        public async Task<IActionResult> GetPackages() => Ok(await packageServices.GetPackages());
    }
}
