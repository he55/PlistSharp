using PlistSharp;

namespace Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadPlist();
        }

        static void ReadPlist()
        {
            PlistDictionary dict = (PlistDictionary)PlistStructure.FromFile("dict.plist");
            PlistArray array = (PlistArray)dict["array"];

            foreach (PlistNode item in array)
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
