using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Controls;
using IceCreamStore.MAUI.Pages;
using IceCreamStore.MAUI.Services;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly ChangePasswordViewModel _changePasswordViewModel;

        public ProfileViewModel(AuthService authService, ChangePasswordViewModel changePasswordViewModel) 
        {
            _authService = authService;
            _changePasswordViewModel = changePasswordViewModel;
        }

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initials))]
        private string _name = "";

        public string Initials
        {
            get
            {
                // Dmitriy Gorbunov -> parts[0] = Dmitriy       parts[1] = gorbunov
                var parts = Name.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                
                if(parts.Length > 1)
                {
                    return $"{parts[0][0]}{parts[1][0]}".ToUpper(); // -> DG
                }
                return Name.Length > 1 ? Name[..1].ToUpper() : Name.ToUpper();
            }
        }

        public void Initialize()
        {
            Name = _authService.User!.Name;
        }

        [RelayCommand]
        private async Task SignoutAsync()
        {
            _authService.Signout();
            await GoToAsync($"//{nameof(OnboardingPage)}");
        }

        [RelayCommand]
        private async Task GoToMyOrdersAsync() => 
            await GoToAsync(nameof(MyOrdersPage), animate: true);

        [RelayCommand]
        private async Task ChangePasswordAsync()
        {
            await Shell.Current.CurrentPage.ShowPopupAsync(new ChangePasswordControl(_changePasswordViewModel));
        }
    }
}
