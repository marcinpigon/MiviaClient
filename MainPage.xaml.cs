using Microsoft.Extensions.DependencyInjection;
using MiviaMaui.Services;
using MiviaMaui.ViewModels;
using MiviaMaui.Views;

namespace MiviaMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly DirectoryService _directoryService;
        private readonly HistoryService _historyService;

        public MainPage(DirectoryService directoryService, HistoryService historyService)
        {
            InitializeComponent();
            _directoryService = directoryService;
            _historyService = historyService;
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void OnNavigateToConfigurationPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigurationPage(_historyService));
        }

        private async void OnNavigateToDirectoriesPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DirectoriesPage(_directoryService));
        }

        private async void OnNavigateToHistoryPageButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage(_directoryService, _historyService));
        }

        private async void OnNavigateToModelsPage(object sender, EventArgs e)
        {
            var modelsViewModel = App.ServiceProvider.GetRequiredService<ModelsViewModel>();
            var modelsPage = new ModelsPage(modelsViewModel);

            await Navigation.PushAsync(modelsPage);
        }

        private async void OnNavigateToImagesPageButtonClicked(object sender, EventArgs e)
        {
            var imagesViewModel = App.ServiceProvider.GetRequiredService<ImagesViewModel>();
            var imagesPage = new ImagesPage(imagesViewModel);

            await Navigation.PushAsync(imagesPage);
        }
    }
}
