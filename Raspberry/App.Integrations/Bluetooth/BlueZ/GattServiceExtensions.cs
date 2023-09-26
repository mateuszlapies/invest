using Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces;
using Tmds.DBus;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ
{
    public static class GattServiceExtensions
    {
        public static Task<string> GetUUIDAsync(this IGattService o) => o.GetAsync<string>("UUID");
        public static Task<IDevice> GetDeviceAsync(this IGattService o) => o.GetAsync<IDevice>("Device");
        public static Task<bool> GetPrimaryAsync(this IGattService o) => o.GetAsync<bool>("Primary");
        public static Task<ObjectPath[]> GetIncludesAsync(this IGattService o) => o.GetAsync<ObjectPath[]>("Includes");
        public static Task SetUUIDAsync(this IGattService o, string uuid) => o.SetAsync("UUID", uuid);
        public static Task SetPrimaryAsync(this IGattService o, bool primary) => o.SetAsync("Primary", primary);
    }
}
