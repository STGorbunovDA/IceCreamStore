using IceCreamStore.Shared.Dtos;
using System.Text.Json;

namespace IceCreamStore.MAUI.Services
{
    public class AuthService
    {
        private const string AuthKey = "AuthKey";
        public LoggedInUser? User { get; private set; }
        public string? Token { get; private set; }

        public void Signin(AuthResponseDto dto)
        {
            var serialized = JsonSerializer.Serialize(dto);
            Preferences.Default.Set(AuthKey, serialized);
            (User, Token) = dto;
        }

        public void Initialize()
        {
            if(Preferences.Default.ContainsKey(AuthKey))
            {
                var serialized = Preferences.Default.Get<string?> (AuthKey, null);

                if(string.IsNullOrWhiteSpace(serialized))
                {
                    Preferences.Default.Remove(AuthKey);
                }
                else
                {
                    (User, Token) = JsonSerializer.Deserialize<AuthResponseDto>(serialized)!;
                }
            }
        }

        public void Signout()
        {
            Preferences.Default.Remove(AuthKey);
            (User, Token) = (null,  null);
        }
    }
}
