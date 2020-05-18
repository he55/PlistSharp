using System;

namespace PlistSharp
{
    public readonly struct plist_dict_iter
    {
        private readonly IntPtr _value;

        public plist_dict_iter(IntPtr value)
        {
            _value = value;
        }

        public override string ToString() => _value.ToString();

        public static implicit operator IntPtr(plist_dict_iter p) => p._value;

        public static explicit operator plist_dict_iter(IntPtr value) => new plist_dict_iter(value);
    }
}
