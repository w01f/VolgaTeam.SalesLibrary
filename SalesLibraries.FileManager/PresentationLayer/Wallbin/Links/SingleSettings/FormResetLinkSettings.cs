using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormResetLinkSettings : MetroForm
	{
		public IList<LinkSettingsGroupType> SettingsGroups =>
			checkedListBoxControlOutputItems.CheckedItems
				.OfType<CheckedListBoxItem>()
				.Select(item => (LinkSettingsGroupType)item.Value)
				.ToList();

		public FormResetLinkSettings()
		{
			InitializeComponent();

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

				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXReset.Font = new Font(buttonXReset.Font.FontFamily, buttonXReset.Font.Size - 2, buttonXReset.Font.Style);
			}
		}

		public FormResetLinkSettings(BaseLibraryLink targetLink) : this()
		{
			Text = "Reset this Link";
			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				targetLink.LinkInfoDisplayName,
				(targetLink as LibraryFileLink)?.NameWithExtension ?? (targetLink as LibraryObjectLink)?.RelativePath ?? String.Empty);

			LoadSettingsGroups(GetLinkCustomizedSettings(targetLink));
		}

		public FormResetLinkSettings(LibraryPage targetPage) : this()
		{
			Text = "Reset all Links on this Page";
			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				String.Format("Page: <b>{0}</b>", targetPage.Name),
				String.Empty);

			LoadSettingsGroups(GetLinksCustomizedSettings(targetPage.AllLinks));
		}

		public FormResetLinkSettings(LibraryFolder targetFolder) : this()
		{
			Text = "Reset all Links in this Window";
			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				String.Format("Window: <b>{0}</b>", targetFolder.Name),
				String.Empty);

			LoadSettingsGroups(GetLinksCustomizedSettings(targetFolder.AllLinks));
		}

		private IList<LinkSettingsGroupType> GetLinkCustomizedSettings(BaseLibraryLink targetLink)
		{
			return GetLinksCustomizedSettings(new[] { targetLink });
		}

		private IList<LinkSettingsGroupType> GetLinksCustomizedSettings(IEnumerable<BaseLibraryLink> targetLinks)
		{
			var customizedSettings = targetLinks
				.SelectMany(link => link.GetCustomizedSettigsGroups())
				.Distinct()
				.ToList();
			customizedSettings.Sort((x, y) => ((Int32)x).CompareTo((Int32)y));
			return customizedSettings;
		}

		private void LoadSettingsGroups(IList<LinkSettingsGroupType> customizedSettingsGroups)
		{
			foreach (var linkSettingsGroupType in customizedSettingsGroups)
			{
				string itemName;
				switch (linkSettingsGroupType)
				{
					case LinkSettingsGroupType.Banners:
						itemName = "Clipart-Logo";
						break;
					case LinkSettingsGroupType.Widgets:
						itemName = "Widget-Icon";
						break;
					case LinkSettingsGroupType.TextFormatting:
						itemName = "Text Formatting";
						break;
					case LinkSettingsGroupType.TextNote:
						itemName = "Link Note";
						break;
					case LinkSettingsGroupType.HoverNote:
						itemName = "Hover Note";
						break;
					case LinkSettingsGroupType.SearchTags:
						itemName = "Category Tags";
						break;
					case LinkSettingsGroupType.Keywords:
						itemName = "Keyword Tags";
						break;
					case LinkSettingsGroupType.Security:
						itemName = "Security";
						break;
					case LinkSettingsGroupType.Expiration:
						itemName = "Expiration Date";
						break;
					case LinkSettingsGroupType.QuickLink:
						itemName = "Quick Link";
						break;
					case LinkSettingsGroupType.AutoWidgets:
						itemName = "Auto Widgets";
						break;
					default:
						throw new ArgumentOutOfRangeException("Undefined Settings Group");
				}
				checkedListBoxControlOutputItems.Items.Add(linkSettingsGroupType, itemName);
			}
		}

		private void OnSelectAllClick(object sender, EventArgs e)
		{
			checkedListBoxControlOutputItems.CheckAll();
		}

		private void OnSelectNoneClick(object sender, EventArgs e)
		{
			checkedListBoxControlOutputItems.UnCheckAll();
		}

		private void OnItemCheck(object sender, ItemCheckEventArgs e)
		{
			buttonXReset.Enabled = checkedListBoxControlOutputItems.CheckedItems.Count > 0;
		}
	}
}
