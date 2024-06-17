using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.Shared.Dtos;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class AuthViewModel : BaseViewModel
    {

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _password;

        [ObservableProperty]
        private string? _address;

        public bool CanSignin => !string.IsNullOrEmpty(Email)
                                    && !string.IsNullOrEmpty(Password);

        public bool CanSignup => CanSignin
                                    && !string.IsNullOrEmpty(Password)
                                    && !string.IsNullOrEmpty(Address);

        [RelayCommand]
        private async Task SignupAsync()
        {
            IsBusy = true;

            try
            {
                var signupDto = new SignupRequestDto (Name, Email, Password, Address);

                // Make Api Call
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
