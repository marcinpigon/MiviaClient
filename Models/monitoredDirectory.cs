using MiviaMaui.Dtos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MiviaMaui.Models
{
    public class MonitoredDirectory : INotifyPropertyChanged
    {
        private int _id;
        private string _name;
        private string _path;
        private List<string> _modelIds;
        private List<string> _modelNames;

        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        public List<string> ModelIds
        {
            get => _modelIds;
            set
            {
                if (_modelIds != value)
                {
                    _modelIds = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> ModelNames
        {
            get => _modelNames;
            set
            {
                if (_modelNames != value)
                {
                    _modelNames = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MonitoredDirectory()
        {
            _modelIds = new List<string>();
            _modelNames = new List<string>();
        }
    }
}
