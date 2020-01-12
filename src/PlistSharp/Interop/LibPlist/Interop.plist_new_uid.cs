using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Create a new plist_t type #PLIST_UID
        /// </summary>
        /// <param name="val">the unsigned integer value</param>
        /// <returns>the created item</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_new_uid(ulong val);
    }
}
