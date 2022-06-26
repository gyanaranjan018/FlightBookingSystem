using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enums;

namespace AirlineService.Models
{
    public class AirlineInventory
    {
        public Guid Id { get; set; }

        [Required]
        public Guid AirlineId { get; set; }

        [ForeignKey(nameof(AirlineId))]
        public Airline Airline { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        public Guid FromPlaceId { get; set; }

        [ForeignKey(nameof(FromPlaceId))]
        public Airport FromPlace { get; set; }

        [Required]
        public Guid ToPlaceId { get; set; }

        [ForeignKey(nameof(ToPlaceId))]
        public Airport ToPlace { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public ScheduledDays ScheduledDays { get; set; }

        public string InstrumentUsed { get; set; }

        [Range(1, 100)]
        public int BusinessClassSeats { get; set; }

        [Range(1, 100)]
        public int NonBusinessClassSeats { get; set; }

        [Range(1, 10000)]
        public int TicketPrice { get; set; }

        [Range(1, 100)]
        public int NumberOfRows { get; set; }

        public Meals Meals { get; set; }
    }
}
