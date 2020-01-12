using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Create a new plist_t type #PLIST_DATA
        /// </summary>
        /// <param name="val">the binary buffer</param>
        /// <param name="length">the length of the buffer</param>
        /// <returns>the created item</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_new_data(/* const char * */ IntPtr val, ulong length);
    }
}
