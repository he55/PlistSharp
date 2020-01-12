using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Remove an existing position in a #PLIST_DICT node.
        /// Removed position will be freed using #plist_free
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">The identifier of the item to remove. Assert if identifier is not present.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_dict_remove_item(plist_t node, /* const char* */ string key);

        /// <summary>
        /// Remove an existing position in a #PLIST_DICT node.
        /// Removed position will be freed using #plist_free
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">The identifier of the item to remove. Assert if identifier is not present.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_dict_remove_item(plist_t node, IntPtr key);
    }
}
