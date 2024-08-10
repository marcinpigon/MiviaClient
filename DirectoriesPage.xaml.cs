using MiviaMaui.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using MiviaMaui.Models;

namespace MiviaMaui
{
    public partial class DirectoriesPage : ContentPage
    {
        private readonly DirectoryService _directoryService;

        public DirectoriesPage(DirectoryService directoryService)
        {
            InitializeComponent();
            _directoryService = directoryService;
            BindingContext = this;

            LoadDirectories();
        }

        public ObservableCollection<MonitoredDirectory> MonitoredDirectories => _directoryService.MonitoredDirectories;

        private void LoadDirectories()
        {
            var directories = _directoryService.MonitoredDirectories;

            if (directories.Count == 0)
            {
                noDirectoriesLabel.IsVisible = true;
                directoriesCollectionView.IsVisible = false;
            }
            else
            {
                noDirectoriesLabel.IsVisible = false;
                directoriesCollectionView.IsVisible = true;
                directoriesCollectionView.ItemsSource = directories;
            }
        }

        private async void OnMonitorNewDirectoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewDirectory(_directoryService, App.ServiceProvider.GetRequiredService<ModelService>()));
        }

        private async void OnDirectoryDoubleTapped(object sender, EventArgs e)
        {
            var grid = sender as Grid;
            var monitoredDirectory = grid?.BindingContext as MonitoredDirectory;
            if (monitoredDirectory != null)
            {
                await Navigation.PushAsync(new EditDirectoryPage(_directoryService, monitoredDirectory, 
                    App.ServiceProvider.GetRequiredService<ModelService>()));
            }
        }
    }
}
