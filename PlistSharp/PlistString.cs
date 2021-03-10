using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistString : PlistNode
    {
        public PlistString(string value, PlistStructure? parent = null)
        {
            _node = plist.plist_new_string(value);
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
                plist.plist_get_string_val(_node, out IntPtr ptr);
                string? value = Marshal.PtrToStringUTF8(ptr);
                if (value == null)
                {
                    throw new NullReferenceException();
                }

                Marshal.FreeHGlobal(ptr);
                return value;
            }
            set => plist.plist_set_string_val(_node, value);
        }
    }
}
