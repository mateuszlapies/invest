using Tmds.DBus;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces
{
    [DBusInterface("org.bluez.GattService1")]
    public interface IGattService : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<GattServiceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }
}
