using IceCreamStore.Shared.Dtos;

namespace IceCreamStore.MAUI.Services
{
    public class AuthService
    {
        public LoggedInUser User { get; private set; }
        public string? Token { get; private set; }

        public void Signin(AuthResponseDto dto)
        {
            (User, Token) = dto;
        }

        public void Signout()
        {

        }
    }
}
