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
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices accountServices;
        public AccountController(IAccountServices accountServices)
        {
            this.accountServices = accountServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await accountServices.GetAccounts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount([FromRoute]int id)
        {
            return Ok(await accountServices.GetAccount(id));
        }

        [HttpPut]
        public async Task<IActionResult> PutAccount(Account account)
        {
            return Ok(await accountServices.UpdateAccount(account));
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount(Account account)
        {
            return Ok(await accountServices.CreateAccount(account));
        }

    }
}
