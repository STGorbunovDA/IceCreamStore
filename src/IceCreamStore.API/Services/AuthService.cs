using IceCreamStore.API.Data;
using IceCreamStore.API.Data.Entities;
using IceCreamStore.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

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

            if (await _context.Users.AsNoTracking().AnyAsync(u => u.Email == signupRequestDto.Email))
            {
                return ResultWithDataDto<AuthResponseDto>.Failure("Email alredy exists");
            }

            var user = new User
            {
                Email = signupRequestDto.Email,
                Address = signupRequestDto.Address,
                Name = signupRequestDto.Name,
            };

            (user.Salt, user.Hash) = _passwordService.GenerateSaltAndHash(signupRequestDto.Password);

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return GenerateAuthResponse(user);
            }
            catch (Exception ex)
            {
                return ResultWithDataDto<AuthResponseDto>.Failure(ex.Message);
            }


        }

        public async Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto signinRequestDto)
        {
            var dbUser = await _context.Users
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(u => u.Email == signinRequestDto.Email);
            if (dbUser is null)
                return ResultWithDataDto<AuthResponseDto>.Failure("User does not exist");

            if(!_passwordService.AreEqual(signinRequestDto.Password, dbUser.Salt, dbUser.Hash))
            {
                return ResultWithDataDto<AuthResponseDto>.Failure("Incorrect password");
            }

            return GenerateAuthResponse(dbUser);
        }

        private ResultWithDataDto<AuthResponseDto> GenerateAuthResponse(User user)
        {
            var loggedInUser = new LoggedInUser(user.Id, user.Name, user.Email, user.Address);
            var token = _tokenService.GeneratedToken(loggedInUser);
            var authResponse = new AuthResponseDto(loggedInUser, token);

            return ResultWithDataDto<AuthResponseDto>.Success(authResponse);
        }
    }
}
