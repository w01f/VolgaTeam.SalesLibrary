using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Utilities
{
	[ToolboxItem(false)]
	public partial class UtilitiesManagerControl : UserControl
	{
		public UtilitiesManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void ClearData()
		{
			memoEditResult.EditValue = null;
		}

		private void simpleButtonUpdateContent_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Updating Data...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessClasses.WebSiteManager.Instance.SelectedSite.UpdateContent(out message));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}
			if (!String.IsNullOrEmpty(message))
				memoEditResult.EditValue = message.Replace("\n", "\r\n");
		}

		private void simpleButtonUpdateHelp_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Updating Help...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessClasses.WebSiteManager.Instance.SelectedSite.UpdateHelp(out message));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}
			if (!String.IsNullOrEmpty(message))
				memoEditResult.EditValue = message.Replace("\n", "\r\n");
		}

		private void simpleButtonCleanExpiredEmails_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Cleaning Expired Emails...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessClasses.WebSiteManager.Instance.SelectedSite.CleanExpiredEmails(out message));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}
			if (!String.IsNullOrEmpty(message))
				memoEditResult.EditValue = message.Replace("\n", "\r\n");
		}

		private void simpleButtonProcessDeadLinks_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Processing Dead Links...";
				form.TopMost = true;
				var thread = new Thread(() => BusinessClasses.WebSiteManager.Instance.SelectedSite.NotifyDeadLinks(out message));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}
			if (!String.IsNullOrEmpty(message))
				memoEditResult.EditValue = message.Replace("\n", "\r\n");
		}
	}
}