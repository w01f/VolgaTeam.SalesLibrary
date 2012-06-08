using System;
using System.IO;
using System.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace AutoSynchronizer.InteropClasses
{
    public class OutlookHelper
    {
        private BusinessClasses.OvernightsCalendar _calendar;
        private Outlook.Application _outlookObject;

        public OutlookHelper(BusinessClasses.OvernightsCalendar calendar)
        {
            _calendar = calendar;
        }

        public bool Connect()
        {
            try
            {
                _outlookObject = new Outlook.Application();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Disconnect()
        {
            AppManager.Instance.ReleaseComObject(_outlookObject);
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public void GrabOvernightsEmail()
        {
            try
            {
                MessageFilter.Register();
                if (Connect() && _calendar.RootFolder.Exists)
                {
                    Outlook.MAPIFolder folder = (_outlookObject.GetNamespace("MAPI")).GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
                    if (folder != null)
                    {
                        foreach (Outlook.MAPIFolder subFolder in folder.Folders)
                        {
                            if (subFolder.Name.Equals(_calendar.InboxSubFolder))
                            {
                                Outlook.Items items = subFolder.Items;
                                items.Sort("[ReceivedTime]", false);
                                foreach (Outlook.MailItem message in items)
                                {
                                    if (message.UnRead)
                                    {
                                        DateTime messageSentDate = message.SentOn;
                                        foreach (Outlook.Attachment attachment in message.Attachments)
                                        {
                                            string attachmentExtension = Path.GetExtension(attachment.FileName).ToLower();
                                            string tempPath = Path.GetTempFileName();
                                            attachment.SaveAsFile(tempPath);
                                            if (attachmentExtension.Equals(".xls") || attachmentExtension.Equals(".xlsx"))
                                            {
                                                ExcelHelper excelHelper = new ExcelHelper();
                                                messageSentDate = excelHelper.GetOvernightsDate(tempPath);
                                            }
                                            BusinessClasses.CalendarYear year = _calendar.Years.Where(x => x.Year.Equals(messageSentDate.Year)).FirstOrDefault();
                                            if (year != null && year.RootFolder.Exists)
                                            {
                                                string filePath = Path.Combine(year.RootFolder.FullName, string.Format("{0}f{1}", new string[] { messageSentDate.ToString("MMddyy"), attachmentExtension }));
                                                File.Copy(tempPath, filePath, true);
                                                File.Delete(tempPath);
                                                message.UnRead = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                                break;
                            }
                        }
                    }
                    Disconnect();
                }
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }
    }
}
