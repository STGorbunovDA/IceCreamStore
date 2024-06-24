using IceCreamStore.MAUI.Services;

namespace IceCreamStore.MAUI
{
    public partial class App : Application
    {
        public App(AuthService authService)
        {
            InitializeComponent();

            authService.Initialize();

            MainPage = new AppShell(authService);
        }
    }
}
