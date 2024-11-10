using MiviaMaui.Interfaces;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.IO;
#if ANDROID
using Android.Content;
#endif

namespace MiviaMaui.Services
{
    public class LocalNotificationService : MiviaMaui.Interfaces.INotificationService
    {
        private static int _notificationId = 100;
        public void ShowNotification(string title, string message)
        {
            var notification = new NotificationRequest
            {
                NotificationId = ++_notificationId,
                Title = title,
                Description = message,
                BadgeNumber = 1,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                },
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
                {
                    LaunchAppWhenTapped = false,
                    TimeoutAfter = TimeSpan.FromMinutes(5),
                    LedColor = 1
                }
            };

            LocalNotificationCenter.Current.Show(notification);
        }

        public void ShowClickableNotification(string title, string message, string filePath)
        {
            var notification = new NotificationRequest
            {
                NotificationId = ++_notificationId,
                Title = title,
                Description = message,
                BadgeNumber = 1,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                },
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions
                {
                    ChannelId = "default",
                    LaunchAppWhenTapped = false,
                },
                ReturningData = filePath
            };

            LocalNotificationCenter.Current.NotificationActionTapped += Current_NotificationActionTapped;
            LocalNotificationCenter.Current.Show(notification);
        }

        private async void Current_NotificationActionTapped(Plugin.LocalNotification.EventArgs.NotificationActionEventArgs e)
        {
            // Retrieve the file path from ReturningData
            var filePath = e.Request.ReturningData;

            if (!string.IsNullOrEmpty(filePath))
            {
                await OpenPdfFile(filePath);
            }
            else
            {
                Console.WriteLine("No file path found in notification data.");
            }

            // Unsubscribe to avoid multiple events
            LocalNotificationCenter.Current.NotificationActionTapped -= Current_NotificationActionTapped;
        }

        private async Task OpenPdfFile(string filePath)
        {
#if ANDROID
            Console.WriteLine("Attempting to open PDF file...");

            try
            {
                var context = Android.App.Application.Context;

                // Create a file URI with FileProvider
                var file = new Java.IO.File(filePath);
                var uri = AndroidX.Core.Content.FileProvider.GetUriForFile(context, $"{context.PackageName}.fileprovider", file);

                // Create the intent
                var intent = new Intent(Intent.ActionView);
                intent.SetDataAndType(uri, "application/pdf");

                // Set flags to grant permissions
                intent.AddFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission | ActivityFlags.GrantWriteUriPermission);

                // Check if there's a compatible PDF viewer
                if (intent.ResolveActivity(context.PackageManager) != null)
                {
                    context.StartActivity(intent);
                }
                else
                {
                    Console.WriteLine("No PDF viewer found to open this file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting PDF intent: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
#endif
        }
    }
}
