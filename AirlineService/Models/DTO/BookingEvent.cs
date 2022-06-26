using System;
using System.ComponentModel.DataAnnotations;
using Utility.Enums;

namespace AirlineService.Models.DTO
{
    public class BookingEvent : Booking
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public FlightDetail OutboundFlight { get; set; }

        public FlightDetail ReturnFlight { get; set; }

        public int DiscountPercent { get; set; }
    }

    public class FlightDetail
    {
        public Meals Meals { get; set; }

        public DateTime Date { get; set; }

        public Guid FlightId { get; set; }

        [Required]
        public string AirlineName { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        public string LogoPath { get; set; }

        public decimal Price { get; set; }
    }
}
