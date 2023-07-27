using HashtagChris.DotNetBlueZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Integrations
{
    public class BluetoothService
    {
        public BluetoothService()
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
            await adapter.
        }
    }
}
