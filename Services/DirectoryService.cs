using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using MiviaMaui.Models;

namespace MiviaMaui.Services
{
    public class DirectoryService
    {
        private const string FileName = "monitored_directories.json";
        private readonly string _filePath;

        private readonly HistoryService _historyService;

        public ObservableCollection<MonitoredDirectory> MonitoredDirectories { get; private set; }

        private readonly List<HistoryRecord> _historyRecords = [];

        public DirectoryService(HistoryService historyService)
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);
            _historyService = historyService;
            MonitoredDirectories = LoadDirectories();
        }

        private ObservableCollection<MonitoredDirectory> LoadDirectories()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    return JsonSerializer.Deserialize<ObservableCollection<MonitoredDirectory>>(json) ?? new ObservableCollection<MonitoredDirectory>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading directories: {ex.Message}");
            }

            return new ObservableCollection<MonitoredDirectory>();
        }

        public async void AddMonitoredDirectory(MonitoredDirectory directory)
        {
            var historyMessage = $"Monitoring new directory: {directory.Name} at {directory.Path}";
            var record = new HistoryRecord(EventType.DirectoryCreated, historyMessage);
            _historyRecords.Add(record);

            await _historyService.SaveHistoryRecordAsync(record);

            directory.Id = MonitoredDirectories.Count > 0 ? MonitoredDirectories.Max(d => d.Id) + 1 : 1;
            MonitoredDirectories.Add(directory);
            SaveDirectories();
        }

        public void UpdateDirectory(MonitoredDirectory updatedDirectory)
        {
            var existingDirectory = MonitoredDirectories.FirstOrDefault(d => d.Id == updatedDirectory.Id);
            if (existingDirectory != null)
            {
                existingDirectory.Name = updatedDirectory.Name;
                existingDirectory.Path = updatedDirectory.Path;
                SaveDirectories();
            }
        }

        public void DeleteDirectory(int id)
        {
            var directoryToDelete = MonitoredDirectories.FirstOrDefault(d => d.Id == id);
            if (directoryToDelete != null)
            {
                MonitoredDirectories.Remove(directoryToDelete);
                SaveDirectories();
            }
        }

        private void SaveDirectories()
        {
            try
            {
                var json = JsonSerializer.Serialize(MonitoredDirectories);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving directories: {ex.Message}");
            }
        }

        public IEnumerable<HistoryRecord> GetHistory()
        {
            return _historyRecords;
        }
    }
}
