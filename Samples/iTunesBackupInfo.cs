using PlistSharp;
using System;
using System.Collections.Generic;

namespace Samples
{
    public class iTunesBackupInfo
    {
        public List<iTunesMetadataInfo> Applications { get; } = new List<iTunesMetadataInfo>();
        public string BuildVersion { get; set; }
        public string DeviceName { get; set; }
        public string DisplayName { get; set; }
        public string GUID { get; set; }
        public string ICCID { get; set; }
        public string IMEI { get; set; }
        public List<string> InstalledApplications { get; } = new List<string>();
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
}
