using CommunityToolkit.Maui.Alerts;
using MiviaMaui.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Services
{
    public class SnackbarService : ISnackbarService
    {
        public async Task ShowSuccessSnackbarAsync(string message, int durationMs = 3000)
        {
            var snackbar = Snackbar.Make(message,
                duration: TimeSpan.FromMilliseconds(durationMs),
                visualOptions: new CommunityToolkit.Maui.Core.SnackbarOptions { BackgroundColor = Colors.Green, TextColor = Colors.White});
            await snackbar.Show();
        }

        public async Task ShowErrorSnackbarAsync(string message, int durationMs = 3000)
        {
            var snackbar = Snackbar.Make(message,
                duration: TimeSpan.FromMilliseconds(durationMs),
                visualOptions: new CommunityToolkit.Maui.Core.SnackbarOptions { BackgroundColor = Colors.Red, TextColor = Colors.White });
            await snackbar.Show();
        }
    }
}
