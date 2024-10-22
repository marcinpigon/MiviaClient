using MiviaMaui.Dtos;
using MiviaMaui.Services;
using MiviaMaui.ViewModels;
using System.Collections.ObjectModel;

namespace MiviaMaui.Views
{
    public partial class SelectedImagesPage : ContentPage
    {
        private readonly SelectedImagesViewModel _viewModel;

        public SelectedImagesPage(ModelService modelService, List<ImageDto> selectedImages)
        {
            InitializeComponent();
            _viewModel = new SelectedImagesViewModel(modelService, selectedImages);
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadModelsAsync();
        }

        private void OnImagesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedImage = e.CurrentSelection.FirstOrDefault() as ImageDto;
            _viewModel.CurrentImage = selectedImage;
        }

        private void OnModelSelectionChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender is CheckBox checkBox &&
                checkBox.BindingContext is ModelDto model)
            {
                // Update the model's IsSelected property first
                model.IsSelected = e.Value;
                _viewModel.HandleModelSelection(model);
            }
        }

        private async void OnProcessImagesClicked(object sender, EventArgs e)
        {
            await _viewModel.ProcessImagesAsync();
            await DisplayAlert("Success", "Images sent for processing", "OK");
            await Navigation.PopAsync();
        }
    }
}