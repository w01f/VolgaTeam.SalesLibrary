using System;
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
	[IntendForClass(typeof(InternalLibraryFolderLink))]
	//public sealed partial class LinkInternalLibraryFolderOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalLibraryFolderOptions : BaseInternalLibraryContentOptions, ILinkSettingsEditControl
	{
		private InternalLibraryFolderLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalLibraryFolderOptions()
		{
			InitializeComponent();
			Text = "Admin";
		}

		public LinkInternalLibraryFolderOptions(LinkType? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_data = (InternalLibraryFolderLink)sourceLink;

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
				.Where(t => t.Type == InternlalLinkTemplateType.Folder).ToArray());

			textEditName.EditValue = _data.Name;
			comboBoxEditLibraryName.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).LibraryName;
			comboBoxEditPageName.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).PageName;
			comboBoxEditWindowName.EditValue = ((InternalLibraryFolderLinkSettings)_data.Settings).WindowName;

			checkEditShowHeaderText.Checked = ((InternalLibraryFolderLinkSettings)_data.Settings).ShowHeaderText;
			checkEditOpenOnSamePage.Checked = !((InternalLibraryFolderLinkSettings)_data.Settings).OpenOnSamePage;
			comboBoxEditStyle.EditValue = ((InternalLibraryFolderLinkSettings) _data.Settings).StyleSettings;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalLibraryFolderLinkSettings)_data.Settings).LibraryName = comboBoxEditLibraryName.EditValue as String;
			((InternalLibraryFolderLinkSettings)_data.Settings).PageName = comboBoxEditPageName.EditValue as String;
			((InternalLibraryFolderLinkSettings)_data.Settings).WindowName = comboBoxEditWindowName.EditValue as String;

			((InternalLibraryFolderLinkSettings)_data.Settings).ShowHeaderText = checkEditShowHeaderText.Checked;
			((InternalLibraryFolderLinkSettings)_data.Settings).OpenOnSamePage = !checkEditOpenOnSamePage.Checked;
			((InternalLibraryFolderLinkSettings)_data.Settings).StyleSettings = comboBoxEditStyle.EditValue as InternalLinkTemplate;

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
		}
	}
}
