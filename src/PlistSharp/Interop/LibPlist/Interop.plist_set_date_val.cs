using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_DATE
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="sec">the number of seconds since 01/01/2001</param>
        /// <param name="usec">the number of microseconds</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_set_date_val(plist_t node, int sec, int usec);
    }
}
