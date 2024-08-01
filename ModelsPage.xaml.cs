using Microsoft.Maui.Controls;
using MiviaMaui.ViewModels;

namespace MiviaMaui.Views
{
    public partial class ModelsPage : ContentPage
    {
        private readonly ModelsViewModel _viewModel;

        public ModelsPage(ModelsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Load models when the page appears
            await _viewModel.LoadModelsAsync();
        }
    }
}
