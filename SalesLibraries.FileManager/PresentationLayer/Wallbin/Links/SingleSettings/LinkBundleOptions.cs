using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LinkBundleLink))]
	//public partial class LinkBundleOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkBundleOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private LinkBundleLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkBundleOptions()
		{
			InitializeComponent();
			Text = "Admin";
			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
			}
		}

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (LinkBundleLink)sourceLink;

			var webFormat = ((LinkBundleLinkSettings)_data.Settings).CustomWebFormat;
			switch (webFormat)
			{
				case WebFormats.PowerPoint:
					checkEditPowerPoint.Checked = true;
					break;
				case WebFormats.Video:
					checkEditVideo.Checked = true;
					break;
				case WebFormats.Pdf:
					checkEditPdf.Checked = true;
					break;
				case WebFormats.Word:
					checkEditWord.Checked = true;
					break;
				case WebFormats.Excel:
					checkEditExcel.Checked = true;
					break;
				case WebFormats.Url:
					checkEditUrl.Checked = true;
					break;
				case WebFormats.Png:
					checkEditPng.Checked = true;
					break;
				case WebFormats.Jpeg:
					checkEditJpeg.Checked = true;
					break;
			}
		}

		public void SaveData()
		{
			var webFormat = WebFormats.PowerPoint;
			if (checkEditPowerPoint.Checked)
				webFormat = WebFormats.PowerPoint;
			else if (checkEditVideo.Checked)
				webFormat = WebFormats.Video;
			else if (checkEditPdf.Checked)
				webFormat = WebFormats.Pdf;
			else if (checkEditWord.Checked)
				webFormat = WebFormats.Word;
			else if (checkEditExcel.Checked)
				webFormat = WebFormats.Excel;
			else if (checkEditUrl.Checked)
				webFormat = WebFormats.Url;
			else if (checkEditPng.Checked)
				webFormat = WebFormats.Png;
			else if (checkEditJpeg.Checked)
				webFormat = WebFormats.Jpeg;

			((LinkBundleLinkSettings)_data.Settings).CustomWebFormat = webFormat;
		}
	}
}
