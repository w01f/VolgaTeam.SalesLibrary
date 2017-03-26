using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.ServiceConnector.WallbinContentService;
using AppLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.AppLinkSettings;
using BaseLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.BaseLinkSettings;
using Font = SalesLibraries.ServiceConnector.WallbinContentService.Font;
using HyperLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.HyperLinkSettings;
using InternalWallbinLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.InternalWallbinLinkSettings;
using InternalLibraryPageLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.InternalLibraryPageLinkSettings;
using InternalLibraryFolderLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.InternalLibraryFolderLinkSettings;
using InternalLibraryObjectLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.InternalLibraryObjectLinkSettings;
using InternalShortcutLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.InternalShortcutLinkSettings;
using VideoLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.VideoLinkSettings;
using PowerPointLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.PowerPointLinkSettings;
using DocumentLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.DocumentLinkSettings;
using ExcelLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.ExcelLinkSettings;
using LineBreak = SalesLibraries.ServiceConnector.WallbinContentService.LineBreak;
using LinkBundleLinkSettings = SalesLibraries.ServiceConnector.WallbinContentService.LinkBundleLinkSettings;

namespace SalesLibraries.FileManager.Business.Services
{
	static class WebContentManager
	{
		public static void GenerateWebContent(Library sourceLibrary)
		{
			var targetLibrary = new SoapLibrary();
			targetLibrary.ImportData(sourceLibrary);

			var config = new LibraryConfig();
			config.libraryId = sourceLibrary.ExtId.ToString();
			config.LoadData(Configuration.RemoteResourceManager.Instance.ErrorEmailSettingsFile.LocalPath);
			targetLibrary.config = config;

			#region Pages
			var pages = new List<SoapLibraryPage>();
			foreach (var sourcePage in sourceLibrary.Pages)
			{
				var page = new SoapLibraryPage();
				page.ImportData(sourcePage);
				pages.Add(page);
			}
			targetLibrary.pages = pages.ToArray();
			#endregion

			var autoWidgets = new List<SoapAutoWidget>();
			foreach (var sourceAutoWidget in sourceLibrary.Settings.AutoWidgets)
			{
				var autoWidget = new SoapAutoWidget();
				autoWidget.libraryId = sourceLibrary.ExtId.ToString();
				autoWidget.ImportData(sourceAutoWidget);
				autoWidgets.Add(autoWidget);
			}
			targetLibrary.autoWidgets = autoWidgets.ToArray();

			var previewContainers = new List<SoapUniversalPreviewContainer>();
			foreach (var sourcePreviewContainer in sourceLibrary.PreviewContainers)
			{
				var previewContainer = new SoapUniversalPreviewContainer();
				previewContainer.ImportData(sourcePreviewContainer);
				previewContainers.Add(previewContainer);
			}
			targetLibrary.previewContainers = previewContainers.ToArray();

			var jsonString = JsonConvert.SerializeObject(targetLibrary);
			using (var sw = new StreamWriter(Path.Combine(sourceLibrary.Path, Constants.LibrariesJsonFileName), false))
			{
				sw.Write(jsonString);
				sw.Flush();
				sw.Close();
			}

			var xml = new StringBuilder();
			xml.AppendLine("<Library>");
			xml.AppendLine(@"<Identifier>" + sourceLibrary.ExtId + @"</Identifier>");
			xml.AppendLine(@"</Library>");
			using (var sw = new StreamWriter(Path.Combine(sourceLibrary.Path, Constants.ShortLibraryInfoFileName), false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}
		}

		private static void ImportData(
			this SoapLibrary target,
			Library source)
		{
			target.id = source.ExtId.ToString();
			target.name = source.Name;
		}

		private static void ImportData(
			this SoapLibraryPage target,
			LibraryPage source)
		{
			target.id = source.ExtId.ToString();
			target.libraryId = source.Library.ExtId.ToString();
			target.name = source.Name;
			target.order = source.Order;
			target.settings = new SoapLibraryPageSettings
			{
				icon = source.Settings.Icon,
				iconColor = source.Settings.IconColor.ToHex()
			};
			target.enableColumns = source.Settings.EnableColumnTitles;
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");

			#region Columns
			var columns = new List<SoapColumn>();
			foreach (var columnTitle in source.ColumnTitles)
			{
				var column = new SoapColumn();
				column.ImportData(columnTitle);

				#region Banner
				column.banner = new SoapBanner();
				column.banner.libraryId = columnTitle.Page.Library.ToString();
				column.banner.ImportData(columnTitle.Banner);
				#endregion

				columns.Add(column);
			}
			target.columns = columns.ToArray();
			#endregion

			#region Folders
			var folders = new List<SoapLibraryFolder>();
			foreach (var sourceFolder in source.Folders)
			{
				var folder = new SoapLibraryFolder();
				folder.ImportData(sourceFolder);
				folders.Add(folder);
			}
			target.folders = folders.ToArray();
			#endregion
		}

		private static void ImportData(
			this SoapLibraryFolder target,
			LibraryFolder source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.id = source.ExtId.ToString();
			target.pageId = source.Page.ExtId.ToString();
			target.libraryId = source.Page.Library.ExtId.ToString();
			target.name = source.Name;
			target.columnOrder = source.ColumnOrder;
			target.rowOrder = source.RowOrder;
			target.windowBackColor = source.Settings.BackgroundWindowColor.ToHex();
			target.windowForeColor = source.Settings.ForeWindowColor.ToHex();
			target.headerBackColor = source.Settings.BackgroundHeaderColor.ToHex();
			target.headerForeColor = source.Settings.ForeHeaderColor.ToHex();
			target.borderColor = source.Settings.BorderColor.ToHex();
			target.headerAlignment = source.Settings.HeaderAlignment.ToString().ToLower();
			target.enableWidget = source.Widget.Enabled;
			target.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Widget.DisplayedImage, typeof(byte[])));
			target.windowFont = new Font();
			target.windowFont.ImportData(source.Settings.WindowFont);
			target.headerFont = new Font();
			target.headerFont.ImportData(source.Settings.HeaderFont);
			target.dateAdd = source.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");

