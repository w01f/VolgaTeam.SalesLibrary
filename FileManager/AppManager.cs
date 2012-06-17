using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileManager
{
    class AppManager
    {
        private delegate void NoParamsDelegate();

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

        private bool Init()
        {
            bool result = false;
            ConfigurationClasses.SettingsManager.Instance.Load();
            ConfigurationClasses.ListManager.Instance.Init();
            if (string.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.BackupPath) || !Directory.Exists(ConfigurationClasses.SettingsManager.Instance.BackupPath))
            {
                AppManager.Instance.ShowWarning("Primary Backup Root is not set or unavailable.\nYou need to configure application");
                using (ToolForms.Settings.FormPaths form = new ToolForms.Settings.FormPaths())
                {
                    if (form.ShowDialog() == DialogResult.Cancel)
                    {
                        AppManager.Instance.ShowWarning("Application is not configured and will be closed");
                    }
                    else
                        result = true;
                }
            }
            else
                result = true;
            return result;
        }

        public void RunForm()
        {
            if (Init())
            {
                FormMain.Instance.ShowInTaskbar = true;
                Application.Run(FormMain.Instance);
            }
        }

        public void ActivateMainForm()
        {
            IntPtr mainFormHandle = IntPtr.Zero;
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList.Where(x => x.ProcessName.Contains("FileManager")))
            {
                if (process.MainWindowHandle.ToInt32() != 0)
                {
                    mainFormHandle = process.MainWindowHandle;
                    break;
                }
            }
            if (mainFormHandle.ToInt32() != 0)
            {
                InteropClasses.WinAPIHelper.ShowWindow(mainFormHandle, InteropClasses.WindowShowStyle.ShowMaximized);
                InteropClasses.WinAPIHelper.MakeTopMost(mainFormHandle);
                InteropClasses.WinAPIHelper.MakeNormal(mainFormHandle);
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(mainFormHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
        }

        public void ShowInfo(string text)
        {
            MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowWarning(string text)
        {
            MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public DialogResult ShowQuestion(string text)
        {
            return MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, "Digital Wall Bin Administrator", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
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
    }
}
