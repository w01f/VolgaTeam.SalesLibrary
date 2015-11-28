using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class LinkTextOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkTextOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LibraryObjectLink _data;

		public LinkSettingsType SettingsType
		{
			get { return LinkSettingsType.Notes; }
		}
		public int Order
		{
			get { return 1; }
		}
		public bool AvailableForEmbedded
		{
			get { return false; }
		}
		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkTextOptions(LibraryObjectLink data)
		{
			InitializeComponent();
			Text = "Text Format";
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
				rbLinkRegularFormat.Font = new Font(rbLinkRegularFormat.Font.FontFamily, rbLinkRegularFormat.Font.Size - 2, rbLinkRegularFormat.Font.Style);
				rbLinkBoldFormat.Font = new Font(rbLinkBoldFormat.Font.FontFamily, rbLinkBoldFormat.Font.Size - 2, rbLinkBoldFormat.Font.Style);
				rbLinkSpecialFormat.Font = new Font(rbLinkSpecialFormat.Font.FontFamily, rbLinkSpecialFormat.Font.Size - 2, rbLinkSpecialFormat.Font.Style);
				labelControlForeColor.Font = new Font(labelControlForeColor.Font.FontFamily, labelControlForeColor.Font.Size - 2, labelControlForeColor.Font.Style);
			}
		}

		public void LoadData()
		{
			rbLinkRegularFormat.Checked = ((LibraryObjectLinkSettings)_data.Settings).IsRegularFormat;
			rbLinkBoldFormat.Checked = ((LibraryObjectLinkSettings)_data.Settings).IsBold;
			rbLinkSpecialFormat.Checked = ((LibraryObjectLinkSettings)_data.Settings).IsSpecialFormat;
			buttonEditLinkSpecialFont.EditValue = _data.Settings.Font != null ? Utils.FontToString(_data.Settings.Font) : null;
			buttonEditLinkSpecialFont.Tag = _data.Settings.Font;
			colorEditLinkSpecialColor.Color = _data.Settings.ForeColor;
		}

		public void SaveData()
		{
			((LibraryObjectLinkSettings)_data.Settings).IsBold = rbLinkBoldFormat.Checked;
			((LibraryObjectLinkSettings)_data.Settings).IsSpecialFormat = rbLinkSpecialFormat.Checked;
			if (((LibraryObjectLinkSettings)_data.Settings).IsSpecialFormat)
			{
				_data.Settings.Font = buttonEditLinkSpecialFont.Tag as Font;
				_data.Settings.ForeColor = colorEditLinkSpecialColor.Color;
			}
			else
			{
				_data.Settings.Font = null;
				_data.Settings.ForeColor = Color.Black;
			}
		}

		private void rbLinkSpecialFormat_CheckedChanged(object sender, EventArgs e)
		{
			buttonEditLinkSpecialFont.Enabled = rbLinkSpecialFormat.Checked;
			labelControlForeColor.Enabled = rbLinkSpecialFormat.Checked;
			colorEditLinkSpecialColor.Enabled = rbLinkSpecialFormat.Checked;
		}

		private void FontEdit_Click(object sender, EventArgs e)
		{
			var fontEdit = sender as ButtonEdit;
			if (fontEdit == null) return;
			using (var dlgFont = new FontDialog())
			{
				dlgFont.Font = fontEdit.Tag as Font;
				if (dlgFont.ShowDialog() != DialogResult.OK) return;
				fontEdit.Tag = dlgFont.Font;
				fontEdit.EditValue = Utils.FontToString(dlgFont.Font);
			}
		}

		private void FontEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			FontEdit_Click(sender, null);
		}
	}
}
