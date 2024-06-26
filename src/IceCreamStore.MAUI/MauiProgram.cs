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
                .UseMauiCommunityToolkit();

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<AuthViewModel>()
                            .AddTransient<SignupPage>()
                            .AddTransient<SigninPage>();

            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddTransient<OnboardingPage>();
            builder.Services.AddSingleton<HomeViewModel>()
                            .AddSingleton<HomePage>();

            builder.Services.AddTransient<DetailsViewModel>()
                            .AddTransient<DetailsPage>();

            ConfigureRefit(builder.Services);

            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
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
                }
            };

            services.AddRefitClient<IAuthApi>(refitSetting)
                .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IIcecreamsApi>(refitSetting)
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
        }
    }
}
