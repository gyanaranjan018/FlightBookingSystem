using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enums;

namespace AirlineService.Models
{
    public class Airline
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ContactNo { get; set; }

        public string ContactAddress { get; set; }

        public string LogoPath { get; set; }

        //[NotMapped]
        //public IFormFile File { get; set; }

        public AirlineStatus Status { get; set; }

        public ICollection<AirlineInventory> AirlineInventories { get; set; }
    }
}
