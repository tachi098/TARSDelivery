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
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices roleServices;

        public RoleController(IRoleServices roleServices)
        {
            this.roleServices = roleServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await roleServices.GetRoles());
        }

        [HttpGet("{accountid}")]
        public async Task<IActionResult> GetRole(int accountid)
        {
            return Ok(await roleServices.GetRole(accountid));
        }

        [HttpPost]
        public async Task<IActionResult> PostRole(Role role)
        {
            return Ok(await roleServices.CreateRole(role));
        }
    }
}
