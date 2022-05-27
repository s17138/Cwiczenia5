using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zad7.Models.DTO;
using Zad7.Services;

namespace Zad7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public TripsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _dbService.GetTrips();
            return Ok(trips);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveTrip(int id)
        {
            await _dbService.RemoveTrip(id);
            return Ok("Removed Trip");
        }

        [HttpPost]
        [Route("{idTrip}/clients")]
        public async Task<IActionResult> AsssignClientToTrip(int idTrip, RequestClient client)
        {
            var result = await _dbService.AssignClientToTrip(idTrip, client);
            if (result)
            {
                return Ok("Assigned Sucesfully");
            } else
            {
                return BadRequest();
            }
        }
    }
}
