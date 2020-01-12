using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Remove a node that is a child node of a #PLIST_ARRAY node.
        /// node will be freed using #plist_free.
        /// </summary>
        /// <param name="node">The node to be removed from its #PLIST_ARRAY parent.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_array_item_remove(plist_t node);
    }
}
