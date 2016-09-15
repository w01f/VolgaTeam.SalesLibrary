using System;
using System.Drawing;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(DocumentLink))]
	[IntendForClass(typeof(PowerPointLink))]
	//public partial class LinkDocumentOptions : UserControl, ILinkProperties
	public sealed partial class LinkDocumentOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly DocumentLinkSettings _settings;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkDocumentOptions()
		{
			InitializeComponent();
			Text = "Advanced";
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

		public LinkDocumentOptions(DocumentLink data) : this()
		{
			_settings = (DocumentLinkSettings)data.Settings;
		}

		public LinkDocumentOptions(PowerPointLink data) : this()
		{
			_settings = (DocumentLinkSettings)data.Settings;
		}

		public void LoadData()
		{
			ckDoNotGeneratePreview.Checked = !_settings.GeneratePreviewImages;
			ckDoNotGenerateText.Checked = !_settings.GenerateContentText;
		}

		public void SaveData()
		{
			_settings.GeneratePreviewImages = !ckDoNotGeneratePreview.Checked;
			_settings.GenerateContentText = !ckDoNotGenerateText.Checked;
		}
	}
}
