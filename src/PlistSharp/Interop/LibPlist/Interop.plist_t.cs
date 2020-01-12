using System;

public static partial class Interop
{
    public static partial class LibPlist
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
}
