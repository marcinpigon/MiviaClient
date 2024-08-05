using MiviaMaui.ViewModels;

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
    }
}
