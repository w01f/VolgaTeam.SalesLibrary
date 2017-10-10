using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings.ResetLinks
{
	//public partial class ResetInfoControl : UserControl, IResetLibraryLinkSettingsControl
	public partial class ResetInfoControl : XtraTabPage, IResetLibraryLinkSettingsControl
	{
		private readonly List<CheckEdit> _fileLinkToggles = new List<CheckEdit>();
		private readonly List<CheckEdit> _otherLinkToggles = new List<CheckEdit>();
		private readonly List<CheckEdit> _internalLinkToggles = new List<CheckEdit>();
		private readonly List<CheckEdit> _hyperLinkToggles = new List<CheckEdit>();

		public bool SelectionMade =>
				(buttonXNotes.Checked || buttonXHoverNotes.Checked || buttonXExpirationDates.Checked || buttonXSecurity.Checked) &&
				(_fileLinkToggles.Any(item => item.Checked) || _otherLinkToggles.Any(item => item.Checked));

		public event EventHandler<EventArgs> SelectionChanged;

		public ResetInfoControl()
		{
			InitializeComponent();

			Text = "Info-Data";

			_fileLinkToggles.AddRange(new[]
			{
				checkEditLinks7z,
				checkEditLinksAep,
				checkEditLinksAet,
				checkEditLinksAi,
				checkEditLinksAit,
				checkEditLinksDoc,
				checkEditLinksDocx,
				checkEditLinksEps,
				checkEditLinksFileUndefined,
				checkEditLinksJpeg,
				checkEditLinksJpg,
				checkEditLinksKey,
				checkEditLinksMov,
				checkEditLinksMp3,
				checkEditLinksMp4,
				checkEditLinksPdd,
				checkEditLinksPdf,
				checkEditLinksPng,
				checkEditLinksPps,
				checkEditLinksPpt,
				checkEditLinksPptx,
				checkEditLinksPsd,
				checkEditLinksRar,
				checkEditLinksSvg,
				checkEditLinksTxt,
				checkEditLinksWmv,
				checkEditLinksXls,
				checkEditLinksXlsx,
				checkEditLinksXml,
				checkEditLinksZip
			});

			_otherLinkToggles.AddRange(new[]
			{
				checkEditLinksApp,
				checkEditLinksBundle,
				checkEditLinksHtml5,
				checkEditLinksInternalLibrary,
				checkEditLinksInternalLibraryFolder,
				checkEditLinksInternalLibraryObject,
				checkEditLinksInternalLibraryPage,
				checkEditLinksInternalShortcut,
				checkEditLinksLan,
				checkEditLinksQuicksite,
				checkEditLinksUrl,
				checkEditLinksVimeo,
				checkEditLinksYoutube
			});

			_internalLinkToggles.AddRange(new[]
			{
				checkEditLinksInternalLibrary,
				checkEditLinksInternalLibraryFolder,
				checkEditLinksInternalLibraryObject,
				checkEditLinksInternalLibraryPage,
				checkEditLinksInternalShortcut
			});

			_hyperLinkToggles.AddRange(new[]
			{
				checkEditLinksApp,
				checkEditLinksBundle,
				checkEditLinksHtml5,
				checkEditLinksLan,
				checkEditLinksQuicksite,
				checkEditLinksUrl,
				checkEditLinksVimeo,
				checkEditLinksYoutube
			});

			checkEditLinks7z.Tag = LinkFileType.SevenZip;
			checkEditLinksAep.Tag = LinkFileType.Aep;
			checkEditLinksAet.Tag = LinkFileType.Aet;
			checkEditLinksAi.Tag = LinkFileType.Ai;
			checkEditLinksAit.Tag = LinkFileType.Ait;
			checkEditLinksDoc.Tag = LinkFileType.Doc;
			checkEditLinksDocx.Tag = LinkFileType.Docx;
			checkEditLinksEps.Tag = LinkFileType.Eps;
			checkEditLinksFileUndefined.Tag = LinkFileType.Other;
			checkEditLinksJpeg.Tag = LinkFileType.Jpeg;
			checkEditLinksJpg.Tag = LinkFileType.Jpg;
			checkEditLinksKey.Tag = LinkFileType.Key;
			checkEditLinksMov.Tag = LinkFileType.Mov;
			checkEditLinksMp3.Tag = LinkFileType.Mp3;
			checkEditLinksMp4.Tag = LinkFileType.Mp4;
			checkEditLinksPdd.Tag = LinkFileType.Pdd;
			checkEditLinksPdf.Tag = LinkFileType.Pdf;
			checkEditLinksPng.Tag = LinkFileType.Png;
			checkEditLinksPps.Tag = LinkFileType.Pps;
			checkEditLinksPpt.Tag = LinkFileType.Ppt;
			checkEditLinksPptx.Tag = LinkFileType.Pptx;
			checkEditLinksPsd.Tag = LinkFileType.Psd;
			checkEditLinksRar.Tag = LinkFileType.Rar;
			checkEditLinksSvg.Tag = LinkFileType.Svg;
			checkEditLinksTxt.Tag = LinkFileType.Txt;
			checkEditLinksWmv.Tag = LinkFileType.Wmv;
			checkEditLinksXls.Tag = LinkFileType.Xls;
			checkEditLinksXlsx.Tag = LinkFileType.Xlsx;
			checkEditLinksXml.Tag = LinkFileType.Xml;
			checkEditLinksZip.Tag = LinkFileType.Zip;

			checkEditLinksApp.Tag = LinkType.AppLink;
			checkEditLinksBundle.Tag = LinkType.LinkBundle;
			checkEditLinksHtml5.Tag = LinkType.Html5;
			checkEditLinksInternalLibrary.Tag = InternalLinkType.Wallbin;
			checkEditLinksInternalLibraryFolder.Tag = InternalLinkType.LibraryFolder;
			checkEditLinksInternalLibraryObject.Tag = InternalLinkType.LibraryObject;
			checkEditLinksInternalLibraryPage.Tag = InternalLinkType.LibraryPage;
			checkEditLinksInternalShortcut.Tag = InternalLinkType.Shortcut;
			checkEditLinksLan.Tag = LinkType.Network;
			checkEditLinksQuicksite.Tag = LinkType.QPageLink;
			checkEditLinksUrl.Tag = LinkType.Url;
			checkEditLinksVimeo.Tag = LinkType.Vimeo;
			checkEditLinksYoutube.Tag = LinkType.YouTube;

			layoutControlGroupLinksFiles.Enabled =
				layoutControlGroupLinksAllOthers.Enabled = false;

			layoutControlItemNotes.MinSize = RectangleHelper.ScaleSize(layoutControlItemNotes.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemNotes.MaxSize = RectangleHelper.ScaleSize(layoutControlItemNotes.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemHoverNotes.MinSize = RectangleHelper.ScaleSize(layoutControlItemHoverNotes.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemHoverNotes.MaxSize = RectangleHelper.ScaleSize(layoutControlItemHoverNotes.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExpirationDates.MinSize = RectangleHelper.ScaleSize(layoutControlItemExpirationDates.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExpirationDates.MaxSize = RectangleHelper.ScaleSize(layoutControlItemExpirationDates.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSecurity.MinSize = RectangleHelper.ScaleSize(layoutControlItemSecurity.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemSecurity.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSecurity.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));

			layoutControlItemSecurity.Visibility = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit
				? LayoutVisibility.Always
				: LayoutVisibility.Never;
		}

		public IList<LinkSettingsGroupType> GetSelectedSettingsGroups()
		{
			var linkGroupsForReset = new List<LinkSettingsGroupType>();
			if (buttonXNotes.Checked)
				linkGroupsForReset.Add(LinkSettingsGroupType.TextNote);
			if (buttonXHoverNotes.Checked)
				linkGroupsForReset.Add(LinkSettingsGroupType.HoverNote);
			if (buttonXExpirationDates.Checked)
				linkGroupsForReset.Add(LinkSettingsGroupType.Expiration);
			if (buttonXSecurity.Checked)
				linkGroupsForReset.Add(LinkSettingsGroupType.Security);
			return linkGroupsForReset;
		}

		public void ResetContent(Library library)
		{
			var linkGroupsForReset = GetSelectedSettingsGroups();

			var selectedLinkFileTypes = _fileLinkToggles
					.Where(item => item.Checked)
					.Select(item => (LinkFileType)item.Tag)
					.ToList();
			var fileLinkFilter = new Func<BaseLibraryLink, bool>(link => selectedLinkFileTypes.Contains(link.SubType));

			var selectedInternalLinkTypes = _internalLinkToggles
					.Where(item => item.Checked)
					.Select(item => (InternalLinkType)item.Tag)
					.ToList();
			var internalLinkFilter = new Func<BaseLibraryLink, bool>(link => link.Settings is InternalLinkSettings && selectedInternalLinkTypes.Contains(((InternalLinkSettings)link.Settings).InternalLinkType));

			var selectedOtherLinkTypes = _hyperLinkToggles
					.Where(item => item.Checked)
					.Select(item => (LinkType)item.Tag)
					.ToList();
			var otherlinkFilter = new Func<BaseLibraryLink, bool>(link => selectedOtherLinkTypes.Contains(link.Type));

			var linksFilter = new Func<BaseLibraryLink, bool>(link =>
				fileLinkFilter(link) ||
				internalLinkFilter(link) ||
				otherlinkFilter(link));

			library.ResetLinksToDefault(linkGroupsForReset, linksFilter);
		}

		private void OnSelectionChanged(object sender, EventArgs e)
		{
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnLinksFileAllCheckedChanged(object sender, EventArgs e)
		{
			_fileLinkToggles.ForEach(item => item.Checked = checkEditLinksFileAll.Checked);
		}

		private void OnLinksAllOthersCheckedChanged(object sender, EventArgs e)
		{
			_otherLinkToggles.ForEach(item => item.Checked = checkEditLinksAllOthers.Checked);
		}

		private void OnSettingsTypeSelectionChanged(object sender, EventArgs e)
		{
			layoutControlItemLinksFileAll.Enabled =
			layoutControlItemLinksAllOthers.Enabled =
			layoutControlGroupLinksFiles.Enabled =
			layoutControlGroupLinksAllOthers.Enabled =
				buttonXNotes.Checked || buttonXHoverNotes.Checked || buttonXExpirationDates.Checked || buttonXSecurity.Checked;
			SelectionChanged?.Invoke(sender, e);
		}
	}
}
