using CommunityToolkit.Maui.Storage;

public class WindowsFolderPickerService : MiviaMaui.Interfaces.IFolderPicker
{
    public async Task<string> PickFolderAsync()
    {
        var folder = await FolderPicker.PickAsync(default);
        if (folder == null)
            return string.Empty;
        return folder.Folder?.Path ?? string.Empty;
    }
}

