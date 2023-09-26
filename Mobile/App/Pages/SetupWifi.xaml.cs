using Plugin.BLE.Abstractions;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using System.Net;
using System.Text;

namespace Mobile.App.Pages;

public partial class SetupWifi : ContentPage
{
    private readonly IAdapter adapter;
    private readonly ScanFilterOptions filter;

    private string wifiName;
    private string wifiPass;

	public SetupWifi(string address)
	{
		InitializeComponent();
        adapter = CrossBluetoothLE.Current.Adapter;
        filter = new ScanFilterOptions() { DeviceAddresses = new[] { address } };
    }

    public void OnWifiNameTextChanged(object sender, TextChangedEventArgs e)
    {
        wifiName = e.NewTextValue;
    }

    public void OnWifiPassTextChanged(object sender, TextChangedEventArgs e)
    {
        wifiPass = e.NewTextValue;
    }

    public async Task OnCompleted(object sender, EventArgs e)
    {
        var attempts = 1;
        await adapter.StartScanningForDevicesAsync(filter);

        adapter.DeviceDiscovered += async (sender, e) =>
        {
            await adapter.StopScanningForDevicesAsync();
            await adapter.ConnectToDeviceAsync(e.Device);
        };

        adapter.DeviceConnected += async (sender, e) =>
        {
            var service = await e.Device.GetServiceAsync(Guid.NewGuid());
            var characteristic = await service.GetCharacteristicAsync(Guid.NewGuid());
            var data = string.Format("{0} {1}", wifiName, wifiPass);
            await characteristic.WriteAsync(Encoding.ASCII.GetBytes(data));
        };

        adapter.ScanTimeoutElapsed += async (sender, e) =>
        {
            if (attempts == 1)
            {
                attempts++;
                await adapter.StartScanningForDevicesAsync();
            }
        };
    }
}