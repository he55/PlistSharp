using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get size of a #PLIST_ARRAY node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_ARRAY</param>
        /// <returns>size of the #PLIST_ARRAY node</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern uint plist_array_get_size(plist_t node);
    }
}
