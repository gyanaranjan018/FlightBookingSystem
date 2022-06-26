using AirlineService.Models;
using AirlineService.Models.DTO;
using AirlineService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Exceptions;

namespace AirlineService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineInventoryController : Controller
    {
        private readonly IAirlineInventoryManager manager;

        public AirlineInventoryController(IAirlineInventoryManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(AirlineInventory inventory)
        {
            ModelState.Validate();
            return Ok(manager.Add(inventory));
        }

        [HttpPost("/api/flight/search")]
        public IActionResult SearchAirline(SearchParameter parameter)
        {
            return Ok(manager.SearchFlights(parameter));
        }

        [HttpGet("/api/flight/{id}")]
        public IActionResult GetFlight(Guid id)
        {
            return Ok(manager.GetFlight(id));
        }

        [HttpGet("/api/flight/airports")]
        public IActionResult GetAirports()
        {
            return Ok(manager.GetAirports());
        }

        [HttpPost("/api/booking")]
        [Authorize]
        public IActionResult BookFlight(Booking booking)
        {
            ModelState.Validate();
            return Ok(manager.BookFlight(booking));
        }

    }
}
