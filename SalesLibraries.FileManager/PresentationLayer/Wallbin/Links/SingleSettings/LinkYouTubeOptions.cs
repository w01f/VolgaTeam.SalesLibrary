﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(YouTubeLink))]
	//public sealed partial class LinkYouTubeOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkYouTubeOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private YouTubeLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>YouTube</size>", Logo = Resources.LinkAddYoutube };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkYouTubeOptions()
		{
			InitializeComponent();
			Text = "Admin";
			
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
				ckForcePreview.Font = new Font(ckForcePreview.Font.FontFamily, ckForcePreview.Font.Size - 2, ckForcePreview.Font.Style);
			}
		}

		public LinkYouTubeOptions(FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (YouTubeLink)sourceLink;

			textEditName.EditValue = _data.Name;
			textEditPath.EditValue = _data.RelativePath;
			ckForcePreview.Checked = ((HyperLinkSettings)_data.Settings).ForcePreview;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			_data.RelativePath = textEditPath.EditValue as String;
			((HyperLinkSettings)_data.Settings).ForcePreview = ckForcePreview.Checked;
		}
	}
}
