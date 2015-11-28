using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Sync
{
	public partial class FormSyncLock : MetroForm
	{
		public FormSyncLock(int noTags, int inactive, int noVideo)
		{
			InitializeComponent();

			var info = new List<string>();
			if (noTags > 0)
				info.Add(String.Format("<b><u>{0}</u></b> Links NEED Tags", noTags));
			if (inactive > 0)
				info.Add(String.Format("<b><u>{0}</u></b> Links are INACTIVE and need to be DELETED", inactive));
			if (noVideo > 0)
				info.Add(String.Format("<b><u>{0}</u></b> Videos NEED to be CONVERTED", noVideo));
			labelControlContent.Text = String.Join("<br><br>", info);

			labelControlContacts.Text = String.Format(labelControlContacts.Text, "127.0.0.1", MainController.Instance.Settings.SyncSupportEmail);
		}

		private void FormSyncLock_Shown(object sender, EventArgs e)
		{
			labelControlTitle.BackColor =
			labelControlContent.BackColor =
			labelControlContacts.BackColor =
			BackColor = Color.Red;

			labelControlTitle.ForeColor =
			labelControlContent.ForeColor =
			labelControlContacts.ForeColor =
			pnButtonBorder.BackColor =
			simpleButtonOK.ForeColor = Color.White;
		}

		private void labelControlContacts_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
		{
			Process.Start(String.Format("mailto:{0}", MainController.Instance.Settings.SyncSupportEmail));
		}

		private void labelCaption_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			WinAPIHelper.ReleaseCapture();
			WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
		}
	}
}