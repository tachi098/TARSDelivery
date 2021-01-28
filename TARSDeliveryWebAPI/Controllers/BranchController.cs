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
    public class BranchController : ControllerBase
    {
        private IBranchServices branchServices;
        public BranchController(IBranchServices branchServices)
        {
            this.branchServices = branchServices;
        }
        public async Task<IActionResult> GetBranches()
        {

            return Ok(await branchServices.GetBranches());
        }
        [HttpPost]
        public async Task<IActionResult> Post(Branch branch)
        {
            return Ok(await branchServices.PostBranch(branch));
        }
    }
}
