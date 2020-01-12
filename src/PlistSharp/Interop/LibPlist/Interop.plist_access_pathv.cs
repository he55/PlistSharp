using System.Runtime.InteropServices;
using va_list = System.IntPtr;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Variadic version of #plist_access_path.
        /// </summary>
        /// <param name="plist">the node to access result from.</param>
        /// <param name="length">length of the path to access</param>
        /// <param name="v">list of array's index and dic'st key</param>
        /// <returns>the value to access.</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_access_pathv(plist_t plist, uint length, va_list v);
    }
}
