using Tmds.DBus;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces
{
    [DBusInterface("org.bluez.GattDescriptor1")]
    public interface IGattDescriptor : IDBusObject
    {
        Task<byte[]> ReadValueAsync(IDictionary<string, object> Options);
        Task WriteValueAsync(byte[] Value, IDictionary<string, object> Options);
        Task<T> GetAsync<T>(string prop);
        Task<GattDescriptorProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }
}
