using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistKey : PlistNode
    {
        public PlistKey(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_KEY, parent);
        }

        public PlistKey(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistKey(string key)
        {
            CreatePlistNode(plist_type.PLIST_KEY);
            LibPlist.plist_set_key_val(_node, key);
        }

        public override PlistNode Clone()
        {
            PlistKey plistKey = new PlistKey();
            LibPlist.plist_set_key_val(plistKey._node, GetValue());

            return plistKey;
        }

        public void SetValue(string key)
        {
            LibPlist.plist_set_key_val(_node, key);
        }

        public string GetValue()
        {
            LibPlist.plist_get_key_val(_node, out IntPtr ptr);
            string key = Marshal.PtrToStringUTF8(ptr);
            Marshal.FreeHGlobal(ptr);

            return key;
        }
    }
}
