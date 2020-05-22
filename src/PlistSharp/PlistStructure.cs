using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PlistSharp
{
    public class PlistStructure : PlistNode
    {
        protected PlistStructure()
        {
        }

        protected PlistStructure(plist_type type, PlistNode? parent = null)
            : base(type, parent)
        {
        }

        public bool IsBinary { get; private set; }

        public uint GetSize()
        {
            uint size = 0;
            plist_type type = LibPlist.plist_get_node_type(_node);
            if (type == plist_type.PLIST_ARRAY)
            {
                size = LibPlist.plist_array_get_size(_node);
            }
            else if (type == plist_type.PLIST_DICT)
            {
                size = LibPlist.plist_dict_get_size(_node);
            }
            return size;
        }

        public string ToXml()
        {
            LibPlist.plist_to_xml(_node, out IntPtr xml, out uint length);

            string ret = Marshal.PtrToStringUTF8(xml, (int)length);
            Marshal.FreeHGlobal(xml);
            return ret;
        }

        public byte[] ToBin()
        {
            LibPlist.plist_to_bin(_node, out IntPtr bin, out uint length);

            byte[] ret = new byte[length];
            Marshal.Copy(bin, ret, 0, (int)length);
            Marshal.FreeHGlobal(bin);
            return ret;
        }

        public virtual void Remove(PlistNode node)
        {
            throw new NotImplementedException();
        }

        protected void UpdateNodeParent(PlistNode node)
        {
            //Unlink node first
            PlistNode? parent = node._parent;
            if (parent != null)
            {
                plist_type type = LibPlist.plist_get_node_type(parent._node);
                if (type == plist_type.PLIST_ARRAY || type == plist_type.PLIST_DICT)
                {
                    PlistStructure s = (PlistStructure)parent;
                    s.Remove(node);
                }
            }
            node._parent = this;
        }

        private static PlistStructure? ImportStruct(plist_t root)
        {
            PlistStructure? ret = null;
            plist_type type = LibPlist.plist_get_node_type(root);

            if (type == plist_type.PLIST_ARRAY || type == plist_type.PLIST_DICT)
            {
                ret = (PlistStructure?)FromPlist(root);
            }
            else
            {
                LibPlist.plist_free(root);
            }
            return ret;
        }

        public static PlistStructure? FromXml(string xml)
        {
            uint length = (uint)Encoding.UTF8.GetByteCount(xml);
            LibPlist.plist_from_xml(xml, length, out plist_t root);
            return ImportStruct(root);
        }

        private static PlistStructure? FromBin(byte[] bin)
        {
            uint length = (uint)bin.Length;

            GCHandle pinned = GCHandle.Alloc(bin, GCHandleType.Pinned);
            IntPtr data = pinned.AddrOfPinnedObject();

            LibPlist.plist_from_bin(data, length, out plist_t root);
            PlistStructure? structure = ImportStruct(root);
            if (structure != null)
            {
                structure.IsBinary = LibPlist.plist_is_binary(data, length) != 0;
            }
            pinned.Free();

            return structure;
        }

        public static PlistStructure? FromFile(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open);
            return FromFile(fileStream);
        }

        public static PlistStructure? FromFile(Stream stream)
        {
            using MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            byte[] bin = memoryStream.ToArray();
            return FromFile(bin);
        }

        public static unsafe PlistStructure? FromFile(ReadOnlySpan<byte> buffer)
        {
            uint length = (uint)buffer.Length;

            fixed (byte* p = buffer)
            {
                LibPlist.plist_from_memory(p, length, out plist_t root);
                PlistStructure? structure = ImportStruct(root);
                if (structure != null)
                {
                    structure.IsBinary = LibPlist.plist_is_binary(p, length) != 0;
                }

                return structure;
            }
        }
    }
}
