using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Get the parent of a node
        /// </summary>
        /// <param name="node">the parent (NULL if node is root)</param>
        /// <returns></returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_get_parent(plist_t node);
    }
}
