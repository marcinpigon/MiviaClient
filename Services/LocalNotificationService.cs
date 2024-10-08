using MiviaMaui.Interfaces;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                }
            };

            LocalNotificationCenter.Current.Show(notification);
        }
    }
}
