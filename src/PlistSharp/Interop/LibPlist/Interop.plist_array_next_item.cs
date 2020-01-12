using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Increment iterator of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">The node of type #PLIST_ARRAY.</param>
        /// <param name="iter">Iterator of the array</param>
        /// <param name="item">
        /// Location to store the item. The caller must *not* free the
        /// returned item. Will be set to NULL when no more items are left
        /// to iterate.
        /// </param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_array_next_item(plist_t node, plist_array_iter iter, out plist_t item);
    }
}
