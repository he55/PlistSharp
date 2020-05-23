using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistString : PlistNode
    {
        public PlistString(string value, PlistStructure? parent = null)
        {
            _node = LibPlist.plist_new_string(value);
            _parent = parent;
        }

        public PlistString(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistString(Value);

        public string Value
        {
            get
            {
                LibPlist.plist_get_string_val(_node, out IntPtr ptr);
                string value = Marshal.PtrToStringUTF8(ptr);

                Marshal.FreeHGlobal(ptr);

                return value;
            }

            set => LibPlist.plist_set_string_val(_node, value);
        }
    }
}
