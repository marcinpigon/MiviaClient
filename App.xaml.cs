using Microsoft.Extensions.DependencyInjection;
using MiviaMaui.Interfaces;
using MiviaMaui.Services;

namespace MiviaMaui
{
    public partial class App : Application
    {
        private readonly IDirectoryWatcherService _directoryWatcherService;
        private readonly HistoryService _historyService;
        private readonly INotificationService _notificationService;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _directoryWatcherService = serviceProvider.GetRequiredService<IDirectoryWatcherService>();
            _historyService = serviceProvider.GetRequiredService<HistoryService>();
            _notificationService = serviceProvider.GetRequiredService<INotificationService>();

            StartBackgroundTasks();

            MainPage = new NavigationPage(
                new MainPage(serviceProvider.GetRequiredService<DirectoryService>(),
                serviceProvider.GetRequiredService<HistoryService>()
                ));

            ServiceProvider = serviceProvider;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Title = "Mivia Client";

            return window;
        }


        private void StartBackgroundTasks()
        {
            Task.Run(() => _directoryWatcherService.StartWatching());
        }

        protected override void OnStart()
        {
            _notificationService.ShowNotification("Directory Watcher", "Monitoring directories");
        }

        protected override void OnSleep()
        {
            _directoryWatcherService.StopWatching();
        }

        protected override void OnResume()
        {
            StartBackgroundTasks();
        }

        public static IServiceProvider ServiceProvider { get; private set; }
    }
}
