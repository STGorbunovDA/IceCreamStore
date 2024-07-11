using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.Services
{
    public interface IAuthApi
    {
        [Post("/api/auth/signup")]
        Task<ResultWithDataDto<AuthResponseDto>> SignupAsync(SignupRequestDto signupRequestDto);

        [Post("/api/auth/signin")]
        Task<ResultWithDataDto<AuthResponseDto>> SigninAsync(SigninRequestDto signinRequestDto);

        [Headers("Authorization: Bearer")]
        [Post("/api/auth/change-password")]
        Task<ResultDto> ChangePasswordAsync(ChangePasswordDto dto);
    }
}
