﻿using System;
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
	//public partial class LinkHoverNoteOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkHoverNoteOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private LibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes };
		public int Order => 1;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkHoverNoteOptions()
		{
			InitializeComponent();
			Text = "Hover Note";

			memoEditNote.Enter += EditorHelper.OnEditorEnter;
			memoEditNote.MouseUp += EditorHelper.OnEditorMouseUp;
			memoEditNote.MouseDown += EditorHelper.OnEditorMouseUp;

			layoutControlGroupControls.Enabled = false;
		}

		public LinkHoverNoteOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (LibraryObjectLink)sourceLink;

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
			layoutControlGroupControls.Enabled = checkEditNote.Checked;
			if (!checkEditNote.Checked)
			{
				memoEditNote.EditValue = null;
				checkEditShowOnlyCustomNote.Checked = false;
			}
		}
	}
}
