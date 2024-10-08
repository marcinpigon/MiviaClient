using CommunityToolkit.Maui.Storage;

    public class WindowsFolderPickerService : MiviaMaui.Interfaces.IFolderPicker
    {
        public async Task<string> PickFolderAsync()
        {
            var folder = await FolderPicker.PickAsync(default);

            return folder?.Folder.Path;
        }
    }

