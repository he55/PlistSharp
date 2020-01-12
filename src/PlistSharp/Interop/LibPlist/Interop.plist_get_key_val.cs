using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the value of a #PLIST_KEY node.
        /// This function does nothing if node is not of type #PLIST_KEY
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// a pointer to a C-string. This function allocates the memory,
        /// caller is responsible for freeing it.
        /// </param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_get_key_val(plist_t node, /* char** */ out IntPtr val);
    }
}
