﻿using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public partial class InternalLibraryPageLinkEditControl : BaseInternalLibraryContentEditControl, IInternalLinkEditControl
	{
		public InternalLibraryPageLinkEditControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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
				.Where(t => t.Type == InternlalLinkTemplateType.Page).ToArray());
		}

		public bool ValidateLinkInfo()
		{
			var linkInfo = (InternalLibraryPageLinkInfo)PrepareHyperLinkInfo();
			if (String.IsNullOrEmpty(linkInfo.LibraryName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target library before saving");
				return false;
			}
			if (String.IsNullOrEmpty(linkInfo.PageName))
			{
				MainController.Instance.PopupMessages.ShowWarning("You should set the target page before saving");
				return false;
			}
			return true;
		}

		public InternalLinkInfo PrepareHyperLinkInfo()
		{
			return new InternalLibraryPageLinkInfo
			{
				LibraryName = comboBoxEditLibraryName.EditValue as String,
				PageName = comboBoxEditPageName.EditValue as String,
				ShowHeaderText = checkEditShowHeaderText.Checked,
				OpenOnSamePage = !checkEditOpenOnSamePage.Checked,
				StyleSettings = comboBoxEditStyle.EditValue as InternalLinkTemplate
			};
		}

		public InternalLinkInfo GetFinalHyperLinkInfo()
		{
			return PrepareHyperLinkInfo();
		}

		public void ApplySharedSettings(InternalLinkInfo templateInfo)
		{
			if (templateInfo is InternalWallbinLinkInfo)
			{
				comboBoxEditLibraryName.EditValue = ((InternalWallbinLinkInfo)templateInfo).LibraryName;
				comboBoxEditPageName.EditValue = ((InternalWallbinLinkInfo)templateInfo).PageName;
				checkEditShowHeaderText.Checked = ((InternalWallbinLinkInfo)templateInfo).ShowHeaderText;
			}
			if (templateInfo is InternalLibraryFolderLinkInfo)
			{
				comboBoxEditLibraryName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).LibraryName;
				comboBoxEditPageName.EditValue = ((InternalLibraryFolderLinkInfo)templateInfo).PageName;
				checkEditShowHeaderText.Checked = ((InternalLibraryFolderLinkInfo)templateInfo).ShowHeaderText;
			}
			if (templateInfo is InternalLibraryObjectLinkInfo)
			{
				comboBoxEditPageName.EditValue = ((InternalLibraryObjectLinkInfo)templateInfo).PageName;
			}
			if (templateInfo is InternalShortcutLinkInfo)
			{
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
