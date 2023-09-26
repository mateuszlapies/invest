
using ZXing.Net.Maui;

namespace Mobile.App.Pages;

public partial class SetupScan : ContentPage
{
	public SetupScan()
	{
        InitializeComponent();
    }

    private async Task QRCodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        var address = (string)e.Results.GetValue(0);
        await Navigation.PushAsync(new SetupWifi(address));
    }
}