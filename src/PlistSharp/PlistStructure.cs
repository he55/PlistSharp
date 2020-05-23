using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PlistSharp
{
    public abstract class PlistStructure : PlistNode
    {
        public bool IsBinary { get; private set; }

        public uint Size => PlistType switch
        {
            plist_type.PLIST_ARRAY => LibPlist.plist_array_get_size(_node),
            plist_type.PLIST_DICT => LibPlist.plist_dict_get_size(_node),
            _ => throw new NotSupportedException()
        };

        public string ToPlistXml()
        {
            LibPlist.plist_to_xml(_node, out IntPtr ptr, out uint length);
            string xml = Marshal.PtrToStringUTF8(ptr, (int)length);

            Marshal.FreeHGlobal(ptr);

            return xml;
        }

        public byte[] ToPlistBin()
        {
            LibPlist.plist_to_bin(_node, out IntPtr ptr, out uint length);
            byte[] buffer = new byte[length];

            Marshal.Copy(ptr, buffer, 0, (int)length);
            Marshal.FreeHGlobal(ptr);

            return buffer;
        }

        public static PlistStructure FromPlistXml(string xml)
        {
            int length = Encoding.UTF8.GetByteCount(xml);
            LibPlist.plist_from_xml(xml, (uint)length, out plist_t root);

            return ImportStruct(root);
        }

        private static unsafe PlistStructure FromPlistBin(ReadOnlySpan<byte> bin)
        {
            uint length = (uint)bin.Length;

            fixed (byte* p = bin)
            {
                LibPlist.plist_from_bin((IntPtr)p, length, out plist_t root);
                PlistStructure structure = ImportStruct(root);
                structure.IsBinary = LibPlist.plist_is_binary((IntPtr)p, length) != 0;

                return structure;
            }
        }

        public static PlistStructure FromFile(string path)
        {
            using FileStream fileStream = new FileStream(path, FileMode.Open);
            return FromFile(fileStream);
        }

        public static PlistStructure FromFile(Stream stream)
        {
            using MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return FromFile(memoryStream.ToArray());
        }

        public static unsafe PlistStructure FromFile(ReadOnlySpan<byte> buffer)
        {
            uint length = (uint)buffer.Length;

            fixed (byte* p = buffer)
            {
                LibPlist.plist_from_memory(p, length, out plist_t root);
                PlistStructure structure = ImportStruct(root);
                structure.IsBinary = LibPlist.plist_is_binary(p, length) != 0;

                return structure;
            }
        }

        private static PlistStructure ImportStruct(plist_t root)
        {
            plist_type type = LibPlist.plist_get_node_type(root);

            switch (type)
            {
                case plist_type.PLIST_ARRAY:
                case plist_type.PLIST_DICT:
                    return (PlistStructure)FromPlist(root)!;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
