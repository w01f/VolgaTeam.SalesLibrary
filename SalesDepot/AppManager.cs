using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace SalesDepot
{
    public class AppManager
    {
        private static AppManager _instance = new AppManager();
        public delegate void NoParamDelegate();
        public delegate void SingleParamDelegate(object parameter);
        public DirectoryInfo TempFolder { get; set; }

        private AppManager()
        {
            if (!Directory.Exists(ConfigurationClasses.SettingsManager.Instance.TempPath))
                Directory.CreateDirectory(ConfigurationClasses.SettingsManager.Instance.TempPath);
            this.TempFolder = new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.TempPath);
        }

        ~AppManager()
        {
            DeleteFolder(this.TempFolder);
            if (!Directory.Exists(ConfigurationClasses.SettingsManager.Instance.TempPath))
                Directory.CreateDirectory(ConfigurationClasses.SettingsManager.Instance.TempPath);

            DeleteFolder(new DirectoryInfo(ConfigurationClasses.SettingsManager.Instance.LocalLibraryCacheFolder));
            if (!Directory.Exists(ConfigurationClasses.SettingsManager.Instance.LocalLibraryCacheFolder))
                Directory.CreateDirectory(ConfigurationClasses.SettingsManager.Instance.LocalLibraryCacheFolder);
        }

        public static AppManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void ShowMainForm()
        {
            Application.Run(FormMain.Instance);
        }

        public void RunForm()
        {
            ToolClasses.ActivityRecorder.Instance.StartRecording();
            ToolClasses.SDRecorder.Instance.StartRecording();
            ConfigurationClasses.ListManager.Instance.Init();
            ShowMainForm();
        }

        public void ActivateForm(IntPtr handle, bool maximized, bool topMost)
        {
            InteropClasses.WinAPIHelper.ShowWindow(handle, maximized ? InteropClasses.WindowShowStyle.ShowMaximized : InteropClasses.WindowShowStyle.ShowNormal);
            uint lpdwProcessId = 0;
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
            InteropClasses.WinAPIHelper.SetForegroundWindow(handle);
            InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            if (topMost)
                InteropClasses.WinAPIHelper.MakeTopMost(handle);
            else
                InteropClasses.WinAPIHelper.MakeNormal(handle);
        }

        public void ActivateMainForm()
        {
            IntPtr handle = ConfigurationClasses.RegistryHelper.RemoteLibraryHandle;
            if (handle.Equals(IntPtr.Zero))
            {
                handle = FormMain.Instance.Handle;
                if (handle.Equals(IntPtr.Zero))
                {
                    Process[] proc = Process.GetProcessesByName("RemoteLibrary");
                    if (proc.Length > 0)
                        handle = proc[0].MainWindowHandle;
                }
            }
            ActivateForm(handle, ConfigurationClasses.RegistryHelper.MaximizeRemoteLibrary, false);
        }

        public void ActivatePowerPoint()
        {
            if (InteropClasses.PowerPointHelper.Instance.PowerPointObject != null)
            {
                IntPtr powerPointHandle = new IntPtr(InteropClasses.PowerPointHelper.Instance.PowerPointObject.HWND);
                InteropClasses.WinAPIHelper.ShowWindow(powerPointHandle, InteropClasses.WindowShowStyle.ShowMaximized);
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(powerPointHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
            ActivateMiniBar();
        }

        public void ActivateMiniBar()
        {
            IntPtr minibarHandle = ConfigurationClasses.RegistryHelper.MinibarHandle;
            if (minibarHandle.ToInt32() == 0)
            {
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("minibar")))
                {
                    if (process.MainWindowHandle.ToInt32() != 0)
                    {
                        minibarHandle = process.MainWindowHandle;
                        break;
                    }
                }
            }
            if (minibarHandle.ToInt32() != 0)
            {
                uint lpdwProcessId = 0;
                InteropClasses.WinAPIHelper.MakeTopMost(minibarHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
                InteropClasses.WinAPIHelper.SetForegroundWindow(minibarHandle);
                InteropClasses.WinAPIHelper.AttachThreadInput(InteropClasses.WinAPIHelper.GetCurrentThreadId(), InteropClasses.WinAPIHelper.GetWindowThreadProcessId(InteropClasses.WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
            }
        }

        public void RunPowerPointLoader()
        {
            if (File.Exists(ConfigurationClasses.SettingsManager.Instance.PowerPointLoaderPath))
            {
                Process process = new Process();
                process.StartInfo.FileName = ConfigurationClasses.SettingsManager.Instance.PowerPointLoaderPath;
                process.Start();
            }
            else
                ShowWarning("Couldn't find PowerPointLoader app");
        }

        public void ShowInfo(string text)
        {
            MessageBox.Show(text, "Sales Depot", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowWarning(string text)
        {
            MessageBox.Show(text, "Sales Depot", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public DialogResult ShowWarningQuestion(string text)
        {
            return MessageBox.Show(text, "Sales Depot", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }

        public DialogResult ShowInfoQuestion(string text)
        {
            return MessageBox.Show(text, "Sales Depot", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void DeleteFolder(DirectoryInfo folder)
        {
            try
            {
                foreach (DirectoryInfo subFolder in folder.GetDirectories())
                    DeleteFolder(subFolder);
                FileInfo[] files = folder.GetFiles();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (File.Exists(file.FullName))
                        {
                            File.SetAttributes(file.FullName, FileAttributes.Normal);
                            File.Delete(file.FullName);
                        }
                    }
                    catch
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(100);
                            if (File.Exists(file.FullName))
                                File.Delete(file.FullName);
                        }
                        catch
                        {
                        }
                    }
                }
                try
                {
                    if (Directory.Exists(folder.FullName))
                        Directory.Delete(folder.FullName, false);
                }
                catch
                {
                    try
                    {
                        System.Threading.Thread.Sleep(100);
                        if (Directory.Exists(folder.FullName))
                            Directory.Delete(folder.FullName, false);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }
    }
}
