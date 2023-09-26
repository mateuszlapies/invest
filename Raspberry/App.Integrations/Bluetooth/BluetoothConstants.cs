namespace Raspberry.App.Integrations.Bluetooth
{
    public static class BluetoothConstants
    {
        public static readonly string BlueZServiceName = "org.bluez";
        public static readonly string DBusServiceName = "org.freedesktop.DBus";
        public static readonly string BlueZObjectPath = "/org/bluez";
        public static readonly string DBusObjectPath = "/";
        public static readonly string DefaultAdapterObjectPath = "/org/bluez/hci0";
    }
}