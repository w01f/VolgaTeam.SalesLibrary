using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Settings
{
	public partial class FormFileSettings : MetroForm
	{
		public FormFileSettings()
		{
			InitializeComponent();
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void buttonXResetToDefault_Click(object sender, EventArgs e)
		{
			MainController.Instance.Settings.OpenFilePath = null;
			MainController.Instance.Settings.SaveFilePath = null;
			MainController.Instance.Settings.SaveSettings();
			buttonEditOpenFile.EditValue = MainController.Instance.Settings.OpenFilePath;
			buttonEditSaveFile.EditValue = MainController.Instance.Settings.SaveFilePath;
		}

		private void FormFileSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			MainController.Instance.Settings.OpenFilePath = buttonEditOpenFile.EditValue != null ? buttonEditOpenFile.EditValue.ToString() : null;
			MainController.Instance.Settings.SaveFilePath = buttonEditSaveFile.EditValue != null ? buttonEditSaveFile.EditValue.ToString() : null;
			MainController.Instance.Settings.SaveSettings();
		}

		private void FormFileSettings_Load(object sender, EventArgs e)
		{
			buttonEditOpenFile.EditValue = MainController.Instance.Settings.OpenFilePath;
			buttonEditSaveFile.EditValue = MainController.Instance.Settings.SaveFilePath;
		}

		private void buttonEditOpenFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = MainController.Instance.Settings.OpenFilePath;
				if (dialog.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
					buttonEditOpenFile.EditValue = dialog.SelectedPath;
			}
		}

		private void buttonEditSaveFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = MainController.Instance.Settings.SaveFilePath;
				if (dialog.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
					buttonEditSaveFile.EditValue = dialog.SelectedPath;
			}
		}
	}
}
