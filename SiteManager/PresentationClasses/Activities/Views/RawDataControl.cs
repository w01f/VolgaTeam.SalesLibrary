﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.ToolForms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.Views
{
	[ToolboxItem(false)]
	public partial class RawDataControl : UserControl, IActivitiesView
	{
		private readonly List<UserActivity> _activities = new List<UserActivity>();
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		private bool _active;
		public bool Active
		{
			get { return _active; }
			set
			{
				_active = value;
				Visible = _active;
			}
		}

		public Control FilterControl
		{
			get { return null; }
		}

		public RawDataControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void ShowView()
		{
			Active = true;
			BringToFront();
		}

		public void UpdateData(bool showMessages, ref string updateMessage)
		{
			ClearData();
			if (!Active) return;
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() => _activities.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetActivities(StartDate, EndDate, out message)));
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
				var thread = new Thread(() => _activities.AddRange(BusinessClasses.SiteManager.Instance.SelectedSite.GetActivities(StartDate, EndDate, out message)));
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			updateMessage = message;
			gridControlData.DataSource = _activities;
		}

		public void ClearData()
		{
			gridControlData.DataSource = null;
			_activities.Clear();
		}
	}
}