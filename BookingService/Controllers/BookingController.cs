using BookingService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingManager bookingManager;

        public BookingController(IBookingManager bookingManager)
        {
            this.bookingManager = bookingManager;
        }

        [HttpGet("ticket/{pnr}")]
        public IActionResult Get(Guid pnr)
        {
            return Ok(bookingManager.GetBooking(pnr));
        }

        [HttpGet("history")]
        public IActionResult GetByEmail([FromQuery] string emailId)
        {
            return Ok(bookingManager.GetByEmail(emailId));
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            return Ok(bookingManager.GetAll());
        }

        [HttpDelete("cancel/{pnr}")]
        public IActionResult Cancel(Guid pnr)
        {
            return Ok(bookingManager.CancelBooking(pnr));
        }
    }
}
