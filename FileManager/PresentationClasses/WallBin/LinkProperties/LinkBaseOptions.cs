using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class LinkBaseOptions : UserControl, ILinkProperties
	public sealed partial class LinkBaseOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public LinkBaseOptions(LibraryLink data)
		{
			InitializeComponent();
			Text = "Link Notes";
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
				rbCustomNote.Font = new Font(rbCustomNote.Font.FontFamily, rbCustomNote.Font.Size - 2, rbCustomNote.Font.Style);
				rbNew.Font = new Font(rbNew.Font.FontFamily, rbNew.Font.Size - 2, rbNew.Font.Style);
				rbNone.Font = new Font(rbNone.Font.FontFamily, rbNone.Font.Size - 2, rbNone.Font.Style);
				rbSell.Font = new Font(rbSell.Font.FontFamily, rbSell.Font.Size - 2, rbSell.Font.Style);
				rbUpdated.Font = new Font(rbUpdated.Font.FontFamily, rbUpdated.Font.Size - 2, rbUpdated.Font.Style);
				laLinkHoverNote.Font = new Font(laLinkHoverNote.Font.FontFamily, laLinkHoverNote.Font.Size - 2, laLinkHoverNote.Font.Style);
				labelControlTitle.Font = new Font(labelControlTitle.Font.FontFamily, labelControlTitle.Font.Size - 2, labelControlTitle.Font.Style);
				labelControlHoverNoteDescription.Font = new Font(labelControlHoverNoteDescription.Font.FontFamily, labelControlHoverNoteDescription.Font.Size - 2, labelControlHoverNoteDescription.Font.Style);
			}
			textEditLinkHoverNote.Enter += FormMain.Instance.EditorEnter;
			textEditLinkHoverNote.MouseUp += FormMain.Instance.EditorMouseUp;
			textEditLinkHoverNote.MouseDown += FormMain.Instance.EditorMouseUp;
		}

		private void LoadData()
		{
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

		public void SaveData()
		{
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
	}
}
