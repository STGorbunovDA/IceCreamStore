using CommunityToolkit.Mvvm.ComponentModel;
using IceCreamStore.MAUI.Services;
using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.ViewModels
{
    public partial class OrdersViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IOrderApi _orderApi;

        public OrdersViewModel(AuthService authService, IOrderApi orderApi)
        {
            _authService = authService;
            _orderApi = orderApi;
        }

        [ObservableProperty]
        private OrderDto[] _orders = [];

        public async Task InitializeAsync() => await LoadOrdersAsync();

        private async Task LoadOrdersAsync()
        {
            IsBusy = true;
            try
            {
                Orders = await _orderApi.GetMyOrdersAsync();
                if(Orders.Length == 0)
                {
                    await ShowToastAsync("No orders found");
                }
            }
            catch (ApiException ex)
            {
                await HandleApiExeptionAsync(ex, () => _authService.Signout());
            }
            finally { IsBusy = false; }
        }
    }
}
