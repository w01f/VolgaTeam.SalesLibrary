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
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.ServiceConnector.WallbinContentService;
using Font = SalesLibraries.ServiceConnector.WallbinContentService.Font;
using LineBreak = SalesLibraries.ServiceConnector.WallbinContentService.LineBreak;

namespace SalesLibraries.FileManager.Business.Services
{
	static class WebContentManager
	{
		public static void GenerateWebContent(SalesLibraries.Business.Entities.Wallbin.Persistent.Library sourceLibrary)
		{
			var targetLibrary = new Library();
			targetLibrary.ImportData(sourceLibrary);

			var config = new LibraryConfig();
			config.libraryId = sourceLibrary.ExtId.ToString();
			config.LoadData(Configuration.RemoteResourceManager.Instance.ErrorEmailSettingsFile.LocalPath);
			targetLibrary.config = config;

			#region Pages
			var pages = new List<LibraryPage>();
			foreach (var sourcePage in sourceLibrary.Pages)
			{
				var page = new LibraryPage();
				page.ImportData(sourcePage);
				pages.Add(page);
			}
			targetLibrary.pages = pages.ToArray();
			#endregion

			var autoWidgets = new List<AutoWidget>();
			foreach (var sourceAutoWidget in sourceLibrary.Settings.AutoWidgets)
			{
				var autoWidget = new AutoWidget();
				autoWidget.libraryId = sourceLibrary.ExtId.ToString();
				autoWidget.ImportData(sourceAutoWidget);
				autoWidgets.Add(autoWidget);
			}
			targetLibrary.autoWidgets = autoWidgets.ToArray();

			var previewContainers = new List<UniversalPreviewContainer>();
			foreach (var sourcePreviewContainer in sourceLibrary.PreviewContainers)
			{
				var previewContainer = new UniversalPreviewContainer();
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
			this Library target,
			SalesLibraries.Business.Entities.Wallbin.Persistent.Library source)
		{
			target.id = source.ExtId.ToString();
			target.name = source.Name;
		}

		private static void ImportData(
			this LibraryPage target,
			SalesLibraries.Business.Entities.Wallbin.Persistent.LibraryPage source)
		{
			target.id = source.ExtId.ToString();
			target.libraryId = source.Library.ExtId.ToString();
			target.name = source.Name;
			target.order = source.Order;
			target.enableColumns = source.Settings.EnableColumnTitles;
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");

			#region Columns
			var columns = new List<Column>();
			foreach (var columnTitle in source.ColumnTitles)
			{
				var column = new Column();
				column.ImportData(columnTitle);

				#region Banner
				column.banner = new Banner();
				column.banner.libraryId = columnTitle.Page.Library.ToString();
				column.banner.ImportData(columnTitle.Banner);
				#endregion

				columns.Add(column);
			}
			target.columns = columns.ToArray();
			#endregion

			#region Folders
			var folders = new List<LibraryFolder>();
			foreach (var sourceFolder in source.Folders)
			{
				var folder = new LibraryFolder();
				folder.ImportData(sourceFolder);
				folders.Add(folder);
			}
			target.folders = folders.ToArray();
			#endregion
		}

		private static void ImportData(
			this LibraryFolder target,
			SalesLibraries.Business.Entities.Wallbin.Persistent.LibraryFolder source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.id = source.ExtId.ToString();
			target.pageId = source.Page.ExtId.ToString();
			target.libraryId = source.Page.Library.ExtId.ToString();
			target.name = source.Name;
			target.columnOrder = source.ColumnOrder;
			target.rowOrder = (int)source.RowOrder;
			target.windowBackColor = ColorTranslator.ToHtml(source.Settings.BackgroundWindowColor);
			target.windowForeColor = ColorTranslator.ToHtml(source.Settings.ForeWindowColor);
			target.headerBackColor = ColorTranslator.ToHtml(source.Settings.BackgroundHeaderColor);
			target.headerForeColor = ColorTranslator.ToHtml(source.Settings.ForeHeaderColor);
			target.borderColor = ColorTranslator.ToHtml(source.Settings.BorderColor);
			target.headerAlignment = source.Settings.HeaderAlignment.ToString().ToLower();
			target.enableWidget = source.Widget.Enabled;
			target.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Widget.Image, typeof(byte[])));
			target.windowFont = new Font();
			target.windowFont.ImportData(source.Settings.WindowFont);
			target.headerFont = new Font();
			target.headerFont.ImportData(source.Settings.HeaderFont);
			target.dateAdd = source.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");

			#region Banner
			target.banner = new Banner();
			target.banner.libraryId = source.Page.Library.ExtId.ToString();
			target.banner.ImportData(source.Banner);
			#endregion

			#region Files
			var links = new List<LibraryLink>();
			foreach (var sourceLink in source.AllLinks.Where(link =>
				link.Type != FileTypes.Network &&
				!link.Security.IsForbidden &&
				(!link.Security.IsRestricted || !String.IsNullOrEmpty(link.Security.AssignedUsers) || !String.IsNullOrEmpty(link.Security.DeniedUsers))))
			{
				var link = new LibraryLink();
				link.ImportData(sourceLink);
				links.Add(link);
			}
			target.files = links.ToArray();
			#endregion
		}

