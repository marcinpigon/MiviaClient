using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MiviaMaui.Services;
using MiviaMaui.ViewModels;
using MiviaMaui.Views;

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

            builder.Services.AddSingleton<DirectoryWatcherService>();
            builder.Services.AddSingleton<DirectoryService>();
            builder.Services.AddSingleton<HistoryService>();
            builder.Services.AddSingleton<ModelService>();
            builder.Services.AddSingleton<ModelsViewModel>();
            builder.Services.AddSingleton<ImagesViewModel>();
            builder.Services.AddTransient<ModelsPage>();
            builder.Services.AddTransient<ImagesPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var mauiApp = builder.Build();

            var app = new App(mauiApp.Services);
            return mauiApp;
        }
    }
}
