using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get key node associated to an item. Item must be member of a dictionary.
        /// </summary>
        /// <param name="node">the item</param>
        /// <returns>the key node of the given item, or NULL.</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_dict_item_get_key(plist_t node);
    }
}
