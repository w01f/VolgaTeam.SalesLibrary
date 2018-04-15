using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	//public partial class ResetFontControl : UserControl, IResetLibraryLinkSettingsControl
	public partial class ResetFontControl : XtraTabPage, IResetLibraryLinkSettingsControl
	{
		public bool SelectionMade => checkEditApplyForAllLinks.Checked;

		public event EventHandler<EventArgs> SelectionChanged;

		public ResetFontControl(Library library)
		{
			InitializeComponent();

			Text = "Link Font";

			buttonEditFont.ButtonClick += EditorHelper.OnFontEditButtonClick;
			buttonEditFont.Click += EditorHelper.OnFontEditClick;

			buttonEditFont.EditValue = library.Settings.FontSettings.Font != null
				? Utils.FontToString(library.Settings.FontSettings.Font)
				: null;
			buttonEditFont.Tag = library.Settings.FontSettings.Font;
			colorEditFontColor.Color = library.Settings.FontSettings.Color ?? Color.Black;
		}

		public IList<LinkSettingsGroupType> GetSelectedSettingsGroups()
		{
			var linkGroupsForReset = new List<LinkSettingsGroupType> { LinkSettingsGroupType.TextFormatting };
			return linkGroupsForReset;
		}

		public void ResetContent(Library library)
		{
			var newFont = buttonEditFont.Tag is Font font ? 
				new Font(font.FontFamily, font.Size, font.Style) : 
				null;

			var linksToProcess = library.Pages.SelectMany(p => p.TopLevelLinks).ToList();
			foreach (var libraryLink in linksToProcess)
			{
				if (libraryLink is LibraryObjectLink)
				{
					((LibraryObjectLinkSettings)libraryLink.Settings).RegularFontStyle = FontStyle.Regular;
					((LibraryObjectLinkSettings)libraryLink.Settings).IsSpecialFormat = true;

				}
				libraryLink.Settings.Font = newFont;
				libraryLink.Settings.ForeColor = colorEditFontColor.Color;
			}

			if (checkEditMakeDefault.Checked)
			{
				library.Settings.FontSettings.Font = newFont;
				library.Settings.FontSettings.Color = colorEditFontColor.Color;
			}
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			MainController.Instance.Settings.Save();
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnLinksFileAllCheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemFont.Enabled = checkEditApplyForAllLinks.Checked;
			layoutControlItemFontColor.Enabled = checkEditApplyForAllLinks.Checked;
			layoutControlItemMakeDefault.Enabled = checkEditApplyForAllLinks.Checked;
			OnSelectionChanged(sender, e);
		}
	}
}
