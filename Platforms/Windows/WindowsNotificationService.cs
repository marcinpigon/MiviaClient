using MiviaMaui.Interfaces;
using System.Diagnostics;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

public class WindowsNotificationService : INotificationService
{
    public WindowsNotificationService() { }
    public void ShowNotification(string title, string message)
    {
        var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

        var stringElements = toastXml.GetElementsByTagName("text");
        stringElements[0].AppendChild(toastXml.CreateTextNode(title));
        stringElements[1].AppendChild(toastXml.CreateTextNode(message));

        var toast = new ToastNotification(toastXml);
        ToastNotificationManager.CreateToastNotifier().Show(toast);
    }

    public void ShowClickableNotification(string title, string message, string filePath)
    {
        var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

        var stringElements = toastXml.GetElementsByTagName("text");
        stringElements[0].AppendChild(toastXml.CreateTextNode(title));
        stringElements[1].AppendChild(toastXml.CreateTextNode(message));

        var toastElement = (XmlElement)toastXml.SelectSingleNode("/toast");
        toastElement.SetAttribute("launch", filePath);

        var toast = new ToastNotification(toastXml);

        toast.Activated += (s, e) => OpenPdf(filePath);

        ToastNotificationManager.CreateToastNotifier().Show(toast);
    }

    private void OpenPdf(string filePath)
    {
        try
        {
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to open PDF: {ex.Message}");
        }
    }
}
