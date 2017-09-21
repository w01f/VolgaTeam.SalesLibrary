using System;
using System.Collections.Generic;
using System.Linq;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormResetLinkSettings : MetroForm
	{
		public IList<LinkSettingsGroupType> SettingsGroups =>
			checkedListBoxControlItems.CheckedItems
				.OfType<CheckedListBoxItem>()
				.Select(item => (LinkSettingsGroupType)item.Value)
				.ToList();

		public FormResetLinkSettings()
		{
			InitializeComponent();
			checkedListBoxControlItems.ItemHeight = (Int32)(checkedListBoxControlItems.ItemHeight * Utils.GetScaleFactor(CreateGraphics().DpiX).Height);
			layoutControlItemSelectAll.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectAll.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectAll.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectNone.MinSize = RectangleHelper.ScaleSize(layoutControlItemSelectNone.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSelectNone.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSelectNone.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MinSize = RectangleHelper.ScaleSize(layoutControlItemReset.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemReset.MaxSize = RectangleHelper.ScaleSize(layoutControlItemReset.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public FormResetLinkSettings(BaseLibraryLink targetLink) : this()
		{
			Text = "Reset this Link";
			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				targetLink.LinkInfoDisplayName,
				(targetLink as LibraryFileLink)?.NameWithExtension ?? (targetLink as LibraryObjectLink)?.RelativePath ?? String.Empty);

			LoadSettingsGroups(GetLinkCustomizedSettings(targetLink));
		}

		public FormResetLinkSettings(IList<BaseLibraryLink> targetLinks) : this()
		{
			Text = "Reset these Links";
			labelControlTitle.Visible = false;

			LoadSettingsGroups(GetLinksCustomizedSettings(targetLinks));
		}

		public FormResetLinkSettings(LibraryPage targetPage) : this()
		{
			Text = "Reset all Links on this Page";
			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				String.Format("Page: <b>{0}</b>", targetPage.Name),
				String.Empty);

			LoadSettingsGroups(GetLinksCustomizedSettings(targetPage.AllGroupLinks));
		}

		public FormResetLinkSettings(LibraryFolder targetFolder) : this()
		{
			Text = "Reset all Links in this Window";
			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				String.Format("Window: <b>{0}</b>", targetFolder.Name),
				String.Empty);

			LoadSettingsGroups(GetLinksCustomizedSettings(targetFolder.AllGroupLinks));
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
					case LinkSettingsGroupType.Thumbnails:
						itemName = "Thumbnail";
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
				checkedListBoxControlItems.Items.Add(linkSettingsGroupType, itemName);
			}
		}

		private void OnSelectAllClick(object sender, EventArgs e)
		{
			checkedListBoxControlItems.CheckAll();
		}

		private void OnSelectNoneClick(object sender, EventArgs e)
		{
			checkedListBoxControlItems.UnCheckAll();
		}

		private void OnItemCheck(object sender, ItemCheckEventArgs e)
		{
			buttonXReset.Enabled = checkedListBoxControlItems.CheckedItems.Count > 0;
		}
	}
}
