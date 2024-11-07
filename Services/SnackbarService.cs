using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MiviaMaui.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiviaMaui.Services
{
    public class SnackbarService : ISnackbarService
    {
        public async Task ShowSuccessSnackbarAsync(string message, string? filePath = null, int durationMs = 3000)
        {
            if (filePath == null)
            {
                var snackbar = Snackbar.Make(message,
                    duration: TimeSpan.FromMilliseconds(durationMs),
                    visualOptions: new CommunityToolkit.Maui.Core.SnackbarOptions { BackgroundColor = Colors.Green, TextColor = Colors.White });
                await snackbar.Show();
            }
            else
            {
                await ShowClickableSuccessSnackbarAsync(message, filePath, 10000);
            }
        }

        public async Task ShowClickableSuccessSnackbarAsync(string message, string filePath, int durationMs = 3000)
        {
            var snackbar = Snackbar.Make(
            message,
            duration: TimeSpan.FromMilliseconds(durationMs),
            action: async () =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    await ShowErrorSnackbarAsync("Failed to open file.");
                    Debug.WriteLine($"Error opening file: {ex.Message}");
                }
            },
            actionButtonText: "Open",
            visualOptions: new SnackbarOptions
            {
                BackgroundColor = Colors.Green,
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.White
            });

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
