using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Compare two node values
        /// </summary>
        /// <param name="node_l">left node to compare</param>
        /// <param name="node_r">rigth node to compare</param>
        /// <returns>TRUE is type and value match, FALSE otherwise.</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern byte plist_compare_node_value(plist_t node_l, plist_t node_r);
    }
}
