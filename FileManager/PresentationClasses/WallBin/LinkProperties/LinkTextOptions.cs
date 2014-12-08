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
	//public partial class LinkTextOptions : UserControl, ILinkProperties
	public sealed partial class LinkTextOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LinkTextOptions(LibraryLink data)
		{
			InitializeComponent();
			Text = "Text Format";
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
				labelControlTitle.Font = new Font(labelControlTitle.Font.FontFamily, labelControlTitle.Font.Size - 2, labelControlTitle.Font.Style);
				rbLinkRegularFormat.Font = new Font(rbLinkRegularFormat.Font.FontFamily, rbLinkRegularFormat.Font.Size - 2, rbLinkRegularFormat.Font.Style);
				rbLinkBoldFormat.Font = new Font(rbLinkBoldFormat.Font.FontFamily, rbLinkBoldFormat.Font.Size - 2, rbLinkBoldFormat.Font.Style);
				rbLinkSpecialFormat.Font = new Font(rbLinkSpecialFormat.Font.FontFamily, rbLinkSpecialFormat.Font.Size - 2, rbLinkSpecialFormat.Font.Style);
				labelControlForeColor.Font = new Font(labelControlForeColor.Font.FontFamily, labelControlForeColor.Font.Size - 2, labelControlForeColor.Font.Style);
			}
		}

		private void LoadData()
		{
			rbLinkRegularFormat.Checked = _data.ExtendedProperties.IsRegularFormat;
			rbLinkBoldFormat.Checked = _data.ExtendedProperties.IsBold;
			rbLinkSpecialFormat.Checked = _data.ExtendedProperties.IsSpecialFormat;
			buttonEditLinkSpecialFont.EditValue = _data.ExtendedProperties.Font != null ? Utils.FontToString(_data.ExtendedProperties.Font) : null;
			buttonEditLinkSpecialFont.Tag = _data.ExtendedProperties.Font;
			colorEditLinkSpecialColor.Color = _data.ExtendedProperties.ForeColor;
		}

		public void SaveData()
		{
			_data.ExtendedProperties.IsBold = rbLinkBoldFormat.Checked;
			_data.ExtendedProperties.IsSpecialFormat = rbLinkSpecialFormat.Checked;
			if (_data.ExtendedProperties.IsSpecialFormat)
			{
				_data.ExtendedProperties.Font = buttonEditLinkSpecialFont.Tag as Font;
				_data.ExtendedProperties.ForeColor = colorEditLinkSpecialColor.Color;
			}
			else
			{
				_data.ExtendedProperties.Font = null;
				_data.ExtendedProperties.ForeColor = Color.Black;
			}
		}

		private void rbLinkSpecialFormat_CheckedChanged(object sender, EventArgs e)
		{
			buttonEditLinkSpecialFont.Enabled = rbLinkSpecialFormat.Checked;
			labelControlForeColor.Enabled = rbLinkSpecialFormat.Checked;
			colorEditLinkSpecialColor.Enabled = rbLinkSpecialFormat.Checked;
			if (!rbLinkSpecialFormat.Checked)
			{
				buttonEditLinkSpecialFont.EditValue = null;
				buttonEditLinkSpecialFont.Tag = null;
				colorEditLinkSpecialColor.Color = Color.Black;
			}
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
