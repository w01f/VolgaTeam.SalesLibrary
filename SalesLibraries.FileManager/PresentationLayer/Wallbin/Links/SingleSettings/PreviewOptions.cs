using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	//public partial class PreviewOptions : UserControl, ILinkProperties
	[IntendForClass(typeof(DocumentLink))]
	[IntendForClass(typeof(ExcelLink))]
	[IntendForClass(typeof(VideoLink))]
	public sealed partial class PreviewOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly PreviewableLink _data;

		public LinkSettingsType SettingsType
		{
			get { return LinkSettingsType.Notes; }
		}
		public int Order
		{
			get { return 3; }
		}
		public bool AvailableForEmbedded
		{
			get { return false; }
		}
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public PreviewOptions(PreviewableLink data)
		{
			InitializeComponent();
			Text = "Admin";
			_data = data;
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

		public void LoadData()
		{
			buttonXOpenWV.Enabled = Directory.Exists(_data.PreviewContainerPath);
		}

		public void SaveData()
		{
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure want to delete preview files for the link?") != DialogResult.Yes) return;
			_data.ClearPreviewContainer();
			MainController.Instance.PopupMessages.ShowInfo("Library files will refresh when you sync your library.");
		}

		private void buttonXOpenWV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(_data.PreviewContainerPath);
			}
			catch { }
		}
	}
}
