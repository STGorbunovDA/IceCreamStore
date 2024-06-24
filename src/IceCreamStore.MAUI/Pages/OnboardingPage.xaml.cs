using IceCreamStore.MAUI.Services;

namespace IceCreamStore.MAUI.Pages;

public partial class OnboardingPage : ContentPage
{
    private readonly AuthService _authService;

    public OnboardingPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    //  private async void Button_Clicked(object sender, EventArgs e)
    //  {
    //      await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    //  }

    protected async override void OnAppearing()
    {
        if(_authService.User is not null && _authService.User.Id != default
            && !string.IsNullOrWhiteSpace(_authService.Token))
        {
            // user is logged in
            // Navigate user to Home Page
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }

    private async void Signin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SigninPage));
    }

    private async void Signup_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(SignupPage));
    }
}