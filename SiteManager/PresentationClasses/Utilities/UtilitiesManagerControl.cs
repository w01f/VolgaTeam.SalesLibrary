﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.CommonGUI.Forms;

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
				var thread = new Thread(() => BusinessClasses.WebSiteManager.Instance.SelectedSite.UpdateShortcuts(out message));
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
				var thread = new Thread(() => BusinessClasses.WebSiteManager.Instance.SelectedSite.UpdateQuizzes(out message));
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