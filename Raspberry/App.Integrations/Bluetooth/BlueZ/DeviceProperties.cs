using System.Runtime.CompilerServices;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Connection.DynamicAssemblyName)]
namespace Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces
{
    [Dictionary]
    public class DeviceProperties
    {
        private string? _Address = default;
        public string? Address
        {
            get
            {
                return _Address;
            }

            set
            {
                _Address = value;
            }
        }

        private string? _AddressType = default;
        public string? AddressType
        {
            get
            {
                return _AddressType;
            }

            set
            {
                _AddressType = (value);
            }
        }

        private string? _Name = default;
        public string? Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
            }
        }

        private string? _Alias = default;
        public string? Alias
        {
            get
            {
                return _Alias;
            }

            set
            {
                _Alias = value;
            }
        }

        private uint _Class = default;
        public uint Class
        {
            get
            {
                return _Class;
            }

            set
            {
                _Class = value;
            }
        }

        private ushort _Appearance = default;
        public ushort Appearance
        {
            get
            {
                return _Appearance;
            }

            set
            {
                _Appearance = value;
            }
        }

        private string? _Icon = default;
        public string? Icon
        {
            get
            {
                return _Icon;
            }

            set
            {
                _Icon = value;
            }
        }

        private bool _Paired = default;
        public bool Paired
        {
            get
            {
                return _Paired;
            }

            set
            {
                _Paired = value;
            }
        }

        private bool _Trusted = default;
        public bool Trusted
        {
            get
            {
                return _Trusted;
            }

            set
            {
                _Trusted = value;
            }
        }

        private bool _Blocked = default;
        public bool Blocked
        {
            get
            {
                return _Blocked;
            }

            set
            {
                _Blocked = value;
            }
        }

        private bool _LegacyPairing = default;
        public bool LegacyPairing
        {
            get
            {
                return _LegacyPairing;
            }

            set
            {
                _LegacyPairing = value;
            }
        }

        private short _RSSI = default;
        public short RSSI
        {
            get
            {
                return _RSSI;
            }

            set
            {
                _RSSI = value;
            }
        }

        private bool _Connected = default;
        public bool Connected
        {
            get
            {
                return _Connected;
            }

            set
            {
                _Connected = value;
            }
        }

        private string[]? _UUIDs = default;
        public string[]? UUIDs
        {
            get
            {
                return _UUIDs;
            }

            set
            {
                _UUIDs = value;
            }
        }

        private string? _Modalias = default;
        public string? Modalias
        {
            get
            {
                return _Modalias;
            }

            set
            {
                _Modalias = value;
            }
        }

        private ObjectPath _Adapter = default;
        public ObjectPath Adapter
        {
            get
            {
                return _Adapter;
            }

            set
            {
                _Adapter = value;
            }
        }

        private IDictionary<ushort, object>? _ManufacturerData = default;
        public IDictionary<ushort, object>? ManufacturerData
        {
            get
            {
                return _ManufacturerData;
            }

            set
            {
                _ManufacturerData = (value);
            }
        }

        private IDictionary<string, object>? _ServiceData = default;
        public IDictionary<string, object>? ServiceData
        {
            get
            {
                return _ServiceData;
            }

            set
            {
                _ServiceData = value;
            }
        }

        private short _TxPower = default;
        public short TxPower
        {
            get
            {
                return _TxPower;
            }

            set
            {
                _TxPower = value;
            }
        }

        private bool _ServicesResolved = default;
        public bool ServicesResolved
        {
            get
            {
                return _ServicesResolved;
            }

            set
            {
                _ServicesResolved = value;
            }
        }
    }
}