using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTab;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class LineBreakOptions : UserControl, ILinkProperties
	public partial class LineBreakOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LineBreakOptions(LibraryLink data)
		{
			InitializeComponent();
			_data = data;
			LoadData();

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

		private void LoadData()
		{
			buttonEditLineBreakFont.Tag = _data.LineBreakProperties.Font;
			buttonEditLineBreakFont.EditValue = Utils.FontToString(_data.LineBreakProperties.Font);
			colorEditLineBreakFontColor.Color = _data.LineBreakProperties.ForeColor;
			memoEditNote.EditValue = _data.LineBreakProperties.Note;
		}

		public void SaveData()
		{
			_data.LineBreakProperties.Font = buttonEditLineBreakFont.Tag as Font;
			_data.LineBreakProperties.ForeColor = colorEditLineBreakFontColor.Color;
			_data.LineBreakProperties.Note = memoEditNote.EditValue != null ? memoEditNote.EditValue.ToString().Trim() : string.Empty;
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
