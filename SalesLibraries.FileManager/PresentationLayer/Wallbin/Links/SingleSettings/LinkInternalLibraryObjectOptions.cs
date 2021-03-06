﻿using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Models.ExternalLibraryLinks;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(InternalLibraryObjectLink))]
	//public sealed partial class LinkInternalLibraryObjectOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalLibraryObjectOptions : BaseInternalLibraryContentOptions, ILinkSettingsEditControl
	{
		private InternalLibraryObjectLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalLibraryObjectOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkInternalLibraryObjectOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (InternalLibraryObjectLink)sourceLink;

			base.LoadData();

			comboBoxEditLibraryName.Properties.Items.Clear();
			comboBoxEditLibraryName.Properties.Items.AddRange(MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.OrderBy(l => l.Name)
				.Select(l => l.Name)
				.ToArray());

			textEditName.EditValue = _data.Name;
			comboBoxEditLibraryName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).LibraryName;
			comboBoxEditPageName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).PageName;
			comboBoxEditWindowName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).WindowName;
			comboBoxEditLibraryLinkName.EditValue = ((InternalLibraryObjectLinkSettings)_data.Settings).LinkName;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;

			var selectedLink = (LibraryLink)comboBoxEditLibraryLinkName.EditValue;

			((InternalLibraryObjectLinkSettings)_data.Settings).LibraryName = comboBoxEditLibraryName.EditValue as String;
			((InternalLibraryObjectLinkSettings)_data.Settings).PageName = comboBoxEditPageName.EditValue as String;
			((InternalLibraryObjectLinkSettings)_data.Settings).WindowName = comboBoxEditWindowName.EditValue as String;
			((InternalLibraryObjectLinkSettings)_data.Settings).LinkName = selectedLink.ToString();

			MainController.Instance.ProcessManager.Run(
				"Updating Link Thumbnails...",
				(cancelationToken, formProgess) =>
				{
					((InternalLibraryObjectLinkSettings)_data.Settings).ThumbnailUrls = 
						MainController.Instance.Lists.ExternalLibraryLinks.GetLinkThumbnails(selectedLink.Id).ToArray();
				});
		}

		private void OnLibraryChanged(object sender, EventArgs e)
		{
			comboBoxEditPageName.Properties.Items.Clear();
			var libraryName = comboBoxEditLibraryName.EditValue as String;
			var library = MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.FirstOrDefault(l => String.Compare(l.Name, libraryName, StringComparison.OrdinalIgnoreCase) == 0);
			if (library != null)
				comboBoxEditPageName.Properties.Items.AddRange(library.Pages
					.OrderBy(p => p.Order)
					.Select(p => p.Name)
					.ToArray());
			OnLibraryPageChanged(sender, e);
			OnLibraryFolderChanged(sender, e);
		}

		private void OnLibraryPageChanged(object sender, EventArgs e)
		{
			comboBoxEditWindowName.Properties.Items.Clear();
			var libraryName = comboBoxEditLibraryName.EditValue as String;
			var libraryPageName = comboBoxEditPageName.EditValue as String;
			var libraryPage = MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.Where(l => String.Compare(l.Name, libraryName, StringComparison.OrdinalIgnoreCase) == 0)
				.SelectMany(l => l.Pages)
				.FirstOrDefault(p => String.Compare(p.Name, libraryPageName, StringComparison.OrdinalIgnoreCase) == 0);
			if (libraryPage != null)
				comboBoxEditWindowName.Properties.Items.AddRange(libraryPage.Folders
					.OrderBy(f => f.Order)
					.Select(f => f.Name)
					.ToArray());
			OnLibraryFolderChanged(sender, e);
		}

		private void OnLibraryFolderChanged(object sender, EventArgs e)
		{
			comboBoxEditLibraryLinkName.Properties.Items.Clear();
			var libraryName = comboBoxEditLibraryName.EditValue as String;
			var libraryPageName = comboBoxEditPageName.EditValue as String;
			var libraryFolderName = comboBoxEditWindowName.EditValue as String;
			var libraryFolder = MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.Where(l => String.Compare(l.Name, libraryName, StringComparison.OrdinalIgnoreCase) == 0)
				.SelectMany(l => l.Pages)
				.Where(p => String.Compare(p.Name, libraryPageName, StringComparison.OrdinalIgnoreCase) == 0)
				.SelectMany(p => p.Folders)
				.FirstOrDefault(f => String.Compare(f.Name, libraryFolderName, StringComparison.OrdinalIgnoreCase) == 0);
			if (libraryFolder != null)
				comboBoxEditLibraryLinkName.Properties.Items.AddRange(libraryFolder.Links
					.OrderBy(link => link.Order)
					.ToArray());
		}
	}
}
