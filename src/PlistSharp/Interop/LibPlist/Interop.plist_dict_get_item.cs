using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the nth item in a #PLIST_DICT node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <param name="key">the identifier of the item to get.</param>
        /// <returns>
        /// the item or NULL if node is not of type #PLIST_DICT. The caller should not free
        /// the returned node.
        /// </returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_dict_get_item(plist_t node, /* const char* */ string key);
    }
}
