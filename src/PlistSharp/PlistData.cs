using System;
using System.Runtime.InteropServices;
using static Interop.LibPlist;

namespace PlistSharp
{
    public class PlistData : PlistNode
    {
        public PlistData(PlistNode? parent = null)
            : base(plist_type.PLIST_DATA, parent)
        {
        }

        public PlistData(plist_t node, PlistNode? parent = null)
            : base(node, parent)
        {
        }

        public PlistData(PlistData d)
            : base(plist_type.PLIST_DATA)
        {
            byte[] b = d.GetValue();
            GCHandle gcHandle = GCHandle.Alloc(b, GCHandleType.Pinned);
            IntPtr p = gcHandle.AddrOfPinnedObject();

            plist_set_data_val(_node, p, (ulong)b.Length);
            gcHandle.Free();
        }

        public PlistData(byte[] buff)
            : base(plist_type.PLIST_DATA)
        {
            GCHandle gcHandle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            IntPtr p = gcHandle.AddrOfPinnedObject();

            plist_set_data_val(_node, p, (ulong)buff.Length);
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

            plist_set_data_val(_node, p, (ulong)buff.Length);
            gcHandle.Free();
        }

        public byte[] GetValue()
        {
            plist_get_data_val(_node, out IntPtr buff, out ulong length);

            byte[] ret = new byte[length];
            Marshal.Copy(buff, ret, 0, (int)length);
            Marshal.FreeHGlobal(buff);
            return ret;
        }
    }
}
