using System;
using System.Runtime.InteropServices;
using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistString : PlistNode
    {
        public PlistString(PlistNode? parent = null)
            : base(plist_type.PLIST_STRING, parent)
        {
        }

        public PlistString(plist_t node, PlistNode? parent = null)
            : base(node, parent)
        {
        }

        public PlistString(PlistString s)
            : base(plist_type.PLIST_STRING)
        {
            plist_set_string_val(_node, s.GetValue());
        }

        public PlistString(string s)
            : base(plist_type.PLIST_STRING)
        {
            plist_set_string_val(_node, s);
        }

        public override PlistNode Clone()
        {
            return new PlistString(this);
        }

        public void SetValue(string s)
        {
            plist_set_string_val(_node, s);
        }

        public string GetValue()
        {
            plist_get_string_val(_node, out IntPtr s);
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
