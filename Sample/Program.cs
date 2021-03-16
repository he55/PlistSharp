namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            string backupPath = @"C:\Users\Admin\Desktop\iphone7plus\675e9d258cfee8d7023e5e78ceefd4cbdbc16bf1";
            iTunesBackup backup = new iTunesBackup(backupPath);
            iTunesBackupInfo iTunesBackupInfo = backup.GetiTunesBackupInfo();
            //backup.MakeFileSystem();
            string v = backup.GetFilePath("Documents/FaceConfig/sysface_res/static/290@2x.png", FileDomain.AppDomain, "com.tencent.tim");
        }
    }
}
