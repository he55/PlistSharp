using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Import the #plist_t structure from binary format.
        /// </summary>
        /// <param name="plist_bin">a pointer to the xml buffer.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_from_bin(/* const char * */ IntPtr plist_bin, uint length, out plist_t plist);
    }
}
