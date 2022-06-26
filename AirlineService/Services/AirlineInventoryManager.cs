using AirlineService.Models;
using AirlineService.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Enums;
using Utility.Exceptions;

namespace AirlineService.Services
{
    public class AirlineInventoryManager : IAirlineInventoryManager
    {
        private readonly AppDbContext context;
        private readonly HttpContext httpContext;

        public AirlineInventoryManager(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            this.context = context;
            httpContext = contextAccessor.HttpContext;
        }

        public AirlineInventory Add(AirlineInventory inventory)
        {
            context.AirlineInventories.Add(inventory);
            context.SaveChanges();
            return inventory;
        }

        public List<AirlineInventory> GetInventories()
        {
            return context.AirlineInventories.ToList();
        }

        public List<Airport> GetAirports()
        {
            return context.Airports.ToList();
        }

        public FlightList SearchFlights(SearchParameter parameter)
        {
            var result = new FlightList
            {
                OutboundFlights = context.AirlineInventories
                .Where(x => x.EndDate > parameter.Date
                            && x.StartDate < parameter.Date
                            && x.Airline.Status == AirlineStatus.Active
                            && x.FromPlaceId == parameter.FromPlaceId
                            && x.ToPlaceId == parameter.ToPlaceId)
                .Select(x => new FlightResponse
                {
                    InventoryId = x.Id,
                    AirlineName = x.Airline.Name,
                    Date = parameter.Date,
                    FlightNumber = x.FlightNumber,
                    FromPlace = x.FromPlace.City,
                    ToPlace = x.ToPlace.City,
                    Price = x.TicketPrice,
                    LogoPath = x.Airline.LogoPath,
                }).ToList()
            };

            if (parameter.FlightType == FlightType.RoundTrip)
            {
                result.ReturnFlights = context.AirlineInventories
                .Where(x => x.EndDate > parameter.ReturnDate
                            && x.StartDate < parameter.ReturnDate
                            && x.Airline.Status == AirlineStatus.Active
                            && x.FromPlaceId == parameter.ToPlaceId
                            && x.ToPlaceId == parameter.FromPlaceId)
                .Select(x => new FlightResponse
                {
                    InventoryId = x.Id,
                    AirlineName = x.Airline.Name,
                    Date = parameter.ReturnDate.Value,
                    FlightNumber = x.FlightNumber,
                    FromPlace = x.FromPlace.City,
                    ToPlace = x.ToPlace.City,
                    Price = x.TicketPrice,
                    LogoPath = x.Airline.LogoPath,
                }).ToList();
            }
            return result;
        }

        public FlightResponse GetFlight(Guid id)
        {
            var result = context.AirlineInventories.Where(x => x.Id == id).Select(x => new FlightResponse
            {
                InventoryId = x.Id,
                AirlineName = x.Airline.Name,
                Date = DateTime.Now,
                FlightNumber = x.FlightNumber,
                FromPlace = x.FromPlace.City,
                ToPlace = x.ToPlace.City,
                Price = x.TicketPrice,
                LogoPath = x.Airline.LogoPath,
            });

            return result.FirstOrDefault();
        }

        public AirlineInventory Update(AirlineInventory inventory)
        {
            context.Entry(inventory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return inventory;
        }

        public BookingEvent BookFlight(Booking booking)
        {
            if (booking.NoOfSeats != booking.BookingDetails.Count)
                throw new AppException("The count of details doesn't match with number of seats booked");

            AirlineInventory returnInventory = null;

            if (booking.FlightType == FlightType.RoundTrip)
            {
                if (booking.ReturnDate == null)
                    throw new AppException("Return date is required in case of round trip");

                if (booking.ReturnFlightId == null)
                    throw new AppException("Return flight is required in case of round trip");

                returnInventory = context.AirlineInventories.
                Include(x => x.Airline).
                Include(x => x.ToPlace).
                Include(x => x.FromPlace).
                FirstOrDefault(x => x.Id == booking.ReturnFlightId);

                if (returnInventory == null)
                    throw new AppException("Invalid ReturnFlightId");
            }

            var inventory = context.AirlineInventories.
                Include(x => x.Airline).
                Include(x => x.ToPlace).
                Include(x => x.FromPlace).
                FirstOrDefault(x => x.Id == booking.OutBoundFlightId);

            if (inventory == null)
                throw new AppException("Invalid FlightId");

            var bookingEvent = new BookingEvent
            {
                Id = Guid.NewGuid(),
                Date = booking.Date,
                Meals = booking.Meals,
                Name = booking.Name,
                Email = booking.Email,
                FromPlace = inventory.FromPlace.City,
                ToPlace = inventory.ToPlace.City,
                BookingDetails = booking.BookingDetails,
                NoOfSeats = booking.NoOfSeats,
                UserId = GetUserId(),
                FlightType = booking.FlightType
            };

            bookingEvent.OutboundFlight = new FlightDetail
            {
                AirlineName = inventory.Airline.Name,
                Date = booking.Date,
                Meals = booking.Meals,
                FlightId = booking.OutBoundFlightId,
                FlightNumber = inventory.FlightNumber,
                LogoPath = inventory.Airline.LogoPath,
                Price = inventory.TicketPrice
            };

            if (booking.FlightType == FlightType.RoundTrip)
            {
                bookingEvent.ReturnFlight = new FlightDetail
                {
                    AirlineName = returnInventory.Airline.Name,
                    Date = booking.ReturnDate.Value,
                    Meals = booking.ReturnMeals,
                    FlightId = booking.ReturnFlightId.Value,
                    FlightNumber = returnInventory.FlightNumber,
                    LogoPath = returnInventory.Airline.LogoPath,
                    Price = returnInventory.TicketPrice
                };
            }

            if (!string.IsNullOrEmpty(booking.CouponCode))
            {
                var coupon = context.DiscountCoupons.FirstOrDefault(x => x.CouponCode == booking.CouponCode && x.ValidUpto > DateTime.Now);
                if (coupon != null)
                {
                    bookingEvent.CouponCode = coupon.CouponCode;
                    bookingEvent.DiscountPercent = coupon.DiscountPercent;
                }
            }
            var factory = new ConnectionFactory { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("FlightBooking", false, false, false, null);
            var message = JsonConvert.SerializeObject(bookingEvent);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", "FlightBooking", null, body);

            return bookingEvent;
        }

        private Guid GetUserId() => Guid.Parse(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
    }
}
