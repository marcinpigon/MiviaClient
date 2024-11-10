namespace MiviaMaui.Interfaces
{
    public interface INotificationService
    {
        void ShowNotification(string title, string message);
        void ShowClickableNotification(string title, string message, string filePath);
    }
}
