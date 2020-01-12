using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Import the #plist_t structure from memory data.
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_from_memory(/* const char * */ IntPtr plist_data, uint length, out plist_t plist);

        /// <summary>
        /// Import the #plist_t structure from memory data.
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(Libraries.LibPlist)]
        public unsafe static extern void plist_from_memory(byte* plist_data, uint length, out plist_t plist);
    }
}
