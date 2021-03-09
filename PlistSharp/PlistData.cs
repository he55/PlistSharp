using System;
using System.Runtime.InteropServices;

namespace PlistSharp
{
    public class PlistData : PlistNode
    {
        public PlistData(byte[] buffer, PlistStructure? parent = null)
        {
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();

            _node = plist.plist_new_data(ptr, (ulong)buffer.Length);
            _parent = parent;

            handle.Free();
        }

        public PlistData(plist_t node, PlistStructure? parent = null)
        {
            _node = node;
            _parent = parent;
        }

        public override PlistNode Copy() => new PlistData(Value);

        public byte[] Value
        {
            get
            {
                plist.plist_get_data_val(_node, out IntPtr ptr, out ulong length);
                byte[] buffer = new byte[length];

                Marshal.Copy(ptr, buffer, 0, (int)length);
                Marshal.FreeHGlobal(ptr);

                return buffer;
            }

            set
            {
                GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);

                IntPtr ptr = handle.AddrOfPinnedObject();
                plist.plist_set_data_val(_node, ptr, (ulong)value.Length);

                handle.Free();
            }
        }
    }
}
