using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Pages;
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

        [RelayCommand]
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

        [RelayCommand]
        private async Task GoToOrderDetailsPageAsync(long orderId)
        {
            var parametr = new Dictionary<string, object>
            {
                [nameof(OrderDetailsViewModel.OrderId)] = orderId,
            };
            await GoToAsync(nameof(OrderDetailsPage), animate: true, parametr);
        }
    }
}
