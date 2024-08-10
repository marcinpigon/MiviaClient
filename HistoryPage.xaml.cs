using MiviaMaui.Models;
using MiviaMaui.Services;
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

            // Retrieve the history records and set them as the ItemsSource for the ListView
            LoadHistoryRecords();
        }

        private async void LoadHistoryRecords()
        {
            var historyRecords = await _historyService.GetHistoryAsync();
            HistoryListView.ItemsSource = historyRecords;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var record = button?.CommandParameter as HistoryRecord;

            if (record != null)
            {
                bool confirmDelete = await DisplayAlert("Delete", "Are you sure you want to delete this history entry?", "Yes", "No");

                if (confirmDelete)
                {
                    // Delete the record from the database
                    await _historyService.DeleteHistoryRecordAsync(record);

                    // Reload the history records to update the ListView
                    LoadHistoryRecords();
                }
            }
        }

        private async void OnClearHistoryClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Clear History", "Are you sure you want to clear all history?", "Yes", "No");
            if (confirm)
            {
                await _historyService.RecreateHistoryTableAsync();
                LoadHistoryRecords();
            }
        }
    }
}
