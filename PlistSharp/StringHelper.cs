using System;
using System.Runtime.InteropServices;
using System.Text;

namespace PlistSharp
{
    public class StringHelper
    {
        [DllImport("ucrtbased")]
        private static extern int strlen(IntPtr str);

        public static string PtrToStringUTF8(IntPtr ptr)
        {
            unsafe
            {
                return Encoding.UTF8.GetString((byte*)ptr, strlen(ptr));
            }
        }
    }
}
