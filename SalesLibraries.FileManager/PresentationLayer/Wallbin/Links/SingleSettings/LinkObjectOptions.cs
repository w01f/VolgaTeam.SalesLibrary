using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
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

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes };
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Link Settings</size>" };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkObjectOptions(LibraryObjectLink data)
		{
			InitializeComponent();
			Text = "Link Notes";
			_data = data;

			rbNone.Text = BaseLinkSettings.PredefinedNoteNone;
			rbNew.Text = BaseLinkSettings.PredefinedNoteNew;
			rbSold.Text = BaseLinkSettings.PredefinedNoteSold;
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
				rbSold.Font = new Font(rbSold.Font.FontFamily, rbSold.Font.Size - 2, rbSold.Font.Style);
				rbUpdated.Font = new Font(rbUpdated.Font.FontFamily, rbUpdated.Font.Size - 2, rbUpdated.Font.Style);
			}
			textEditCustomNote.Enter += EditorHelper.EditorEnter;
			textEditCustomNote.MouseUp += EditorHelper.EditorMouseUp;
			textEditCustomNote.MouseDown += EditorHelper.EditorMouseUp;
		}

		public void LoadData()
		{
			var note = _data.Settings.Note;
			if (String.IsNullOrEmpty(note))
				rbNone.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteNew))
				rbNew.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteSold))
				rbSold.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteUpdated))
				rbUpdated.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteSellThis))
				rbSell.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteAttention))
				rbAttention.Checked = true;
			else
			{
				rbCustomNote.Checked = true;
				textEditCustomNote.EditValue = note;
			}
		}

		public void SaveData()
		{
			if (rbNew.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteNew;
			else if (rbSold.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteSold;
			else if (rbUpdated.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteUpdated;
			else if (rbSell.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteSellThis;
			else if (rbAttention.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteAttention;
			else if (rbCustomNote.Checked)
				_data.Settings.Note = textEditCustomNote.EditValue as String;
			else
				_data.Settings.Note = String.Empty;
		}

		private void rbCustomNote_CheckedChanged(object sender, EventArgs e)
		{
			textEditCustomNote.Enabled = rbCustomNote.Checked;
		}
	}
}
