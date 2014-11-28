using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using SalesDepot.Services;
using SalesDepot.Services.ContentManagmentService;
using Banner = SalesDepot.Services.ContentManagmentService.Banner;
using Column = SalesDepot.Services.ContentManagmentService.Column;
using Font = SalesDepot.Services.ContentManagmentService.Font;
using Library = SalesDepot.Services.ContentManagmentService.Library;
using LibraryLink = SalesDepot.Services.ContentManagmentService.LibraryLink;
using LineBreak = SalesDepot.Services.ContentManagmentService.LineBreak;
using LinkCategory = SalesDepot.Services.ContentManagmentService.LinkCategory;
using LibraryConfig = SalesDepot.Services.ContentManagmentService.LibraryConfig;
using UserModel = SalesDepot.Services.IPadAdminService.UserModel;
using GroupModel = SalesDepot.Services.IPadAdminService.GroupModel;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class IPadManager
	{
		private SiteClient _siteClient;
		private readonly string _website;
		private readonly string _login;
		private readonly string _password;

		public IPadManager(ILibrary parent, string website, string login, string password)
		{
			Parent = parent;
			SyncDestinationPath = String.Empty;
			_website = website;
			_login = login;
			_password = password;
		}

		public IPadManager(ILibrary parent)
		{
			Parent = parent;
			SyncDestinationPath = String.Empty;
		}

		public ILibrary Parent { get; private set; }
		public string SyncDestinationPath { get; set; }

		public IPadManager Clone(ILibrary parent)
		{
			var ipadManager = new IPadManager(parent, _website, _login, _password);
			ipadManager.SyncDestinationPath = SyncDestinationPath;
			return ipadManager;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<SyncDestinationPath>" + SyncDestinationPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SyncDestinationPath>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SyncDestinationPath":
						SyncDestinationPath = childNode.InnerText;
						break;
				}
			}
			_siteClient = new SiteClient(_website, _login, _password);
		}

		#region Content Manager
		public void SaveJson()
		{
			Library serverLibrary = PrepareServerLibrary();
			string jsonString = JsonConvert.SerializeObject(serverLibrary);
			using (var sw = new StreamWriter(Path.Combine(Parent.Folder.FullName, Constants.LibrariesJsonFileName), false))
			{
				sw.Write(jsonString);
				sw.Flush();
			}
			var references = new References();
			references.categories = PrepareCategories();
			references.superFilters = PrepareSuperFilters();
			jsonString = JsonConvert.SerializeObject(references);
			using (var sw = new StreamWriter(Path.Combine(Parent.Folder.FullName, Constants.ReferencesJsonFileName), false))
			{
				sw.Write(jsonString);
				sw.Flush();
			}
		}

		private Library PrepareServerLibrary()
		{
			TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));

			var library = new Library();
			library.id = Parent.Identifier.ToString();
			library.name = Parent.Name;

			var config = new LibraryConfig();
			config.libraryId = Parent.Identifier.ToString();
			config.LoadData(Path.Combine(Path.GetDirectoryName(typeof(IPadManager).Assembly.Location), "error_email.xml"));
			library.config = config;

			#region Pages
			var pages = new List<Services.ContentManagmentService.LibraryPage>();
			foreach (var libraryPage in Parent.Pages)
			{
				var page = new Services.ContentManagmentService.LibraryPage();
				page.id = libraryPage.Identifier.ToString();
				page.libraryId = Parent.Identifier.ToString();
				page.name = libraryPage.Name;
				page.order = libraryPage.Order;
				page.enableColumns = libraryPage.EnableColumnTitles;
				page.dateModify = libraryPage.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

				#region Columns
				var columns = new List<Column>();
				foreach (var columnTitle in libraryPage.ColumnTitles)
				{
					var column = new Column();
					column.pageId = libraryPage.Identifier.ToString();
					column.libraryId = Parent.Identifier.ToString();
					column.name = columnTitle.Name;
					column.order = columnTitle.ColumnOrder;
					column.backColor = ColorTranslator.ToHtml(columnTitle.BackgroundColor);
					column.foreColor = ColorTranslator.ToHtml(columnTitle.ForeColor);
					column.showText = columnTitle.EnableText;
					column.alignment = columnTitle.HeaderAlignment.ToString().ToLower();
					column.enableWidget = columnTitle.EnableWidget;
					column.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(columnTitle.Widget, typeof(byte[])));
					column.font = new Font();
					column.font.name = columnTitle.HeaderFont.Name;
					column.font.size = (int)Math.Round(columnTitle.HeaderFont.Size, 0);
					column.font.isBold = columnTitle.HeaderFont.Bold;
					column.font.isItalic = columnTitle.HeaderFont.Italic;
					column.dateModify = columnTitle.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

					#region Banner
					column.banner = new Banner();
					column.banner.id = columnTitle.BannerProperties.Identifier.ToString();
					column.banner.libraryId = Parent.Identifier.ToString();
					column.banner.isEnabled = columnTitle.BannerProperties.Enable;
					column.banner.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(columnTitle.BannerProperties.Image, typeof(byte[])));
					column.banner.showText = columnTitle.BannerProperties.ShowText;
					column.banner.imageAlignment = columnTitle.HeaderAlignment.ToString().ToLower();
					column.banner.text = columnTitle.BannerProperties.Text;
					column.banner.foreColor = ColorTranslator.ToHtml(columnTitle.BannerProperties.ForeColor);
					column.banner.font = new Font();
					column.banner.font.name = columnTitle.BannerProperties.Font.Name;
					column.banner.font.size = (int)Math.Round(columnTitle.BannerProperties.Font.Size, 0);
					column.banner.font.isBold = columnTitle.BannerProperties.Font.Bold;
					column.banner.font.isItalic = columnTitle.BannerProperties.Font.Italic;
					column.banner.dateModify = columnTitle.BannerProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
					#endregion

					columns.Add(column);
				}
				page.columns = columns.ToArray();
				#endregion

				#region Folders
				var folders = new List<Services.ContentManagmentService.LibraryFolder>();
				foreach (var libraryFolder in libraryPage.Folders)
				{
					var folder = new Services.ContentManagmentService.LibraryFolder();
					folder.id = libraryFolder.Identifier.ToString();
					folder.pageId = libraryPage.Identifier.ToString();
					folder.libraryId = Parent.Identifier.ToString();
					folder.name = libraryFolder.Name;
					folder.columnOrder = libraryFolder.ColumnOrder;
					folder.rowOrder = (int)libraryFolder.RowOrder;
					folder.windowBackColor = ColorTranslator.ToHtml(libraryFolder.BackgroundWindowColor);
					folder.windowForeColor = ColorTranslator.ToHtml(libraryFolder.ForeWindowColor);
					folder.headerBackColor = ColorTranslator.ToHtml(libraryFolder.BackgroundHeaderColor);
					folder.headerForeColor = ColorTranslator.ToHtml(libraryFolder.ForeHeaderColor);
					folder.borderColor = ColorTranslator.ToHtml(libraryFolder.BorderColor);
					folder.headerAlignment = libraryFolder.HeaderAlignment.ToString().ToLower();
					folder.enableWidget = libraryFolder.EnableWidget;
					folder.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFolder.Widget, typeof(byte[])));
					folder.windowFont = new Font();
					folder.windowFont.name = libraryFolder.WindowFont.Name;
					folder.windowFont.size = (int)Math.Round(libraryFolder.WindowFont.Size, 0);
					folder.windowFont.isBold = libraryFolder.WindowFont.Bold;
					folder.windowFont.isItalic = libraryFolder.WindowFont.Italic;
					folder.headerFont = new Font();
					folder.headerFont.name = libraryFolder.HeaderFont.Name;
					folder.headerFont.size = (int)Math.Round(libraryFolder.HeaderFont.Size, 0);
					folder.headerFont.isBold = libraryFolder.HeaderFont.Bold;
					folder.headerFont.isItalic = libraryFolder.HeaderFont.Italic;
					folder.dateAdd = libraryFolder.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
					folder.dateModify = libraryFolder.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

					#region Banner
					folder.banner = new Banner();
					folder.banner.id = libraryFolder.BannerProperties.Identifier.ToString();
					folder.banner.libraryId = Parent.Identifier.ToString();
					folder.banner.isEnabled = libraryFolder.BannerProperties.Enable;
					folder.banner.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFolder.BannerProperties.Image, typeof(byte[])));
					folder.banner.showText = libraryFolder.BannerProperties.ShowText;
					folder.banner.imageAlignment = libraryFolder.BannerProperties.ImageAlignement.ToString().ToLower();
					folder.banner.text = libraryFolder.BannerProperties.Text;
					folder.banner.foreColor = ColorTranslator.ToHtml(libraryFolder.BannerProperties.ForeColor);
					folder.banner.font = new Font();
					folder.banner.font.name = libraryFolder.BannerProperties.Font.Name;
					folder.banner.font.size = (int)Math.Round(libraryFolder.BannerProperties.Font.Size, 0);
					folder.banner.font.isBold = libraryFolder.BannerProperties.Font.Bold;
					folder.banner.font.isItalic = libraryFolder.BannerProperties.Font.Italic;
					folder.banner.dateModify = libraryFolder.BannerProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
					#endregion

					#region Files
					var links = new List<LibraryLink>();
					foreach (var libraryFile in libraryFolder.Files.Where(link => link.Type != FileTypes.Network &&
						!link.ExtendedProperties.IsForbidden &&
						(!link.ExtendedProperties.IsRestricted || (link.ExtendedProperties.IsRestricted && (!string.IsNullOrEmpty(link.ExtendedProperties.AssignedUsers) || !string.IsNullOrEmpty(link.ExtendedProperties.DeniedUsers))))))
					{
						var link = new LibraryLink();
						link.id = libraryFile.Identifier.ToString();
						link.folderId = libraryFolder.Identifier.ToString();
						link.libraryId = Parent.Identifier.ToString();
						FillLinkProperties(link, libraryFile, libraryFile, Parent, links);
						links.Add(link);
					}
					folder.files = links.ToArray();
					#endregion

					folders.Add(folder);
				}
				page.folders = folders.ToArray();
				#endregion

				pages.Add(page);
			}
			library.pages = pages.ToArray();
			#endregion

			var autoWidgets = new List<Services.ContentManagmentService.AutoWidget>();
			foreach (var libraryAutoWidget in Parent.AutoWidgets)
			{
				var autoWidget = new Services.ContentManagmentService.AutoWidget();
				autoWidget.libraryId = Parent.Identifier.ToString();
				autoWidget.extension = libraryAutoWidget.Extension;
				autoWidget.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryAutoWidget.Widget, typeof(byte[])));
				autoWidgets.Add(autoWidget);
			}
			library.autoWidgets = autoWidgets.ToArray();

			var previewContainers = new List<Services.ContentManagmentService.UniversalPreviewContainer>();
			foreach (var libraryPreviewContainer in Parent.PreviewContainers)
			{
				var previewContainer = new Services.ContentManagmentService.UniversalPreviewContainer();
				previewContainer.id = libraryPreviewContainer.Identifier;
				previewContainer.libraryId = Parent.Identifier.ToString();

				var pdfLinks = libraryPreviewContainer.GetPreviewLinks("pdf");
				if (pdfLinks != null && pdfLinks.Length > 0)
					previewContainer.pdfLinks = pdfLinks;

				var oldOfficeLinks = libraryPreviewContainer.GetPreviewLinks("old office");
				if (oldOfficeLinks != null && oldOfficeLinks.Length > 0)
					previewContainer.oldOfficeFormatLinks = oldOfficeLinks;

				var newOfficeLinks = libraryPreviewContainer.GetPreviewLinks("new office");
				if (newOfficeLinks != null && newOfficeLinks.Length > 0)
					previewContainer.newOfficeFormatLinks = newOfficeLinks;

				if (libraryPreviewContainer.GenerateImages)
				{
					var thumbSize = libraryPreviewContainer.GetThumbSize();
					previewContainer.thumbsWidth = thumbSize.Width;
					previewContainer.thumbsHeight = thumbSize.Height;

					var pngLinks = libraryPreviewContainer.GetPreviewLinks("png");
					if (pngLinks != null && pngLinks.Length > 0)
						previewContainer.pngLinks = pngLinks;

					var pngPhoneLinks = libraryPreviewContainer.GetPreviewLinks("png_phone");
					if (pngPhoneLinks != null && pngPhoneLinks.Length > 0)
						previewContainer.pngPhoneLinks = pngPhoneLinks;

					var jpegLinks = libraryPreviewContainer.GetPreviewLinks("jpg");
					if (jpegLinks != null && jpegLinks.Length > 0)
						previewContainer.jpegLinks = jpegLinks;

					var jpegPhoneLinks = libraryPreviewContainer.GetPreviewLinks("jpg_phone");
					if (jpegPhoneLinks != null && jpegPhoneLinks.Length > 0)
						previewContainer.jpegPhoneLinks = jpegPhoneLinks;

					var thumbsLinks = libraryPreviewContainer.GetPreviewLinks("thumbs");
					if (thumbsLinks != null && thumbsLinks.Length > 0)
						previewContainer.thumbsLinks = thumbsLinks;

					var thumbsPhoneLinks = libraryPreviewContainer.GetPreviewLinks("thumbs_phone");
					if (thumbsPhoneLinks != null && thumbsPhoneLinks.Length > 0)
						previewContainer.thumbsPhoneLinks = thumbsPhoneLinks;
				}
				var wmvLinks = libraryPreviewContainer.GetPreviewLinks("wmv");
				if (wmvLinks != null && wmvLinks.Length > 0)
					previewContainer.wmvLinks = wmvLinks;

				var mp4Links = libraryPreviewContainer.GetPreviewLinks("mp4");
				if (mp4Links != null && mp4Links.Length > 0)
					previewContainer.mp4Links = mp4Links;

				var ogvLinks = libraryPreviewContainer.GetPreviewLinks("ogv");
				if (ogvLinks != null && ogvLinks.Length > 0)
					previewContainer.ogvLinks = ogvLinks;
				previewContainers.Add(previewContainer);
			}
			library.previewContainers = previewContainers.ToArray();

			return library;
		}

		public void FillLinkProperties(LibraryLink destinationLink, ILibraryLink libraryFile, ILibraryLink topLevelFile, ILibrary library, List<LibraryLink> linksCollection)
		{
			TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			destinationLink.name = libraryFile.Name;
			destinationLink.fileRelativePath = libraryFile.WebPath;
			if (File.Exists(libraryFile.OriginalPath))
			{
				destinationLink.fileName = Path.GetFileName(libraryFile.OriginalPath);
				destinationLink.fileExtension = Path.GetExtension(libraryFile.OriginalPath).Replace(".", string.Empty).ToLower();
				destinationLink.fileDate = File.GetLastWriteTime(libraryFile.OriginalPath).ToString("MM/dd/yyyy hh:mm:ss tt");
				destinationLink.fileSize = (int)new FileInfo(libraryFile.OriginalPath).Length;
			}
			else
			{
				destinationLink.fileName = string.Empty;
				destinationLink.fileExtension = string.Empty;
			}
			destinationLink.originalFormat = libraryFile.Format;
			destinationLink.order = libraryFile.Order;
			destinationLink.type = (int)libraryFile.Type;
			destinationLink.enableWidget = libraryFile.EnableWidget;
			destinationLink.widget = libraryFile.EnableWidget ? Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFile.Widget, typeof(byte[]))) : null;

			destinationLink.extendedProperties = new LinkSettings();
			destinationLink.extendedProperties.note = libraryFile.ExtendedProperties.Note;
			destinationLink.extendedProperties.hoverNote = libraryFile.ExtendedProperties.HoverNote;
			destinationLink.extendedProperties.isBold = libraryFile.ExtendedProperties.IsBold;
			destinationLink.extendedProperties.isSpecialFormat = libraryFile.ExtendedProperties.IsSpecialFormat;
			if (libraryFile.ExtendedProperties.IsSpecialFormat)
			{
				destinationLink.extendedProperties.foreColor = ColorTranslator.ToHtml(libraryFile.ExtendedProperties.ForeColor);
				if (libraryFile.ExtendedProperties.Font != null)
				{
					destinationLink.extendedProperties.font = new Font();
					destinationLink.extendedProperties.font.name = libraryFile.ExtendedProperties.Font.Name;
					destinationLink.extendedProperties.font.size = (int)Math.Round(libraryFile.ExtendedProperties.Font.Size, 0);
					destinationLink.extendedProperties.font.isBold = libraryFile.ExtendedProperties.Font.Bold;
					destinationLink.extendedProperties.font.isItalic = libraryFile.ExtendedProperties.Font.Italic;
				}
			}
			else
			{
				destinationLink.extendedProperties.foreColor = ColorTranslator.ToHtml(Color.Black);
				destinationLink.extendedProperties.font = null;
			}
			destinationLink.extendedProperties.isRestricted = topLevelFile.ExtendedProperties.IsRestricted;
			destinationLink.extendedProperties.noShare = topLevelFile.ExtendedProperties.NoShare;
			if (!string.IsNullOrEmpty(topLevelFile.ExtendedProperties.AssignedUsers))
				destinationLink.extendedProperties.assignedUsers = topLevelFile.ExtendedProperties.AssignedUsers;
			if (!string.IsNullOrEmpty(topLevelFile.ExtendedProperties.DeniedUsers))
				destinationLink.extendedProperties.deniedUsers = topLevelFile.ExtendedProperties.DeniedUsers;
			destinationLink.extendedProperties.forcePreview = libraryFile.ExtendedProperties.ForcePreview;
			destinationLink.isDead = libraryFile.IsDead;
			destinationLink.dateAdd = libraryFile.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
			destinationLink.dateModify = topLevelFile.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

			if (libraryFile.Type == FileTypes.BuggyPresentation ||
				libraryFile.Type == FileTypes.FriendlyPresentation ||
				libraryFile.Type == FileTypes.Presentation ||
				libraryFile.Type == FileTypes.Other ||
				libraryFile.Type == FileTypes.MediaPlayerVideo ||
				libraryFile.Type == FileTypes.QuickTimeVideo)
			{
				var previewContainer = library.GetPreviewContainer(libraryFile.OriginalPath);
				if (previewContainer != null)
				{
					destinationLink.previewId = previewContainer.Identifier;
					destinationLink.isPreviewNotReady = !previewContainer.Ready;
					if (previewContainer.GenerateText)
					{
						var txtLinks = previewContainer.GetPreviewLinks("txt");
						if (txtLinks != null && txtLinks.Length > 0)
							destinationLink.contentPath = txtLinks[0];
					}
				}
			}

			#region Line Break
			if (libraryFile.LineBreakProperties != null)
			{
				destinationLink.lineBreakProperties = new LineBreak();
				destinationLink.lineBreakProperties.id = libraryFile.LineBreakProperties.Identifier.ToString();
				destinationLink.lineBreakProperties.libraryId = library.Identifier.ToString();
				destinationLink.lineBreakProperties.note = libraryFile.LineBreakProperties.Note;
				destinationLink.lineBreakProperties.foreColor = ColorTranslator.ToHtml(libraryFile.LineBreakProperties.ForeColor);
				destinationLink.lineBreakProperties.font = new Font();
				destinationLink.lineBreakProperties.font.name = libraryFile.LineBreakProperties.Font.Name;
				destinationLink.lineBreakProperties.font.size = (int)Math.Round(libraryFile.LineBreakProperties.Font.Size, 0);
				destinationLink.lineBreakProperties.font.isBold = libraryFile.LineBreakProperties.Font.Bold;
				destinationLink.lineBreakProperties.font.isItalic = libraryFile.LineBreakProperties.Font.Italic;
				destinationLink.lineBreakProperties.dateModify = libraryFile.LineBreakProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
			}
			#endregion

			#region Banner
			destinationLink.banner = new Banner();
			destinationLink.banner.id = libraryFile.BannerProperties.Identifier.ToString();
			destinationLink.banner.libraryId = library.Identifier.ToString();
			destinationLink.banner.isEnabled = libraryFile.BannerProperties.Enable;
			destinationLink.banner.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFile.BannerProperties.Image, typeof(byte[])));
			destinationLink.banner.showText = libraryFile.BannerProperties.ShowText;
			destinationLink.banner.imageAlignment = libraryFile.BannerProperties.ImageAlignement.ToString().ToLower();
			destinationLink.banner.text = libraryFile.BannerProperties.Text;
			destinationLink.banner.foreColor = ColorTranslator.ToHtml(libraryFile.BannerProperties.ForeColor);
			destinationLink.banner.font = new Font();
			destinationLink.banner.font.name = libraryFile.BannerProperties.Font.Name;
			destinationLink.banner.font.size = (int)Math.Round(libraryFile.BannerProperties.Font.Size, 0);
			destinationLink.banner.font.isBold = libraryFile.BannerProperties.Font.Bold;
			destinationLink.banner.font.isItalic = libraryFile.BannerProperties.Font.Italic;
			destinationLink.banner.dateModify = libraryFile.BannerProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
			#endregion

			#region Tags
			var fileSuperFilters = new List<LinkSuperFilter>();
			foreach (var superFilter in topLevelFile.SuperFilters)
			{
				var linkSuperFilter = new LinkSuperFilter();
				linkSuperFilter.libraryId = library.Identifier.ToString();
				linkSuperFilter.linkId = libraryFile.Identifier.ToString();
				linkSuperFilter.value = superFilter.Name;
				fileSuperFilters.Add(linkSuperFilter);
			}
			if (fileSuperFilters.Count > 0)
				destinationLink.superFilters = fileSuperFilters.ToArray();

			var fileCategories = new List<LinkCategory>();
			foreach (var searchGroup in libraryFile.SearchTags.SearchGroups
				.Union(topLevelFile.SearchTags.SearchGroups))
				foreach (var tag in searchGroup.Tags)
				{
					var category = new LinkCategory();
					category.libraryId = library.Identifier.ToString();
					category.linkId = libraryFile.Identifier.ToString();
					category.category = searchGroup.Name;
					category.tag = tag.Name;
					fileCategories.Add(category);
				}
			if (fileCategories.Count > 0)
				destinationLink.categories = fileCategories.ToArray();

			var keywords = libraryFile.CustomKeywords.Tags.Union(topLevelFile.CustomKeywords.Tags);
			if (keywords.Any())
				destinationLink.tags = String.Join(" ", keywords.Select(x => x.Name).ToArray());
			#endregion

			if (libraryFile is ILibraryFolderLink)
				foreach (var childFile in (libraryFile as ILibraryFolderLink).FolderContent)
				{
					var childLink = new LibraryLink();
					childLink.id = childFile.Identifier.ToString();
					childLink.parentLinkId = destinationLink.id;
					childLink.folderId = destinationLink.folderId;
					childLink.libraryId = destinationLink.libraryId;
					FillLinkProperties(childLink, childFile, topLevelFile, library, linksCollection);
					linksCollection.Add(childLink);
				}
		}

		private Category[] PrepareCategories()
		{
			var searchTags = new SearchTags();
			var result = new List<Category>();
			foreach (var group in searchTags.SearchGroups)
			{
				foreach (var tag in group.Tags)
				{
					var category = new Category();
					category.category = group.Name;
					category.tag = tag.Name;
					result.Add(category);
				}
			}
			return result.ToArray();
		}

		private Services.ContentManagmentService.SuperFilter[] PrepareSuperFilters()
		{
			var result = new List<Services.ContentManagmentService.SuperFilter>();
			foreach (var value in SuperFilter.LoadSuperFilters())
			{
				var superFilter = new Services.ContentManagmentService.SuperFilter();
				superFilter.value = value.Name;
				result.Add(superFilter);
			}
			return result.ToArray();
		}
		#endregion

		#region Permissions Manager

		#region Users
		public bool IsUserPasswordComplex(out string message)
		{
			return _siteClient.IsUserPasswordComplex(out message);
		}

		public UserModel[] GetUsers(out string message)
		{
			return _siteClient.GetUsers(out message);
		}

		public void SetUser(string login, string password, string firstName, string lastName, string email, string phone, int role, GroupModel[] groups, Services.IPadAdminService.LibraryPage[] pages, out string message)
		{
			_siteClient.SetUser(login, password, firstName, lastName, email, phone, role, groups, pages, out message);
		}

		public void DeleteUser(string login, out string message)
		{
			_siteClient.DeleteUser(login, out message);
		}
		#endregion

		#region Groups
		public GroupModel[] GetGroups(out string message)
		{
			return _siteClient.GetGroups(out message);
		}

		private List<GroupModel> _cachedSecurityGroups;
		public IEnumerable<GroupModel> GetGroupsByLibrary(out string message)
		{
			message = String.Empty;
			if (_cachedSecurityGroups == null)
			{
				_cachedSecurityGroups = new List<GroupModel>();
				_cachedSecurityGroups.AddRange(_siteClient.GetGroupsByLibrary(Parent.Identifier.ToString(), out message));
			}
			return _cachedSecurityGroups;
		}

		public void SetGroup(string id, string name, UserModel[] users, Services.IPadAdminService.LibraryPage[] pages, out string message)
		{
			_siteClient.SetGroup(id, name, users, pages, out message);
		}

		public void DeleteGroup(string id, out string message)
		{
			_siteClient.DeleteGroup(id, out message);
		}

		public string[] GetGroupTemplates(out string message)
		{
			return _siteClient.GetGroupTemplates(out message);
		}
		#endregion

		#region Libraraies
		public Services.IPadAdminService.Library[] GetLibraries(out string message)
		{
			return _siteClient.GetLibraries(out message);
		}

		public void SetPage(string id, UserModel[] users, GroupModel[] groups, out string message)
		{
			_siteClient.SetPage(id, users, groups, out message);
		}
		#endregion

		#endregion

		#region Video Management
		public VideoInfo[] VideoFiles
		{
			get
			{
				Parent.UpdatePreviewContainers();
				var videoFiles = new List<VideoInfo>();
				var i = 1;
				foreach (var previewContainer in Parent.PreviewContainers
					.Where(container => !String.IsNullOrEmpty(container.OriginalPath) &&
						File.Exists(container.OriginalPath) &&
						(container.Type == FileTypes.MediaPlayerVideo || container.Type == FileTypes.QuickTimeVideo)))
				{
					var videoFile = new VideoInfo(previewContainer);
					videoFile.Index = i.ToString();
					videoFile.Converted = previewContainer.Ready;
					videoFile.SourceFileName = Path.GetFileName(previewContainer.OriginalPath);
					videoFile.SourceFilePath = previewContainer.OriginalPath;
					videoFile.SourceFolderPath = Path.GetDirectoryName(previewContainer.OriginalPath);
					videoFile.IPadFolderPath = Directory.Exists(previewContainer.ContainerPath) ? previewContainer.ContainerPath : null;

					var wmvPath = previewContainer.Type == FileTypes.MediaPlayerVideo && previewContainer.Extension.ToUpper().Equals(".WMV")
										 ? videoFile.SourceFilePath
										 : Path.Combine(previewContainer.ContainerPath, "wmv", Path.GetFileName(Path.ChangeExtension(previewContainer.OriginalPath, ".wmv")));
					if (File.Exists(wmvPath))
					{
						videoFile.WmvFileName = Path.GetFileName(wmvPath);
						videoFile.WmvFilePath = wmvPath;
					}
					else
					{
						videoFile.WmvFileName = null;
						videoFile.WmvFilePath = null;
					}
					var mp4Path = previewContainer.Type == FileTypes.QuickTimeVideo && previewContainer.Extension.ToUpper().Equals(".MP4")
										 ? videoFile.SourceFilePath
										 : Path.Combine(previewContainer.ContainerPath, "mp4", Path.GetFileName(Path.ChangeExtension(previewContainer.OriginalPath, ".mp4")));
					if (File.Exists(mp4Path))
					{
						videoFile.Mp4FileName = Path.GetFileName(mp4Path);
						videoFile.Mp4FilePath = mp4Path;
					}
					else
					{
						videoFile.Mp4FileName = null;
						videoFile.Mp4FilePath = null;
					}
					string ogvPath = Path.Combine(previewContainer.ContainerPath, "ogv", Path.GetFileName(Path.ChangeExtension(previewContainer.OriginalPath, ".ogv")));
					if (File.Exists(ogvPath))
					{
						videoFile.OgvFileName = Path.GetFileName(ogvPath);
						videoFile.OgvFilePath = ogvPath;
					}
					else
					{
						videoFile.OgvFileName = null;
						videoFile.OgvFilePath = null;
					}
					videoFiles.Add(videoFile);
					i++;
					Application.DoEvents();
				}
				return videoFiles.ToArray();
			}
		}
		#endregion
	}

	public class VideoInfo
	{
		public VideoInfo(IPreviewContainer parent)
		{
			Parent = parent;
		}

		public IPreviewContainer Parent { get; private set; }
		public string Index { get; set; }
		public bool Selected { get; set; }
		public bool Converted { get; set; }
		public string SourceFileName { get; set; }
		public string SourceFilePath { get; set; }
		public string SourceFolderPath { get; set; }
		public string IPadFolderPath { get; set; }
		public string WmvFileName { get; set; }
		public string WmvFilePath { get; set; }
		public string Mp4FileName { get; set; }
		public string Mp4FilePath { get; set; }
		public string OgvFileName { get; set; }
		public string OgvFilePath { get; set; }
	}
}