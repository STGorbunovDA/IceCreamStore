using IceCreamStore.MAUI.ViewModels;

namespace IceCreamStore.MAUI.Pages;

public partial class OrderDetailsPage : ContentPage
{
	public OrderDetailsPage(OrderDetailsViewModel orderDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = orderDetailsViewModel;
	}
}