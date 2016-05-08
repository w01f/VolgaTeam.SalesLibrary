using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	//public partial class LineBreakOptions : UserControl, ILinkProperties
	[IntendForClass(typeof(LineBreak))]
	public partial class LineBreakOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LineBreak _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LineBreakOptions(LineBreak data)
		{
			InitializeComponent();
			_data = data;

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
			buttonEditLineBreakFont.Tag = _data.Settings.Font;
			buttonEditLineBreakFont.EditValue = Utils.FontToString(_data.Settings.Font);
			colorEditLineBreakFontColor.Color = _data.DisplayColor;
			memoEditNote.EditValue = _data.Settings.Note;
		}

		public void SaveData()
		{
			_data.Settings.Font = buttonEditLineBreakFont.Tag as Font;
			if (colorEditLineBreakFontColor.Color != _data.DisplayColor)
				_data.Settings.ForeColor = colorEditLineBreakFontColor.Color;
			_data.Settings.Note = memoEditNote.EditValue != null ? memoEditNote.EditValue.ToString().Trim() : string.Empty;
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
			FontEdit_Click(this, null);
		}
	}
}
