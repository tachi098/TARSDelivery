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
    public class PackageController : ControllerBase
    {
        private readonly IPackageServices packageServices;
        public PackageController(IPackageServices packageServices)
        {
            this.packageServices = packageServices;
        }
        public async Task<IActionResult> GetPackages()
        {
            return Ok(await packageServices.GetPackages());
        }
        [HttpGet("{code}")]
        public async Task<IActionResult> GetPackage([FromRoute] int code)
        {
            return Ok(await packageServices.GetPackage(code));
        }
        [HttpPost()]
        public async Task<IActionResult> CreatePackage([FromBody] Package package)
        {
            return Ok(await packageServices.CreatePackage(package));
        }
    }
}
