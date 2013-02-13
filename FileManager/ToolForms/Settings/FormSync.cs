using System;
using System.Windows.Forms;
using FileManager.Controllers;

namespace FileManager.ToolForms.Settings
{
	public partial class FormSync : Form
	{
		public FormSync()
		{
			InitializeComponent();
		}

		private void Form_Load(object sender, EventArgs e)
		{
			ckMinimizeOnSync.Checked = MainController.Instance.ActiveDecorator.Library.MinimizeOnSync;
			ckCloseAfterSync.Checked = MainController.Instance.ActiveDecorator.Library.CloseAfterSync;
			ckShowSyncStatus.Checked = MainController.Instance.ActiveDecorator.Library.ShowProgressDuringSync;
			ckFullSync.Checked = MainController.Instance.ActiveDecorator.Library.FullSync;
			ckVideoConversionWarning.Checked = MainController.Instance.ActiveDecorator.Library.VideoConversionWarning;
		}

		private void FormSync_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				MainController.Instance.ActiveDecorator.Library.MinimizeOnSync = ckMinimizeOnSync.Checked;
				MainController.Instance.ActiveDecorator.Library.CloseAfterSync = ckCloseAfterSync.Checked;
				MainController.Instance.ActiveDecorator.Library.ShowProgressDuringSync = ckShowSyncStatus.Checked;
				MainController.Instance.ActiveDecorator.Library.FullSync = ckFullSync.Checked;
				MainController.Instance.ActiveDecorator.Library.VideoConversionWarning = ckVideoConversionWarning.Checked;
				MainController.Instance.ActiveDecorator.Library.Save();
			}
		}
	}
}