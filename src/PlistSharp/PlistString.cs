using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistString : PlistNode
    {
        public PlistString(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_STRING, parent);
        }

        public PlistString(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistString(string value)
        {
            CreatePlistNode(plist_type.PLIST_STRING);
            LibPlist.plist_set_string_val(_node, value);
        }

        public override PlistNode Copy()
        {
            PlistString plistString = new PlistString();
            LibPlist.plist_set_string_val(plistString._node, GetValue());

            return plistString;
        }

        public void SetValue(string value)
        {
            LibPlist.plist_set_string_val(_node, value);
        }

        public string GetValue()
        {
            LibPlist.plist_get_string_val(_node, out IntPtr ptr);
            string value = Marshal.PtrToStringUTF8(ptr);
            Marshal.FreeHGlobal(ptr);

            return value;
        }
    }
}
