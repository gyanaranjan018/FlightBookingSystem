using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enums;

namespace AuthService.Models.DTO
{
    public class AuthResponse
    {
        public string Token { get; set; }

        public Guid Id { get; set; }

        public UserRole UserRole { get; set; }
    }
}
