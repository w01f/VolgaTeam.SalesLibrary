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
	//public partial class LinkBaseOptions : UserControl, ILinkProperties
	public partial class LinkBaseOptions : XtraTabPage, ILinkProperties
	{
		protected readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LinkBaseOptions()
		{
			InitializeComponent();
		}

		public LinkBaseOptions(LibraryLink data)
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
				rbAttention.Font = new Font(rbAttention.Font.FontFamily, rbAttention.Font.Size - 2, rbAttention.Font.Style);
				rbLinkRegularFormat.Font = new Font(rbLinkRegularFormat.Font.FontFamily, rbLinkRegularFormat.Font.Size - 2, rbLinkRegularFormat.Font.Style);
				rbLinkBoldFormat.Font = new Font(rbLinkBoldFormat.Font.FontFamily, rbLinkBoldFormat.Font.Size - 2, rbLinkBoldFormat.Font.Style);
				rbLinkSpecialFormat.Font = new Font(rbLinkSpecialFormat.Font.FontFamily, rbLinkSpecialFormat.Font.Size - 2, rbLinkSpecialFormat.Font.Style);
				rbCustomNote.Font = new Font(rbCustomNote.Font.FontFamily, rbCustomNote.Font.Size - 2, rbCustomNote.Font.Style);
				rbNew.Font = new Font(rbNew.Font.FontFamily, rbNew.Font.Size - 2, rbNew.Font.Style);
				rbNone.Font = new Font(rbNone.Font.FontFamily, rbNone.Font.Size - 2, rbNone.Font.Style);
				rbSell.Font = new Font(rbSell.Font.FontFamily, rbSell.Font.Size - 2, rbSell.Font.Style);
				rbUpdated.Font = new Font(rbUpdated.Font.FontFamily, rbUpdated.Font.Size - 2, rbUpdated.Font.Style);
				laLinkHoverNote.Font = new Font(laLinkHoverNote.Font.FontFamily, laLinkHoverNote.Font.Size - 2, laLinkHoverNote.Font.Style);
			}
			textEditLinkHoverNote.Enter += FormMain.Instance.EditorEnter;
			textEditLinkHoverNote.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditLinkHoverNote.MouseDown += FormMain.Instance.EditorMouseUp;
		}

		protected void LoadData()
		{
			rbLinkRegularFormat.Checked = _data.ExtendedProperties.IsRegularFormat;
			rbLinkBoldFormat.Checked = _data.ExtendedProperties.IsBold;
			rbLinkSpecialFormat.Checked = _data.ExtendedProperties.IsSpecialFormat;
			buttonEditLinkSpecialFont.EditValue = _data.ExtendedProperties.Font != null ? Utils.FontToString(_data.ExtendedProperties.Font) : null;
			buttonEditLinkSpecialFont.Tag = _data.ExtendedProperties.Font;
			colorEditLinkSpecialColor.Color = _data.ExtendedProperties.ForeColor;
			var note = _data.ExtendedProperties.Note;
			if (String.IsNullOrEmpty(note))
				rbNone.Checked = true;
			else if (note.Equals(rbNew.Text))
				rbNew.Checked = true;
			else if (note.Equals(rbUpdated.Text))
				rbUpdated.Checked = true;
			else if (note.Equals(rbSell.Text))
				rbSell.Checked = true;
			else if (note.Equals(rbAttention.Text))
				rbAttention.Checked = true;
			else
			{
				rbCustomNote.Checked = true;
				edCustomNote.Text = note;
			}
			textEditLinkHoverNote.EditValue = _data.ExtendedProperties.HoverNote;
		}

		public virtual void SaveData()
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
			if (rbNew.Checked)
				_data.ExtendedProperties.Note = rbNew.Text;
			else if (rbUpdated.Checked)
				_data.ExtendedProperties.Note = rbUpdated.Text;
			else if (rbSell.Checked)
				_data.ExtendedProperties.Note = rbSell.Text;
			else if (rbAttention.Checked)
				_data.ExtendedProperties.Note = rbAttention.Text;
			else if (rbCustomNote.Checked)
				_data.ExtendedProperties.Note = edCustomNote.Text;
			else
				_data.ExtendedProperties.Note = String.Empty;
			_data.ExtendedProperties.HoverNote = textEditLinkHoverNote.EditValue as String ?? String.Empty;
		}

		private void rbCustomNote_CheckedChanged(object sender, EventArgs e)
		{
			edCustomNote.Enabled = rbCustomNote.Checked;
		}

		private void rbLinkSpecialFormat_CheckedChanged(object sender, EventArgs e)
		{
			buttonEditLinkSpecialFont.Enabled = rbLinkSpecialFormat.Checked;
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
			FontEdit_Click(this, null);
		}
	}
}
