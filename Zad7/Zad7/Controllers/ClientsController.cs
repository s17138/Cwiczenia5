using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad7.Services;

namespace Zad7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClientsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveClient(int id)
        {
            var result = await _dbService.RemoveClient(id);
            if (result)
            {
                return Ok("Removed Client");
            } else
            {
                return BadRequest();
            }
        }
    }
}
