using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class LinkAdminOptions : UserControl, ILinkProperties
	public sealed partial class LinkAdminOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LinkAdminOptions(LibraryLink data)
		{
			InitializeComponent();
			Text = "Admin";
			_data = data;
			LoadData();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
				labelControlTitle.Font = new Font(labelControlTitle.Font.FontFamily, labelControlTitle.Font.Size - 2, labelControlTitle.Font.Style);
			}
		}

		private void LoadData()
		{
			buttonXOpenQV.Enabled = _data.PreviewContainer != null && Directory.Exists(_data.PreviewContainer.ContainerPath);
			buttonXOpenWV.Enabled = _data.UniversalPreviewContainer != null && Directory.Exists(_data.UniversalPreviewContainer.ContainerPath);
		}

		public void SaveData()
		{
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure want to delete preview files for the link?") != DialogResult.Yes) return;
			if (_data.PreviewContainer != null)
				_data.PreviewContainer.ClearContent();
			if (_data.UniversalPreviewContainer != null)
				_data.UniversalPreviewContainer.ClearContent();
			AppManager.Instance.ShowInfo("Preview files for the link was deleted and will re-create during next Sync.");
		}

		private void buttonXOpenQV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.PreviewContainer.ContainerPath);
			}
			catch { }
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.UniversalPreviewContainer.ContainerPath);
			}
			catch { }
		}
	}
}
