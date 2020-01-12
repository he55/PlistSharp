using PlistSharp;

namespace ReadPlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PlistDictionary? dict = (PlistDictionary?)PlistDictionary.FromFile("dict.plist");
            if (dict == null)
            {
                return;
            }

            uint dictSize = dict.GetSize();

            PlistArray array = (PlistArray)dict["array"];
            uint arraySize = array.GetSize();

            foreach (var item in array)
            {

            }

            PlistBoolean plistBoolean = (PlistBoolean)dict["boolean"];
            bool booleanValue = plistBoolean.GetValue();

            PlistData plistData = (PlistData)dict["data"];
            byte[] dataValue = plistData.GetValue();

            PlistDate plistDate = (PlistDate)dict["date"];
            timeval timeval = plistDate.GetValue();

            PlistInteger plistInteger = (PlistInteger)dict["integer"];
            ulong integerValue = plistInteger.GetValue();

            PlistReal plistReal = (PlistReal)dict["real"];
            double realValue = plistReal.GetValue();

            PlistString plistString = (PlistString)dict["string"];
            string stringValue = plistString.GetValue();

        }
    }
}
