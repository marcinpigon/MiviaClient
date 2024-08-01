using Microsoft.Extensions.DependencyInjection;
using MiviaMaui.Services;
using MiviaMaui.ViewModels;
using MiviaMaui.Views;

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

        private async void OnNavigateToModelsPage(object sender, EventArgs e)
        {
            // Use ServiceProvider to get the ModelsViewModel
            var modelsViewModel = App.ServiceProvider.GetRequiredService<ModelsViewModel>();
            var modelsPage = new ModelsPage(modelsViewModel);

            await Navigation.PushAsync(modelsPage);
        }
    }
}
