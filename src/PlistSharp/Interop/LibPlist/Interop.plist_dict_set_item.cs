using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Set item identified by key in a #PLIST_DICT node.
        /// The previous item identified by key will be freed using #plist_free.
        /// If there is no item for the given key a new item will be inserted.
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">the identifier of the item to set.</param>
        /// <param name="item">the new item associated to key</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_dict_set_item(plist_t node, /* const char* */ string key, plist_t item);
    }
}
