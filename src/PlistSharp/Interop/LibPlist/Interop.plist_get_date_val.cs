using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the value of a #PLIST_DATE node.
        /// This function does nothing if node is not of type #PLIST_DATE
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="sec">a pointer to an int32_t variable. Represents the number of seconds since 01/01/2001.</param>
        /// <param name="usec">a pointer to an int32_t variable. Represents the number of microseconds</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_get_date_val(plist_t node, out int sec, out int usec);
    }
}
