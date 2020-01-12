using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Increment iterator of a #PLIST_DICT node.
        /// </summary>
        /// <param name="node">The node of type #PLIST_DICT</param>
        /// <param name="iter">Iterator of the dictionary</param>
        /// <param name="key">
        /// Location to store the key, or NULL. The caller is responsible
        /// for freeing the the returned string.
        /// </param>
        /// <param name="val">
        /// Location to store the value, or NULL. The caller must *not*
        /// free the returned value. Will be set to NULL when no more
        /// key/value pairs are left to iterate.
        /// </param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_dict_next_item(plist_t node, plist_dict_iter iter, /* char** */ out IntPtr key, out plist_t val);
    }
}
