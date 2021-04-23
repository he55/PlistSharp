using System;
using System.Text;

namespace PlistSharp
{
    public class StringHelper
    {
        public static string PtrToStringUTF8(IntPtr ptr)
        {
            unsafe
            {
                int len = 0;
                byte* p = (byte*)ptr;
                while (*p++ != 0)
                {
                    len++;
                }
                return Encoding.UTF8.GetString((byte*)ptr, len);
            }
        }
    }
}
