using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Services;
using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class ChangePasswordViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IAuthApi _authApi;

        public ChangePasswordViewModel(AuthService authService, IAuthApi authApi)
        {
            _authService = authService;
            _authApi = authApi;
        }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _oldPassword;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _newPassword;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanChangePassword))]
        private string? _confirmNewPassword;

        public bool CanChangePassword =>
            !string.IsNullOrWhiteSpace(OldPassword) &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            !string.IsNullOrWhiteSpace(ConfirmNewPassword);

        [RelayCommand]
        private async Task ChangePasswordAsync()
        {
            if (NewPassword != ConfirmNewPassword)
            {
                await ShowErrorAlertAsync("New password and confirm new password do not match");
                return;
            }
            IsBusy = true;
            try
            {
                var dto = new ChangePasswordDto(OldPassword!, NewPassword!);
                var result = await _authApi.ChangePasswordAsync(dto);

                if(!result.IsSuccess)
                {
                    await ShowErrorAlertAsync(result.ErrorMessage);
                }

                await ShowAlertAsync("Success", "Password changed successfully");
            }
            catch (ApiException ex)
            {
                await HandleApiExeptionAsync(ex, () => _authService.Signout());
            }
            finally { IsBusy = false; OldPassword = NewPassword = ConfirmNewPassword = null; }
        }
    }
}
