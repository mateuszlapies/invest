using Tmds.DBus;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ
{
    [Dictionary]
    public class GattDescriptorProperties
    {
        private string? _UUID = default;
        public string? UUID
        {
            get
            {
                return _UUID;
            }

            set
            {
                _UUID = (value);
            }
        }

        private ObjectPath _Characteristic = default;
        public ObjectPath Characteristic
        {
            get
            {
                return _Characteristic;
            }

            set
            {
                _Characteristic = (value);
            }
        }

        private byte[]? _Value = default;
        public byte[]? Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = (value);
            }
        }
    }
}
