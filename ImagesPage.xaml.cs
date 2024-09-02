using MiviaMaui.ViewModels;
using System.Diagnostics;
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

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                // Get the imageId from the CommandParameter
                var imageId = button.CommandParameter as string;

                if (imageId == null)
                {
                    Debug.WriteLine("No imageId found.");
                    return;
                }

                bool confirmDelete = await DisplayAlert("Delete", "Are you sure you want to delete this image?", "Yes", "No");

                if (confirmDelete)
                {
                    // Call the ViewModel method to delete the image
                    await _viewModel.DeleteImageAsync(imageId);

                    // Optionally, show a success message to the user
                    await DisplayAlert("Success", "The image has been deleted.", "OK");
                }
            }
        }
    
    }
}
