using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;

namespace AirlineService.Models.DTO
{
    public class SearchParameter
    {
        public DateTime Date { get; set; }

        public DateTime? ReturnDate { get; set; }

        public Guid FromPlaceId { get; set; }

        public Guid ToPlaceId { get; set; }

        public FlightType FlightType { get; set; }
    }
}
