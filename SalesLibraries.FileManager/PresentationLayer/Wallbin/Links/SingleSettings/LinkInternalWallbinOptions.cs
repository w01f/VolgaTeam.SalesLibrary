using System;
using System.Drawing;
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
	[IntendForClass(typeof(InternalWallbinLink))]
	//public sealed partial class LinkInternalWallbinOptions : UserControl, ILinkSettingsEditControl
	public sealed partial class LinkInternalWallbinOptions : BaseInternalLibraryContentOptions, ILinkSettingsEditControl
	{
		private readonly InternalWallbinLink _data;

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.Notes, LinkSettingsType.AdminSettings };
		public int Order => 6;
		public bool AvailableForEmbedded => true;
		public SettingsEditorHeaderInfo HeaderInfo => new SettingsEditorHeaderInfo { Title = "<size=+4>Internal Link</size>", Logo = Resources.LinkAddInternal };

		public event EventHandler<EventArgs> ForceCloseRequested;

		public LinkInternalWallbinOptions(InternalWallbinLink data)
		{
			InitializeComponent();
			Text = "Admin";
			_data = data;
			if ((CreateGraphics()).DpiX > 96)
			{
				var styleControllerFont = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2, styleController.Appearance.Font.Style);
				styleController.AppearanceDisabled.Font = styleControllerFont;
				styleController.AppearanceDropDown.Font = styleControllerFont;
				styleController.AppearanceDropDownHeader.Font = styleControllerFont;
				styleController.AppearanceFocused.Font = styleControllerFont;
				styleController.AppearanceReadOnly.Font = styleControllerFont;
			}
		}

		public override void LoadData()
		{
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
				.Where(t => t.Type == InternlalLinkTemplateType.Library).ToArray());

			textEditName.EditValue = _data.Name;
			comboBoxEditLibraryName.EditValue = ((InternalWallbinLinkSettings)_data.Settings).LibraryName;
			comboBoxEditPageName.EditValue = ((InternalWallbinLinkSettings)_data.Settings).PageName;

			checkEditShowHeaderText.Checked = ((InternalWallbinLinkSettings)_data.Settings).ShowHeaderText;
			checkEditOpenOnSamePage.Checked = !((InternalWallbinLinkSettings)_data.Settings).OpenOnSamePage;
			comboBoxEditStyle.EditValue = ((InternalWallbinLinkSettings)_data.Settings).StyleSettings;
		}

		public void SaveData()
		{
			_data.Name = textEditName.EditValue as String;
			((InternalWallbinLinkSettings)_data.Settings).LibraryName = comboBoxEditLibraryName.EditValue as String;
			((InternalWallbinLinkSettings)_data.Settings).PageName = comboBoxEditPageName.EditValue as String;

			((InternalWallbinLinkSettings)_data.Settings).ShowHeaderText = checkEditShowHeaderText.Checked;
			((InternalWallbinLinkSettings)_data.Settings).OpenOnSamePage = !checkEditOpenOnSamePage.Checked;
			((InternalWallbinLinkSettings)_data.Settings).StyleSettings = comboBoxEditStyle.EditValue as InternalLinkTemplate;
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
