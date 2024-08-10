using MiviaMaui;
using MiviaMaui.Dtos;
using MiviaMaui.Services;
using System.Collections.ObjectModel;

namespace MiviaMaui.ViewModels;

public class ModelsViewModel : BaseViewModel
{
    private readonly ModelService _modelService;
    public ObservableCollection<ModelDto> Models { get; } = new ObservableCollection<ModelDto>();
    private readonly object _lock = new object();

    public ModelsViewModel(ModelService modelService)
    {
        _modelService = modelService;
    }

    public async Task LoadModelsAsync()
    {
        lock (_lock)
        {
            if (IsBusy ) return;

            IsBusy = true;
        }

        try
        {
            var models = await _modelService.GetModelsAsync();
            Models.Clear();
            foreach (var model in _modelService._models)
            {
                Models.Add(model);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }
}
