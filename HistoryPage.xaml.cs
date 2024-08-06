using MiviaMaui.Models;
using MiviaMaui.Services;
using System.Collections.Generic;

namespace MiviaMaui
{
    public partial class HistoryPage : ContentPage
    {
        private readonly DirectoryService _directoryService;

        public HistoryPage(DirectoryService directoryService)
        {
            InitializeComponent();
            _directoryService = directoryService;

            // Retrieve the history records and set them as the ItemsSource for the ListView
            LoadHistoryRecords();
        }

        private void LoadHistoryRecords()
        {
            IEnumerable<HistoryRecord> historyRecords = _directoryService.GetHistory();
            HistoryListView.ItemsSource = historyRecords;
        }
    }
}
