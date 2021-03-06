﻿using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public partial class FormProgressWithAbort : FormProgressBase
	{
		private readonly Timer _timer = new Timer();
		private int _ticks;
		public override string Title
		{
			get => laTitle.Text;
			set => laTitle.Text = value;
		}

		public FormProgressWithAbort()
		{
			InitializeComponent();
			Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 20;
			Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 20;

			pbCancel.Buttonize();

			_timer.Interval = 1000;
			_timer.Tick += (timerSender, timerE) =>
			{
				_ticks++;
				var hours = _ticks / 3600;
				var minutes = (_ticks - (hours * 3600)) / 60;
				var seconds = _ticks - (hours * 3600) - (minutes * 60);
				Invoke(new MethodInvoker(() =>
				{
					laTime.Text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
					Application.DoEvents();
				}));
			};

			Closing += OnFormClosing;

			InitForm();
		}

		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_timer.Stop();
		}

		protected virtual void InitForm() { }

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
			_ticks = 0;
			_timer.Start();
		}

		private void pbCancel_Click(object sender, EventArgs e)
		{
			Title = "Aborting process...";
			AbortProcess();
		}
	}
}