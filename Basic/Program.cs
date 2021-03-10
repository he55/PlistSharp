using System;
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
            PlistDictionary infoDict = (PlistDictionary)PlistStructure.FromFile(@"C:\Users\Admin\Desktop\iphonebak\00008030-000A6C562E92802E\Info.plist");

            BackupInfo backupInfo = new BackupInfo
            {
                Applications = (PlistDictionary)infoDict["Applications"],
                BuildVersion = ((PlistString)infoDict["Build Version"]).Value,
                DeviceName = ((PlistString)infoDict["Device Name"]).Value,
                DisplayName = ((PlistString)infoDict["Display Name"]).Value,
                GUID = ((PlistString)infoDict["GUID"]).Value,
                ICCID = ((PlistString)infoDict["ICCID"]).Value,
                IMEI = ((PlistString)infoDict["IMEI"]).Value,
                InstalledApplications = (PlistArray)infoDict["Installed Applications"],
                LastBackupDate = ((PlistDate)infoDict["Last Backup Date"]).Value,
                MEID = ((PlistString)infoDict["MEID"]).Value,
                PhoneNumber = ((PlistString)infoDict["Phone Number"]).Value,
                ProductType = ((PlistString)infoDict["Product Type"]).Value,
                ProductVersion = ((PlistString)infoDict["Product Version"]).Value,
                SerialNumber = ((PlistString)infoDict["Serial Number"]).Value,
                TargetIdentifier = ((PlistString)infoDict["Target Identifier"]).Value,
                TargetType = ((PlistString)infoDict["Target Type"]).Value,
                UniqueIdentifier = ((PlistString)infoDict["Unique Identifier"]).Value,
                iTunesFiles = (PlistDictionary)infoDict["iTunes Files"],
                iTunesSettings = (PlistDictionary)infoDict["iTunes Settings"],
                iTunesVersion = ((PlistString)infoDict["iTunes Version"]).Value,
            };



        }
    }

    public class BackupInfo
    {
        public PlistDictionary Applications { get; set; }
        public string BuildVersion { get; set; }
        public string DeviceName { get; set; }
        public string DisplayName { get; set; }
        public string GUID { get; set; }
        public string ICCID { get; set; }
        public string IMEI { get; set; }
        public PlistArray InstalledApplications { get; set; }
        public DateTime LastBackupDate { get; set; }
        public string MEID { get; set; }
        public string PhoneNumber { get; set; }
        public string ProductType { get; set; }
        public string ProductVersion { get; set; }
        public string SerialNumber { get; set; }
        public string TargetIdentifier { get; set; }
        public string TargetType { get; set; }
        public string UniqueIdentifier { get; set; }
        public PlistDictionary iTunesFiles { get; set; }
        public PlistDictionary iTunesSettings { get; set; }
        public string iTunesVersion { get; set; }
    }

    public class Application
    {
        public object ApplicationSINF { get; set; }
        public object PlaceholderIcon { get; set; }
        public object iTunesMetadata { get; set; }
    }
}
