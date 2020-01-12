using System.Runtime.InteropServices;

public static partial class Interop
{
    public static partial class LibPlist
    {
        /// <summary>
        /// Create a new plist_t type #PLIST_DATE
        /// </summary>
        /// <param name="sec">the number of seconds since 01/01/2001</param>
        /// <param name="usec">the number of microseconds</param>
        /// <returns>the created item</returns>
        [DllImport(Libraries.LibPlist)]
        public static extern plist_t plist_new_date(int sec, int usec);
    }
}
