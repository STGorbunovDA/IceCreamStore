using IceCreamStore.API.Data;
using IceCreamStore.Shared.Dtos;
using System.Threading.Tasks;

namespace IceCreamStore.API.Services
{
    public class AuthService(DataContext context, TokenService tokenService, 
        PasswordService passwordService)
    {
        private readonly DataContext _context = context;
        private readonly TokenService _tokenService = tokenService;
        private readonly PasswordService _passwordService = passwordService;

        public async Task<ResultWithDataDto<AuthResponseDto>> SignupAsync(SignupRequestDto signupRequestDto)
        {
            return null;
        }

        public async Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto signinRequestDto)
        {
            return null;
        }
    }
}
