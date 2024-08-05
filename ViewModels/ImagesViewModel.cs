using MiviaMaui;
using MiviaMaui.Dtos;
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
                // Handle the exception appropriately (e.g., show an error message)
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
