using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;

namespace AuthService.Models
{
    public class User
    {
        
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public UserRole Role { get; set; }

    }
}
