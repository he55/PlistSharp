using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistData : PlistNode
    {
        public PlistData(PlistStructure? parent = null)
        {
            CreatePlistNode(plist_type.PLIST_DATA, parent);
        }

        public PlistData(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public PlistData(PlistData d)
        {
            CreatePlistNode(plist_type.PLIST_DATA);

            byte[] b = d.GetValue();
            GCHandle gcHandle = GCHandle.Alloc(b, GCHandleType.Pinned);

            IntPtr p = gcHandle.AddrOfPinnedObject();
            LibPlist.plist_set_data_val(_node, p, (ulong)b.Length);

            gcHandle.Free();
        }

        public PlistData(byte[] buff)
        {
            CreatePlistNode(plist_type.PLIST_DATA);

            GCHandle gcHandle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            IntPtr p = gcHandle.AddrOfPinnedObject();

            LibPlist.plist_set_data_val(_node, p, (ulong)buff.Length);

            gcHandle.Free();
        }

        public override PlistNode Clone()
        {
            return new PlistData(this);
        }

        public void SetValue(byte[] buff)
        {
            GCHandle gcHandle = GCHandle.Alloc(buff, GCHandleType.Pinned);

            IntPtr p = gcHandle.AddrOfPinnedObject();
            LibPlist.plist_set_data_val(_node, p, (ulong)buff.Length);

            gcHandle.Free();
        }

        public byte[] GetValue()
        {
            LibPlist.plist_get_data_val(_node, out IntPtr buff, out ulong length);

            byte[] ret = new byte[length];
            Marshal.Copy(buff, ret, 0, (int)length);
            Marshal.FreeHGlobal(buff);

            return ret;
        }
    }
}
