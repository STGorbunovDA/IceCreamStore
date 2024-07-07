using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Pages;
using IceCreamStore.MAUI.Services;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        public ProfileViewModel(AuthService authService) 
        {
            _authService = authService;
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
            await GoToAsync($"//{nameof(OnboardingPage)}");
        }
    }
}
