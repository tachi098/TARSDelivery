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
    public class CommentController : ControllerBase
    {
        private readonly ICommentServices commentServices;
        public CommentController(ICommentServices commentServices)
        {
            this.commentServices = commentServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetComments() => Ok(await commentServices.comments());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id) => Ok(await commentServices.GetComment(id));

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment) =>  Ok(await commentServices.Create(comment));


        [HttpPut("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id) => Ok(await commentServices.DeleteComment(id));

    }
}
