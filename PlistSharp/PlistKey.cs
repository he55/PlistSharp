using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistKey : PlistNode
    {
        public PlistKey(string key, PlistStructure? parent = null)
        {
            _node = plist.plist_new_string(key);
            _parent = parent;
        }

        public PlistKey(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistKey(Value);

        public string Value
        {
            get
            {
                plist.plist_get_key_val(_node, out IntPtr ptr);
                string? value = Marshal.PtrToStringUTF8(ptr);
                if (value == null)
                {
                    throw new NullReferenceException();
                }

                Marshal.FreeHGlobal(ptr);
                return value;
            }

            set => plist.plist_set_key_val(_node, value);
        }
    }
}
