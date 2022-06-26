using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;

namespace AirlineService.Models.DTO
{
    public class BookingDetail
    {
        [Required(ErrorMessage = "Pessenger name is required")]
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public int SeatNumber { get; set; }
    }
}
