using System;
using System.Collections.Generic;

namespace AirlineService.Models.DTO
{
    public class FlightResponse
    {
        public Guid InventoryId { get; set; }

        public string AirlineName { get; set; }

        public string FlightNumber { get; set; }

        public DateTime Date { get; set; }

        public string FromPlace { get; set; }

        public string ToPlace { get; set; }

        public int Price { get; set; }

        public string LogoPath { get; set; }
    }

    public class FlightList
    {
        public List<FlightResponse> OutboundFlights { get; set; }

        public List<FlightResponse> ReturnFlights { get; set; }
    }
}
