using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormResetLinkSettings : MetroForm
	{
		private readonly BaseLibraryLink _targetLink;

		public IList<LinkSettingsGroupType> SettingsGroups =>
			checkedListBoxControlOutputItems.CheckedItems
				.OfType<CheckedListBoxItem>()
				.Select(item => (LinkSettingsGroupType)item.Value)
				.ToList();

		public FormResetLinkSettings(BaseLibraryLink targetLink)
		{
			InitializeComponent();

			_targetLink = targetLink;

			labelControlTitle.Text = String.Format(labelControlTitle.Text,
				_targetLink.LinkInfoDisplayName,
				(_targetLink as LibraryFileLink)?.NameWithExtension ?? (_targetLink as LibraryObjectLink)?.RelativePath ?? String.Empty);

			LoadSettingsGroups();

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

		private void LoadSettingsGroups()
		{
			var customizedSettingsGroups = _targetLink.GetCustomizedSettigsGroups();
			foreach (var linkSettingsGroupType in customizedSettingsGroups)
			{
				string itemName;
				switch (linkSettingsGroupType)
				{
					case LinkSettingsGroupType.Banners:
						itemName = "Banner";
						break;
					case LinkSettingsGroupType.Widgets:
						itemName = "Widget";
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
