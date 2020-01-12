using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the value of a #PLIST_UID node.
        /// This function does nothing if node is not of type #PLIST_UID
        /// </summary>
        /// <param name="node">the node</param>
        /// <param name="val">a pointer to a uint64_t variable.</param>
        [DllImport(Libraries.LibPlist)]
        public static extern void plist_get_uid_val(plist_t node, out ulong val);
    }
}
