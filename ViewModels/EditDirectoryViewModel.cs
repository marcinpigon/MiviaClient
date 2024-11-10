using MiviaMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.ViewModels
{
    public class EditDirectoryViewModel : BaseViewModel
    {
        private string _name;
        private string _path;
        public ObservableCollection<string> ModelIds { get; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EditDirectoryViewModel(MonitoredDirectory directory)
        {
            _name = directory.Name;
            _path = directory.Path;
            ModelIds = new ObservableCollection<string>(directory.ModelIds);
        }

        public void ApplyTo(MonitoredDirectory directory)
        {
            directory.Name = _name;
            directory.Path = _path;
            directory.ModelIds.Clear();
            foreach (var modelId in ModelIds)
            {
                directory.ModelIds.Add(modelId);
            }
        }
    }
}
