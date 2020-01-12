using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Destruct a plist_t node and all its children recursively
        /// </summary>
        /// <param name="plist">the plist to free</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_free(plist_t plist);
    }
}
