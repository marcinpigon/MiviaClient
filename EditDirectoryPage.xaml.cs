using MiviaMaui.Services;
using Microsoft.Maui.Controls;
using MiviaMaui.Models;
using MiviaMaui.Resources.Languages;
using System.Collections.ObjectModel;
using MiviaMaui.ViewModels;

namespace MiviaMaui
{
    public partial class EditDirectoryPage : ContentPage
    {
        private readonly DirectoryService _directoryService;
        private readonly ModelService _modelService;
        private readonly MonitoredDirectory _originalDirectory;
        private readonly EditDirectoryViewModel _viewModel;

        public EditDirectoryPage(DirectoryService directoryService, MonitoredDirectory monitoredDirectory, ModelService modelService)
        {
            InitializeComponent();
            _directoryService = directoryService;
            _modelService = modelService;
            _originalDirectory = monitoredDirectory;

            _viewModel = new EditDirectoryViewModel(monitoredDirectory);
            BindingContext = _viewModel; ;

            LoadModelsAsync();
        }

        private async Task LoadModelsAsync()
        {
            try
            {
                var models = await _modelService.GetModelsAsync();

                modelOptionsStackLayoutLeft.Children.Clear();
                modelOptionsStackLayoutRight.Children.Clear();

                int midPoint = (models.Count + 1) / 2;

                for (int i = 0; i < models.Count; i++)
                {
                    var model = models[i];
                    var modelOption = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 10,
                        Padding = new Thickness(5)
                    };

                    var checkbox = new CheckBox
                    {
                        IsChecked = _viewModel.ModelIds.Contains(model.Id),
                        Color = Colors.DarkSlateGray
                    };
                    checkbox.CheckedChanged += (s, e) => OnModelToggled(model.Id, e.Value);

                    var modelLabel = new Label
                    {
                        Text = model.DisplayName,
                        TextColor = Colors.DarkSlateGray,
                        VerticalOptions = LayoutOptions.Center
                    };

                    modelOption.Children.Add(checkbox);
                    modelOption.Children.Add(modelLabel);

                    if (i < midPoint)
                        modelOptionsStackLayoutLeft.Children.Add(modelOption);
                    else
                        modelOptionsStackLayoutRight.Children.Add(modelOption);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(AppResources.error, ex.Message, "OK");
            }
        }

        private void OnModelToggled(string modelId, bool isChecked)
        {
            if (isChecked)
            {
                if (!_viewModel.ModelIds.Contains(modelId))
                {
                    _viewModel.ModelIds.Add(modelId);
                }
            }
            else
            {
                _viewModel.ModelIds.Remove(modelId);
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _viewModel.ApplyTo(_originalDirectory);
            _directoryService.UpdateDirectory(_originalDirectory);
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
                _directoryService.DeleteDirectory(_originalDirectory.Id);
                await Navigation.PopAsync();
            }
        }
    }
}
