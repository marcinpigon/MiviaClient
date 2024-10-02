using MiviaMaui.Models;
using MiviaMaui.Resources.Languages;
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

            LoadHistoryRecords();
        }

        private async void LoadHistoryRecords()
        {
            var historyRecords = await _historyService.GetHistoryAsync();
            var sortedHistoryRecords = historyRecords.OrderByDescending(record => record.Timestamp).ToList();
            HistoryListView.ItemsSource = sortedHistoryRecords;
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var record = button?.CommandParameter as HistoryRecord;

            if (record != null)
            {
                await _historyService.DeleteHistoryRecordAsync(record);
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
    }
}