			#region Banner
			target.banner = new SoapBanner();
			target.banner.libraryId = source.Page.Library.ExtId.ToString();
			target.banner.ImportData(source.Banner);
			#endregion

			#region Files
			var links = new List<SoapLibraryLink>();
			foreach (var sourceLink in source.AllGroupLinks.Where(link =>
				!(link is LibraryFileLink && ((LibraryFileLink)link).IsDead) &&
				!link.Security.IsForbidden &&
				(!link.Security.IsRestricted || !String.IsNullOrEmpty(link.Security.AssignedUsers) || !String.IsNullOrEmpty(link.Security.DeniedUsers))))
			{
				var link = new SoapLibraryLink();
				link.ImportData(sourceLink);
				links.Add(link);
			}
			target.files = links.ToArray();
			#endregion
		}

		private static void ImportData(
			this SoapLibraryLink target,
			BaseLibraryLink source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.id = source.ExtId.ToString();
			target.folderId = source.ParentFolder.ExtId.ToString();
			target.libraryId = source.ParentLibrary.ExtId.ToString();
			target.parentLinkId = source.ExtId != source.TopLevelLink.ExtId ? source.TopLevelLink.ExtId.ToString() : null;
			target.name = source.Name;
			target.originalFormat = source.WebFormat;
			target.searchFormat = source.WebFormat;
			target.order = source.Order;
			target.type = (int)source.Type;
			target.widgetType = (Int32)source.Widget.WidgetType;
			target.widget = source.Widget.Enabled ? Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Widget.DisplayedImage, typeof(byte[]))) : null;

			target.banner = new SoapBanner();
			target.banner.libraryId = source.ParentLibrary.ExtId.ToString();
			target.banner.ImportData(source.Banner);

			target.thumbnail = new SoapThumbnail();
			target.thumbnail.ImportData(source.Thumbnail);

			target.dateAdd = source.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");

			switch (source.Type)
			{
				case FileTypes.Video:
					target.extendedProperties = new VideoLinkSettings();
					((VideoLinkSettings)target.extendedProperties).ImportData(
						(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.VideoLinkSettings)source.Settings);
					break;
				case FileTypes.PowerPoint:
					target.extendedProperties = new PowerPointLinkSettings();
					((PowerPointLinkSettings)target.extendedProperties).ImportData(
						(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.PowerPointLinkSettings)source.Settings);
					break;
				case FileTypes.Word:
				case FileTypes.Pdf:
					target.extendedProperties = new DocumentLinkSettings();
					((DocumentLinkSettings)target.extendedProperties).ImportData(
						(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.DocumentLinkSettings)source.Settings);
					break;
				case FileTypes.Excel:
					target.extendedProperties = new ExcelLinkSettings();
					((ExcelLinkSettings)target.extendedProperties).ImportData(
						(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.ExcelLinkSettings)source.Settings);
					break;
				case FileTypes.Url:
				case FileTypes.YouTube:
				case FileTypes.Vimeo:
				case FileTypes.QPageLink:
				case FileTypes.Html5:
					if (source is QuickSiteLink)
					{
						target.extendedProperties = new QPageLinkSettings();
						((QPageLinkSettings)target.extendedProperties).ImportData(
							(QuickSiteSettings)source.Settings);
					}
					else
					{
						target.extendedProperties = new HyperLinkSettings();
						((HyperLinkSettings)target.extendedProperties).ImportData(
							(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.HyperLinkSettings)source.Settings);
					}
					break;
				case FileTypes.AppLink:
					target.extendedProperties = new AppLinkSettings();
					((AppLinkSettings)target.extendedProperties).ImportData(
						(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.AppLinkSettings)source.Settings);
					break;
				case FileTypes.InternalLink:
					if (source is InternalWallbinLink)
					{
						target.extendedProperties = new InternalWallbinLinkSettings();
						((InternalWallbinLinkSettings) target.extendedProperties).ImportData(
							(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalWallbinLinkSettings) source.Settings);
					}
					else if (source is InternalLibraryPageLink)
					{
						target.extendedProperties = new InternalLibraryPageLinkSettings();
						((InternalLibraryPageLinkSettings) target.extendedProperties).ImportData(
							(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalLibraryPageLinkSettings)
								source.Settings);
					}
					else if (source is InternalLibraryFolderLink)
					{
						target.extendedProperties = new InternalLibraryFolderLinkSettings();
						((InternalLibraryFolderLinkSettings)target.extendedProperties).ImportData(
							(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalLibraryFolderLinkSettings)
								source.Settings);
					}
					else if (source is InternalLibraryObjectLink)
					{
						target.extendedProperties = new InternalLibraryObjectLinkSettings();
						((InternalLibraryObjectLinkSettings)target.extendedProperties).ImportData(
							(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalLibraryObjectLinkSettings)
								source.Settings);
					}
					else if (source is InternalShortcutLink)
					{
						target.extendedProperties = new InternalShortcutLinkSettings();
						((InternalShortcutLinkSettings)target.extendedProperties).ImportData(
							(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalShortcutLinkSettings)
								source.Settings);
					}
					break;
				case FileTypes.LinkBundle:
					target.extendedProperties = new LinkBundleLinkSettings();
					target.searchFormat =
						((SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.LinkBundleLinkSettings) source.Settings)
							.CustomWebFormat;
					((LinkBundleLinkSettings)target.extendedProperties).ImportData(
						(SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.LinkBundleLinkSettings)source.Settings);
					break;
				default:
					target.extendedProperties = new BaseLinkSettings();
					target.extendedProperties.ImportData(source.Settings);
					break;
			}
			target.extendedProperties.ImportData(source.Security);

			if (source is LibraryFileLink)
			{
				var sourceFile = (LibraryFileLink)source;
				target.fileName = sourceFile.NameWithExtension;
				target.fileExtension = sourceFile.Extension.Replace(".", String.Empty).ToLower();
				target.fileDate = File.GetLastWriteTime(sourceFile.FullPath).ToString("MM/dd/yyyy hh:mm:ss tt");
				if (!sourceFile.IsFolder)
					target.fileSize = (Int32)new FileInfo(sourceFile.FullPath).Length;
			}
			if (source is LibraryObjectLink)
			{
				var sourceObject = (LibraryObjectLink)source;
				target.fileRelativePath = sourceObject.WebPath;
				target.fileDate =
					((LibraryObjectLinkSettings)sourceObject.Settings)
						.FakeFileDate?.ToString("MM/dd/yyyy hh:mm:ss tt") ?? target.fileDate ?? target.dateAdd;

				target.extendedProperties.ImportData(sourceObject.QuickLinkSettings);
			}
			if (source is PreviewableLink)
			{
				var sourcePreviewObject = (PreviewableLink)source;
				var previewContainer = source.ParentLibrary.GetPreviewContainerBySourcePath(sourcePreviewObject.FullPath);
				if (previewContainer != null)
				{
					target.previewId = previewContainer.ExtId.ToString();
					target.contentPath = previewContainer
						.GetPreviewLinksByFormat(PreviewFormats.Text)
						.Select(path => path.Replace(source.ParentLibrary.Path, String.Empty))
						.FirstOrDefault();
				}
			}
			if (source is SalesLibraries.Business.Entities.Wallbin.Persistent.Links.LineBreak)
			{
				target.lineBreakProperties = new LineBreak();
				target.lineBreakProperties.id = source.ExtId.ToString();
				target.lineBreakProperties.libraryId = source.ParentLibrary.ExtId.ToString();
				target.lineBreakProperties.ImportData((LineBreakSettings)source.Settings);
			}

			#region Tags
			var fileSuperFilters = new List<LinkSuperFilter>();
			foreach (var superFilter in source.Tags.SuperFilters.Union(source.TopLevelLink.Tags.SuperFilters).Distinct())
			{
				var linkSuperFilter = new LinkSuperFilter();
				linkSuperFilter.libraryId = source.ParentLibrary.ExtId.ToString();
				linkSuperFilter.linkId = source.ExtId.ToString();
				linkSuperFilter.value = superFilter;
				fileSuperFilters.Add(linkSuperFilter);
			}
			if (fileSuperFilters.Any())
				target.superFilters = fileSuperFilters.ToArray();

			var fileCategories = new List<LinkCategory>();
			foreach (var searchGroup in source.Tags.Categories.Union(source.TopLevelLink.Tags.Categories))
				foreach (var tag in searchGroup.Tags)
				{
					var category = new LinkCategory();
					category.libraryId = source.ParentLibrary.ExtId.ToString();
					category.linkId = source.ExtId.ToString();
					category.group = searchGroup.SuperGroup;
					category.category = searchGroup.Name;
					category.tag = tag.Name;
					fileCategories.Add(category);
				}
			if (fileCategories.Count > 0)
				target.categories = fileCategories.ToArray();

			var keywords = source.Tags.Keywords.Union(source.Tags.Keywords).ToList();
			if (keywords.Any())
				target.tags = String.Join(" ", keywords.Select(x => x.Name).ToArray());
			#endregion
		}

		private static void ImportData(
			this IBaseLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.BaseLinkSettings source)
		{
			target.isTextWordWrap = source.TextWordWrap;
			if (source is LibraryObjectLinkSettings)
			{
				target.note = source.Note;

				var objectSource = (LibraryObjectLinkSettings)source;
				target.hoverNote = objectSource.HoverNote;
				target.showOnlyCustomHoverNote = objectSource.ShowOnlyCustomHoverNote;
				target.isBold = (objectSource.RegularFontStyle & FontStyle.Bold) == FontStyle.Bold;
				target.isItalic = (objectSource.RegularFontStyle & FontStyle.Italic) == FontStyle.Italic;
				target.isUnderline = (objectSource.RegularFontStyle & FontStyle.Underline) == FontStyle.Underline;
				target.foreColor = source.ForeColor?.ToHex();
				target.isSpecialFormat = objectSource.IsSpecialFormat;
				if (objectSource.IsSpecialFormat)
				{
					target.font = new Font();
					target.font.ImportData(source.Font);
				}
				else
					target.font = null;
			}
		}

		private static void ImportData(
			this VideoLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.VideoLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.forcePreview = source.ForcePreview;
			target.downloadSource = source.DownloadSource;
		}

		private static void ImportData(
			this PowerPointLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.PowerPointLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.slideWidth = Convert.ToSingle(source.Width);
			target.slideHeight = Convert.ToSingle(source.Height);
		}

		private static void ImportData(
			this HyperLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.HyperLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.forcePreview = source.ForcePreview;
		}

		private static void ImportData(
			this QPageLinkSettings target,
			QuickSiteSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.forcePreview = source.ForcePreview;
			target.qpageId = source.QuickSiteId;
		}
		
		private static void ImportData(
			this AppLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.AppLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.secondPath = source.SecondPath;
		}

		private static void ImportData(
			this InternalWallbinLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalWallbinLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.internalLinkType = (Int32)source.InternalLinkType;
			target.libraryName = source.LibraryName;
			target.pageName = source.PageName;
			target.showHeaderText = source.ShowHeaderText;
			target.openOnSamePage = source.OpenOnSamePage;
			target.styleSettingsEncoded = source.StyleSettings?.Serialize();
		}

		private static void ImportData(
			this InternalLibraryPageLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalLibraryPageLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.internalLinkType = (Int32)source.InternalLinkType;
			target.libraryName = source.LibraryName;
			target.pageName = source.PageName;
			target.showHeaderText = source.ShowHeaderText;
			target.openOnSamePage = source.OpenOnSamePage;
			target.styleSettingsEncoded = source.StyleSettings?.Serialize();
		}

		private static void ImportData(
			this InternalLibraryFolderLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalLibraryFolderLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.internalLinkType = (Int32)source.InternalLinkType;
			target.libraryName = source.LibraryName;
			target.pageName = source.PageName;
			target.windowName = source.WindowName;
			target.showHeaderText = source.ShowHeaderText;
			target.openOnSamePage = source.OpenOnSamePage;
			target.styleSettingsEncoded = source.StyleSettings?.Serialize();
		}

		private static void ImportData(
			this InternalLibraryObjectLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalLibraryObjectLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.internalLinkType = (Int32)source.InternalLinkType;
			target.libraryName = source.LibraryName;
			target.pageName = source.PageName;
			target.windowName = source.WindowName;
			target.linkName = source.LinkName;
		}

		private static void ImportData(
			this InternalShortcutLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.InternalShortcutLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.internalLinkType = (Int32)source.InternalLinkType;
			target.shortcutId = source.ShortcutId;
			target.openOnSamePage = source.OpenOnSamePage;
		}

		private static void ImportData(
			this ExcelLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.ExcelLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.forceDownload = source.ForceDownload;
			target.forceOpen = source.ForceOpen;
		}

		private static void ImportData(
			this DocumentLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.DocumentLinkSettings source)
		{
			((IBaseLinkSettings)target).ImportData(source);
			target.forcePreview = source.ForcePreview;
		}

		private static void ImportData(
			this IBaseLinkSettings target,
			SecuritySettings source)
		{
			target.isRestricted = source.IsRestricted;
			target.noShare = source.NoShare;
			target.assignedUsers = source.AssignedUsers;
			target.deniedUsers = source.DeniedUsers;
		}

		private static void ImportData(
			this IBaseLinkSettings target,
			QuickLinkSettings source)
		{
			target.quickLinkUrl = source.Url;
			target.quickLinkTitle = source.Title;
		}

		private static void ImportData(
			this LineBreak target,
			LineBreakSettings source)
		{
			target.note = source.Note;
			target.foreColor = source.ForeColor?.ToHex();
			target.font = new Font();
			target.font.ImportData(source.Font);
			target.dateModify = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private static void ImportData(
			this LinkBundleLinkSettings target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings.LinkBundleLinkSettings source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));

			((IBaseLinkSettings)target).ImportData(source);
			target.customWebFormat = source.CustomWebFormat;
			target.bundleItems = source.Bundle.Settings.Items
				.Where(sourceBundleItem => sourceBundleItem.Visible)
				.Select(sourceBundleItem =>
				{
					IBaseLinkBundleItem linkItem;
					switch (sourceBundleItem.ItemType)
					{
						case LinkBundleItemType.LibraryLink:
							linkItem = new LibraryLinkBundleItem();
							((LibraryLinkBundleItem)linkItem).libraryLinkId = ((LibraryLinkItem)sourceBundleItem).LinkId.ToString();
							break;
						case LinkBundleItemType.Url:
							linkItem = new UrlLinkBundleItem();
							((UrlLinkBundleItem)linkItem).url = ((UrlItem)sourceBundleItem).Url;
							break;
						case LinkBundleItemType.LaunchScreen:
							linkItem = new LinkBundleLaunchScreenItem();
							((LinkBundleLaunchScreenItem)linkItem).header = ((LaunchScreenItem)sourceBundleItem).Header;
							((LinkBundleLaunchScreenItem)linkItem).footer = ((LaunchScreenItem)sourceBundleItem).Footer;
							((LinkBundleLaunchScreenItem)linkItem).logo = Convert.ToBase64String((byte[])imageConverter.ConvertTo(((LaunchScreenItem)sourceBundleItem).Logo, typeof(byte[])));
							((LinkBundleLaunchScreenItem)linkItem).banner = Convert.ToBase64String((byte[])imageConverter.ConvertTo(((LaunchScreenItem)sourceBundleItem).Banner, typeof(byte[])));

							((LinkBundleLaunchScreenItem)linkItem).headerForeColor = ((LaunchScreenItem)sourceBundleItem).HeaderForeColor.ToHex();
							((LinkBundleLaunchScreenItem)linkItem).headerBackColor = ((LaunchScreenItem)sourceBundleItem).HeaderBackColor.ToHex();
							((LinkBundleLaunchScreenItem)linkItem).headerFont = new Font();
							((LinkBundleLaunchScreenItem)linkItem).headerFont.ImportData(((LaunchScreenItem)sourceBundleItem).HeaderFont);

							((LinkBundleLaunchScreenItem)linkItem).footerForeColor = ((LaunchScreenItem)sourceBundleItem).FooterForeColor.ToHex();
							((LinkBundleLaunchScreenItem)linkItem).footerBackColor = ((LaunchScreenItem)sourceBundleItem).FooterBackColor.ToHex();
							((LinkBundleLaunchScreenItem)linkItem).footerFont = new Font();
							((LinkBundleLaunchScreenItem)linkItem).footerFont.ImportData(((LaunchScreenItem)sourceBundleItem).FooterFont);
							break;
						case LinkBundleItemType.Info:
							linkItem = new LinkBundleInfoItem();
							((LinkBundleInfoItem)linkItem).header = ((InfoItem)sourceBundleItem).Header;
							((LinkBundleInfoItem)linkItem).body = ((InfoItem)sourceBundleItem).Body;
							((LinkBundleInfoItem)linkItem).foreColor = ((InfoItem)sourceBundleItem).ForeColor.ToHex();
							((LinkBundleInfoItem)linkItem).backColor = ((InfoItem)sourceBundleItem).BackColor.ToHex();
							((LinkBundleInfoItem)linkItem).font = new Font();
							((LinkBundleInfoItem)linkItem).font.ImportData(((InfoItem)sourceBundleItem).Font);
							break;
						case LinkBundleItemType.Strategy:
							linkItem = new LinkBundleStrategyItem();
							((LinkBundleStrategyItem)linkItem).header = ((StrategyItem)sourceBundleItem).Header;
							((LinkBundleStrategyItem)linkItem).body = ((StrategyItem)sourceBundleItem).Body;
							((LinkBundleStrategyItem)linkItem).foreColor = ((StrategyItem)sourceBundleItem).ForeColor.ToHex();
							((LinkBundleStrategyItem)linkItem).backColor = ((StrategyItem)sourceBundleItem).BackColor.ToHex();
							((LinkBundleStrategyItem)linkItem).font = new Font();
							((LinkBundleStrategyItem)linkItem).font.ImportData(((StrategyItem)sourceBundleItem).Font);
							break;
						case LinkBundleItemType.Revenue:
							linkItem = new LinkBundleRevenueItem();
							((LinkBundleRevenueItem)linkItem).revenueType = (Int32)((RevenueItem)sourceBundleItem).RevenueType;
							((LinkBundleRevenueItem)linkItem).infoItems = ((RevenueItem)sourceBundleItem).InfoItems
								.Select(sourceInfoItem =>
								{
									var infoItem = new LinkBundleRevenueInfoItem();
									infoItem.infoType = sourceInfoItem.Title;
									infoItem.value = (float)sourceInfoItem.Value;
									return infoItem;
								})
								.ToArray();
							((LinkBundleRevenueItem)linkItem).additionalInfo = ((RevenueItem)sourceBundleItem).AdditionalInfo;
							((LinkBundleRevenueItem)linkItem).foreColor = ((RevenueItem)sourceBundleItem).ForeColor.ToHex();
							((LinkBundleRevenueItem)linkItem).backColor = ((RevenueItem)sourceBundleItem).BackColor.ToHex();
							((LinkBundleRevenueItem)linkItem).font = new Font();
							((LinkBundleRevenueItem)linkItem).font.ImportData(((RevenueItem)sourceBundleItem).Font);
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
					linkItem.id = sourceBundleItem.Id.ToString();
					linkItem.itemType = (Int32)sourceBundleItem.ItemType;
					linkItem.collectionOrder = sourceBundleItem.CollectionOrder;
					linkItem.title = sourceBundleItem.Title;
					linkItem.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(sourceBundleItem.Image, typeof(byte[])));
					linkItem.hoverTip = sourceBundleItem.HoverTip;
					return linkItem;
				})
				.ToArray();
		}

		private static void ImportData(
			this SoapColumn target,
			ColumnTitle source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.pageId = source.Page.ExtId.ToString();
			target.libraryId = source.Page.Library.ExtId.ToString();
			target.name = source.Settings.Text;
			target.order = source.ColumnOrder;
			target.backColor = source.Settings.BackgroundColor.ToHex();
			target.foreColor = source.Settings.ForeColor.ToHex();
			target.showText = source.Settings.ShowText;
			target.alignment = source.Settings.HeaderAlignment.ToString().ToLower();
			target.enableWidget = source.Widget.Enabled;
			target.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Widget.DisplayedImage, typeof(byte[])));
			target.font = new Font();
			target.font.name = source.Settings.HeaderFont.Name;
			target.font.size = (int)Math.Round(source.Settings.HeaderFont.Size, 0);
			target.font.isBold = source.Settings.HeaderFont.Bold;
			target.font.isItalic = source.Settings.HeaderFont.Italic;
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private static void ImportData(
			this SoapUniversalPreviewContainer target,
			BasePreviewContainer source)
		{
			target.id = source.ExtId.ToString();
			target.libraryId = source.Library.ExtId.ToString();
			if (source is DocumentPreviewContainer && ((DocumentPreviewContainer)source).GenerateImages)
			{
				target.pngLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.Png)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray(); ;
				target.pngPhoneLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.PngForMobile)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray(); ;
				target.thumbsPhoneLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.ThumbnailsForMobile)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray();

				var thumbnails = source.GetPreviewLinksByFormat(PreviewFormats.Thumbnails).ToList();
				target.thumbsLinks = thumbnails.Select(path => path.Replace(source.Library.Path, String.Empty)).ToArray();
				if (target.thumbsLinks.Any())
				{
					var thumbSize = GetThumbSize(thumbnails.First());
					target.thumbsWidth = thumbSize.Width;
					target.thumbsHeight = thumbSize.Height;
				}
			}
			if (source is PowerPointPreviewContainer)
			{
				target.pdfLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.Pdf)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray();
				target.newOfficeFormatLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.PowerPoint)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray();
			}
			if (source is WordPreviewContainer)
			{
				target.pdfLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.Pdf)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray();
				target.newOfficeFormatLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.Word)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray();
			}
			if (source is VideoPreviewContainer)
			{
				target.mp4Links = source
					.GetPreviewLinksByFormat(PreviewFormats.VideoMp4)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray();
				target.mp4ThumbLinks = source
					.GetPreviewLinksByFormat(PreviewFormats.VideoThumbnail)
					.Select(path => path.Replace(source.Library.Path, String.Empty))
					.ToArray();
			}
		}

		private static void ImportData(
			this SoapBanner target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.BannerSettings source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.id = Guid.NewGuid().ToString();
			target.isEnabled = source.Enable;
			target.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.DisplayedImage, typeof(byte[])));
			target.showText = source.TextEnabled;
			target.imageAlignment = source.ImageAlignement.ToString().ToLower();
			target.imageVerticalAlignment = source.ImageVerticalAlignement.ToString().ToLower();
			target.text = source.TextEnabled ? source.DisplayedText : null;
			target.foreColor = source.ForeColor.ToHex();
			target.font = new Font();
			target.font.ImportData(source.Font);
			target.dateModify = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private static void ImportData(
			this SoapThumbnail target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.ThumbnailSettings source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.isEnabled = source.Enable;
			target.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Image, typeof(byte[])));
			target.imageAlignment = source.ImageAlignement.ToString().ToLower();
			target.imagePadding = source.ImagePadding;
			target.showText = source.TextEnabled;
			target.text = source.TextEnabled ? source.DisplayedText : null;
			target.foreColor = source.ForeColor.ToHex();
			target.font = new Font();
			target.font.ImportData(source.Font);
			target.textPosition = source.TextPosition.ToString().ToLower();
			target.textAlignment = source.TextAlignement.ToString().ToLower();
		}

		private static void ImportData(
			this SoapAutoWidget target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.AutoWidget source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.extension = source.Extension;
			target.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.DisplayedImage, typeof(byte[])));
		}

		private static void ImportData(
			this Font target,
			System.Drawing.Font source)
		{
			target.name = source.Name;
			target.size = (int)Math.Round(source.Size, 0);
			target.isBold = source.Bold;
			target.isItalic = source.Italic;
			target.isUnderlined = source.Underline;
		}

		private static Size GetThumbSize(string filePath)
		{
			var size = new Size(0, 0);
			try
			{
				using (var image = Image.FromFile(filePath))
				{
					size = image.Size;
				}
			}
			catch { }
			return size;
		}
	}
}
