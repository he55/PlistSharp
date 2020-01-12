using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get key associated key to an item. Item must be member of a dictionary.
        /// </summary>
        /// <param name="node">the item</param>
        /// <param name="key">a location to store the key. The caller is responsible for freeing the returned string.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_dict_get_item_key(plist_t node, /* char** */ out IntPtr key);
    }
}
