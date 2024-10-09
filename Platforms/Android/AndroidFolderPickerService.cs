using MiviaMaui.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;


public class AndroidFolderPickerService : IFolderPicker
{
    private static TaskCompletionSource<string> _folderPathTCS;

    public Task<string> PickFolderAsync()
    {
        _folderPathTCS = new TaskCompletionSource<string>();

        try
        {
            var intent = new Intent(Intent.ActionOpenDocumentTree);
            intent.AddFlags(ActivityFlags.GrantPersistableUriPermission |
                              ActivityFlags.GrantReadUriPermission |
                              ActivityFlags.GrantWriteUriPermission);

            var activity = Platform.CurrentActivity;
            if (activity == null)
                throw new InvalidOperationException("Current activity is null");

            activity.StartActivityForResult(intent, 9999);
        }
        catch (Exception ex)
        {
            _folderPathTCS.SetException(ex);
        }

        return _folderPathTCS.Task;
    }

    public static void HandleActivityResult(Context context, Result resultCode, Intent data)
    {
        if (_folderPathTCS == null) return;

        try
        {
            if (resultCode == Result.Ok && data?.Data != null)
            {
                var uri = data.Data;
                // Take persistable permission
                context.ContentResolver?.TakePersistableUriPermission(
                    uri,
                    ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission);

                _folderPathTCS.TrySetResult(uri.ToString());
            }
            else
            {
                _folderPathTCS.TrySetResult(null);
            }
        }
        catch (Exception ex)
        {
            _folderPathTCS.TrySetException(ex);
        }
    }
}

