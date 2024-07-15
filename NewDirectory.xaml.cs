using CommunityToolkit.Maui.Storage;
using MiviaMaui.Models;
using MiviaMaui.Services;
using Microsoft.Maui.Controls;

namespace MiviaMaui
{
    public partial class NewDirectory : ContentPage
    {
        private readonly DirectoryService _directoryService;

        public NewDirectory(DirectoryService directoryService)
        {
            InitializeComponent();
            _directoryService = directoryService;
        }

        private async void OnPickFolderClicked(object sender, EventArgs e)
        {
            try
            {
                var folder = await FolderPicker.PickAsync(default);

                folderPath.Text = folder?.Folder.Path;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to pick folder: {ex.Message}", "OK");
            }
        }

        private async void OnSaveDirectoryClicked(object sender, EventArgs e)
        {
            var directoryName = directoryNameEntry.Text;
            var directoryPath = folderPath.Text;

            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrEmpty(directoryPath))
            {
                await DisplayAlert("Error", "Please provide both directory name and path.", "OK");
                return;
            }

            var newDirectory = new MonitoredDirectory
            {
                Name = directoryName,
                Path = directoryPath
            };

            _directoryService.AddMonitoredDirectory(newDirectory);

            await DisplayAlert("Success", "Directory saved successfully!", "OK");

            // Navigate back to DirectoriesPage
            await Navigation.PopAsync();
        }
    }
}
