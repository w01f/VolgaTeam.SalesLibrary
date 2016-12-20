using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(ExcelLink))]
	//public partial class LinkExcelOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkExcelOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly ExcelLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkExcelOptions(ExcelLink data)
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

				ckIsArchiveResource.Font = new Font(ckIsArchiveResource.Font.FontFamily, ckIsArchiveResource.Font.Size - 2, ckIsArchiveResource.Font.Style);
				ckDoNotGenerateText.Font = new Font(ckDoNotGenerateText.Font.FontFamily, ckDoNotGenerateText.Font.Size - 2, ckDoNotGenerateText.Font.Style);
				ckForceDownload.Font = new Font(ckForceDownload.Font.FontFamily, ckForceDownload.Font.Size - 2, ckForceDownload.Font.Style);
				ckForceOpen.Font = new Font(ckForceOpen.Font.FontFamily, ckForceOpen.Font.Size - 2, ckForceOpen.Font.Style);
			}
		}

		public void LoadData()
		{
			ckIsArchiveResource.Checked = ((ExcelLinkSettings)_data.Settings).IsArchiveResource;
			ckDoNotGenerateText.Checked = !((ExcelLinkSettings)_data.Settings).GenerateContentText;
			ckForceDownload.Checked = ((ExcelLinkSettings)_data.Settings).ForceDownload;
			ckForceOpen.Checked = ((ExcelLinkSettings)_data.Settings).ForceOpen;
		}

		public void SaveData()
		{
			((ExcelLinkSettings)_data.Settings).IsArchiveResource = ckIsArchiveResource.Checked;
			if (ckIsArchiveResource.Checked)
			{
				((ExcelLinkSettings)_data.Settings).GenerateContentText = false;
				((ExcelLinkSettings)_data.Settings).ForceDownload = true;
			}
			else
			{
				((ExcelLinkSettings)_data.Settings).GenerateContentText = !ckDoNotGenerateText.Checked;
				((ExcelLinkSettings)_data.Settings).ForceDownload = ckForceDownload.Checked;
			}
			((ExcelLinkSettings)_data.Settings).ForceOpen = ckForceOpen.Checked;
		}

		private void ckIsArchiveResource_CheckedChanged(object sender, EventArgs e)
		{
			ckDoNotGenerateText.Enabled =
				ckForceDownload.Enabled = !ckIsArchiveResource.Checked;
			if (ckIsArchiveResource.Checked)
			{
				ckDoNotGenerateText.Checked =
					ckForceDownload.Checked = true;
			}
		}
	}
}
