using AirlineService.Models;
using AirlineService.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;
using Utility.Exceptions;

namespace AirlineService.Services
{
    public class AirlineManager : IAirlineManager
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment environment;

        public AirlineManager(AppDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public Airline Add(Airline airline)
        {
            if (context.Airlines.Any(x => x.Name == airline.Name))
                throw new AppException($"Airline with the name {{{airline.Name}}} already exists");

            context.Airlines.Add(airline);
            context.SaveChanges();
            return airline;
        }

        public AirlineResponse AirlineDetails(Guid airlineId)
        {
            var airline = context.Airlines.Select(x => new AirlineResponse
            {
                ContactAddress = x.ContactAddress,
                ContactNo = x.ContactNo,
                Id = x.Id,
                LogoPath = x.LogoPath,
                Name = x.Name,
                Status = x.Status,
                Inventories = x.AirlineInventories.Select(z => new InventoryResponse
                {
                    BusinessClassSeats = z.BusinessClassSeats,
                    EndDate = z.EndDate,
                    FlightNumber = z.FlightNumber,
                    FromPlace = z.FromPlace.City,
                    FromPlaceId = z.FromPlaceId,
                    Id = z.Id,
                    InstrumentUsed = z.InstrumentUsed,
                    Meals = z.Meals,
                    NonBusinessClassSeats = z.NonBusinessClassSeats,
                    NumberOfRows = z.NumberOfRows,
                    ScheduledDays = z.ScheduledDays,
                    StartDate = z.StartDate,
                    TicketPrice = z.TicketPrice,
                    ToPlace = z.ToPlace.City,
                    ToPlaceId = z.ToPlaceId

                }).ToList()
            }).FirstOrDefault(x => x.Id == airlineId);

            return airline;
        }

        public Airline ChangeAirlineStatus(Guid id, AirlineStatus status)
        {
            var airline = context.Airlines.Find(id);
            if (airline == null)
                throw new AppException("Invalid airline Id");

            airline.Status = status;
            context.Entry(airline).State = EntityState.Modified;
            context.SaveChanges();
            return airline;
        }

        public List<Airline> GetAirlines(bool fetchOnlyActive)
        {
            return context.Airlines.Where(x => !fetchOnlyActive || x.Status == AirlineStatus.Active).ToList();
        }

        public Airline Update(Airline airline)
        {
            if (context.Airlines.Any(x => x.Name == airline.Name && x.Id != airline.Id))
                throw new AppException($"Airline with the name {{{airline.Name}}} already exists");

            context.Entry(airline).State = EntityState.Modified;
            context.SaveChanges();
            return airline;
        }
    }
}
