using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Merge a dictionary into another. This will add all key/value pairs
        /// from the source dictionary to the target dictionary, overwriting
        /// any existing key/value pairs that are already present in target.
        /// </summary>
        /// <param name="target">pointer to an existing node of type #PLIST_DICT</param>
        /// <param name="source">node of type #PLIST_DICT that should be merged into target</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_dict_merge(ref plist_t target, plist_t source);
    }
}
