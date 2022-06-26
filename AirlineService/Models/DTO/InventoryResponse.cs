using System;
using Utility.Enums;

namespace AirlineService.Models.DTO
{
    public class InventoryResponse
    {
        public Guid Id { get; set; }

        public string FlightNumber { get; set; }

        public Guid FromPlaceId { get; set; }

        public string FromPlace { get; set; }

        public Guid ToPlaceId { get; set; }

        public string ToPlace { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ScheduledDays ScheduledDays { get; set; }

        public string InstrumentUsed { get; set; }

        public int BusinessClassSeats { get; set; }

        public int NonBusinessClassSeats { get; set; }

        public int TicketPrice { get; set; }

        public int NumberOfRows { get; set; }

        public Meals Meals { get; set; }
    }
}
