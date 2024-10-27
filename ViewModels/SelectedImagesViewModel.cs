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
        private readonly IMiviaClient _miviaClient;

        public ObservableCollection<ImageDto> Images { get; } = new();
        private ObservableCollection<ModelDto> _models = new();
        private readonly IImagePathService _imagePathService;
        private readonly INotificationService _notificationService;
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

        private string _processingStatusText = "";
        public string ProcessingStatusText
        {
            get => _processingStatusText;
            set
            {
                _processingStatusText = value;
                OnPropertyChanged(nameof(ProcessingStatusText));
            }
        }

        private CancellationTokenSource _cancellationTokenSource;

        public bool CanProcess => Images.Any() &&
                                Images.Any(img => img.SelectedModels.Any());

        public Command<ImageDto> ToggleImageSelectionCommand { get; }
        public Command ToggleAllSelectionCommand { get; }
        public Command ClearSelectionCommand { get; }
        public Command CancelProcessingCommand { get; }

        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public SelectedImagesViewModel(
       ModelService modelService,
       List<ImageDto> selectedImages,
       ICommandBus commandBus,
       IQueryBus queryBus,
       IImagePathService imagePathService,
       INotificationService notificationService,
       IMiviaClient miviaClient)
        {
            _modelService = modelService;
            _miviaClient = miviaClient;
            _imagePathService = imagePathService;
            _notificationService = notificationService;
            _commandBus = commandBus;
            _queryBus = queryBus;

            ToggleImageSelectionCommand = new Command<ImageDto>(OnToggleImageSelection);
            ToggleAllSelectionCommand = new Command(OnToggleAllSelection);
            ClearSelectionCommand = new Command(OnClearSelection);
            CancelProcessingCommand = new Command(CancelProcessing);
        }

        public async Task LoadImagesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                ProcessingStatusText = "Loading images";
                var images = await _miviaClient.GetImagesAsync();
                Images.Clear();
                foreach (var image in images)
                {
                    image.SelectedModels = new ObservableCollection<ModelDto>();
                    image.IsCurrentlySelected = false;
                    await LoadImage(image);
                    Images.Add(image);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading images: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadImage(ImageDto image)
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
            var selectedImages = Images.Where(img => img.IsCurrentlySelected).ToList();

            foreach (var model in _models)
            {
                bool isSelected = false;

                if (selectedImages.Any())
                {
                    // Check if ALL selected images have this model
                    isSelected = selectedImages.All(img =>
                        img.SelectedModels.Any(m => m.Name == model.Name));
                }

                updatedModels.Add(new ModelDto
                {
                    Name = model.Name,
                    DisplayName = model.DisplayName,
                    Id = model.Id,
                    IsSelected = isSelected
                });
            }

            Models = updatedModels;
            OnPropertyChanged(nameof(CanProcess));
        }

        public void HandleModelSelection(ModelDto model)
        {
            if (model == null) return;

            var selectedImages = Images.Where(img => img.IsCurrentlySelected).ToList();
            if (!selectedImages.Any()) return;

            // If ANY image has this model and we're unchecking, remove from ALL
            // If NOT ALL images have this model and we're checking, add to ALL
            bool shouldAddToAll = !selectedImages.All(img =>
                img.SelectedModels.Any(m => m.Name == model.Name));

            foreach (var image in selectedImages)
            {
                if (shouldAddToAll)
                {
                    if (!image.SelectedModels.Any(m => m.Name == model.Name))
                    {
                        image.SelectedModels.Add(new ModelDto
                        {
                            Name = model.Name,
                            DisplayName = model.DisplayName,
                            Id = model.Id,
                            IsSelected = true
                        });
                    }
                }
                else
                {
                    var existingModel = image.SelectedModels.FirstOrDefault(m => m.Name == model.Name);
                    if (existingModel != null)
                    {
                        image.SelectedModels.Remove(existingModel);
                    }
                }
            }

            // Update the model's IsSelected state based on the new state
            model.IsSelected = shouldAddToAll;

            OnPropertyChanged(nameof(CanProcess));
            UpdateModelSelections(); // Refresh all checkboxes to maintain consistency
        }

        public void UpdateModelSelections()
        {
            var selectedImages = Images.Where(img => img.IsCurrentlySelected).ToList();

            if (selectedImages.Any())
            {
                foreach (var model in Models)
                {
                    // Check if ALL selected images have this model
                    model.IsSelected = selectedImages.All(img =>
                        img.SelectedModels.Any(m => m.Name == model.Name));
                }
            }
            else
            {
                foreach (var model in Models)
                {
                    model.IsSelected = false;
                }
            }

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

            foreach (var img in Images)
            {
                img.IsCurrentlySelected = false;
            }

            image.IsCurrentlySelected = true;
            CurrentImage = image;
        }

        private void OnToggleAllSelection()
        {
            bool shouldSelect = !Images.All(x => x.IsCurrentlySelected);

            foreach (var image in Images)
            {
                image.IsCurrentlySelected = shouldSelect;

                if (shouldSelect)
                {
                    // Clear existing selected models to avoid duplicates
                    image.SelectedModels.Clear();

                    // Add all currently selected models to this image
                    foreach (var model in Models.Where(m => m.IsSelected))
                    {
                        image.SelectedModels.Add(new ModelDto
                        {
                            Name = model.Name,
                            DisplayName = model.DisplayName,
                            Id = model.Id,
                            IsSelected = true
                        });
                    }
                }
                else
                {
                    // If deselecting all, clear the selected models
                    image.SelectedModels.Clear();
                }
            }

            CurrentImage = shouldSelect ? Images.FirstOrDefault() : null;
            UpdateModelSelections();
            OnPropertyChanged(nameof(CanProcess));
        }

        private void OnClearSelection()
        {
            foreach (var image in Images)
            {
                image.IsCurrentlySelected = false;
            }
            CurrentImage = null;
            OnPropertyChanged(nameof(CanProcess));
        }

        private void CancelProcessing()
        {
            _cancellationTokenSource?.Cancel();
            ProcessingStatusText = "Cancelling...";
        }

        public async Task ProcessImagesAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            ProcessingStatusText = "Initializing...";

            _cancellationTokenSource = new CancellationTokenSource();
            try
            {
                var jobIds = new List<string>();
                var selectedImages = Images.Where(image => image.SelectedModels.Count > 0);
                var currentImage = 0;
                foreach (var image in selectedImages)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    currentImage++;
                    ProcessingStatusText = $"Processing image {currentImage} of {selectedImages.Count()}";

                    foreach (var model in image.SelectedModels)
                    {
                        _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                        var jobId = await _commandBus.SendAsync<ScheduleJobCommand, string>(new ScheduleJobCommand
                        {
                            ImageId = image.Id,
                            ModelId = model.Id
                        });
                        if (!string.IsNullOrEmpty(jobId))
                            jobIds.Add(jobId);
                    }
                }

                ProcessingStatusText = "Checking job status...";
                var finishedJobIds = new List<string>();
                foreach (var jobId in jobIds)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    var isJobFinished = await _queryBus.SendAsync<IsJobFinishedQuery, bool>(new IsJobFinishedQuery
                    {
                        JobId = jobId
                    });

                    if (isJobFinished) finishedJobIds.Add(jobId);
                }

                if (finishedJobIds.Count > 0)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                    ProcessingStatusText = "Generating report...";
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string reportPrefix = $"ImageAnalysisReport_{finishedJobIds.Count}images_{timestamp}";
                    string fileName = $"{reportPrefix}.pdf";

                    string downloadsPath = GetDownloadsPath();
                    string outputPath = Path.Combine(downloadsPath, fileName);
                    var success = await _commandBus.SendAsync<GenerateReportMultipleJobsCommand, bool>(new GenerateReportMultipleJobsCommand
                    {
                        JobIds = finishedJobIds,
                        OutputPath = outputPath
                    });

                    if (success)
                    {
                        _notificationService.ShowNotification("Report saved", $"Collective report saved in {outputPath}");
                    }
                }
                else
                {
                    _notificationService.ShowNotification("Report failed", "No images were succesfully analysed within the timeframe.");
                }
            }
            catch (OperationCanceledException)
            {
                _notificationService.ShowNotification("Cancelled", "Image processing was cancelled by user.");
            }
            catch (Exception ex)
            {
                _notificationService.ShowNotification("Report failed", "Failed to generate collection report for images");
            }
            finally
            {
                ProcessingStatusText = "";
                IsBusy = false;
                _cancellationTokenSource?.Dispose();
                _cancellationTokenSource = null;
            }
        }

        private string GetDownloadsPath()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Downloads");
            }

            // For Windows, use the standard Downloads folder
            return Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );
        }
    }
}
