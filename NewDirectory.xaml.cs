using MiviaMaui.Models;
using MiviaMaui.Services;
using Microsoft.Maui.Controls;
using MiviaMaui.Dtos;
using MiviaMaui.Resources.Languages;
using MiviaMaui.Interfaces;

namespace MiviaMaui
{
    public partial class NewDirectory : ContentPage
    {
        private readonly DirectoryService _directoryService;
        private readonly ModelService _modelService;
        private readonly IFolderPicker _folderPicker;

        private List<string> _selectedModelIds = new List<string>();
        private List<string> _selectedModelNames = new List<string>();

        public NewDirectory(DirectoryService directoryService, ModelService modelService, IFolderPicker folderPicker)
        {
            InitializeComponent();
            _directoryService = directoryService;
            _modelService = modelService;
            _folderPicker = folderPicker;

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

        // In your code-behind file where you populate the models
        private void PopulateModelOptions(List<ModelDto> models)
        {
            // Clear existing options
            modelOptionsStackLayoutLeft.Children.Clear();
            modelOptionsStackLayoutRight.Children.Clear();

            for (int i = 0; i < models.Count; i++)
            {
                var model = models[i];
                var checkbox = new CheckBox
                {
                    // Your existing checkbox configuration
                    IsChecked = false,
                    Color = Colors.DarkSlateGray
                };

                var label = new Label
                {
                    Text = model.Name,
                    TextColor = Colors.DarkSlateGray,
                    VerticalOptions = LayoutOptions.Center
                };

                var container = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children = { checkbox, label },
                    Spacing = 10
                };

                // Add to left column if index is even, right column if odd
                if (i % 2 == 0)
                    modelOptionsStackLayoutLeft.Children.Add(container);
                else
                    modelOptionsStackLayoutRight.Children.Add(container);
            }
        }

        private void UpdateModelUI(List<ModelDto> models)
        {
            // Clear existing options
            modelOptionsStackLayoutLeft.Children.Clear();
            modelOptionsStackLayoutRight.Children.Clear();

            // Calculate midpoint to ensure even distribution
            int midPoint = (models.Count + 1) / 2;  // Adding 1 ensures proper distribution for odd numbers

            for (int i = 0; i < models.Count; i++)
            {
                var model = models[i];
                var modelLayout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Padding = new Thickness(5),
                    Spacing = 10
                };

                var checkbox = new CheckBox
                {
                    IsChecked = false,
                    Color = Colors.DarkSlateGray
                };
                checkbox.CheckedChanged += (sender, args) => OnModelToggled(model.Id, model.DisplayName, args.Value);

                var label = new Label
                {
                    Text = model.DisplayName,
                    TextColor = Colors.DarkSlateGray,
                    VerticalOptions = LayoutOptions.Center
                };

                modelLayout.Children.Add(checkbox);
                modelLayout.Children.Add(label);

                // Add to left column for first half, right column for second half
                if (i < midPoint)
                    modelOptionsStackLayoutLeft.Children.Add(modelLayout);
                else
                    modelOptionsStackLayoutRight.Children.Add(modelLayout);
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
                var path = await _folderPicker.PickFolderAsync();

                folderPath.Text = path;
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
