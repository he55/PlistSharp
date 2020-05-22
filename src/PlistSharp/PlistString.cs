using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistString : PlistNode
    {
        public PlistString(PlistStructure? parent = null)
            : base(plist_type.PLIST_STRING, parent)
        {
        }

        public PlistString(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistString(PlistString s)
            : base(plist_type.PLIST_STRING)
        {
            LibPlist.plist_set_string_val(_node, s.GetValue());
        }

        public PlistString(string s)
            : base(plist_type.PLIST_STRING)
        {
            LibPlist.plist_set_string_val(_node, s);
        }

        public override PlistNode Clone()
        {
            return new PlistString(this);
        }

        public void SetValue(string s)
        {
            LibPlist.plist_set_string_val(_node, s);
        }

        public string GetValue()
        {
            LibPlist.plist_get_string_val(_node, out IntPtr s);
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
