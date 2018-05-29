using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.Utilities
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
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.UpdateContent(out message));
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

		private void simpleButtonUpdateShorcuts_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Updating Shortcuts...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.UpdateShortcuts(out message));
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

		private void simpleButtonUpdateQuizzes_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Updating Quizzes...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.UpdateQuizzes(out message));
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

		private void simpleButtonResetOpCache_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Resetting Cache...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.ResetOpCache(out message));
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

		private void simpleButtonResetQueryDataCache_Click(object sender, EventArgs e)
		{
			string message = string.Empty;
			memoEditResult.EditValue = null;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Resetting Snapshots...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.ResetQueruyDataCache(out message));
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