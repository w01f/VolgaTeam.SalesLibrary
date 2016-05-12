using System;
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
	[IntendForClass(typeof(InternalLink))]
	//public sealed partial class LinkInternalOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly InternalLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 2;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "Internal Link", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalOptions(InternalLink data)
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
				labelControlLibraryName.Font = new Font(labelControlLibraryName.Font.FontFamily, labelControlLibraryName.Font.Size - 2, labelControlLibraryName.Font.Style);
				labelControlPageName.Font = new Font(labelControlPageName.Font.FontFamily, labelControlPageName.Font.Size - 2, labelControlPageName.Font.Style);
				labelControlWindowName.Font = new Font(labelControlWindowName.Font.FontFamily, labelControlWindowName.Font.Size - 2, labelControlWindowName.Font.Style);
				labelControlLinkName.Font = new Font(labelControlLinkName.Font.FontFamily, labelControlLinkName.Font.Size - 2, labelControlLinkName.Font.Style);
				ckForcePreview.Font = new Font(ckForcePreview.Font.FontFamily, ckForcePreview.Font.Size - 2, ckForcePreview.Font.Style);
			}
		}

		public void LoadData()
		{
			textEditName.EditValue = _data.Name;
			textEditLibraryName.EditValue = ((InternalLinkSettings)_data.Settings).LibraryName;
			textEditPageName.EditValue = ((InternalLinkSettings)_data.Settings).PageName;
			textEditWindowName.EditValue = ((InternalLinkSettings)_data.Settings).WindowName;
			textEditLinkName.EditValue = ((InternalLinkSettings)_data.Settings).LinkName;
			ckForcePreview.Checked = ((InternalLinkSettings) _data.Settings).ForcePreview;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalLinkSettings)_data.Settings).LibraryName = textEditLibraryName.EditValue as String;
			((InternalLinkSettings)_data.Settings).PageName = textEditPageName.EditValue as String;
			((InternalLinkSettings)_data.Settings).WindowName = textEditWindowName.EditValue as String;
			((InternalLinkSettings)_data.Settings).LinkName = textEditLinkName.EditValue as String;
			((InternalLinkSettings)_data.Settings).ForcePreview = ckForcePreview.Checked;
		}
	}
}
