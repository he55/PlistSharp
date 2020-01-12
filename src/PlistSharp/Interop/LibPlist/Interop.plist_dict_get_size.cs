using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get size of a #PLIST_DICT node.
        /// </summary>
        /// <param name="node">the node of type #PLIST_DICT</param>
        /// <returns>size of the #PLIST_DICT node</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern uint plist_dict_get_size(plist_t node);
    }
}
