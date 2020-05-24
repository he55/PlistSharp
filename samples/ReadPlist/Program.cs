using PlistSharp;

namespace ReadPlist
{
    class Program
    {
        static void Main(string[] args)
        {
            PlistDictionary dict = (PlistDictionary)PlistDictionary.FromFile("dict.plist");

            PlistArray array = (PlistArray)dict["array"];

            foreach (var item in array)
            {

            }

            PlistBoolean plistBoolean = (PlistBoolean)dict["boolean"];
            PlistData plistData = (PlistData)dict["data"];
            PlistDate plistDate = (PlistDate)dict["date"];
            PlistInteger plistInteger = (PlistInteger)dict["integer"];
            PlistReal plistReal = (PlistReal)dict["real"];
            PlistString plistString = (PlistString)dict["string"];

        }
    }
}
