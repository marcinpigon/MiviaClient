using Microsoft.Extensions.DependencyInjection;
using MiviaMaui.Services;

namespace MiviaMaui
{
    public partial class App : Application
    {
        private readonly DirectoryWatcherService _directoryWatcherService;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _directoryWatcherService = serviceProvider.GetRequiredService<DirectoryWatcherService>();
            StartBackgroundTasks();

            // Use the serviceProvider to set up the MainPage
            MainPage = new NavigationPage(new MainPage(serviceProvider.GetRequiredService<DirectoryService>()));

            // Store the serviceProvider in a static property
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
            // Run the directory watcher in a background task
            Task.Run(() => _directoryWatcherService.StartWatching());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            _directoryWatcherService.StopWatching();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            StartBackgroundTasks();
        }

        // Static property to hold the ServiceProvider
        public static IServiceProvider ServiceProvider { get; private set; }
    }
}
