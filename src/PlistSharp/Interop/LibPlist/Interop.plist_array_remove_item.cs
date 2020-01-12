using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Remove an existing position in a #PLIST_ARRAY node.
        /// Removed position will be freed using #plist_free.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="n">The position to remove. Range is [0, array_size[. Assert if n is not in range.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_array_remove_item(plist_t node, uint n);
    }
}
