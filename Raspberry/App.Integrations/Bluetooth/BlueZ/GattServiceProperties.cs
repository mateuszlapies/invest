using Tmds.DBus;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ
{
    [Dictionary]
    public class GattServiceProperties
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
                _UUID = value;
            }
        }

        private ObjectPath _Device = default;
        public ObjectPath Device
        {
            get
            {
                return _Device;
            }

            set
            {
                _Device = (value);
            }
        }

        private bool _Primary = default;
        public bool Primary
        {
            get
            {
                return _Primary;
            }

            set
            {
                _Primary = value;
            }
        }

        private ObjectPath[]? _Includes = default;
        public ObjectPath[]? Includes
        {
            get
            {
                return _Includes;
            }

            set
            {
                _Includes = value;
            }
        }
    }
}
