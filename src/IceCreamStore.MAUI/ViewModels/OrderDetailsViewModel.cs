using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IceCreamStore.MAUI.Pages;
using IceCreamStore.MAUI.Services;
using IceCreamStore.Shared.Dtos;
using Refit;

namespace IceCreamStore.MAUI.ViewModels
{
    [QueryProperty(nameof(OrderId), nameof(OrderId))]
    public partial class OrderDetailsViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly IOrderApi _orderApi;

        public OrderDetailsViewModel(AuthService authService, IOrderApi orderApi) 
        {
            _authService = authService;
            _orderApi = orderApi;
        }

        [ObservableProperty]
        private long _orderId;

        [ObservableProperty]
        private OrderItemDto[] _orderItems = [];

        [ObservableProperty]
        private string _title = "Order Items";

        partial void OnOrderIdChanged(long value)
        {
            Title = $"Order #{value}";
            LoadOrderItemsAsync(value);
        }

        private async Task LoadOrderItemsAsync(long orderId)
        {
            IsBusy = true;
            try
            {
                OrderItems = await _orderApi.GetOrderItemsAsync(orderId);
            }
            catch (ApiException ex)
            {
                await HandleApiExeptionAsync(ex, () => _authService.Signout());
            }
            finally { IsBusy = false; }
        }

        
    }
}
