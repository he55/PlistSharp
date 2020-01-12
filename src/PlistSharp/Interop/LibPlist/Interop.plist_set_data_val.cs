using System;
using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_DATA
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// the binary buffer. The buffer is copied when set and will
        /// be freed by the node.
        /// </param>
        /// <param name="length">the length of the buffer</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_set_data_val(plist_t node, /* const char * */ IntPtr val, ulong length);
    }
}
