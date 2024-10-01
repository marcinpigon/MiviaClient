using MiviaMaui.Services;
using Microsoft.Maui.Controls;
using MiviaMaui.Models;
using MiviaMaui.Resources.Languages;

namespace MiviaMaui
{
    public partial class EditDirectoryPage : ContentPage
    {
        private readonly DirectoryService _directoryService;
        private readonly ModelService _modelService;

        private readonly MonitoredDirectory _monitoredDirectory;

        public EditDirectoryPage(DirectoryService directoryService, MonitoredDirectory monitoredDirectory, ModelService modelService)
        {
            InitializeComponent();
            _directoryService = directoryService;
            _monitoredDirectory = monitoredDirectory;

            BindingContext = _monitoredDirectory;
            _modelService = modelService;

            LoadModelsAsync();
        }

        private async Task LoadModelsAsync()
        {
            try
            {
                var models = await _modelService.GetModelsAsync();
                modelOptionsStackLayout.Children.Clear();

                foreach (var model in models)
                {
                    var modelOption = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.Center
                    };

                    var modelSwitch = new Switch
                    {
                        IsToggled = _monitoredDirectory.ModelIds.Contains(model.Id)
                    };
                    modelSwitch.Toggled += (s, e) => OnModelToggled(model.Id, modelSwitch.IsToggled);

                    var modelLabel = new Label
                    {
                        Text = model.DisplayName,
                        VerticalOptions = LayoutOptions.Center
                    };

                    modelOption.Children.Add(modelSwitch);
                    modelOption.Children.Add(modelLabel);

                    modelOptionsStackLayout.Children.Add(modelOption);
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., show an error message)
                Console.WriteLine(ex.Message);
            }
        }

        private void OnModelToggled(string modelId, bool isToggled)
        {
            if (isToggled)
            {
                if (!_monitoredDirectory.ModelIds.Contains(modelId))
                {
                    _monitoredDirectory.ModelIds.Add(modelId);
                }
            }
            else
            {
                _monitoredDirectory.ModelIds.Remove(modelId);
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _directoryService.UpdateDirectory(_monitoredDirectory);
            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert(AppResources.editdirectoryDeleteDirectory, 
                AppResources.editdirectoryConfirmDeleteMessage, 
                AppResources.yes, 
                AppResources.no);
            if (confirm)
            {
                _directoryService.DeleteDirectory(_monitoredDirectory.Id);
                await Navigation.PopAsync();
            }
        }
    }
}
