using Microsoft.Extensions.Configuration;
using MiviaMaui.Services;

namespace MiviaMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly DirectoryService _directoryService;

        public MainPage(DirectoryService directoryService)
        {
            InitializeComponent();
            _directoryService = directoryService;
        }

        private async void OnNavigateToConfigurationPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigurationPage());
        }

        private async void OnNavigateToDirectoriesPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DirectoriesPage(_directoryService));
        }

        private async void OnNavigateToHistoryPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }
    }
}
