using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using MiviaMaui.Services;
using MiviaMaui.ViewModels;
using MiviaMaui.Views;
using MiviaMaui.Interfaces;
using Plugin.LocalNotification;
using MiviaMaui.Command_Handlers;
using MiviaMaui.Query_Handlers;
using MiviaMaui.Commands;
using MiviaMaui.Handlers.Command_Handlers;
using MiviaMaui.Queries;
using MiviaMaui.Bus;

namespace MiviaMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit(options =>
                {
                    options.SetShouldEnableSnackbarOnWindows(true);
                })
                .UseLocalNotification()
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
            builder.Services.AddSingleton<HistoryService>();
            builder.Services.AddSingleton<ModelService>();

            builder.Services.AddScoped<IImageUploadContextService,  ImageUploadContextService>();

            builder.Services.AddSingleton<ModelsViewModel>();

            builder.Services.AddTransient<ModelsPage>();
            builder.Services.AddTransient<ImagesPage>();

            builder.Services.AddSingleton<ISnackbarService, SnackbarService>();

            builder.Services.AddImagePathService();

            // CQRS
            builder.Services.AddTransient<ICommandHandler<UploadImageCommand, string>, UploadImageCommandHandler>();
            builder.Services.AddTransient<ICommandHandler<ScheduleJobCommand, string>, ScheduleJobCommandHandler>();
            builder.Services.AddTransient<ICommandHandler<GenerateReportCommand, bool>, GenerateReportCommandHandler>();
            builder.Services.AddTransient<ICommandHandler<GenerateReportMultipleJobsCommand, bool>, GenerateReportMultipleJobsHandler>();

            builder.Services.AddTransient<IQueryHandler<IsJobFinishedQuery, bool>, IsJobFinishedQueryHandler>();
            builder.Services.AddSingleton<ICommandBus, CommandBus>();
            builder.Services.AddSingleton<IQueryBus, QueryBus>();

#if ANDROID
            builder.Services.AddSingleton<MiviaMaui.Interfaces.INotificationService, LocalNotificationService>();
            builder.Services.AddSingleton<Interfaces.IFolderPicker, AndroidFolderPickerService>();
            builder.Services.AddSingleton<Interfaces.IDirectoryWatcherService, AndroidDirectoryWatcherService>();
#elif WINDOWS
            builder.Services.AddSingleton<MiviaMaui.Interfaces.INotificationService, WindowsNotificationService>();
            builder.Services.AddSingleton<Interfaces.IFolderPicker, WindowsFolderPickerService>();
            builder.Services.AddSingleton<Interfaces.IDirectoryWatcherService, DirectoryWatcherService>();
#endif

#if DEBUG
            builder.Logging.AddDebug();
#endif

            var mauiApp = builder.Build();

            var app = new App(mauiApp.Services);
            return mauiApp;
        }
    }
}
