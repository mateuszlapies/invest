namespace Raspberry.App.Integrations.Bluetooth.Interfaces
{
    public interface IBluetoothIntegration
    {
        void InitializeAdapter();
        Task<string> GetAdapterAddress();
    }
}
