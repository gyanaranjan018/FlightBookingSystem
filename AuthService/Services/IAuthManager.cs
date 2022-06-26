using AuthService.Models.DTO;

namespace AuthService.Services
{
    public interface IAuthManager
    {
        AuthResponse AuthenticateAdmin(AuthRequest User);
        AuthResponse AuthenticateUser(AuthRequest user);

        void UserRegister(AuthRequest user);
    }
}
