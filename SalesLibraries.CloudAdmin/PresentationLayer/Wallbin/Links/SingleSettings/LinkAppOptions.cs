using System;
using System.Drawing;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.CloudAdmin.Properties;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(AppLink))]
	//public sealed partial class LinkAppOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkAppOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly AppLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 2;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "App", Logo = Resources.LinkAddApp };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkAppOptions(AppLink data)
		{
			InitializeComponent();
			Text = "Advanced";
			_data = data;
			if ((CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
				labelControlTitle.Font = new Font(labelControlTitle.Font.FontFamily, labelControlTitle.Font.Size - 2, labelControlTitle.Font.Style);
				labelControlName.Font = new Font(labelControlName.Font.FontFamily, labelControlName.Font.Size - 2, labelControlName.Font.Style);
				labelControlPath.Font = new Font(labelControlPath.Font.FontFamily, labelControlPath.Font.Size - 2, labelControlPath.Font.Style);
				labelControlSecondPath.Font = new Font(labelControlSecondPath.Font.FontFamily, labelControlSecondPath.Font.Size - 2, labelControlSecondPath.Font.Style);
			}
		}

		public void LoadData()
		{
			textEditName.EditValue = _data.Name;
			textEditPath.EditValue = _data.RelativePath;
			textEditSecondPath.EditValue = ((AppLinkSettings)_data.Settings).SecondPath;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			_data.RelativePath = textEditPath.EditValue as String;
			((AppLinkSettings)_data.Settings).SecondPath = textEditSecondPath.EditValue as String;
		}
	}
}
