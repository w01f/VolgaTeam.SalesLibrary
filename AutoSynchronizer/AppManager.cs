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

        private void Init()
        {
            ConfigurationClasses.SettingsManager.Instance.Load();
            BusinessClasses.LibraryManager.Instance.LoadLibraries();
        }

        public void RunForm()
        {
            Init();
            Application.Run(new FormHidden());
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
