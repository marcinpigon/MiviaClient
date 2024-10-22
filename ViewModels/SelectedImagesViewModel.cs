using MiviaMaui.Dtos;
using MiviaMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.ViewModels
{
    public class SelectedImagesViewModel : BaseViewModel
    {
        private readonly ModelService _modelService;
        public ObservableCollection<ImageDto> SelectedImages { get; } = new();
        private ObservableCollection<ModelDto> _models = new();
        public ObservableCollection<ModelDto> Models
        {
            get => _models;
            set
            {
                _models = value;
                OnPropertyChanged(nameof(Models));
            }
        }

        private ImageDto _currentImage;
        public ImageDto CurrentImage
        {
            get => _currentImage;
            set
            {
                if (_currentImage != value)
                {
                    if (_currentImage != null)
                        _currentImage.IsCurrentlySelected = false;

                    _currentImage = value;

                    if (_currentImage != null)
                        _currentImage.IsCurrentlySelected = true;

                    OnPropertyChanged(nameof(CurrentImage));
                    RefreshModelSelections(); 
                }
            }
        }

        public bool CanProcess => SelectedImages.Any() &&
                                SelectedImages.Any(img => img.SelectedModels.Any());

        public Command<ImageDto> ToggleImageSelectionCommand { get; }
        public Command ToggleAllSelectionCommand { get; }
        public Command ClearSelectionCommand { get; }

        public SelectedImagesViewModel(ModelService modelService,
                                     List<ImageDto> selectedImages)
        {
            _modelService = modelService;

            foreach (var image in selectedImages)
            {
                image.SelectedModels = new ObservableCollection<ModelDto>();
                image.IsCurrentlySelected = false;
                SelectedImages.Add(image);
            }

            ToggleImageSelectionCommand = new Command<ImageDto>(OnToggleImageSelection);
            ToggleAllSelectionCommand = new Command(OnToggleAllSelection);
            ClearSelectionCommand = new Command(OnClearSelection);
        }

        private void RefreshModelSelections()
        {
            // Create new instances of models to force UI update
            var updatedModels = new ObservableCollection<ModelDto>();

            foreach (var model in _models)
            {
                var isSelected = _currentImage?.SelectedModels.Any(m => m.Name == model.Name) ?? false;

                updatedModels.Add(new ModelDto
                {
                    Name = model.Name,
                    DisplayName = model.DisplayName,
                    IsSelected = isSelected
                });
            }

            Models = updatedModels;  // This will trigger the property changed notification
        }

        public void HandleModelSelection(ModelDto model)
        {
            if (_currentImage == null) return;

            if (model.IsSelected)
            {
                if (!_currentImage.SelectedModels.Any(m => m.Name == model.Name))
                {
                    _currentImage.SelectedModels.Add(new ModelDto
                    {
                        Name = model.Name,
                        DisplayName = model.DisplayName,
                        IsSelected = true
                    });
                }
            }
            else
            {
                var existingModel = _currentImage.SelectedModels.FirstOrDefault(m => m.Name == model.Name);
                if (existingModel != null)
                {
                    _currentImage.SelectedModels.Remove(existingModel);
                }
            }

            OnPropertyChanged(nameof(CanProcess));
        }

        public void UpdateModelSelections()
        {
            if (_currentImage != null)
            {
                foreach (var model in Models)
                {
                    model.IsSelected = _currentImage.SelectedModels.Any(m => m.Name == model.Name);
                }
            }
            else
            {
                foreach (var model in Models)
                {
                    model.IsSelected = false;
                }
            }

            // Force UI update for the entire Models collection
            OnPropertyChanged(nameof(Models));
            OnPropertyChanged(nameof(CanProcess));
        }

        public async Task LoadModelsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var models = await _modelService.GetModelsAsync();
                Models.Clear();
                foreach (var model in models)
                {
                    Models.Add(model);
                }
            }
            catch (Exception ex)
            {
                // Handle error appropriately
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ToggleModelForImage(ModelDto model, ImageDto image)
        {
            if (image.SelectedModels.Contains(model))
                image.SelectedModels.Remove(model);
            else
                image.SelectedModels.Add(model);

            OnPropertyChanged(nameof(CanProcess));
        }

        private void OnToggleImageSelection(ImageDto image)
        {
            if (image == null) return;

            foreach (var img in SelectedImages)
            {
                img.IsCurrentlySelected = false;
            }

            image.IsCurrentlySelected = true;
            CurrentImage = image;
        }

        private void OnToggleAllSelection()
        {
            bool shouldSelect = !SelectedImages.All(x => x.IsCurrentlySelected);

            foreach (var image in SelectedImages)
            {
                image.IsCurrentlySelected = shouldSelect;
            }

            CurrentImage = shouldSelect ? SelectedImages.FirstOrDefault() : null;
            UpdateModelSelections();
        }

        private void OnClearSelection()
        {
            foreach (var image in SelectedImages)
            {
                image.IsCurrentlySelected = false;
            }
            CurrentImage = null;
            OnPropertyChanged(nameof(CanProcess));
        }

        public async Task ProcessImagesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                foreach (var image in SelectedImages)
                {
                    foreach (var model in image.SelectedModels)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle error appropriately
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
