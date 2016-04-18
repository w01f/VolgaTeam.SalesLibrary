using System;
using System.Drawing;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	//public partial class LinkExcelOptions : UserControl, ILinkProperties
	[IntendForClass(typeof(ExcelLink))]
	public sealed partial class LinkExcelOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly ExcelLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 2;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkExcelOptions(ExcelLink data)
		{
			InitializeComponent();
			Text = "Advanced";
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
			ckDoNotGenerateText.Checked = !((ExcelLinkSettings)_data.Settings).GenerateContentText;
		}

		public void SaveData()
		{
			((ExcelLinkSettings)_data.Settings).GenerateContentText = !ckDoNotGenerateText.Checked;
		}
	}
}
