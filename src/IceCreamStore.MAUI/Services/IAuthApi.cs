using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.Services
{
    public interface IAuthApi
    {
        [Post("/api/signup")]
        Task<ResultWithDataDto<AuthResponseDto>> SignupAsync(SignupRequestDto signupRequestDto);

        [Post("/api/signin")]
        Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto signinRequestDto);

    }
}
