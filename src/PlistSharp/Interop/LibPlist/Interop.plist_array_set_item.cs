using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Set the nth item in a #PLIST_ARRAY node.
        /// The previous item at index n will be freed using #plist_free
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="item">the new item at index n. The array is responsible for freeing item when it is no longer needed.</param>
        /// <param name="n">the index of the item to get. Range is [0, array_size[. Assert if n is not in range.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_array_set_item(plist_t node, plist_t item, uint n);
    }
}
