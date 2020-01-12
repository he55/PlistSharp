using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_STRING
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">
        /// the string value. The string is copied when set and will be
        /// freed by the node.
        /// </param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_set_string_val(plist_t node, /* const char * */ string val);
    }
}
