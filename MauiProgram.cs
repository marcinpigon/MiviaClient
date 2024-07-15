using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using System.Reflection;
using MiviaMaui.Services;

namespace MiviaMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<IMiviaClient, MiviaClient>(client =>
            {
                client.BaseAddress = new Uri("https://app.mivia.ai");
            });

            builder.Services.AddSingleton<DirectoryService>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            var mauiApp = builder.Build();

            // Set the ServiceProvider to be used in App.xaml.cs
            App.ServiceProvider = mauiApp.Services;

            return mauiApp;
        }
    }
}
