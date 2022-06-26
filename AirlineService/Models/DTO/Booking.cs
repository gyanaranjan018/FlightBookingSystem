using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Utility.Enums;

namespace AirlineService.Models.DTO
{
    public class Booking
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }

        [Range(0, 100)]
        public int NoOfSeats { get; set; }

        public Meals Meals { get; set; }

        public Meals ReturnMeals { get; set; }

        public Guid OutBoundFlightId { get; set; }

        public Guid? ReturnFlightId { get; set; }

        public DateTime Date { get; set; }

        public DateTime? ReturnDate { get; set; }

        public FlightType FlightType { get; set; }

        public string CouponCode { get; set; }

        [Required]
        public List<BookingDetail> BookingDetails { get; set; }
    }
}
