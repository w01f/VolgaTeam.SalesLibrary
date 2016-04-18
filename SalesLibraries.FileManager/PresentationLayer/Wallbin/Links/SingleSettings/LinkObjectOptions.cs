using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class LinkObjectOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkObjectOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LibraryObjectLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.Notes;
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkObjectOptions(LibraryObjectLink data)
		{
			InitializeComponent();
			Text = "Link Notes";
			_data = data;

			rbNone.Text = BaseLinkSettings.PredefinedNoteNone;
			rbNew.Text = BaseLinkSettings.PredefinedNoteNew;
			rbUpdated.Text = BaseLinkSettings.PredefinedNoteUpdated;
			rbSell.Text = BaseLinkSettings.PredefinedNoteSellThis;
			rbAttention.Text = BaseLinkSettings.PredefinedNoteAttention;

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
			textEditLinkHoverNote.Enter += EditorHelper.EditorEnter;
			textEditLinkHoverNote.MouseUp += EditorHelper.EditorMouseUp;
			textEditLinkHoverNote.MouseDown += EditorHelper.EditorMouseUp;
		}

		public void LoadData()
		{
			var note = _data.Settings.Note;
			if (String.IsNullOrEmpty(note))
				rbNone.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteNew))
				rbNew.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteUpdated))
				rbUpdated.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteSellThis))
				rbSell.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteAttention))
				rbAttention.Checked = true;
			else
			{
				rbCustomNote.Checked = true;
				edCustomNote.Text = note;
			}
			textEditLinkHoverNote.EditValue = ((LibraryObjectLinkSettings)_data.Settings).HoverNote;
		}

		public void SaveData()
		{
			if (rbNew.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteNew;
			else if (rbUpdated.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteUpdated;
			else if (rbSell.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteSellThis;
			else if (rbAttention.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteAttention;
			else if (rbCustomNote.Checked)
				_data.Settings.Note = edCustomNote.Text;
			else
				_data.Settings.Note = String.Empty;
			((LibraryObjectLinkSettings)_data.Settings).HoverNote = textEditLinkHoverNote.EditValue as String ?? String.Empty;
		}

		private void rbCustomNote_CheckedChanged(object sender, EventArgs e)
		{
			edCustomNote.Enabled = rbCustomNote.Checked;
		}
	}
}
