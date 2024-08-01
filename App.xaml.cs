using Microsoft.Extensions.DependencyInjection;
using MiviaMaui.Services;

namespace MiviaMaui
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Use the serviceProvider to set up the MainPage
            MainPage = new NavigationPage(new MainPage(serviceProvider.GetRequiredService<DirectoryService>()));

            // Store the serviceProvider in a static property
            ServiceProvider = serviceProvider;
        }

        // Static property to hold the ServiceProvider
        public static IServiceProvider ServiceProvider { get; private set; }
    }
}
