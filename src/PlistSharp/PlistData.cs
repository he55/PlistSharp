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

        public PlistData(byte[] buffer)
        {
            CreatePlistNode(plist_type.PLIST_DATA);

            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            LibPlist.plist_set_data_val(_node, ptr, (ulong)buffer.Length);

            handle.Free();
        }

        public override PlistNode Clone()
        {
            PlistData plistData = new PlistData();

            byte[] buffer = GetValue();
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

            IntPtr ptr = handle.AddrOfPinnedObject();
            LibPlist.plist_set_data_val(plistData._node, ptr, (ulong)buffer.Length);
            handle.Free();

            return plistData;
        }

        public void SetValue(byte[] buffer)
        {
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            LibPlist.plist_set_data_val(_node, ptr, (ulong)buffer.Length);

            handle.Free();
        }

        public byte[] GetValue()
        {
            LibPlist.plist_get_data_val(_node, out IntPtr ptr, out ulong length);

            byte[] buffer = new byte[length];
            Marshal.Copy(ptr, buffer, 0, (int)length);
            Marshal.FreeHGlobal(ptr);

            return buffer;
        }
    }
}
