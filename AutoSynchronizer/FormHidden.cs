using System;
using System.Windows.Forms;
using SalesDepot.CoreObjects.ToolClasses;

namespace AutoSynchronizer
{
	public partial class FormHidden : Form
	{
		private static FormHidden _instance = null;
		private FormProgressSyncFilesRegular _formSyncRegular = null;
		private FormProgressSyncFilesIPad _formSyncIpad = null;

		public static FormHidden Instance
		{
			get
			{
				if (_instance == null)
					_instance = new FormHidden();
				return _instance;
			}
		}

		private FormHidden()
		{
			InitializeComponent();
		}

		public void ShowSyncProgressRegular()
		{
			_formSyncRegular = new FormProgressSyncFilesRegular();
			_formSyncRegular.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
			{
				Globals.ThreadAborted = true;
			});
			_formSyncRegular.Show();
		}

		public void HideSyncProgressRegular()
		{
			_formSyncRegular.Close();
			_formSyncRegular = null;
		}

		public void ShowSyncProgressIpad()
		{
			_formSyncIpad = new FormProgressSyncFilesIPad();
			_formSyncIpad.ProcessAborted += new EventHandler<EventArgs>((progressSender, progressE) =>
			{
				Globals.ThreadAborted = true;
			});
			_formSyncIpad.Show();
		}

		public void HideSyncProgressIpad()
		{
			_formSyncIpad.Close();
			_formSyncIpad = null;
		}
	}
}
