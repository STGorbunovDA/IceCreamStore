using IceCreamStore.MAUI.ViewModels;

namespace IceCreamStore.MAUI.Pages;

public partial class CartPage : ContentPage
{
	public CartPage(CartViewModel cartViewModel)
	{
		InitializeComponent();
		BindingContext = cartViewModel;
	}
}