using MiviaMaui.Interfaces;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

public class WindowsNotificationService : INotificationService
{
    public void ShowNotification(string title, string message)
    {
        // Create the toast content using an XML template
        var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

        // Fill in the text elements
        var stringElements = toastXml.GetElementsByTagName("text");
        stringElements[0].AppendChild(toastXml.CreateTextNode(title));
        stringElements[1].AppendChild(toastXml.CreateTextNode(message));

        // Create the toast notification and set it to show
        var toast = new ToastNotification(toastXml);
        ToastNotificationManager.CreateToastNotifier().Show(toast);
    }
}
