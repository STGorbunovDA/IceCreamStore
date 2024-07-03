using IceCreamStore.MAUI.Services;
using IceCreamStore.MAUI.ViewModels;

namespace IceCreamStore.MAUI
{
    public partial class App : Application
    {
        private readonly CartViewModel _cartViewModel;

        public App(AuthService authService, CartViewModel cartViewModel)
        {
            InitializeComponent();

            authService.Initialize();

            MainPage = new AppShell(authService);
            _cartViewModel = cartViewModel;
        }

        protected override async void OnStart()
        {
            await _cartViewModel.InitializeCartAsync();
        }
    }
}
