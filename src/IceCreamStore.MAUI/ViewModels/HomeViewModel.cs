using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Pages;
using IceCreamStore.MAUI.Services;
using IceCreamStore.Shared.Dtos;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class HomeViewModel(IIcecreamsApi icecreamsApi, AuthService authService) : BaseViewModel
    {
        private readonly IIcecreamsApi _icecreamsApi = icecreamsApi;
        private readonly AuthService _authService = authService;

        [ObservableProperty]
        private IcecreamDto[] _icecreams = [];

        [ObservableProperty]
        private string _userName = string.Empty;

        private bool _isInitialized;

        public async Task InitializeAsync()
        {
            UserName = _authService.User!.Name;

            if (_isInitialized) 
                return;

            IsBusy = true;

            try
            {
                _isInitialized = true;
                Icecreams = await _icecreamsApi.GetIcecreamsAsync();

            }
            catch (Exception ex)
            {
                _isInitialized = false;
                await ShowErrorAlertAsync(ex.Message);
            }
            finally 
            { 
                IsBusy = false; 
            }
        }

        [RelayCommand]
        private async Task GoToDetailsPageAsync(IcecreamDto icecream)
        {
            var parameter = new Dictionary<string, object>()
            {
                [nameof(DetailsViewModel.Icecream)] = icecream,
            };
            await GoToAsync(nameof(DetailsPage), animate: true, parameter);
        }
    }
}
