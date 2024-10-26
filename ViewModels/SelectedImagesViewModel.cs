using MiviaMaui.Bus;
using MiviaMaui.Commands;
using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using MiviaMaui.Queries;
using MiviaMaui.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.ViewModels
{
    public class SelectedImagesViewModel : BaseViewModel
    {
        private readonly ModelService _modelService;
        public Queue<ImageDto> RemainingImages { get; set; }
        private bool _isLoadingMore;
        public ObservableCollection<ImageDto> SelectedImages { get; } = new();
        private ObservableCollection<ModelDto> _models = new();
        private readonly IImagePathService _imagePathService;
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

        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public SelectedImagesViewModel(ModelService modelService,
                                     List<ImageDto> selectedImages,
                                     ICommandBus commandBus,
                                     IQueryBus queryBus,
                                     IImagePathService imagePathService)
        {
            _modelService = modelService;
            _imagePathService = imagePathService;

            foreach (var image in selectedImages)
            {
                image.SelectedModels = new ObservableCollection<ModelDto>();
                image.IsCurrentlySelected = false;

                LoadImage(image);

                SelectedImages.Add(image);
            }

            ToggleImageSelectionCommand = new Command<ImageDto>(OnToggleImageSelection);
            ToggleAllSelectionCommand = new Command(OnToggleAllSelection);
            ClearSelectionCommand = new Command(OnClearSelection);
            _queryBus = queryBus;
            _commandBus = commandBus;
        }

        private async void LoadImage(ImageDto image)
        {
            try
            {
                image.ImagePath = await _imagePathService.GetImagePath(image.Id);
            }
            catch (Exception)
            {
                image.ImagePath = "image_unavailable.png";
            }
        }

        private void RefreshModelSelections()
        {
            var updatedModels = new ObservableCollection<ModelDto>();

            foreach (var model in _models)
            {
                var isSelected = _currentImage?.SelectedModels.Any(m => m.Name == model.Name) ?? false;

                updatedModels.Add(new ModelDto
                {
                    Name = model.Name,
                    DisplayName = model.DisplayName,
                    Id = model.Id,
                    IsSelected = isSelected
                });
            }

            Models = updatedModels; 
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
                        Id= model.Id,
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
                var jobIds = new List<string>();
                foreach (var image in SelectedImages)
                {
                    foreach (var model in image.SelectedModels)
                    {
                        var jobId = await _commandBus.SendAsync<ScheduleJobCommand, string>(new ScheduleJobCommand
                        {
                            ImageId = image.Id,
                            ModelId = model.Id
                        });
                        if (!string.IsNullOrEmpty(jobId))
                            jobIds.Add(jobId);
                    }
                }

                var finishedJobIds = new List<string>();
                foreach(var jobId in jobIds)
                {
                    var isJobFinished = await _queryBus.SendAsync<IsJobFinishedQuery, bool>(new IsJobFinishedQuery
                    {
                        JobId = jobId
                    });

                    if(isJobFinished) finishedJobIds.Add(jobId);
                }

                string downloadsPath = @"C:\Users\marci\Downloads";
                string fileName = "Report1.pdf";
                string outputPath = Path.Combine(downloadsPath, fileName);
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Downloads");
                }
                outputPath = Path.Combine(downloadsPath, fileName);

                await _commandBus.SendAsync<GenerateReportMultipleJobsCommand, bool>(new GenerateReportMultipleJobsCommand
                {
                    JobIds = finishedJobIds,
                    OutputPath = outputPath
                });
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
