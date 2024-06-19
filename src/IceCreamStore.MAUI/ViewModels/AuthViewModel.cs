using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Pages;
using IceCreamStore.MAUI.Services;
using IceCreamStore.Shared.Dtos;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class AuthViewModel(IAuthApi authApi) : BaseViewModel
    {
        private readonly IAuthApi _authApi;


        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _name;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _email;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignin)), NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _password;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(CanSignup))]
        private string? _address;

        public bool CanSignin => !string.IsNullOrEmpty(Email)
                                    && !string.IsNullOrEmpty(Password);

        public bool CanSignup => CanSignin
                                    && !string.IsNullOrEmpty(Name)
                                    && !string.IsNullOrEmpty(Address);

        [RelayCommand]
        private async Task SignupAsync()
        {
            IsBusy = true;

            try
            {
                var signupDto = new SignupRequestDto (Name, Email, Password, Address);

                var result = await _authApi.SignupAsync(signupDto);

                if(result.IsSuccess)
                {
                    await ShowAlertAsync(result.Data.Token);
                    // Navigate to HomePage
                    await GoToAsync($"//{nameof(HomePage)}", animate: true);
                }
                else
                {
                    // Display Error Alert
                    await ShowErrorAlertAsync(result.ErrorMessage ?? "Unnow error in signing up");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SigninAsync()
        {
            IsBusy = true;

            try
            {
                var signinDto = new SigninRequestDto(Email, Password);

                var result = await _authApi.SigninAsync(signinDto);

                if (result.IsSuccess)
                {
                    await ShowAlertAsync(result.Data.User.Email);
                    // Navigate to HomePage
                    await GoToAsync($"//{nameof(HomePage)}", animate: true);
                }
                else
                {
                    // Display Error Alert
                    await ShowErrorAlertAsync(result.ErrorMessage ?? "Unnow error in signing up");
                }
            }
            catch (Exception ex)
            {
                await ShowErrorAlertAsync(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
