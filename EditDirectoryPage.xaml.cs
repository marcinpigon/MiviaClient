using MiviaMaui.Services;
using Microsoft.Maui.Controls;
using MiviaMaui.Models;

namespace MiviaMaui
{
    public partial class EditDirectoryPage : ContentPage
    {
        private readonly DirectoryService _directoryService;
        private readonly MonitoredDirectory _monitoredDirectory;

        public EditDirectoryPage(DirectoryService directoryService, MonitoredDirectory monitoredDirectory)
        {
            InitializeComponent();
            _directoryService = directoryService;
            _monitoredDirectory = monitoredDirectory;

            BindingContext = _monitoredDirectory;
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
            bool confirm = await DisplayAlert("Delete Directory", "Are you sure you want to delete this directory?", "Yes", "No");
            if (confirm)
            {
                _directoryService.DeleteDirectory(_monitoredDirectory.Id);
                await Navigation.PopAsync();
            }
        }
    }
}
