using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(PowerPointLink))]
	//public partial class PowerPointPreviewOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class PowerPointPreviewOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly PowerPointLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 3;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public PowerPointPreviewOptions(PowerPointLink data)
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
			buttonXOpenQV.Enabled = MainController.Instance.Settings.EnableLocalSync &&
				Directory.Exists(((PowerPointLinkSettings)_data.Settings).ContainerPath);
			buttonXOpenWV.Enabled = Directory.Exists(_data.PreviewContainerPath);
		}

		public void SaveData()
		{
		}

		private void buttonXRefreshPreview_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure want to delete preview files for the link?") != DialogResult.Yes) return;
			_data.ClearPreviewContainer();
			((PowerPointLinkSettings)_data.Settings).ClearQuickViewContent();
			MainController.Instance.PopupMessages.ShowInfo("Library files will refresh when you sync your library.");
		}

		private void buttonXOpenQV_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(((PowerPointLinkSettings)_data.Settings).ContainerPath);
			}
			catch { }
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
