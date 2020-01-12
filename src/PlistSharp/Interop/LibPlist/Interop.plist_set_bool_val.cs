using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Set the value of a node.
        /// Forces type of node to #PLIST_BOOLEAN
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">the boolean value</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_set_bool_val(plist_t node, byte val);
    }
}
