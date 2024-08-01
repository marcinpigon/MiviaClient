using MiviaMaui;
using MiviaMaui.Dtos;
using System.Collections.ObjectModel;

namespace MiviaMaui.ViewModels;

public class ModelsViewModel : BaseViewModel
{
    private readonly IMiviaClient _miviaClient;
    public ObservableCollection<ModelDto> Models { get; } = new ObservableCollection<ModelDto>();

    public ModelsViewModel(IMiviaClient miviaClient)
    {
        _miviaClient = miviaClient;
    }

    public async Task LoadModelsAsync()
    {
        if (IsBusy) return;

        IsBusy = true;

        try
        {
            var models = await _miviaClient.GetModelsAsync();
            Models.Clear();
            foreach (var model in models)
            {
                Models.Add(model);
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
