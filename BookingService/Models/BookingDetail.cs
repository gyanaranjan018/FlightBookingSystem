using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;

namespace BookingService.Models
{
    public class BookingDetail
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public Guid BookingId { get; set; }

        [ForeignKey(nameof(BookingId))]
        public Booking Booking { get; set; }

        public int SeatNumber { get; set; }
    }
}
