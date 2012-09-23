using System;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SalesDepot.CoreObjects.InteropClasses
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

        private Outlook.Application outlookObject;

        public bool Connect()
        {
            try
            {
                outlookObject =
                    System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application") as Outlook.Application;
            }
            catch
            {
                outlookObject = null;
            }
            if (outlookObject == null)
            {
                try
                {
                    outlookObject = new Outlook.Application();
                    Outlook.MAPIFolder folder = (outlookObject.GetNamespace("MAPI")).GetDefaultFolder(Microsoft.Office.Interop.Outlook.OlDefaultFolders.olFolderInbox);
                    folder.Display();
                    outlookObject.Explorers.Add(folder, Microsoft.Office.Interop.Outlook.OlFolderDisplayMode.olFolderDisplayNormal);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public void Disconnect()
        {
            outlookObject = null;
        }

        public void CreateMessage(string[] adresses, string body, bool sendMessage)
        {
            try
            {
                Outlook.MailItem mi = (Outlook.MailItem)outlookObject.CreateItem(Outlook.OlItemType.olMailItem);
                mi.Subject = "Sales Library Files Have EXPIRED!";
                foreach (string adress in adresses)
                    mi.Recipients.Add(adress);
                mi.Body = "Below are the Sales Library Links that are Currently Expired:" + Environment.NewLine + Environment.NewLine + body;
                if (sendMessage)
                    (mi as Outlook._MailItem).Send();
                else
                    mi.Display(new object());
                outlookObject = null;
            }
            catch
            {
            }
        }
    }
}

