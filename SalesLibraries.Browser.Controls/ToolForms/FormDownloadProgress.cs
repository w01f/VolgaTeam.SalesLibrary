using System;
using System.Windows.Forms;

namespace SalesLibraries.Browser.Controls.ToolForms
{
	public partial class FormDownloadProgress : Form
	{
		private static FormDownloadProgress _instance = new FormDownloadProgress();

		private FormDownloadProgress()
		{
			InitializeComponent();
		}

		public static void ShowProgress(Form parent)
		{
			if (_instance == null)
				_instance = new FormDownloadProgress();
			_instance.Left = parent.Left + (parent.Width - _instance.Width - 20);
			_instance.Top = parent.Top + (parent.Height - _instance.Height - 20);
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
				{
					_instance.Show();
					Application.DoEvents();
				}));
			else
				_instance.Show();
			Application.DoEvents();
		}

		public static void CloseProgress()
		{
			if (_instance == null) return;
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
				{
					_instance.Close();
					_instance = null;
					Application.DoEvents();
				}));
			else
			{
				_instance.Close();
				_instance = null;
			}
			Application.DoEvents();
		}

		public static void SetTitle(string text)
		{
			if (_instance == null)
				_instance = new FormDownloadProgress();
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
				{
					_instance.laTitle.Text = text;
					Application.DoEvents();
					SetDetails(String.Empty);
					Application.DoEvents();
				}));
			else
			{
				_instance.laTitle.Text = text;
				Application.DoEvents();
				SetDetails(String.Empty);
				Application.DoEvents();
			}
		}

		public static void SetDetails(string text)
		{
			if (_instance == null)
				_instance = new FormDownloadProgress();
			if (_instance.InvokeRequired)
				_instance.BeginInvoke(new MethodInvoker(() =>
			{
				_instance.laDetails.Text = text;
				if (!String.IsNullOrEmpty(text))
					_instance.laDetails.BringToFront();
				else
					_instance.laDetails.SendToBack();
				Application.DoEvents();
			}));
			else
			{
				_instance.laDetails.Text = text;
				if (!String.IsNullOrEmpty(text))
					_instance.laDetails.BringToFront();
				else
					_instance.laDetails.SendToBack();
				Application.DoEvents();
			}
		}

		private void OnFormShown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}
	}
}