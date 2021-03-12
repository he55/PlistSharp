using System.Collections.Generic;
using System.IO;
using Dapper;
using Microsoft.Data.Sqlite;
using PlistSharp;

namespace Basic
{
    class Program
    {
        private const string InfoPlistFile = "Info.plist";
        private const string ManifestDbFile = "Manifest.db";
        private const string ManifestPlistFile = "Manifest.plist";
        private const string StatusPlistFile = "Status.plist";

        static void Main(string[] args)
        {
            string backupPath = @"C:\Users\Admin\Desktop\iphone7plus\675e9d258cfee8d7023e5e78ceefd4cbdbc16bf1";
            string savePath = Path.Combine(backupPath, "_0");

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            PlistDictionary infoDict = (PlistDictionary)PlistStructure.FromFile(Path.Combine(backupPath, InfoPlistFile));

            BackupInfo backupInfo = new BackupInfo
            {
                Applications = (PlistDictionary)infoDict["Applications"],
                BuildVersion = ((PlistString)infoDict["Build Version"]).Value,
                DeviceName = ((PlistString)infoDict["Device Name"]).Value,
                DisplayName = ((PlistString)infoDict["Display Name"]).Value,
                GUID = ((PlistString)infoDict["GUID"]).Value,
                ICCID = ((PlistString)infoDict["ICCID"]).Value,
                IMEI = ((PlistString)infoDict["IMEI"]).Value,
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

            foreach (PlistString item in (PlistArray)infoDict["Installed Applications"])
            {
                backupInfo.InstalledApplications.Add(item.Value);
            }


            List<iTunesMetadataInfo> iTunesMetadataInfos = GetApplicationiTunesMetadataInfos(backupPath, savePath);

            string manifestDbPath = Path.Combine(backupPath, ManifestDbFile);
            SqliteConnection sqliteConnection = new SqliteConnection($"Data Source={manifestDbPath}");
            IEnumerable<Files> enumerable1 = sqliteConnection.Query<Files>("SELECT * FROM Files ORDER BY relativePath");

        }

        public static List<iTunesMetadataInfo> GetApplicationiTunesMetadataInfos(string backupPath, string savePath)
        {
            PlistDictionary infoDict = (PlistDictionary)PlistStructure.FromFile(Path.Combine(backupPath, InfoPlistFile));
            List<iTunesMetadataInfo> iTunesMetadataInfos = new List<iTunesMetadataInfo>();

            foreach (var item in (PlistDictionary)infoDict["Applications"])
            {
                PlistDictionary value = (PlistDictionary)item.Value;
                PlistDictionary iTunesMetadata = (PlistDictionary)PlistDictionary.FromPlistBin(((PlistData)value["iTunesMetadata"]).Value);

                iTunesMetadataInfo iTunesMetadataInfo = new iTunesMetadataInfo
                {
                    bundleId = item.Key,
                    bundleShortVersionString = ((PlistString)iTunesMetadata.GetValueOrDefault("bundleShortVersionString"))?.Value,
                    itemName = ((PlistString)iTunesMetadata["itemName"]).Value,
                    genre = ((PlistString)iTunesMetadata.GetValueOrDefault("genre"))?.Value,
                    iconPath = Path.Combine(savePath, $"{item.Key}.png"),
                    accountInfo = ((PlistString)((PlistDictionary)((PlistDictionary)iTunesMetadata["com.apple.iTunesStore.downloadInfo"])["accountInfo"])["AppleID"]).Value
                };

                iTunesMetadataInfos.Add(iTunesMetadataInfo);

                using (FileStream fileStream = new FileStream(iTunesMetadataInfo.iconPath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = ((PlistData)value["PlaceholderIcon"]).Value;
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }

            return iTunesMetadataInfos;
        }

        public static void iTunesBackup2FileSystem(string backupPath, string savePath)
        {
            string manifestDbPath = Path.Combine(backupPath, ManifestDbFile);
            SqliteConnection sqliteConnection = new SqliteConnection($"Data Source={manifestDbPath}");
            IEnumerable<Files> enumerable1 = sqliteConnection.Query<Files>("SELECT * FROM Files ORDER BY relativePath");

            foreach (var item in enumerable1)
            {
                if (string.IsNullOrEmpty(item.relativePath))
                {
                    string domainPath = Path.Combine(savePath, item.domain);
                    Directory.CreateDirectory(domainPath);
                }
                else
                {
                    string origPath = Path.Combine(backupPath, item.fileID.Substring(0, 2), item.fileID);
                    string relativeSafePath = item.relativePath.Replace("/", "\\").Replace(":", "__").Replace("?", "__").Replace("|", "__");
                    string newSafePath = "\\\\?\\" + Path.Combine(savePath, item.domain, relativeSafePath);

                    if (File.Exists(origPath))
                    {
                        File.Copy(origPath, newSafePath, true);
                    }
                    else
                    {
                        Directory.CreateDirectory(newSafePath);
                    }
                }
            }
        }
    }
}
