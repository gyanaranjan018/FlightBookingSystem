using AirlineService.Models;
using AirlineService.Models.DTO;
using System;
using System.Collections.Generic;
using Utility.Enums;

namespace AirlineService.Services
{
    public interface IAirlineManager
    {
        Airline Add(Airline airline);

        AirlineResponse AirlineDetails(Guid airline);

        Airline Update(Airline airline);

        Airline ChangeAirlineStatus(Guid id, AirlineStatus status);

        List<Airline> GetAirlines(bool fetchOnlyActive);
    }
}
