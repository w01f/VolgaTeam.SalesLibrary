using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Office.Interop.Outlook;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.OfficeInterops
{
	public class OutlookHelper
	{
		private static readonly OutlookHelper _instance = new OutlookHelper();

		private OutlookHelper() { }

		public static OutlookHelper Instance
		{
			get
			{
				return _instance;
			}
		}

		private Application _outlookObject;

		public bool Connect()
		{
			try
			{
				_outlookObject =
					System.Runtime.InteropServices.Marshal.GetActiveObject("Outlook.Application") as Application;
			}
			catch
			{
				_outlookObject = null;
			}
			if (_outlookObject == null)
			{
				try
				{
					_outlookObject = new Application();
					var folder = (_outlookObject.GetNamespace("MAPI")).GetDefaultFolder(OlDefaultFolders.olFolderInbox);
					folder.Display();
					_outlookObject.Explorers.Add(folder, OlFolderDisplayMode.olFolderDisplayNormal);
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
			_outlookObject = null;
		}

		public void CreateMessage(IEnumerable<string> adresses, string body, bool sendMessage)
		{
			try
			{
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				mi.Subject = "Sales Library Files Have EXPIRED!";
				foreach (var adress in adresses)
					mi.Recipients.Add(adress);
				mi.Body = "Below are the Sales Library Links that are Currently Expired:" + Environment.NewLine + Environment.NewLine + body;
				if (sendMessage)
					mi.Send();
				else
					mi.Display(new object());
				_outlookObject = null;
			}
			catch { }
		}

		public void CreateMessage(string subject, IEnumerable<string> attachmentPaths)
		{
			try
			{
				var handle = IntPtr.Zero;
				var processList = Process.GetProcesses();
				foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("outlook")))
				{
					if (process.MainWindowHandle.ToInt32() != 0)
					{
						handle = process.MainWindowHandle;
						break;
					}
				}
				Utils.ActivateForm(handle, true, false);
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				foreach (var attachmentPath in attachmentPaths)
					mi.Attachments.Add(attachmentPath, OlAttachmentType.olByValue, 1, "Attachment");
				mi.Subject = subject;
				mi.Display();
				var count = 100000;
				handle = IntPtr.Zero;
				while (handle == IntPtr.Zero && count > 0)
				{
					handle = WinAPIHelper.FindWindow(string.Empty, subject + "- Message (HTML)");
					count--;
					System.Windows.Forms.Application.DoEvents();
				}
				if (handle != IntPtr.Zero)
					Utils.ActivateForm(handle, true, true);
			}
			catch { }
		}
	}
}
