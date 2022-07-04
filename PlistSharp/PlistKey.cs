using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistKey : PlistNode
    {
        public PlistKey(string key, PlistStructure parent = null)
        {
            _node = plist.plist_new_string(key);
            _parent = parent;
        }

        public PlistKey(plist_t node, PlistStructure parent = null)
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

#if NETCOREAPP
                string value = Marshal.PtrToStringUTF8(ptr);
#else
                string value = StringHelper.PtrToStringUTF8(ptr);
#endif

                Marshal.FreeHGlobal(ptr);

                if (value == null)
                    throw new NullReferenceException();
                return value;
            }
            set => plist.plist_set_key_val(_node, value);
        }
    }
}
