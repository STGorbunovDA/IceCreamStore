using CommunityToolkit.Maui.Views;
using IceCreamStore.MAUI.ViewModels;

namespace IceCreamStore.MAUI.Controls;

public partial class ChangePasswordControl : Popup
{
	public ChangePasswordControl(ChangePasswordViewModel changePasswordViewModel)
	{
		InitializeComponent();
		BindingContext = changePasswordViewModel;
	}

    private async void ClosePopup_Tapped(object sender, TappedEventArgs e) =>
		await CloseAsync();

}