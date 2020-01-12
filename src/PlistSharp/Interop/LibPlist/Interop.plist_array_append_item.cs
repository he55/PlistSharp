using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Append a new item at the end of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="item">the new item. The array is responsible for freeing item when it is no longer needed.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_array_append_item(plist_t node, plist_t item);
    }
}
