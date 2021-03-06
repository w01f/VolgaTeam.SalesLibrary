﻿using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(InternalLibraryPageLink))]
	//public sealed partial class LinkInternalLibraryPageOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalLibraryPageOptions : BaseInternalLibraryContentOptions, ILinkSettingsEditControl
	{
		private InternalLibraryPageLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalLibraryPageOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkInternalLibraryPageOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (InternalLibraryPageLink)sourceLink;

			base.LoadData();

			comboBoxEditLibraryName.Properties.Items.Clear();
			comboBoxEditLibraryName.Properties.Items.AddRange(MainController.Instance.Lists.ExternalLibraryLinks.Libraries
				.OrderBy(l => l.Name)
				.Select(l => l.Name)
				.ToArray());

			comboBoxEditStyle.Properties.Items.Clear();
			comboBoxEditStyle.Properties.Items.AddRange(
				MainController.Instance.Lists.InternalLinkTemplates.Templates
				.OrderBy(t => t.Name)
				.Where(t => t.Type == InternlalLinkTemplateType.Page).ToArray());

			textEditName.EditValue = _data.Name;
			comboBoxEditLibraryName.EditValue = ((InternalLibraryPageLinkSettings)_data.Settings).LibraryName;
			comboBoxEditPageName.EditValue = ((InternalLibraryPageLinkSettings)_data.Settings).PageName;

			checkEditShowHeaderText.Checked = ((InternalLibraryPageLinkSettings)_data.Settings).ShowHeaderText;
			checkEditOpenOnSamePage.Checked = !((InternalLibraryPageLinkSettings)_data.Settings).OpenOnSamePage;
			comboBoxEditStyle.EditValue = ((InternalLibraryPageLinkSettings)_data.Settings).StyleSettings;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalLibraryPageLinkSettings)_data.Settings).LibraryName = comboBoxEditLibraryName.EditValue as String;
			((InternalLibraryPageLinkSettings)_data.Settings).PageName = comboBoxEditPageName.EditValue as String;

			((InternalLibraryPageLinkSettings)_data.Settings).ShowHeaderText = checkEditShowHeaderText.Checked;
			((InternalLibraryPageLinkSettings)_data.Settings).OpenOnSamePage = !checkEditOpenOnSamePage.Checked;
			((InternalLibraryPageLinkSettings)_data.Settings).StyleSettings = comboBoxEditStyle.EditValue as InternalLinkTemplate;
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
		}
	}
}
