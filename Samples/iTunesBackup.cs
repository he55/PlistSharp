using Dapper;
using Microsoft.Data.Sqlite;
using PlistSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace Samples
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
                PlistDictionary iTunesMetadata = (PlistDictionary)PlistDictionary.FromPlistXml(((PlistData)value["iTunesMetadata"]).Value);

                iTunesMetadataInfo metadataInfo = new iTunesMetadataInfo
                {
                    bundleId = item.Key,
                    bundleShortVersionString = ((PlistString)iTunesMetadata.GetValueOrDefault("bundleShortVersionString"))?.Value,
                    itemName = ((PlistString)iTunesMetadata["itemName"]).Value,
                    genre = ((PlistString)iTunesMetadata.GetValueOrDefault("genre"))?.Value,
                    iconPath = Path.Combine(_savePath, $"{item.Key}.png"),
                    accountInfo = ((PlistString)((PlistDictionary)((PlistDictionary)iTunesMetadata["com.apple.iTunesStore.downloadInfo"])["accountInfo"])["AppleID"]).Value
                };
                backupInfo.Applications.Add(metadataInfo);

                using (FileStream fileStream = new FileStream(metadataInfo.iconPath, FileMode.Create, FileAccess.Write))
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

            int count = sqliteConnection.QueryFirst<int>("SELECT count(*) FROM Files");
            int i = 0;

            Console.Write($"0.00% (0/{count})");

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

#if NETFRAMEWORK
                    string newSafePath = "\\\\?\\" + Path.Combine(_savePath, item.domain, relativeSafePath);
#else
                    string newSafePath;
                    if (OperatingSystem.IsWindows())
                    {
                        newSafePath = "\\\\?\\" + Path.Combine(_savePath, item.domain, relativeSafePath);
                    }
                    else
                    {
                        newSafePath = Path.Combine(_savePath, item.domain, relativeSafePath);
                    }
#endif

                    if (File.Exists(origPath))
                    {
                        File.Copy(origPath, newSafePath, true);
                    }
                    else
                    {
                        Directory.CreateDirectory(newSafePath);
                    }
                }

                i++;
                Console.Write($"\r{((double)i / count):P2} ({i}/{count})");
            }
        }

        public string GetFilePath(string relativePath, string domain, string bundleId = null)
        {
            switch (domain)
            {
                case FileDomain.AppDomain:
                case FileDomain.AppDomainGroup:
                case FileDomain.AppDomainPlugin:
                case FileDomain.SysContainerDomain:
                case FileDomain.SysSharedContainerDomain:
                    if (string.IsNullOrEmpty(bundleId))
                    {
                        throw new ArgumentException();
                    }
                    domain = $"{domain}-{bundleId}";
                    break;
            }

            string manifestDbPath = Path.Combine(_backupPath, ManifestDbFile);
            SqliteConnection sqliteConnection = new SqliteConnection($"Data Source={manifestDbPath}");
            string fileID = sqliteConnection.QueryFirstOrDefault<string>(
                "SELECT fileID FROM Files WHERE domain=@domain AND relativePath=@relativePath",
                new { domain, relativePath });

            if (string.IsNullOrEmpty(fileID))
            {
                return null;
            }
            return Path.Combine(_backupPath, fileID.Substring(0, 2), fileID);
        }
    }
}
