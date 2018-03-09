using System;
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
		private LibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes };
		public int Order => 0;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Link Settings</size>" };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkObjectOptions()
		{
			InitializeComponent();
			Text = "Link Notes";

			checkEditNone.Text = String.Format("<size=+2><b>{0}</b></size>", BaseLinkSettings.PredefinedNoteNone);
			checkEditNew.Text = String.Format("<size=+2><b>{0}</b></size>", BaseLinkSettings.PredefinedNoteNew);
			checkEditSold.Text = String.Format("<size=+2><b>{0}</b></size>", BaseLinkSettings.PredefinedNoteSold);
			checkEditUpdated.Text = String.Format("<size=+2><b>{0}</b></size>", BaseLinkSettings.PredefinedNoteUpdated);
			checkEditSellThis.Text = String.Format("<size=+2><b>{0}</b></size>", BaseLinkSettings.PredefinedNoteSellThis);
			checkEditAttention.Text = String.Format("<size=+2><b>{0}</b></size>", BaseLinkSettings.PredefinedNoteAttention);

			textEditCustomNote.Enter += EditorHelper.OnEditorEnter;
			textEditCustomNote.MouseUp += EditorHelper.OnEditorMouseUp;
			textEditCustomNote.MouseDown += EditorHelper.OnEditorMouseUp;
		}

		public LinkObjectOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (LibraryObjectLink)sourceLink;

			var note = _data.Settings.Note;
			if (String.IsNullOrEmpty(note))
				checkEditNone.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteNew))
				checkEditNew.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteSold))
				checkEditSold.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteUpdated))
				checkEditUpdated.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteSellThis))
				checkEditSellThis.Checked = true;
			else if (note.Equals(BaseLinkSettings.PredefinedNoteAttention))
				checkEditAttention.Checked = true;
			else
			{
				checkEditCustomNote.Checked = true;
				textEditCustomNote.EditValue = note;
			}
		}

		public void SaveData()
		{
			if (checkEditNew.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteNew;
			else if (checkEditSold.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteSold;
			else if (checkEditUpdated.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteUpdated;
			else if (checkEditSellThis.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteSellThis;
			else if (checkEditAttention.Checked)
				_data.Settings.Note = BaseLinkSettings.PredefinedNoteAttention;
			else if (checkEditCustomNote.Checked)
				_data.Settings.Note = textEditCustomNote.EditValue as String;
			else
				_data.Settings.Note = String.Empty;
		}

		private void OnCustomNoteCheckedChanged(object sender, EventArgs e)
		{
			textEditCustomNote.Enabled = checkEditCustomNote.Checked;
		}
	}
}
