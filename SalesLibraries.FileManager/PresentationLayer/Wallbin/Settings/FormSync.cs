using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormSync : MetroForm
	{
		public Library Library { get; set; }

		public FormSync()
		{
			InitializeComponent();

			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void Form_Load(object sender, EventArgs e)
		{
			checkEditMinimizeOnSync.Checked = Library.SyncSettings.MinimizeOnSync;
			checkEditCloseAfterSync.Checked = Library.SyncSettings.CloseAfterSync;
		}

		private void FormSync_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			Library.SyncSettings.MinimizeOnSync = checkEditMinimizeOnSync.Checked;
			Library.SyncSettings.CloseAfterSync = checkEditCloseAfterSync.Checked;
		}
	}
}