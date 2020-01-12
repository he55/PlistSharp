using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Create a new plist_t type #PLIST_BOOLEAN
        /// </summary>
        /// <param name="val">the boolean value, 0 is false, other values are true.</param>
        /// <returns>the created item</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_new_bool(byte val);
    }
}
