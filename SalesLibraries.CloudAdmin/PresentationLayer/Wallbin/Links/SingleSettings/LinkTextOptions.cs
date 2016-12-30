using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class LinkTextOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkTextOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes };
		public int Order => 2;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkTextOptions(LibraryObjectLink data)
		{
			InitializeComponent();
			Text = "Text Format";
			_data = data;

			buttonEditLinkSpecialFont.ButtonClick += EditorHelper.FontEdit_ButtonClick;
			buttonEditLinkSpecialFont.Click += EditorHelper.FontEdit_Click;

			if ((CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
			}
		}

		public void LoadData()
		{
			var regularFontStyle = ((LibraryObjectLinkSettings)_data.Settings).RegularFontStyle;
			checkEditBold.Checked = (regularFontStyle & FontStyle.Bold) == FontStyle.Bold;
			checkEditItalic.Checked = (regularFontStyle & FontStyle.Italic) == FontStyle.Italic;
			checkEditUnderlined.Checked = (regularFontStyle & FontStyle.Underline) == FontStyle.Underline;
			checkEditSpecialFormat.Checked = ((LibraryObjectLinkSettings)_data.Settings).IsSpecialFormat;
			buttonEditLinkSpecialFont.EditValue = _data.Settings.Font != null ? Utils.FontToString(_data.Settings.Font) : null;
			buttonEditLinkSpecialFont.Tag = _data.Settings.Font;
			colorEditLinkSpecialColor.Color = _data.DisplayColor;
			checkEditTextWordWrap.Checked = _data.Settings.TextWordWrap;
			checkEditFakeDate.Checked = ((LibraryObjectLinkSettings)_data.Settings).FakeFileDate.HasValue;
			dateEditFakeDate.EditValue = ((LibraryObjectLinkSettings)_data.Settings).FakeFileDate;
		}

		public void SaveData()
		{
			var regulraStyle = FontStyle.Regular;
			if (checkEditBold.Checked)
				regulraStyle = regulraStyle | FontStyle.Bold;
			if (checkEditItalic.Checked)
				regulraStyle = regulraStyle | FontStyle.Italic;
			if (checkEditUnderlined.Checked)
				regulraStyle = regulraStyle | FontStyle.Underline;
			((LibraryObjectLinkSettings)_data.Settings).RegularFontStyle = regulraStyle;
			((LibraryObjectLinkSettings)_data.Settings).IsSpecialFormat = checkEditSpecialFormat.Checked;
			if (((LibraryObjectLinkSettings)_data.Settings).IsSpecialFormat)
				_data.Settings.Font = buttonEditLinkSpecialFont.Tag as Font;
			else
				_data.Settings.Font = null;
			if (_data.DisplayColor != colorEditLinkSpecialColor.Color)
				_data.Settings.ForeColor = colorEditLinkSpecialColor.Color;
			_data.Settings.TextWordWrap = checkEditTextWordWrap.Checked;
			((LibraryObjectLinkSettings)_data.Settings).FakeFileDate = checkEditFakeDate.Checked &&
														 dateEditFakeDate.EditValue != null
				? (DateTime?)dateEditFakeDate.EditValue
				: null;
		}

		private void checkEditSpecialFormat_CheckedChanged(object sender, EventArgs e)
		{
			buttonEditLinkSpecialFont.Enabled = checkEditSpecialFormat.Checked;
			if (checkEditSpecialFormat.Checked)
			{
				checkEditBold.Checked = false;
				checkEditItalic.Checked = false;
				checkEditUnderlined.Checked = false;
			}
		}

		private void checkEditRegularStyle_CheckedChanged(object sender, EventArgs e)
		{
			if (checkEditBold.Checked ||
				checkEditItalic.Checked ||
				checkEditUnderlined.Checked)
				checkEditSpecialFormat.Checked = false;
		}

		private void checkEditFakeDate_CheckedChanged(object sender, EventArgs e)
		{
			dateEditFakeDate.Visible = checkEditFakeDate.Checked;
		}
	}
}
