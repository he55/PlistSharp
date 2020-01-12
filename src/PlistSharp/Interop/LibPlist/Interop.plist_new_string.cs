using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Create a new plist_t type #PLIST_STRING
        /// </summary>
        /// <param name="val">the sting value, encoded in UTF8.</param>
        /// <returns>the created item</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_new_string(/* const char * */ string val);
    }
}
