using System;

namespace PlistSharp
{
    public readonly struct plist_t
    {
        private readonly IntPtr _value;

        public plist_t(IntPtr value)
        {
            _value = value;
        }

        public override string ToString() => _value.ToString();

        public static implicit operator IntPtr(plist_t p) => p._value;

        public static explicit operator plist_t(IntPtr value) => new plist_t(value);
    }
}
