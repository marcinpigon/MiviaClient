using Android.Content;
using Android.Database;
using Android.OS;
using Android.Provider;
using Android.Webkit;

public static class FileUtils
{
    public static string GetFullPathFromUri(Context context, Android.Net.Uri uri)
    {
        string fullPath = null;

        if (DocumentsContract.IsDocumentUri(context, uri))
        {
            string documentId = DocumentsContract.GetDocumentId(uri);
            string[] parts = documentId.Split(':');
            string type = parts[0];
            string realId = parts[1];

            if ("primary".Equals(type, StringComparison.OrdinalIgnoreCase))
            {
                fullPath = Android.OS.Environment.ExternalStorageDirectory + "/" + realId;
            }
        }
        else if ("content".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
        {
            string[] projection = { MediaStore.MediaColumns.Data };
            var cursor = context.ContentResolver.Query(uri, projection, null, null, null);
            if (cursor != null)
            {
                if (cursor.MoveToFirst())
                {
                    fullPath = cursor.GetString(cursor.GetColumnIndexOrThrow(MediaStore.MediaColumns.Data));
                }
                cursor.Close();
            }
        }
        else if ("file".Equals(uri.Scheme, StringComparison.OrdinalIgnoreCase))
        {
            fullPath = uri.Path;
        }

        return fullPath;
    }
}
