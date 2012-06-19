using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AutoSynchronizer
{
    class AppManager
    {
        public static object Locker = new object();

        private static AppManager instance = new AppManager();

        private AppManager()
        {
        }

        public static AppManager Instance
        {
            get
            {
                return instance;
            }
        }

        public void RunForm()
        {
            ConfigurationClasses.SettingsManager.Instance.Load();
            if (!string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.BackupPath) && System.IO.Directory.Exists(ConfigurationClasses.SettingsManager.Instance.BackupPath))
            {
                BusinessClasses.LibraryManager.Instance.LoadLibraries();
                Application.Run(new FormHidden());
            }
        }

        public void ReleaseComObject(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }

        public bool FileManagerActive()
        {
            return Process.GetProcesses().Where(x => x.ProcessName.ToLower().Contains("filemanager")).Count() > 0;
        }
    }
}
