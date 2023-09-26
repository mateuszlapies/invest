using System.Runtime.CompilerServices;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Connection.DynamicAssemblyName)]
namespace Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces
{
    [DBusInterface("org.freedesktop.DBus.ObjectManager")]
    public interface IDeviceManager : IDBusObject
    {
        Task<IDictionary<ObjectPath, IDictionary<string, IDictionary<string, object>>>> GetManagedObjectsAsync();
        Task<IDisposable> WatchInterfacesAddedAsync(Action<(ObjectPath device, IDictionary<string, IDictionary<string, object>> interfaces)> handler, Action<Exception>? onError = null);
        Task<IDisposable> WatchInterfacesRemovedAsync(Action<(ObjectPath device, string[] interfaces)> handler, Action<Exception>? onError = null);
    }
}