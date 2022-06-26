using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enums;

namespace BookingService.Models
{
    public class Booking
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress), Required]
        public string Email { get; set; }

        [Range(0, 100)]
        public int NoOfSeats { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public string FromPlace { get; set; }

        [Required]
        public string ToPlace { get; set; }

        public BookingStatus Status { get; set; }

        public FlightType FlightType { get; set; }

        [ForeignKey(nameof(OutBoundFlight))]
        public Guid OutBoundId { get; set; }

        [ForeignKey(nameof(ReturnFlight))]
        public Guid? ReturnId { get; set; }

        public FlightDetail OutBoundFlight { get; set; }

        public FlightDetail ReturnFlight { get; set; }

        public string CouponCode { get; set; }

        public int DiscountPercent { get; set; }

        [Required]
        public ICollection<BookingDetail> BookingDetails { get; set; }
    }

    public class FlightDetail
    {
        public Guid Id { get; set; }

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
