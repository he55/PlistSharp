using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the index of an item. item must be a member of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node</param>
        /// <returns>the node index or UINT_MAX if node index can't be determined</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern uint plist_array_get_item_index(plist_t node);
    }
}
