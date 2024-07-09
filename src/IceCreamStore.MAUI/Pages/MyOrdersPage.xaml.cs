using IceCreamStore.MAUI.ViewModels;

namespace IceCreamStore.MAUI.Pages;

public partial class MyOrdersPage : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;

    public MyOrdersPage(OrdersViewModel ordersViewModel)
	{
		InitializeComponent();
        _ordersViewModel = ordersViewModel;
        BindingContext = ordersViewModel;
    }

    protected override async void OnAppearing()
    {
        await _ordersViewModel.InitializeAsync();
    }
}