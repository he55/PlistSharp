using System;

namespace PlistSharp
{
    public readonly struct plist_array_iter
    {
        private readonly IntPtr _value;

        public plist_array_iter(IntPtr value)
        {
            _value = value;
        }

        public override string ToString() => _value.ToString();

        public static implicit operator IntPtr(plist_array_iter p) => p._value;

        public static explicit operator plist_array_iter(IntPtr value) => new plist_array_iter(value);
    }
}
