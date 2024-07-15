using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using MiviaMaui.Models;

namespace MiviaMaui.Services
{
    public class DirectoryService
    {
        private const string FileName = "monitored_directories.json";
        private readonly string _filePath;

        public ObservableCollection<MonitoredDirectory> MonitoredDirectories { get; private set; }

        public DirectoryService()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FileName);
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
                // Log the exception or handle it as needed
                Console.WriteLine($"Error loading directories: {ex.Message}");
            }

            return new ObservableCollection<MonitoredDirectory>();
        }

        public void AddMonitoredDirectory(MonitoredDirectory directory)
        {
            MonitoredDirectories.Add(directory);
            SaveDirectories();
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
                // Log the exception or handle it as needed
                Console.WriteLine($"Error saving directories: {ex.Message}");
            }
        }
    }
}
