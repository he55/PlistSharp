using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Export the #plist_t structure to binary format.
        /// </summary>
        /// <param name="plist">the root node to export</param>
        /// <param name="plist_bin">
        /// a pointer to a char* buffer. This function allocates the memory,
        /// caller is responsible for freeing it.
        /// </param>
        /// <param name="length">a pointer to an uint32_t variable. Represents the length of the allocated buffer.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_to_bin(plist_t plist, /* char** */ out IntPtr plist_bin, out uint length);
    }
}
