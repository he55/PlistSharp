namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string backupPath = @"C:\Users\luckh\AppData\Roaming\Apple Computer\MobileSync\Backup\27ce173b4c4224b63ba7fb8666e3450045b0a310";
            iTunesBackup backup = new iTunesBackup(backupPath);
            iTunesBackupInfo backupInfo = backup.GetiTunesBackupInfo();
            //backup.MakeFileSystem();
            string filePath = backup.GetFilePath("Documents/FaceConfig/sysface_res/static/290@2x.png", FileDomain.AppDomain, "com.tencent.tim");
        }
    }
}
