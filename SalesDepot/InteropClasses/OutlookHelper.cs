using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SalesDepot.InteropClasses
{
    public class OutlookHelper
    {
        private static OutlookHelper instance = new OutlookHelper();

        private OutlookHelper()
        {
        }

        public static OutlookHelper Instance
        {
            get
            {
                return instance;
            }
        }

        private Outlook.Application _outlookObject;

        public bool Open()
        {
            try
            {
                _outlookObject =
                    System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            catch
            {
                _outlookObject = null;
            }
            if (_outlookObject == null)
            {
                try
                {
                    _outlookObject = new Outlook.Application();
                    Outlook.MAPIFolder folder = (_outlookObject.GetNamespace("MAPI")).GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
                    folder.Display();
                    Outlook.Explorer explorer = _outlookObject.Explorers.Add(folder, Microsoft.Office.Interop.Outlook.OlFolderDisplayMode.olFolderDisplayNormal);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public void Close()
        {
            AppManager.Instance.ReleaseComObject(_outlookObject);
        }

        public void CreateMessage(string subject, string attachmentPath)
        {
            try
            {
                IntPtr handle = IntPtr.Zero;
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("outlook")))
                {
                    if (process.MainWindowHandle.ToInt32() != 0)
                    {
                        handle = process.MainWindowHandle;
                        break;
                    }
                }
                AppManager.Instance.ActivateForm(handle, true, false);
                Outlook.MailItem mi = (Outlook.MailItem)_outlookObject.CreateItem(Outlook.OlItemType.olMailItem);
                mi.Attachments.Add(attachmentPath, Outlook.OlAttachmentType.olByValue, 1, "Attachment");
                mi.Subject = subject;
                mi.Display();
                int count = 100000;
                handle = IntPtr.Zero;
                while (handle == IntPtr.Zero && count > 0)
                {
                    handle = WinAPIHelper.FindWindow(string.Empty, subject + "- Message (HTML)");
                    count--;
                    Application.DoEvents();
                }
                if (handle != IntPtr.Zero)
                    AppManager.Instance.ActivateForm(handle, true, true);
            }
            catch (Exception e)
            {
                AppManager.Instance.ShowWarning(e.Message);
            }
        }

        public void CreateMessage(string subject, string[] attachmentPaths)
        {
            try
            {
                IntPtr handle = IntPtr.Zero;
                Process[] processList = Process.GetProcesses();
                foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("outlook")))
                {
                    if (process.MainWindowHandle.ToInt32() != 0)
                    {
                        handle = process.MainWindowHandle;
                        break;
                    }
                }
                AppManager.Instance.ActivateForm(handle, true, false);
                Outlook.MailItem mi = (Outlook.MailItem)_outlookObject.CreateItem(Outlook.OlItemType.olMailItem);
                foreach (string attachmentPath in attachmentPaths)
                    mi.Attachments.Add(attachmentPath, Outlook.OlAttachmentType.olByValue, 1, "Attachment");
                mi.Subject = subject;
                mi.Display();
                int count = 100000;
                handle = IntPtr.Zero;
                while (handle == IntPtr.Zero && count > 0)
                {
                    handle = WinAPIHelper.FindWindow(string.Empty, subject + "- Message (HTML)");
                    count--;
                    Application.DoEvents();
                }
                if (handle != IntPtr.Zero)
                    AppManager.Instance.ActivateForm(handle, true, true);
            }
            catch (Exception e)
            {
                AppManager.Instance.ShowWarning(e.Message);
            }
        }
    }
}

