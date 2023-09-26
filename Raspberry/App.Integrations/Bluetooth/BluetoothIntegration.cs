using Microsoft.Extensions.Logging;
using Raspberry.App.Integrations.Bluetooth.Interfaces;

namespace Raspberry.App.Integrations.Bluetooth
{
    public class BluetoothIntegration : IBluetoothIntegration
    {
        private readonly ILogger logger;
        private readonly IBluetoothAdapter adapter;

        public BluetoothIntegration(ILogger logger)
        {
            this.logger = logger;
            adapter = new BluetoothAdapter(this.logger);
        }

        public void InitializeAdapter()
        {
            adapter.Initialize();
        }

        public async Task<string> GetAdapterAddress()
        {
            return await adapter.GetAddress();
        }
    }
}
