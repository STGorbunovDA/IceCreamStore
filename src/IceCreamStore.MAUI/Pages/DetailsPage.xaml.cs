using IceCreamStore.MAUI.ViewModels;

namespace IceCreamStore.MAUI.Pages;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();
		BindingContext = detailsViewModel;
	}
}