using Tmds.DBus;

namespace Raspberry.App.Integrations.Bluetooth.BlueZ
{
    [Dictionary]
    public class GattCharacteristicProperties
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

        private ObjectPath _Service = default;
        public ObjectPath Service
        {
            get
            {
                return _Service;
            }

            set
            {
                _Service = value;
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
                _Value = value;
            }
        }

        private bool _Notifying = default;
        public bool Notifying
        {
            get
            {
                return _Notifying;
            }

            set
            {
                _Notifying = value;
            }
        }

        private string[]? _Flags = default;
        public string[]? Flags
        {
            get
            {
                return _Flags;
            }

            set
            {
                _Flags = value;
            }
        }

        private bool _WriteAcquired = default;
        public bool WriteAcquired
        {
            get
            {
                return _WriteAcquired;
            }

            set
            {
                _WriteAcquired = value;
            }
        }

        private bool _NotifyAcquired = default;
        public bool NotifyAcquired
        {
            get
            {
                return _NotifyAcquired;
            }

            set
            {
                _NotifyAcquired = value;
            }
        }
    }
}
