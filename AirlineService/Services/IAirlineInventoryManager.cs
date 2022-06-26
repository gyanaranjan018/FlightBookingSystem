using AirlineService.Models;
using AirlineService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineService.Services
{
    public interface IAirlineInventoryManager
    {
        AirlineInventory Add(AirlineInventory airline);

        AirlineInventory Update(AirlineInventory airline);

        List<AirlineInventory> GetInventories();

        List<Airport> GetAirports();

        FlightList SearchFlights(SearchParameter parameter);

        FlightResponse GetFlight(Guid id);

        BookingEvent BookFlight(Booking booking);
    }
}
