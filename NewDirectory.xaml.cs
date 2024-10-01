using CommunityToolkit.Maui.Storage;
using MiviaMaui.Models;
using MiviaMaui.Services;
using Microsoft.Maui.Controls;
using MiviaMaui.Dtos;
using MiviaMaui.Resources.Languages;

namespace MiviaMaui
{
    public partial class NewDirectory : ContentPage
    {
        private readonly DirectoryService _directoryService;
        private readonly ModelService _modelService;

        private List<string> _selectedModelIds = new List<string>();
        private List<string> _selectedModelNames = new List<string>();

        public NewDirectory(DirectoryService directoryService, ModelService modelService)
        {
            InitializeComponent();
            _directoryService = directoryService;
            _modelService = modelService;

            LoadModelsAsync();
        }

        private async void LoadModelsAsync()
        {
            try
            {
                var models = await _modelService.GetModelsAsync();
                UpdateModelUI(models);
            }
            catch (Exception ex)
            {
                await DisplayAlert(AppResources.error, $"{AppResources.newdirectoryUnableToLoadModels} {ex.Message}", "OK");
            }
        }

        private void UpdateModelUI(List<ModelDto> models)
        {     
            foreach (var model in models)
            {
                var modelLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(5)
                };

                var switchControl = new Switch
                {
                    IsToggled = false 
                };
                switchControl.Toggled += (sender, args) => OnModelToggled(model.Id, model.DisplayName, args.Value);

                var label = new Label
                {
                    Text = model.DisplayName,
                    VerticalOptions = LayoutOptions.Center
                };

                modelLayout.Children.Add(switchControl);
                modelLayout.Children.Add(label);

                modelOptionsStackLayout.Children.Add(modelLayout);
            }
        }

        private void OnModelToggled(string modelId, string modelName, bool isToggled)
        {
            if (isToggled)
            {
                if (!_selectedModelIds.Contains(modelId))
                {
                    _selectedModelIds.Add(modelId);
                    _selectedModelNames.Add(modelName);
                }
            }
            else
            {
                _selectedModelIds.Remove(modelId);
                _selectedModelNames.Remove(modelName);
            }
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
                await DisplayAlert(AppResources.error, $"{AppResources.newdirectoryUnableToPickFolder} {ex.Message}", "OK");
            }
        }

        private async void OnSaveDirectoryClicked(object sender, EventArgs e)
        {
            var directoryName = directoryNameEntry.Text;
            var directoryPath = folderPath.Text;

            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrEmpty(directoryPath))
            {
                await DisplayAlert(AppResources.error, AppResources.newdirectoryMissingInfo, "OK");
                return;
            }

            var newDirectory = new MonitoredDirectory
            {
                Name = directoryName,
                Path = directoryPath,
                ModelIds = _selectedModelIds,
                ModelNames = _selectedModelNames
            };

            _directoryService.AddMonitoredDirectory(newDirectory);

            await DisplayAlert(AppResources.success, AppResources.newdirectorySavedSuccess, "OK");

            // Navigate back to DirectoriesPage
            await Navigation.PopAsync();
        }
    }
}
