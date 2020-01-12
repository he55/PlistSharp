using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the value of a #PLIST_BOOLEAN node.
        /// This function does nothing if node is not of type #PLIST_BOOLEAN
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">a pointer to a uint8_t variable.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_get_bool_val(plist_t node, out byte val);
    }
}
