using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class LinkHoverNoteOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkHoverNoteOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes };
		public int Order => 1;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkHoverNoteOptions(LibraryObjectLink data)
		{
			InitializeComponent();
			Text = "Hover Note";
			_data = data;

			if ((base.CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
			}
			memoEditNote.Enter += EditorHelper.EditorEnter;
			memoEditNote.MouseUp += EditorHelper.EditorMouseUp;
			memoEditNote.MouseDown += EditorHelper.EditorMouseUp;
		}

		public void LoadData()
		{
			checkEditNote.Checked = !String.IsNullOrEmpty(((LibraryObjectLinkSettings)_data.Settings).HoverNote);
			memoEditNote.EditValue = ((LibraryObjectLinkSettings)_data.Settings).HoverNote;
			checkEditShowOnlyCustomNote.Checked = ((LibraryObjectLinkSettings)_data.Settings).ShowOnlyCustomHoverNote;
		}

		public void SaveData()
		{
			((LibraryObjectLinkSettings)_data.Settings).HoverNote = memoEditNote.EditValue as String ?? String.Empty;
			((LibraryObjectLinkSettings)_data.Settings).ShowOnlyCustomHoverNote = checkEditNote.Checked;
		}

		private void checkEditNote_CheckedChanged(object sender, EventArgs e)
		{
			memoEditNote.Enabled = checkEditNote.Checked;
			checkEditShowOnlyCustomNote.Enabled = checkEditNote.Checked;
			if (!checkEditNote.Checked)
			{
				memoEditNote.EditValue = null;
				checkEditShowOnlyCustomNote.Checked = false;
			}
		}
	}
}