		private static void ImportData(
			this LibraryLink target,
			BaseLibraryLink source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.id = source.ExtId.ToString();
			target.folderId = source.ParentFolder.ExtId.ToString();
			target.libraryId = source.ParentLibrary.ExtId.ToString();
			target.parentLinkId = source.ExtId != source.TopLevelLink.ExtId ? source.TopLevelLink.ExtId.ToString() : null;
			target.name = source.Name;
			target.originalFormat = source.WebFormat;
			target.order = source.Order;
			target.type = (int)source.Type;
			target.widgetType = (Int32)source.Widget.WidgetType;
			target.widget = source.Widget.Enabled ? Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Widget.Image, typeof(byte[]))) : null;

			target.banner = new Banner();
			target.banner.libraryId = source.ParentLibrary.ExtId.ToString();
			target.banner.ImportData(source.Banner);

			target.extendedProperties = new LinkSettings();
			target.extendedProperties.ImportData(source.Settings);
			target.extendedProperties.ImportData(source.Security);

			if (source is LibraryObjectLink)
			{
				var sourceObject = (LibraryObjectLink)source;
				target.fileRelativePath = sourceObject.WebPath;
			}
			if (source is LibraryFileLink)
			{
				var sourceFile = (LibraryFileLink)source;
				target.fileName = sourceFile.NameWithExtension;
				target.fileExtension = sourceFile.Extension.Replace(".", String.Empty).ToLower();
				target.fileDate = File.GetLastWriteTime(sourceFile.FullPath).ToString("MM/dd/yyyy hh:mm:ss tt");
				target.isDead = sourceFile.CheckIfDead();
				if (!sourceFile.IsFolder)
					target.fileSize = (Int32)new FileInfo(sourceFile.FullPath).Length;
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

			target.dateAdd = source.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private static void ImportData(
			this LinkSettings target,
			BaseLinkSettings source)
		{
			target.note = source.Note;

			if (source is LibraryObjectLinkSettings)
			{
				var objectSource = (LibraryObjectLinkSettings)source;
				target.hoverNote = objectSource.HoverNote;
				target.isBold = objectSource.IsBold;
				target.foreColor = ColorTranslator.ToHtml(source.ForeColor.HasValue ? source.ForeColor.Value : Color.Black);
				target.isSpecialFormat = objectSource.IsSpecialFormat;
				if (objectSource.IsSpecialFormat)
				{
					if (source.Font != null)
					{
						target.font = new Font();
						target.font.ImportData(source.Font);
					}
				}
				else
					target.font = null;
			}
			if (source is VideoLinkSettings)
				target.forcePreview = ((VideoLinkSettings)source).ForcePreview;
			if (source is WebLinkSettings)
				target.forcePreview = ((WebLinkSettings)source).ForcePreview;
		}

		private static void ImportData(
			this LinkSettings target,
			SecuritySettings source)
		{
			target.isRestricted = source.IsRestricted;
			target.noShare = source.NoShare;
			target.assignedUsers = source.AssignedUsers;
			target.deniedUsers = source.DeniedUsers;
		}

		private static void ImportData(
			this LineBreak target,
			LineBreakSettings source)
		{
			target.note = source.Note;
			target.foreColor = ColorTranslator.ToHtml(source.ForeColor.HasValue ? source.ForeColor.Value : Color.Black);
			target.font = new Font();
			target.font.ImportData(source.Font);
			target.dateModify = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private static void ImportData(
			this Column target,
			SalesLibraries.Business.Entities.Wallbin.Persistent.ColumnTitle source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.pageId = source.Page.ExtId.ToString();
			target.libraryId = source.Page.Library.ExtId.ToString();
			target.name = source.Settings.Text;
			target.order = source.ColumnOrder;
			target.backColor = ColorTranslator.ToHtml(source.Settings.BackgroundColor);
			target.foreColor = ColorTranslator.ToHtml(source.Settings.ForeColor);
			target.showText = source.Settings.ShowText;
			target.alignment = source.Settings.HeaderAlignment.ToString().ToLower();
			target.enableWidget = source.Widget.Enabled;
			target.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Widget.Image, typeof(byte[])));
			target.font = new Font();
			target.font.name = source.Settings.HeaderFont.Name;
			target.font.size = (int)Math.Round(source.Settings.HeaderFont.Size, 0);
			target.font.isBold = source.Settings.HeaderFont.Bold;
			target.font.isItalic = source.Settings.HeaderFont.Italic;
			target.dateModify = source.LastModified.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private static void ImportData(
			this UniversalPreviewContainer target,
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
			this Banner target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.BannerSettings source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.id = Guid.NewGuid().ToString();
			target.isEnabled = source.Enable;
			target.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Image, typeof(byte[])));
			target.showText = source.ShowText;
			target.imageAlignment = source.ImageAlignement.ToString().ToLower();
			target.text = source.Text;
			target.foreColor = ColorTranslator.ToHtml(source.ForeColor);
			target.font = new Font();
			target.font.ImportData(source.Font);
			target.dateModify = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt");
		}

		private static void ImportData(
			this AutoWidget target,
			SalesLibraries.Business.Entities.Wallbin.NonPersistent.AutoWidget source)
		{
			var imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			target.extension = source.Extension;
			target.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(source.Widget, typeof(byte[])));
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
