using Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ
{
    public static class GattDescriptorExtensions
    {
        public static Task<string> GetUUIDAsync(this IGattDescriptor o) => o.GetAsync<string>("UUID");
        public static Task<IGattCharacteristic> GetCharacteristicAsync(this IGattDescriptor o) => o.GetAsync<IGattCharacteristic>("Characteristic");
        public static Task<byte[]> GetValueAsync(this IGattDescriptor o) => o.GetAsync<byte[]>("Value");
    }
}
