using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the #plist_type of a node.
        /// </summary>
        /// <param name="node">the node</param>
        /// <returns>the type of the node</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_type plist_get_node_type(plist_t node);
    }
}
