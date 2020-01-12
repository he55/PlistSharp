using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Return a copy of passed node and it's children
        /// </summary>
        /// <param name="node">node the plist to copy</param>
        /// <returns>copied plist</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_copy(plist_t node);
    }
}
