using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Test if in-memory plist data is binary or XML
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// This method is not validating the whole memory buffer to check if the
        /// content is truly a plist, it's only using some heuristic on the first few
        /// bytes of plist_data.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <returns>1 if the buffer is a binary plist, 0 otherwise.</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern int plist_is_binary(/* const char * */ IntPtr plist_data, uint length);

        /// <summary>
        /// Test if in-memory plist data is binary or XML
        /// This method will look at the first bytes of plist_data
        /// to determine if plist_data contains a binary or XML plist.
        /// This method is not validating the whole memory buffer to check if the
        /// content is truly a plist, it's only using some heuristic on the first few
        /// bytes of plist_data.
        /// </summary>
        /// <param name="plist_data">a pointer to the memory buffer containing plist data.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <returns>1 if the buffer is a binary plist, 0 otherwise.</returns>
        [DllImport(Libraries.LibPlist)]
        public unsafe static extern int plist_is_binary(byte* plist_data, uint length);
    }
}
