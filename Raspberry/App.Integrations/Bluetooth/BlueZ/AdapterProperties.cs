using System.Runtime.CompilerServices;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Connection.DynamicAssemblyName)]
namespace Raspberry.App.Integrations.Bluetooth.BlueZ.Interfaces
{
    [Dictionary]
    public class AdapterProperties
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
                _Alias = (value);
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

        private bool _Powered = default;
        public bool Powered
        {
            get
            {
                return _Powered;
            }

            set
            {
                _Powered = value;
            }
        }

        private bool _Discoverable = default;
        public bool Discoverable
        {
            get
            {
                return _Discoverable;
            }

            set
            {
                _Discoverable = value;
            }
        }

        private uint _DiscoverableTimeout = default;
        public uint DiscoverableTimeout
        {
            get
            {
                return _DiscoverableTimeout;
            }

            set
            {
                _DiscoverableTimeout = value;
            }
        }

        private bool _Pairable = default;
        public bool Pairable
        {
            get
            {
                return _Pairable;
            }

            set
            {
                _Pairable = value;
            }
        }

        private uint _PairableTimeout = default;
        public uint PairableTimeout
        {
            get
            {
                return _PairableTimeout;
            }

            set
            {
                _PairableTimeout = value;
            }
        }

        private bool _Discovering = default;
        public bool Discovering
        {
            get
            {
                return _Discovering;
            }

            set
            {
                _Discovering = value;
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

        private string[]? _Roles = default;
        public string[]? Roles
        {
            get
            {
                return _Roles;
            }

            set
            {
                _Roles = value;
            }
        }

        private string[]? _ExperimentalFeatures = default;
        public string[]? ExperimentalFeatures
        {
            get
            {
                return _ExperimentalFeatures;
            }

            set
            {
                _ExperimentalFeatures = value;
            }
        }
    }
}