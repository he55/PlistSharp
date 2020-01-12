using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Create an iterator of a #PLIST_DICT node.
        /// The allocated iterator should be freed with the standard free function.
        /// </summary>
        /// <param name="node">The node of type #PLIST_DICT.</param>
        /// <param name="iter">Location to store the iterator for the dictionary.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_dict_new_iter(plist_t node, out plist_dict_iter iter);
    }
}
