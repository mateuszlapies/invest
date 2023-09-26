namespace Raspberry.App.Integrations.Bluetooth.Interfaces
{
    public interface IBluetoothAdapter
    {
        Task Initialize();
        Task<string> GetAddress();
        Task PowerOff();
    }
}