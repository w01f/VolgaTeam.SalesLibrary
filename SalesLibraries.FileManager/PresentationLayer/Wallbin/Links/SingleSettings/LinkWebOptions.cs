using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(WebLink))]
	//public sealed partial class LinkWebOptions : UserControl, ILinkProperties
	public sealed partial class LinkWebOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly WebLink _data;

		public LinkSettingsType SettingsType
		{
			get { return LinkSettingsType.Notes; }
		}
		public int Order
		{
			get { return 2; }
		}
		public bool AvailableForEmbedded
		{
			get { return true; }
		}
		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkWebOptions(WebLink data)
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
			}
		}

		public void LoadData()
		{
			ckIsUrl365.Checked = ((WebLinkSettings)_data.Settings).IsUrl365;
			ckForcePreview.Checked = ((WebLinkSettings)_data.Settings).ForcePreview;
		}

		public void SaveData()
		{
			((WebLinkSettings)_data.Settings).IsUrl365 = ckIsUrl365.Checked;
			((WebLinkSettings)_data.Settings).ForcePreview = ckForcePreview.Checked;
		}
	}
}
