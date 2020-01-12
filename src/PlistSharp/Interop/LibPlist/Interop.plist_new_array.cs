using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Create a new root plist_t type #PLIST_ARRAY
        /// </summary>
        /// <returns>the created plist</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_new_array();
    }
}
