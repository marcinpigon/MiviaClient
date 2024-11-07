using MiviaMaui.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using MiviaMaui.Models;
using MiviaMaui.Interfaces;
using System.Diagnostics;

namespace MiviaMaui
{
    public partial class DirectoriesPage : ContentPage
    {
        private readonly DirectoryService _directoryService;

        public DirectoriesPage(DirectoryService directoryService)
        {
            InitializeComponent();
            _directoryService = directoryService;
            BindingContext = this;

            LoadDirectories();
        }

        public ObservableCollection<MonitoredDirectory> MonitoredDirectories => _directoryService.MonitoredDirectories;

        private void LoadDirectories()
        {
            var directories = _directoryService.MonitoredDirectories;

            if (directories.Count == 0)
            {
                noDirectoriesLabel.IsVisible = true;
                directoriesCollectionView.IsVisible = false;
            }
            else
            {
                noDirectoriesLabel.IsVisible = false;
                directoriesCollectionView.IsVisible = true;
                directoriesCollectionView.ItemsSource = directories;
            }
        }

        private async void OnMonitorNewDirectoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewDirectory(_directoryService, 
                App.ServiceProvider.GetRequiredService<ModelService>(), 
                App.ServiceProvider.GetRequiredService<IFolderPicker>()));
        }



        private async void OnEditDirectoryClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.BindingContext is MonitoredDirectory directory)
            {
                await Navigation.PushAsync(new EditDirectoryPage(
                    _directoryService,
                    directory,
                    App.ServiceProvider.GetRequiredService<ModelService>()));
            }
        }

        private async void OnOpenDirectoryClicked(object sender, EventArgs e)
        {
            if (sender is Image image && image.BindingContext is MonitoredDirectory directory)
            {
                await OpenDirectoryAsync(directory);
            }
        }

        private async void OnDeleteDirectoryClicked(object sender, EventArgs e)
        {
            if (sender is ImageButton button && button.BindingContext is MonitoredDirectory directory)
            {
                bool answer = await DisplayAlert(
                    "Confirm Delete",
                    $"Are you sure you want to stop monitoring the directory '{directory.Name}'?",
                    "Yes",
                    "No");

                if (answer)
                {
                    _directoryService.DeleteDirectory(directory.Id);
                    LoadDirectories();
                }
            }
        }
        private async Task OpenDirectoryAsync(MonitoredDirectory directory)
        {
            try
            {
                if (!Directory.Exists(directory.Path))
                {
                    await DisplayAlert("Error", "Directory not found. Please verify the path exists.", "OK");
                    return;
                }

                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = $"\"{directory.Path}\"",
                        UseShellExecute = true
                    });
                }
                else if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    try
                    {
#if ANDROID
                        var androidPath = directory.Path;
                        var intent = new Android.Content.Intent();
                        intent.SetAction(Android.Content.Intent.ActionView);
                        var androidUri = AndroidX.DocumentFile.Provider.DocumentFile
                            .FromFile(new Java.IO.File(androidPath))
                            ?.Uri;

                        if (androidUri != null)
                        {
                            intent.SetDataAndType(androidUri, "resource/folder");
                            intent.AddFlags(Android.Content.ActivityFlags.NewTask);
                            Android.App.Application.Context.StartActivity(intent);
                        }
                        else
                        {
                            await DisplayAlert("Error", "Could not access the directory.", "OK");
                        }
#endif
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error",
                            $"Failed to open directory on Android: {ex.Message}", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Information",
                        "Opening directories is currently supported only on Windows & Android.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",
                    $"Failed to open directory: {ex.Message}", "OK");
            }
        }
    }
}
