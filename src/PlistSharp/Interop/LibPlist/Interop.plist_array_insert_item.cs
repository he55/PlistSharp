using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Insert a new item at position n in a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="item">the new item to insert. The array is responsible for freeing item when it is no longer needed.</param>
        /// <param name="n">The position at which the node will be stored. Range is [0, array_size[. Assert if n is not in range.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_array_insert_item(plist_t node, plist_t item, uint n);
    }
}
