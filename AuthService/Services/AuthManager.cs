using AuthService.Models;
using AuthService.Models.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Enums;
using Utility.Exceptions;

namespace AuthService.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly string key;
        private readonly string issuer;
        private readonly string audience;
        private readonly AppDbContext context;

        public AuthManager(IConfiguration configuration, AppDbContext context)
        {
            key = configuration["JWT:Secret"];
            issuer = configuration["JWT:Issuer"];
            audience = configuration["JWT:Audience"];
            this.context = context;
        }

        public AuthResponse AuthenticateAdmin(AuthRequest user)
        {
            var dbUser = context.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password && x.Role == UserRole.Admin);
            if (dbUser == null)
                throw new AuthorizationException("Either username or password is incorrect");

            return GenerateToken(dbUser);
        }
        public AuthResponse AuthenticateUser(AuthRequest user)
        {
            var dbUser = context.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (dbUser == null)
                throw new Exception("Either username or password is incorrect!");

            return GenerateToken(dbUser);
        }

        private AuthResponse GenerateToken(User user)
        {
            var tokenHandlder = new JwtSecurityTokenHandler();

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256)
            };

            var securityToken = tokenHandlder.CreateToken(descriptor);
            var token = tokenHandlder.WriteToken(securityToken);
            return new AuthResponse
            {
                Token = token,
                Id = user.Id,
                UserRole = user.Role
            };
        }

        public void UserRegister(AuthRequest authRequest)
        {
            var user = new User
            {
                Username = authRequest.Username,
                Password = authRequest.Password,
                Role = UserRole.User
            };

            if (context.Users.Any(x => x.Username == user.Username))
                throw new AppException($"User with username {{{user.Username}}} already exists");

            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
