using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using MiviaMaui.Interfaces;
using MiviaMaui.Platforms.Android;
using MiviaMaui.Services;

namespace MiviaMaui
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private AndroidFileObserver? _directoryFileObserver;
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 9999)
            {
                AndroidFolderPickerService.HandleActivityResult(this, resultCode, data);
            }
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (OperatingSystem.IsAndroidVersionAtLeast(23))
            {
                var readPermissionStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
                var writePermissionStatus = await Permissions.RequestAsync<Permissions.StorageWrite>();

                if (readPermissionStatus != PermissionStatus.Granted || writePermissionStatus != PermissionStatus.Granted)
                {
                    Android.Widget.Toast.MakeText(this, "Storage permissions are required to save files.", Android.Widget.ToastLength.Long).Show();
                }
            }

            var directoryWatcherService = App.ServiceProvider.GetRequiredService<IDirectoryWatcherService>();
            directoryWatcherService.StartWatching();

            Platforms.Android.ActivityStateManager.CurrentActivity = this;
        }


        protected override void OnResume()
        {
            base.OnResume();
            Platforms.Android.ActivityStateManager.CurrentActivity = this;
        }

        protected override void OnPause()
        {
            Platforms.Android.ActivityStateManager.CurrentActivity = null;
            base.OnPause();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Stop observing when the activity is destroyed to avoid memory leaks
            _directoryFileObserver?.StopWatching();
        }
    }
}
