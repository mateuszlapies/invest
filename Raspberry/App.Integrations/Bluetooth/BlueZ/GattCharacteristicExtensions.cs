using Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ
{
    public static class GattCharacteristicExtensions
    {
        public static Task<string> GetUUIDAsync(this IGattCharacteristic o) => o.GetAsync<string>("UUID");
        public static Task<IGattService> GetServiceAsync(this IGattCharacteristic o) => o.GetAsync<IGattService>("Service");
        public static Task<byte[]> GetValueAsync(this IGattCharacteristic o) => o.GetAsync<byte[]>("Value");
        public static Task<bool> GetNotifyingAsync(this IGattCharacteristic o) => o.GetAsync<bool>("Notifying");
        public static Task<string[]> GetFlagsAsync(this IGattCharacteristic o) => o.GetAsync<string[]>("Flags");
        public static Task<bool> GetWriteAcquiredAsync(this IGattCharacteristic o) => o.GetAsync<bool>("WriteAcquired");
        public static Task<bool> GetNotifyAcquiredAsync(this IGattCharacteristic o) => o.GetAsync<bool>("NotifyAcquired");
    }
}
