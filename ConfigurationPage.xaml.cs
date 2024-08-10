using MiviaMaui.Models;
using MiviaMaui.Services;

namespace MiviaMaui;

public partial class ConfigurationPage : ContentPage
{
    private bool isVisible = false;

    private readonly HistoryService _historyService;

    public ConfigurationPage(HistoryService historyService)
	{
		InitializeComponent();
        _historyService = historyService;
        LoadAccessToken();
    }

    private void OnToggleVisibilityClicked(object sender, EventArgs e)
    {
        isVisible = !isVisible;

        if (isVisible)
        {
            accessTokenEntry.IsPassword = false; // Show plain text
            toggleVisibilityButton.Text = "Hide";
        }
        else
        {
            accessTokenEntry.IsPassword = true; // Show dots
            toggleVisibilityButton.Text = "Show";
        }
    }


    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // Get the access token from the Entry
        string accessToken = accessTokenEntry.Text;

        // Save the access token securely using SecureStorage
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            await SecureStorage.SetAsync("AccessToken", accessToken);
            await DisplayAlert("Success", "Access token saved successfully!", "OK");

            var historyMessage = $"Updated access token";
            var record = new HistoryRecord(EventType.ConfigurationUpdated, historyMessage);

            await _historyService.SaveHistoryRecordAsync(record);

        }
        else
        {
            await DisplayAlert("Error", "Access token cannot be empty!", "OK");
        }
    }

    private async void LoadAccessToken()
    {
        // Load the access token from SecureStorage
        string accessToken = await SecureStorage.GetAsync("AccessToken") ?? "";

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            accessTokenEntry.Text = accessToken;
        }
    }
}