using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LineBreak))]
	//public partial class LineBreakOptions : UserControl, ILinkSettingsEditControl
	public partial class LineBreakOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private LineBreak _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes };
		public int Order => 0;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Line Break Settings</size>" };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LineBreakOptions()
		{
			InitializeComponent();

			Text = "Line Break";

			buttonEditLineBreakFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditLineBreakFont.Click += EditorHelper.OnFontEditClick;
		}

		public LineBreakOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (LineBreak)sourceLink;

			buttonEditLineBreakFont.Tag = _data.Settings.Font;
			buttonEditLineBreakFont.EditValue = Utils.FontToString(_data.Settings.Font);
			colorEditLineBreakFontColor.Color = _data.DisplayColor;
			checkEditTextWordWrap.Checked = _data.Settings.TextWordWrap;
			checkEditNote.Checked = !String.IsNullOrEmpty(_data.Settings.Note);
			memoEditNote.EditValue = _data.Settings.Note;
		}

		public void SaveData()
		{
			_data.Settings.Font = buttonEditLineBreakFont.Tag as Font;
			if (colorEditLineBreakFontColor.Color != _data.DisplayColor)
				_data.Settings.ForeColor = colorEditLineBreakFontColor.Color;
			_data.Settings.TextWordWrap = checkEditTextWordWrap.Checked;
			_data.Settings.Note = memoEditNote.EditValue as String;
		}

		private void checkEditNote_CheckedChanged(object sender, EventArgs e)
		{
			memoEditNote.Enabled = checkEditNote.Checked;
			if (!checkEditNote.Checked)
				memoEditNote.EditValue = null;
		}
	}
}
