using CommunityToolkit.Maui;
using IceCreamStore.MAUI.Pages;
using IceCreamStore.MAUI.Services;
using IceCreamStore.MAUI.ViewModels;
using Microsoft.Extensions.Logging;
using Refit;

#if ANDROID
using System.Net.Security;
using Xamarin.Android.Net;
#elif IOS
using Security;
#endif

namespace IceCreamStore.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(h =>
                {
#if ANDROID || IOS
                    // для этого меняем namespace
                    h.AddHandler<Shell, TabbarBadgeRenderer>(); 
#endif
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DatabaseService>();

            builder.Services.AddTransient<AuthViewModel>()
                            .AddTransient<SignupPage>()
                            .AddTransient<SigninPage>();

            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddTransient<OnboardingPage>();
            builder.Services.AddSingleton<HomeViewModel>()
                            .AddSingleton<HomePage>();

            builder.Services.AddTransient<DetailsViewModel>()
                            .AddTransient<DetailsPage>();

            builder.Services.AddSingleton<CartViewModel>()
                            .AddTransient<CartPage>();

            builder.Services.AddTransient<ProfileViewModel>()
                            .AddTransient<ProfilePage>();

            ConfigureRefit(builder.Services);

            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
            services.AddRefitClient<IAuthApi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IIcecreamsApi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IOrderApi>(GetRefitSettings)
                .ConfigureHttpClient(SetHttpClient);


            static void SetHttpClient(HttpClient httpClient)
            {
                var baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7240"
                                                                            : "https://localhost:7240";
                if(DeviceInfo.DeviceType == DeviceType.Physical)
                {
                    baseUrl = "https://hx1dv2gm-7240.euw.devtunnels.ms";
                }

                httpClient.BaseAddress = new Uri(baseUrl);
            }

            static RefitSettings GetRefitSettings(IServiceProvider serviceProvider)
            {
                var authService = serviceProvider.GetRequiredService<AuthService>();

                var refitSetting = new RefitSettings
                {
                    HttpMessageHandlerFactory = () =>
                    {
                        // return HttpMessageHandler

#if ANDROID
                        return new AndroidMessageHandler
                        {
                            ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
                            {
                                return certificate?.Issuer == "CN=localhost" || sslPolicyErrors == SslPolicyErrors.None;
                            }
                        };

#elif IOS
                    return new NSUrlSessionHandler
                    {
                        TrustOverrideForUrl = (NSUrlSessionHandler sender, string url, SecTrust trust) =>
                        url.StartsWith("http://localhost")
                    };

#endif
                        return null;
                    },
                    AuthorizationHeaderValueGetter = (_, __) => 
                            Task.FromResult(authService.Token ?? string.Empty)
                };


                return refitSetting;
            }
        }
    }
}
