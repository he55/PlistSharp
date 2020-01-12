using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the value of a #PLIST_DATA node.
        /// This function does nothing if node is not of type #PLIST_DATA
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// a pointer to an unallocated char buffer. This function allocates the memory,
        /// caller is responsible for freeing it.
        /// </param>
        /// <param name="length">the length of the buffer</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_get_data_val(plist_t node, /* char** */ out IntPtr val, out ulong length);
    }
}
