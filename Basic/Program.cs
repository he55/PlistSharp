using System;
using System.Collections.Generic;
using System.IO;
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


            List<string> installedApplications = new List<string>();

            foreach (PlistString item in backupInfo.InstalledApplications)
            {
                installedApplications.Add(item.Value);
            }


            List<Application> applications = new List<Application>();

            foreach (var item in backupInfo.Applications)
            {
                string key = item.Key;
                PlistDictionary value = (PlistDictionary)item.Value;

                Application application = new Application
                {
                    ApplicationSINF = (PlistData)value["ApplicationSINF"],
                    PlaceholderIcon = (PlistData)value["PlaceholderIcon"],
                    iTunesMetadata = (PlistData)value["iTunesMetadata"]
                };
                applications.Add(application);

                string path = Path.Combine(@"C:\Users\Admin\Desktop\iphonebak", $"{key}.png");
                using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = application.PlaceholderIcon.Value;
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
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
        public PlistData ApplicationSINF { get; set; }
        public PlistData PlaceholderIcon { get; set; }
        public PlistData iTunesMetadata { get; set; }
    }
}
