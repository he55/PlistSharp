using System;
using System.Runtime.InteropServices;
using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistKey : PlistNode
    {
        public PlistKey(PlistNode? parent = null)
            : base(plist_type.PLIST_KEY, parent)
        {
        }

        public PlistKey(plist_t node, PlistNode? parent = null)
            : base(node, parent)
        {
        }

        public PlistKey(PlistKey k)
            : base(plist_type.PLIST_KEY)
        {
            plist_set_key_val(_node, k.GetValue());
        }

        public PlistKey(string s)
            : base(plist_type.PLIST_KEY)
        {
            plist_set_key_val(_node, s);
        }

        public override PlistNode Clone()
        {
            return new PlistKey(this);
        }

        public void SetValue(string s)
        {
            plist_set_key_val(_node, s);
        }

        public string GetValue()
        {
            plist_get_key_val(_node, out IntPtr s);
            if (s == IntPtr.Zero)
            {
                return string.Empty;
            }

            string ret = Marshal.PtrToStringUTF8(s);
            Marshal.FreeHGlobal(s);
            return ret;
        }
    }
}
