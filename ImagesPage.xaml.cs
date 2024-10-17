using MiviaMaui.Converters;
using MiviaMaui.Dtos;
using MiviaMaui.Resources.Languages;
using MiviaMaui.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;

namespace MiviaMaui.Views
{
    public partial class ImagesPage : ContentPage
    {
        private readonly ImagesViewModel _viewModel;
        public ImagesPage(ImagesViewModel imagesViewModel)
        {
            InitializeComponent();
            _viewModel = imagesViewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadImagesAsync();
        }

        private void OnImageTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame && frame.BindingContext is ImageDto image)
            {
                _viewModel.ToggleImageSelection(image);
            }
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                var imageId = button.CommandParameter as string;

                if (imageId == null)
                {
                    return;
                }

                bool confirmDelete = await DisplayAlert(AppResources.delete, 
                    AppResources.imagesDeleteConfirmMessage, 
                    AppResources.yes, 
                    AppResources.no);

                if (confirmDelete)
                {
                    await _viewModel.DeleteImageAsync(imageId);
                    await DisplayAlert(AppResources.success, AppResources.imagesImageDeleted, "OK");
                }
            }
        }
    
    }
}
