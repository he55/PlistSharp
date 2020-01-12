using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the nth item in a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <param name="n">the index of the item to get. Range is [0, array_size[</param>
        /// <returns>the nth item or NULL if node is not of type #PLIST_ARRAY</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_array_get_item(plist_t node, uint n);
    }
}
