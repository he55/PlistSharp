using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Export the #plist_t structure to XML format.
        /// </summary>
        /// <param name="plist">the root node to export</param>
        /// <param name="plist_xml">
        /// a pointer to a C-string. This function allocates the memory,
        /// caller is responsible for freeing it. Data is UTF-8 encoded.
        /// </param>
        /// <param name="length">a pointer to an uint32_t variable. Represents the length of the allocated buffer.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_to_xml(plist_t plist, /* char** */ out IntPtr plist_xml, out uint length);
    }
}
