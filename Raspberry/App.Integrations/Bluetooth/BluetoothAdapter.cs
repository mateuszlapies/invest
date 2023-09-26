using Microsoft.Extensions.Logging;
using Raspberry.App.Integrations.Bluetooth.BlueZ;
using Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces;
using Raspberry.App.Integrations.Bluetooth.Interfaces;
using Tmds.DBus;

namespace Raspberry.App.Integrations.Bluetooth
{
    internal class BluetoothAdapter : IBluetoothAdapter
    {
        private readonly ILogger logger;
        private readonly Connection connection;
        private readonly IAdapter adapter;
        private readonly IAgentManager agentManager;
        private readonly IDeviceManager objectManager;
        private readonly IGattService gattService;
        private readonly IGattDescriptor gattDescriptor;
        private readonly IGattCharacteristic gattCharacteristic;

        public BluetoothAdapter(ILogger logger)
        {
            this.logger = logger;
            connection = Connection.System;
            adapter = connection.CreateProxy<IAdapter>(BluetoothConstants.BlueZServiceName, BluetoothConstants.DefaultAdapterObjectPath);
            agentManager = connection.CreateProxy<IAgentManager>(BluetoothConstants.BlueZServiceName, BluetoothConstants.BlueZObjectPath);
            objectManager = connection.CreateProxy<IDeviceManager>(BluetoothConstants.DBusServiceName, BluetoothConstants.DBusObjectPath);
            gattService = connection.CreateProxy<IGattService>(BluetoothConstants.DBusServiceName, "/invest/hci0/");
            gattDescriptor = connection.CreateProxy<IGattDescriptor>(BluetoothConstants.DBusServiceName, "/invest/hci0/");
            gattCharacteristic = connection.CreateProxy<IGattCharacteristic>(BluetoothConstants.DBusServiceName, "/invest/hci0/");
        }

        public async Task<string> GetName()
        {
            var address = await GetAddress();
            var addressPart = string.Concat(address.Split(":").TakeLast(2));
            return string.Format("Invest-{0}", addressPart);
        }

        public async Task<string> GetAddress()
        {
            return await adapter.GetAddressAsync();
        }

        public async Task Initialize()
        {
            logger.LogInformation("Initializing Bluetooth...");
            await Restart();
            await ConfigureAgentManager();
            await ConfigureObjectManager();
            await ConfigureAdapter();
            logger.LogInformation("Bluetooth initialized");
        }

        async Task ConfigureAgentManager()
        {
            logger.LogDebug("Configuring AgentManager...");
            await agentManager.RegisterAgentAsync(BluetoothConstants.BlueZServiceName, "NoInputNoOutput");
            logger.LogDebug("AgentManger configured");
        }

        async Task ConfigureObjectManager()
        {
            logger.LogDebug("Configuring ObjectManager...");
            await objectManager.WatchInterfacesAddedAsync(OnDeviceAdded);
            logger.LogDebug("ObjectManager configured");
        }

        async Task ConfigureGattManager()
        {
            logger.LogDebug("Configure GattManager");
            await gattService.SetUUIDAsync("a1b7ca33-16f1-4ce6-b80c-8bdb0aaa7270");
            await gattService.SetPrimaryAsync(true);
            logger.LogDebug("GatManager configured");
        }

        async Task ConfigureAdapter()
        {
            logger.LogDebug("Configuring Adapter...");
            var name = await GetName();            
            await adapter.SetAliasAsync(name);
            await adapter.SetDiscoverableAsync(true);
            await adapter.SetPairableAsync(true);
            logger.LogDebug("Adapter configured");
        }

        public async Task PowerOff()
        {
            logger.LogDebug("Powering off...");
            var powered = await adapter.GetPoweredAsync();
            if (powered)
            {
                await adapter.SetPoweredAsync(false);
                logger.LogDebug("Powered off");
            } else {
                logger.LogDebug("Already powered off");
            }
        }

        public async Task PowerOn()
        {
            logger.LogDebug("Powering on...");
            var powered = await adapter.GetPoweredAsync();
            if (!powered)
            {
                await adapter.SetPoweredAsync(true);
                logger.LogDebug("Powered on");
            } else {
                logger.LogDebug("Already powered on");
            }
        }

        public async Task Restart()
        {
            logger.LogDebug("Restarting Bluetooth...");
            await PowerOff();
            await PowerOn();
            logger.LogDebug("Bluetooth restarted");
        }

        private void OnDeviceAdded((ObjectPath device, IDictionary<string, IDictionary<string, object>> interfaces) args)
        {
            
        }
    }
}