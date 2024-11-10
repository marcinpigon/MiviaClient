using MiviaMaui.Models;
using MiviaMaui.Resources.Languages;
using MiviaMaui.Services;
using MiviaMaui.ViewModels;
using System.Collections.Generic;

namespace MiviaMaui
{
    public partial class HistoryPage : ContentPage
    {
        private readonly DirectoryService _directoryService;
        private readonly HistoryService _historyService;

        public HistoryPage(DirectoryService directoryService, HistoryService historyService)
        {
            InitializeComponent();
            _directoryService = directoryService;
            _historyService = historyService;
            LoadHistoryRecords();
        }

        private async void LoadHistoryRecords()
        {
            var historyRecords = await _historyService.GetHistoryAsync();
            var sortedHistoryRecords = historyRecords
            .OrderByDescending(record => record.Timestamp)
            .Select(record => new HistoryRecordViewModel(record))
            .ToList();
            HistoryListView.ItemsSource = sortedHistoryRecords;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var viewModel = button?.CommandParameter as HistoryRecordViewModel;
            if (viewModel?.Record != null)
            {
                await _historyService.DeleteHistoryRecordAsync(viewModel.Record);
                LoadHistoryRecords();
            }
        }

        private async void OnClearHistoryClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert(AppResources.historyClearHistory, 
                AppResources.historyClearHistoryConfirmMessage, 
                AppResources.yes, 
                AppResources.no);
            if (confirm)
            {
                await _historyService.RecreateHistoryTableAsync();
                LoadHistoryRecords();
            }
        }

        private async void OnFolderClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var record = (button?.CommandParameter as HistoryRecordViewModel)?.Record;

            if (record?.FolderPath != null)
            {
                try
                {
                    if (Directory.Exists(record.FolderPath))
                    {
                        await Launcher.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(record.FolderPath)
                        });
                    }
                    else
                    {
                        await DisplayAlert("Error", "Folder not found", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Could not open folder", "OK");
                }
            }
            else if (record?.FilePath != null)
            {
                try
                {
                    if (File.Exists(record.FilePath))
                    {
                        await Launcher.OpenAsync(new OpenFileRequest
                        {
                            File = new ReadOnlyFile(record.FilePath)
                        });
                    }
                    else
                    {
                        await DisplayAlert("Error", "File not found", "OK");
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Could not open file", "OK");
                }
            }
        }
    }
}
