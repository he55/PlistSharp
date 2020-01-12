using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Import the #plist_t structure from XML format.
        /// </summary>
        /// <param name="plist_xml">a pointer to the xml buffer.</param>
        /// <param name="length">length of the buffer to read.</param>
        /// <param name="plist">a pointer to the imported plist.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_from_xml(/* const char * */ string plist_xml, uint length, out plist_t plist);
    }
}
