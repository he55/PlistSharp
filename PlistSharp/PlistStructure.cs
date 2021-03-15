using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PlistSharp
{
    public abstract class PlistStructure : PlistNode
    {
        protected abstract void Fill();

        public abstract int Count { get; }

        public bool IsBinary { get; private set; }

        public string ToPlistXml()
        {
            plist.plist_to_xml(_node, out IntPtr ptr, out uint length);

#if NET5_0_OR_GREATER
            string xml = Marshal.PtrToStringUTF8(ptr, (int)length);
            Marshal.FreeHGlobal(ptr);

            return xml;
#else
            unsafe
            {
                string xml = Encoding.UTF8.GetString((byte*)ptr, (int)length);
                Marshal.FreeHGlobal(ptr);

                return xml;
            }
#endif
        }

        public byte[] ToPlistBin()
        {
            plist.plist_to_bin(_node, out IntPtr ptr, out uint length);
            byte[] buffer = new byte[length];

            Marshal.Copy(ptr, buffer, 0, (int)length);
            Marshal.FreeHGlobal(ptr);

            return buffer;
        }

        public static PlistStructure FromPlistXml(string xml)
        {
            int length = Encoding.UTF8.GetByteCount(xml);
            plist.plist_from_xml(xml, (uint)length, out plist_t root);

            return ImportStruct(root);
        }

        public static PlistStructure FromPlistBin(byte[] bin)
        {
            uint length = (uint)bin.Length;

            unsafe
            {
                fixed (byte* p = bin)
                {
                    plist.plist_from_bin((IntPtr)p, length, out plist_t root);
                    PlistStructure structure = ImportStruct(root);
                    structure.IsBinary = plist.plist_is_binary((IntPtr)p, length) != 0;

                    return structure;
                }
            }
        }

        public static PlistStructure FromFile(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return FromFile(fileStream);
            }
        }

        public static unsafe PlistStructure FromFile(Stream stream)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);

                uint length = (uint)memoryStream.Length;

                fixed (byte* p = memoryStream.ToArray())
                {
                    plist.plist_from_memory(p, length, out plist_t root);
                    PlistStructure structure = ImportStruct(root);
                    structure.IsBinary = plist.plist_is_binary(p, length) != 0;

                    return structure;
                }
            }
        }

        private static PlistStructure ImportStruct(plist_t root)
        {
            plist_type type = plist.plist_get_node_type(root);

            switch (type)
            {
                case plist_type.PLIST_ARRAY:
                case plist_type.PLIST_DICT:
                    return (PlistStructure)FromPlist(root);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
