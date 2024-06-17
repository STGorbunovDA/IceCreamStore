﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

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
            ConfigureRefit(builder.Services);
            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
