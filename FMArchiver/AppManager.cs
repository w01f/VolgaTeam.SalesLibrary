using System;
using System.IO;
using System.Threading;
using System.Xml;

namespace FMArchiver
{
    class AppManager
    {
        public const string SalesSyncSettingsFileName = @"C:\Program Files\newlocaldirect.com\Sales-Sync\SalesSyncSettings.xml";
        public const string UserSettingsFileName = @"SalesDepotUserSettings.xml";
        public const string StorageFileName = @"SalesDepotCache.xml";
        public const string StyleFileName = @"SalesDepotStyle.xml";
        public const string ArchiveRootPath = @"C:\Program Files\newlocaldirect.com\02. Content Captain\FMArchiver";

        private static AppManager _instance = new AppManager();
        private Timer _timer = null;
        private string _salesDepotRootPath;

        private AppManager()
        {
        }

        public static AppManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public void Run()
        {
            InitSourceFolder();
            StartArchiving();
            Thread.CurrentThread.Suspend();
        }

        private void InitSourceFolder()
        {
            if (File.Exists(SalesSyncSettingsFileName))
            {

                XmlDocument document = new XmlDocument();
                document.Load(SalesSyncSettingsFileName);

                XmlNode salesDepotPathNode = document.SelectSingleNode("/settings/folder2a");
                if (salesDepotPathNode != null)
                {
                    _salesDepotRootPath = salesDepotPathNode.InnerXml;
                }
            }
        }

        private long GetMillisecondsForNextInvocation()
        {
            DateTime nowTime = DateTime.Now;
            DateTime nextTime = DateTime.Now;
            
            while (nextTime.Second != 0)
            {
                nextTime = nextTime.AddSeconds(1);
            }
            while (nextTime.Minute != 0)
            {
                nextTime = nextTime.AddMinutes(1);
            }
            TimeSpan difference = nextTime.Subtract(nowTime);
            long totalMilliseconds = (long)difference.TotalMilliseconds;
            return totalMilliseconds;
        }

        private void StartArchiving()
        {
            if (_timer != null)
                _timer.Dispose();
            _timer = new Timer(Archive, null, GetMillisecondsForNextInvocation(), Timeout.Infinite);
        }

        private void Archive(object state)
        {
            DirectoryInfo salesDepotRoot = new DirectoryInfo(_salesDepotRootPath);
            if (salesDepotRoot.Exists)
            {
                DateTime archiveDateTime = DateTime.Now;
                foreach (DirectoryInfo salesDepotFolder in salesDepotRoot.GetDirectories())
                {
                    if (!Directory.Exists(ArchiveRootPath))
                        Directory.CreateDirectory(ArchiveRootPath);

                    if (!Directory.Exists(ArchiveRootPath + @"\" + salesDepotFolder.Name))
                        Directory.CreateDirectory(ArchiveRootPath + @"\" + salesDepotFolder.Name);

                    string archiveFolderPath = ArchiveRootPath + @"\" + salesDepotFolder.Name + @"\" + archiveDateTime.ToString("MMddyyyy") + ". " + archiveDateTime.ToString("hmmtt");
                    if (!Directory.Exists(archiveFolderPath))
                        Directory.CreateDirectory(archiveFolderPath);

                    foreach (FileInfo file in salesDepotFolder.GetFiles("*.xml"))
                        file.CopyTo(archiveFolderPath + @"\" + file.Name, true);
                }
            }

            StartArchiving();
        }
    }
}
