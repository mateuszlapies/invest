using HashtagChris.DotNetBlueZ;

namespace Raspberry.App.Integrations.Bluetooth
{
    public class BluetoothIntegration
    {
        public BluetoothIntegration()
        {
            var adapters = BlueZManager.GetAdaptersAsync().GetAwaiter().GetResult();
            foreach (var adapter in adapters)
            {
                adapter.PoweredOn += Adapter_PoweredOn;
                adapter.SetPoweredAsync(true);
            }
        }

        private async Task Adapter_PoweredOn(Adapter adapter, BlueZEventArgs eventArgs)
        {
            await adapter.SetAliasAsync("Invest");
            await adapter.SetDiscoverableAsync(true);
        }
    }
}
