using Microsoft.Extensions.DependencyInjection;
using MiviaMaui.Services;

namespace MiviaMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var directoryService = ServiceProvider.GetService<DirectoryService>();

            MainPage = new NavigationPage(new MainPage(directoryService));
        }

        public static IServiceProvider ServiceProvider { get; set; }
    }
}
