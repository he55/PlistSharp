using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_KEY
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">the key value</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_set_key_val(plist_t node, /* const char * */ string val);
    }
}
