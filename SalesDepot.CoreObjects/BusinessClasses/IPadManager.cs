﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using SalesDepot.Services.ContentManagmentService;
using SalesDepot.Services.IPadAdminService;
using SalesDepot.Services.StatisticService;
using SalesDepot.Services.TickerService;
using Attachment = SalesDepot.Services.ContentManagmentService.Attachment;
using Banner = SalesDepot.Services.ContentManagmentService.Banner;
using Column = SalesDepot.Services.ContentManagmentService.Column;
using Font = SalesDepot.Services.ContentManagmentService.Font;
using Library = SalesDepot.Services.ContentManagmentService.Library;
using LibraryLink = SalesDepot.Services.ContentManagmentService.LibraryLink;
using LineBreak = SalesDepot.Services.ContentManagmentService.LineBreak;
using LinkCategory = SalesDepot.Services.ContentManagmentService.LinkCategory;
using LibraryConfig = SalesDepot.Services.ContentManagmentService.LibraryConfig;
using UserRecord = SalesDepot.Services.IPadAdminService.UserRecord;
using GroupRecord = SalesDepot.Services.IPadAdminService.GroupRecord;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class IPadManager
	{
		private SiteClient _siteClient;

		public IPadManager(ILibrary parent)
		{
			Parent = parent;
			SyncDestinationPath = string.Empty;
			Website = string.Empty;
			Login = string.Empty;
			Password = string.Empty;
		}

		public ILibrary Parent { get; private set; }
		public bool Enabled { get; set; }
		public string SyncDestinationPath { get; set; }
		public string Website { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public IPadManager Clone(ILibrary parent)
		{
			var ipadManager = new IPadManager(parent);
			ipadManager.Enabled = Enabled;
			ipadManager.SyncDestinationPath = SyncDestinationPath;
			ipadManager.Website = Website;
			ipadManager.Login = Login;
			ipadManager.Password = Password;
			return ipadManager;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Enabled>" + Enabled.ToString() + @"</Enabled>");
			result.AppendLine(@"<SyncDestinationPath>" + SyncDestinationPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SyncDestinationPath>");
			result.AppendLine(@"<Website>" + Website.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Website>");
			result.AppendLine(@"<User>" + Login.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</User>");
			result.AppendLine(@"<Password>" + Password.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Password>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Enabled":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							Enabled = tempBool;
						break;
					case "SyncDestinationPath":
						SyncDestinationPath = childNode.InnerText;
						break;
					case "Website":
						Website = childNode.InnerText;
						break;
					case "User":
						Login = childNode.InnerText;
						break;
					case "Password":
						Password = childNode.InnerText;
						break;
				}
			}
			_siteClient = new SiteClient(Website, Login, Password);
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

			jsonString = JsonConvert.SerializeObject(PrepareCategories());
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
					foreach (var libraryFile in libraryFolder.Files.Where(x => x.Type != FileTypes.Network && (!x.IsRestricted || (x.IsRestricted && !(string.IsNullOrEmpty(x.AssignedUsers))))))
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

				if (!libraryPreviewContainer.OnlyText)
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

					var pdfLinks = libraryPreviewContainer.GetPreviewLinks("pdf");
					if (pdfLinks != null && pdfLinks.Length > 0)
						previewContainer.pdfLinks = pdfLinks;

					var oldOfficeLinks = libraryPreviewContainer.GetPreviewLinks("old office");
					if (oldOfficeLinks != null && oldOfficeLinks.Length > 0)
						previewContainer.oldOfficeFormatLinks = oldOfficeLinks;

					var newOfficeLinks = libraryPreviewContainer.GetPreviewLinks("new office");
					if (newOfficeLinks != null && newOfficeLinks.Length > 0)
						previewContainer.newOfficeFormatLinks = newOfficeLinks;

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
			destinationLink.note = libraryFile.Note;
			destinationLink.originalFormat = libraryFile.Format;
			destinationLink.isBold = libraryFile.IsBold;
			destinationLink.order = libraryFile.Order;
			destinationLink.type = (int)libraryFile.Type;
			destinationLink.enableWidget = libraryFile.EnableWidget;
			destinationLink.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFile.Widget, typeof(byte[])));
			if (topLevelFile.CustomKeywords.Tags.Count > 0)
				destinationLink.tags = string.Join(" ", topLevelFile.CustomKeywords.Tags.Select(x => x.Name).ToArray());
			destinationLink.isRestricted = topLevelFile.IsRestricted;
			if (!string.IsNullOrEmpty(topLevelFile.AssignedUsers))
				destinationLink.assignedUsers = topLevelFile.AssignedUsers;
			destinationLink.isDead = libraryFile.IsDead;
			destinationLink.forcePreview = libraryFile.ForcePreview;
			destinationLink.dateAdd = libraryFile.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
			destinationLink.dateModify = topLevelFile.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

			if (libraryFile.Type == FileTypes.BuggyPresentation || libraryFile.Type == FileTypes.FriendlyPresentation || libraryFile.Type == FileTypes.Presentation || libraryFile.Type == FileTypes.Other || libraryFile.Type == FileTypes.MediaPlayerVideo || libraryFile.Type == FileTypes.QuickTimeVideo)
			{
				var previewContainer = library.GetPreviewContainer(libraryFile.OriginalPath);
				if (previewContainer != null)
				{
					destinationLink.previewId = !previewContainer.OnlyText || libraryFile.Type == FileTypes.MediaPlayerVideo || libraryFile.Type == FileTypes.QuickTimeVideo ? previewContainer.Identifier : null;
					destinationLink.isPreviewNotReady = !previewContainer.Ready;
					var txtLinks = previewContainer.GetPreviewLinks("txt");
					if (txtLinks != null && txtLinks.Length > 0)
						destinationLink.contentPath = txtLinks[0];
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

			#region Categories
			var fileCategories = new List<LinkCategory>();
			foreach (SearchGroup searchGroup in topLevelFile.SearchTags.SearchGroups)
				foreach (SearchTag tag in searchGroup.Tags)
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
			#endregion

			#region File Card
			destinationLink.enableFileCard = topLevelFile.FileCard.Enable;
			destinationLink.fileCard = new Services.ContentManagmentService.FileCard();
			destinationLink.fileCard.id = topLevelFile.FileCard.Identifier.ToString();
			destinationLink.fileCard.libraryId = library.Identifier.ToString();
			destinationLink.fileCard.title = topLevelFile.FileCard.Title;
			destinationLink.fileCard.advertiser = topLevelFile.FileCard.Advertiser;
			destinationLink.fileCard.dateSold = topLevelFile.FileCard.DateSold.HasValue ? topLevelFile.FileCard.DateSold.Value.ToString("MM/dd/yyyy hh:mm:ss tt") : null;
			destinationLink.fileCard.broadcastClosed = topLevelFile.FileCard.BroadcastClosed.HasValue ? (float)topLevelFile.FileCard.BroadcastClosed.Value : 0;
			destinationLink.fileCard.digitalClosed = topLevelFile.FileCard.DigitalClosed.HasValue ? (float)topLevelFile.FileCard.DigitalClosed.Value : 0;
			destinationLink.fileCard.publishingClosed = topLevelFile.FileCard.PublishingClosed.HasValue ? (float)topLevelFile.FileCard.PublishingClosed.Value : 0;
			destinationLink.fileCard.salesName = topLevelFile.FileCard.SalesName;
			destinationLink.fileCard.salesEmail = topLevelFile.FileCard.SalesEmail;
			destinationLink.fileCard.salesPhone = topLevelFile.FileCard.SalesPhone;
			destinationLink.fileCard.salesStation = topLevelFile.FileCard.SalesStation;
			if (topLevelFile.FileCard.Notes.Count > 0)
				destinationLink.fileCard.notes = topLevelFile.FileCard.Notes.ToArray();
			#endregion

			#region Attachments
			var fileAttachments = new List<Attachment>();
			var attachmentProperties = topLevelFile.AttachmentProperties;
			if (attachmentProperties != null)
			{
				destinationLink.enableAttachments = attachmentProperties.Enable;
				foreach (var linkAttachment in attachmentProperties.FilesAttachments)
				{
					var attachment = new Attachment();
					attachment.linkId = libraryFile.Identifier.ToString();
					attachment.libraryId = library.Identifier.ToString();
					attachment.name = linkAttachment.Name;
					attachment.originalFormat = linkAttachment.Format;
					attachment.path = linkAttachment.DestinationRelativePath;
					attachment.isDead = !linkAttachment.IsDestinationAvailable;

					var previewContainer = library.GetPreviewContainer(linkAttachment.OriginalPath);
					if (previewContainer != null)
					{
						attachment.previewId = previewContainer.Identifier;
						attachment.isPreviewNotReady = !previewContainer.Ready;
					}
					fileAttachments.Add(attachment);
				}
				foreach (var linkAttachment in attachmentProperties.WebAttachments)
				{
					var attachment = new Attachment();
					attachment.linkId = libraryFile.Identifier.ToString();
					attachment.libraryId = library.Identifier.ToString();
					attachment.name = linkAttachment.Name;
					attachment.originalFormat = "url";
					attachment.path = linkAttachment.DestinationRelativePath;

					fileAttachments.Add(attachment);
				}
			}
			else
			{
				destinationLink.enableAttachments = false;
			}
			#endregion

			if (libraryFile is ILibraryFolderLink)
				foreach (ILibraryLink childFile in (libraryFile as ILibraryFolderLink).FolderContent)
				{
					var childLink = new LibraryLink();
					childLink.id = childFile.Identifier.ToString();
					childLink.parentLinkId = destinationLink.id;
					childLink.folderId = destinationLink.folderId;
					childLink.libraryId = destinationLink.libraryId;
					FillLinkProperties(childLink, childFile, topLevelFile, library, linksCollection);
					linksCollection.Add(childLink);
				}

			if (fileAttachments.Count > 0)
				destinationLink.attachments = fileAttachments.ToArray();
		}

		private Category[] PrepareCategories()
		{
			var searchTags = new SearchTags();
			var result = new List<Category>();
			foreach (SearchGroup group in searchTags.SearchGroups)
			{
				foreach (SearchTag tag in group.Tags)
				{
					var category = new Category();
					category.category = group.Name;
					category.tag = tag.Name;
					result.Add(category);
				}
			}
			return result.ToArray();
		}
		#endregion

		#region Permissions Manager

		#region Users
		public UserRecord[] GetUsers(out string message)
		{
			return _siteClient.GetUsers(out message);
		}

		public void SetUser(string login, string password, string firstName, string lastName, string email, string phone, GroupRecord[] groups, Services.IPadAdminService.LibraryPage[] pages, out string message)
		{
			_siteClient.SetUser(login, password, firstName, lastName, email, phone, groups, pages, out message);
		}

		public void DeleteUser(string login, out string message)
		{
			_siteClient.DeleteUser(login, out message);
		}
		#endregion

		#region Groups
		public GroupRecord[] GetGroups(out string message)
		{
			return _siteClient.GetGroups(out message);
		}

		public void SetGroup(string id, string name, UserRecord[] users, Services.IPadAdminService.LibraryPage[] pages, out string message)
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

		public void SetPage(string id, UserRecord[] users, GroupRecord[] groups, out string message)
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
				var videoFiles = new List<VideoInfo>();
				var i = 1;
				foreach (IPreviewContainer previewContainer in Parent.PreviewContainers.Where(x => !string.IsNullOrEmpty(x.OriginalPath) && x.Type == FileTypes.MediaPlayerVideo || x.Type == FileTypes.QuickTimeVideo))
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

	public class SiteClient
	{
		private string _login;
		private string _password;

		public string _website;

		public SiteClient(XmlNode node)
		{
			Deserialize(node);
		}

		public SiteClient(string website, string login, string password)
		{
			_website = website;
			_login = login;
			_password = password;
		}

		public string Website
		{
			get { return _website; }
		}

		public override string ToString()
		{
			return _website;
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Url":
						_website = childNode.InnerText;
						break;
					case "User":
						_login = childNode.InnerText;
						break;
					case "Password":
						_password = childNode.InnerText;
						break;
				}
			}
		}

		private AdminControllerService GetAdminClient()
		{
			try
			{
				var client = new AdminControllerService();
				client.Url = string.Format("{0}/admin/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		private StatisticControllerService GetStatisticClient()
		{
			try
			{
				var client = new StatisticControllerService();
				client.Url = string.Format("{0}/statistic/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		private TickerControllerService GetTickerClient()
		{
			try
			{
				var client = new TickerControllerService();
				client.Url = string.Format("{0}/ticker/quote?ws=1", _website);
				return client;
			}
			catch
			{
				return null;
			}
		}

		#region Users
		public UserRecord[] GetUsers(out string message)
		{
			message = string.Empty;
			var users = new List<UserRecord>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						users.AddRange(client.getUsers(sessionKey) ?? new UserRecord[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return users.ToArray();
		}

		public void SetUser(string login, string password, string firstName, string lastName, string email, string phone, GroupRecord[] groups, Services.IPadAdminService.LibraryPage[] pages, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setUser(sessionKey, login, password, firstName, lastName, email, phone, groups, pages);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public void SetUsers(UserInfo[] users, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						var uniqueGroups = users.SelectMany(user => user.Groups).Where(group => group.IsNew).Distinct();
						foreach (var group in uniqueGroups)
							client.setGroup(sessionKey, group.id, group.name, new UserRecord[] { }, new Services.IPadAdminService.LibraryPage[] { });
						foreach (var user in users)
							client.setUser(sessionKey, user.Login, user.Password, user.FirstName, user.LastName, user.Email, user.Phone, user.Groups.ToArray(), user.Pages.ToArray());
					}
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public void DeleteUser(string login, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteUser(sessionKey, login);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}
		#endregion

		#region Groups
		public GroupRecord[] GetGroups(out string message)
		{
			message = string.Empty;
			var groups = new List<GroupRecord>();
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						groups.AddRange(client.getGroups(sessionKey) ?? new GroupRecord[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return groups.ToArray();
		}

		public void SetGroup(string id, string name, UserRecord[] users, Services.IPadAdminService.LibraryPage[] pages, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setGroup(sessionKey, id, name, users, pages);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public void DeleteGroup(string id, out string message)
		{
			message = string.Empty;
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.deleteGroup(sessionKey, id);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		public string[] GetGroupTemplates(out string message)
		{
			message = string.Empty;
			var groupTemplates = new List<string>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						groupTemplates.AddRange(client.getGroupTemplates(sessionKey) ?? new string[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return groupTemplates.ToArray();
		}
		#endregion

		#region Libraraies
		public Services.IPadAdminService.Library[] GetLibraries(out string message)
		{
			message = string.Empty;
			var libraries = new List<Services.IPadAdminService.Library>();
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						libraries.AddRange(client.getLibraries(sessionKey) ?? new Services.IPadAdminService.Library[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return libraries.ToArray();
		}

		public void SetPage(string id, UserRecord[] users, GroupRecord[] groups, out string message)
		{
			message = string.Empty;
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setPage(sessionKey, id, users, groups);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}
		#endregion

		#region Activities
		public UserActivity[] GetActivities(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<UserActivity>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						while (startDate < endDate)
						{
							var nextDate = startDate.AddDays(10);
							if (nextDate > endDate)
								nextDate = endDate;
							activities.AddRange(client.getActivities(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), nextDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new UserActivity[] { });
							startDate = nextDate;
						}
					}
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return activities.ToArray();
		}

		public MainUserReportRecord[] GetMainUserReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<MainUserReportRecord>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getMainUserReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new MainUserReportRecord[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return activities.ToArray();
		}

		public MainGroupReportRecord[] GetMainGroupReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<MainGroupReportRecord>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getMainGroupReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new MainGroupReportRecord[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return activities.ToArray();
		}

		public NavigationUserReportRecord[] GetNavigationUserReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<NavigationUserReportRecord>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
					{
						activities.AddRange(client.getNavigationUserReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new NavigationUserReportRecord[] { });
					}
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return activities.ToArray();
		}

		public NavigationGroupReportRecord[] GetNavigationGroupReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<NavigationGroupReportRecord>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getNavigationGroupReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new NavigationGroupReportRecord[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return activities.ToArray();
		}

		public AccessReportRecord[] GetAccessReport(DateTime startDate, DateTime endDate, out string message)
		{
			message = string.Empty;
			var activities = new List<AccessReportRecord>();
			var client = GetStatisticClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						activities.AddRange(client.getAccessReport(sessionKey, startDate.ToString("MM/dd/yyyy hh:mm tt"), endDate.ToString("MM/dd/yyyy hh:mm tt")) ?? new AccessReportRecord[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return activities.ToArray();
		}
		#endregion

		#region Ticker
		public TickerLink[] GetTickerLinks(out string message)
		{
			message = string.Empty;
			var records = new List<TickerLink>();
			var client = GetTickerClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						records.AddRange(client.getTickerLinks(sessionKey) ?? new TickerLink[] { });
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
			return records.ToArray();
		}

		public void SetTickerLinks(TickerLink[] tickerLinks, out string message)
		{
			message = string.Empty;
			var client = GetTickerClient();
			if (client != null)
			{
				try
				{
					var sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setTickerLinks(sessionKey, tickerLinks);
					else
						message = "Couldn't complete operation.\nLogin or password are not correct.";
				}
				catch (Exception ex)
				{
					message = string.Format("Couldn't complete operation.\n{0}.", ex.Message);
				}
			}
			else
				message = "Couldn't complete operation.\nServer is unavailable.";
		}

		#endregion
	}
}