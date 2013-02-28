using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	[ToolboxItem(false)]
	public sealed partial class ActivitiesManagerControl : UserControl
	{
		private readonly List<UserActivity> _activities = new List<UserActivity>();

		public ActivitiesManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			var now = DateTime.Now;
			dateEditStart.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
			now = now.AddDays(1);
			dateEditEnd.DateTime = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
		}

		public void RefreshData(bool showMessages)
		{
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading Activities...";
					form.TopMost = true;
					form.Show();
					Application.DoEvents();
					UpdateActivities(false, ref message);
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
				UpdateActivities(showMessages, ref message);
		}

		public void ClearData()
		{
			gridControlActivities.DataSource = null;
			_activities.Clear();
		}

		public void UpdateActivities(bool showMessages, ref string updateMessage)
		{
			ClearData();
			var startDate = dateEditStart.DateTime;
			var endDate = dateEditEnd.DateTime;
			string message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading users...";
					form.TopMost = true;
					var thread = new Thread(() => _activities.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetActivities(startDate, endDate, out message)));
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
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() => _activities.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetActivities(startDate, endDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			gridControlActivities.DataSource = _activities;
		}

		private void buttonXApplyFilter_Click(object sender, EventArgs e)
		{
			RefreshData(true);
		}
	}
}