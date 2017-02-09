using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalWallbinLinkEditControl : BaseInternalLibraryContentEditControl, IInternalLinkEditControl
	{
		public InternalWallbinLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				laLibraryName.Font = new Font(laLibraryName.Font.FontFamily, laLibraryName.Font.Size - 2, laLibraryName.Font.Style);
				laPageName.Font = new Font(laPageName.Font.FontFamily, laPageName.Font.Size - 2, laPageName.Font.Style);
			}
		}

		public override void InitControl()
		{
			if (IsListsLoaded) return;
			base.InitControl();

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
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalWallbinLinkInfo)GetHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.LibraryName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target library before saving");
				return false;
			}
			return true;
		}

		public InternalLinkInfo GetHyperLinkInfo()
		{
			return new InternalWallbinLinkInfo
			{
				LibraryName = comboBoxEditLibraryName.EditValue as String,
				PageName = comboBoxEditPageName.EditValue as String,
				ShowHeaderText = checkEditShowHeaderText.Checked,
				OpenOnSamePage = !checkEditOpenOnSamePage.Checked,
				StyleSettings = comboBoxEditStyle.EditValue as InternalLinkTemplate
			};
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo)
		{
			if (templateInfo is InternalLibraryPageLinkInfo)
			{
				comboBoxEditLibraryName.EditValue = ((InternalLibraryPageLinkInfo) templateInfo).LibraryName;
				comboBoxEditPageName.EditValue = ((InternalLibraryPageLinkInfo) templateInfo).PageName;
				checkEditShowHeaderText.Checked = ((InternalLibraryPageLinkInfo) templateInfo).ShowHeaderText;
				checkEditOpenOnSamePage.Checked = ((InternalLibraryPageLinkInfo) templateInfo).OpenOnSamePage;
			}
			if (templateInfo is InternalLibraryFolderLinkInfo)
			{
				comboBoxEditLibraryName.EditValue = ((InternalLibraryFolderLinkInfo) templateInfo).LibraryName;
				comboBoxEditPageName.EditValue = ((InternalLibraryFolderLinkInfo) templateInfo).PageName;
				checkEditShowHeaderText.Checked = ((InternalLibraryFolderLinkInfo) templateInfo).ShowHeaderText;
				checkEditOpenOnSamePage.Checked = ((InternalLibraryFolderLinkInfo) templateInfo).OpenOnSamePage;
			}
			if (templateInfo is InternalShortcutLinkInfo)
			{
				checkEditOpenOnSamePage.Checked = ((InternalShortcutLinkInfo) templateInfo).OpenOnSamePage;
			}
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
