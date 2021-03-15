using System.Collections.Generic;
using System.IO;
using Dapper;
using Microsoft.Data.Sqlite;
using PlistSharp;

namespace Sample
{
    public class iTunesBackup
    {
        private const string InfoPlistFile = "Info.plist";
        private const string ManifestDbFile = "Manifest.db";
        private const string ManifestPlistFile = "Manifest.plist";
        private const string StatusPlistFile = "Status.plist";

        private readonly string _backupPath;
        private readonly string _savePath;

        public iTunesBackup(string backupPath)
        {
            _backupPath = backupPath;
            _savePath = Path.Combine(backupPath, "_00");

            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }
        }

        public string BackupPath => _backupPath;

        public string SavePath => _savePath;

        public iTunesBackupInfo GetiTunesBackupInfo()
        {
            PlistDictionary infoDict = (PlistDictionary)PlistStructure.FromFile(Path.Combine(_backupPath, InfoPlistFile));

            iTunesBackupInfo backupInfo = new iTunesBackupInfo
            {
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
                    iconPath = Path.Combine(_savePath, $"{item.Key}.png"),
                    accountInfo = ((PlistString)((PlistDictionary)((PlistDictionary)iTunesMetadata["com.apple.iTunesStore.downloadInfo"])["accountInfo"])["AppleID"]).Value
                };

                backupInfo.Applications.Add(iTunesMetadataInfo);

                using (FileStream fileStream = new FileStream(iTunesMetadataInfo.iconPath, FileMode.Create, FileAccess.Write))
                {
                    byte[] buffer = ((PlistData)value["PlaceholderIcon"]).Value;
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }

            return backupInfo;
        }

        public void MakeFileSystem()
        {
            string manifestDbPath = Path.Combine(_backupPath, ManifestDbFile);
            SqliteConnection sqliteConnection = new SqliteConnection($"Data Source={manifestDbPath}");
            IEnumerable<ManifestFile> manifestFiles = sqliteConnection.Query<ManifestFile>("SELECT * FROM Files ORDER BY relativePath");

            foreach (var item in manifestFiles)
            {
                if (string.IsNullOrEmpty(item.relativePath))
                {
                    string domainPath = Path.Combine(_savePath, item.domain);
                    Directory.CreateDirectory(domainPath);
                }
                else
                {
                    string origPath = Path.Combine(_backupPath, item.fileID.Substring(0, 2), item.fileID);
                    string relativeSafePath = item.relativePath.Replace("/", "\\").Replace(":", "__").Replace("?", "__").Replace("|", "__");
                    string newSafePath = "\\\\?\\" + Path.Combine(_savePath, item.domain, relativeSafePath);

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
