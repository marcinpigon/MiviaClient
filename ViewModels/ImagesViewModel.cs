using MiviaMaui;
using MiviaMaui.Dtos;
using MiviaMaui.Interfaces;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MiviaMaui.ViewModels
{
    public class ImagesViewModel : BaseViewModel
    {
        private readonly IMiviaClient _miviaClient;
        public ObservableCollection<ImageDto> Images { get; } = new ObservableCollection<ImageDto>();
        private readonly object _lock = new object();

        public ImagesViewModel(IMiviaClient miviaClient)
        {
            _miviaClient = miviaClient;
        }

        public async Task LoadImagesAsync()
        {
            lock (_lock)
            {
                if (IsBusy) return;

                IsBusy = true;
            }

            try
            {
                var images = await _miviaClient.GetImagesAsync();
                Images.Clear();
                foreach (var image in images)
                {
                    Images.Add(image);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ToggleImageSelection(ImageDto image)
        {
            image.IsSelected = !image.IsSelected;
        }

        public async Task DeleteImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id)) return;

            lock (_lock)
            {
                if (IsBusy) return;
                IsBusy = true;
            }

            try
            {
                await _miviaClient.DeleteImageAsync(id);

                var imageToRemove = Images.FirstOrDefault(img => img.Id == id);
                if (imageToRemove != null)
                {
                    Images.Remove(imageToRemove);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error deleting image: {ex.Message}");
            }
            finally
            { 
                IsBusy = false; 
            }
        }
    }
}
