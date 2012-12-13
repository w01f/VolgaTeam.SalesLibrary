﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using SalesDepot.CoreObjects.ContentManagmentService;
using SalesDepot.CoreObjects.IPadAdminService;
using Attachment = SalesDepot.CoreObjects.ContentManagmentService.Attachment;
using Banner = SalesDepot.CoreObjects.ContentManagmentService.Banner;
using Column = SalesDepot.CoreObjects.ContentManagmentService.Column;
using Font = SalesDepot.CoreObjects.ContentManagmentService.Font;
using GroupRecord = SalesDepot.CoreObjects.IPadAdminService.GroupRecord;
using Library = SalesDepot.CoreObjects.ContentManagmentService.Library;
using LibraryLink = SalesDepot.CoreObjects.ContentManagmentService.LibraryLink;
using LineBreak = SalesDepot.CoreObjects.ContentManagmentService.LineBreak;
using LinkCategory = SalesDepot.CoreObjects.ContentManagmentService.LinkCategory;
using UserRecord = SalesDepot.CoreObjects.IPadAdminService.UserRecord;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class IPadManager
	{
		private SitePermissionsManager _sitePermissionsManager;

		public ILibrary Parent { get; private set; }
		public bool Enabled { get; set; }
		public string SyncDestinationPath { get; set; }
		public string Website { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public IPadManager(ILibrary parent)
		{
			Parent = parent;
			SyncDestinationPath = string.Empty;
			Website = string.Empty;
			Login = string.Empty;
			Password = string.Empty;
		}

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
			_sitePermissionsManager = new SitePermissionsManager(this.Website, this.Login, this.Password);
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

			#region Pages
			var pages = new List<ContentManagmentService.LibraryPage>();
			foreach (LibraryPage libraryPage in Parent.Pages)
			{
				var page = new ContentManagmentService.LibraryPage();
				page.id = libraryPage.Identifier.ToString();
				page.libraryId = Parent.Identifier.ToString();
				page.name = libraryPage.Name;
				page.order = libraryPage.Order;
				page.enableColumns = libraryPage.EnableColumnTitles;
				page.dateModify = libraryPage.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

				#region Columns
				var columns = new List<Column>();
				foreach (ColumnTitle columnTitle in libraryPage.ColumnTitles)
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
					column.banner.imageAlignment = columnTitle.BannerProperties.ImageAlignement.ToString().ToLower();
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
				var folders = new List<ContentManagmentService.LibraryFolder>();
				foreach (LibraryFolder libraryFolder in libraryPage.Folders)
				{
					var folder = new ContentManagmentService.LibraryFolder();
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
					foreach (ILibraryFile libraryFile in libraryFolder.Files)
					{
						var link = new LibraryLink();
						link.id = libraryFile.Identifier.ToString();
						link.folderId = libraryFolder.Identifier.ToString();
						link.libraryId = Parent.Identifier.ToString();
						link.name = libraryFile.Name;
						link.fileRelativePath = libraryFile.RelativePath;
						if (File.Exists(libraryFile.OriginalPath))
						{
							link.fileName = Path.GetFileName(libraryFile.OriginalPath);
							link.fileExtension = Path.GetExtension(libraryFile.OriginalPath).Replace(".", string.Empty).ToLower();
							link.fileDate = File.GetLastWriteTime(libraryFile.OriginalPath).ToString("MM/dd/yyyy hh:mm:ss tt");
							link.fileSize = (int)new FileInfo(libraryFile.OriginalPath).Length;
						}
						else
						{
							link.fileName = string.Empty;
							link.fileExtension = string.Empty;
						}
						link.note = libraryFile.Note;
						link.originalFormat = libraryFile.Format;
						link.isBold = libraryFile.IsBold;
						link.order = libraryFile.Order;
						link.type = (int)libraryFile.Type;
						link.enableWidget = libraryFile.EnableWidget;
						link.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFile.Widget, typeof(byte[])));
						if (libraryFile.CustomKeywords.Tags.Count > 0)
							link.tags = string.Join(" ", libraryFile.CustomKeywords.Tags.ToArray());
						link.dateAdd = libraryFile.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
						link.dateModify = libraryFile.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

						if (libraryFile.Type == FileTypes.BuggyPresentation || libraryFile.Type == FileTypes.FriendlyPresentation || libraryFile.Type == FileTypes.Presentation || libraryFile.Type == FileTypes.Other || libraryFile.Type == FileTypes.MediaPlayerVideo || libraryFile.Type == FileTypes.QuickTimeVideo)
						{
							IPreviewContainer previewContainer = Parent.GetPreviewContainer(libraryFile.OriginalPath);
							if (previewContainer != null)
							{
								link.previewId = previewContainer.Identifier;
								string[] txtLinks = previewContainer.GetPreviewLinks("txt");
								if (txtLinks != null && txtLinks.Length > 0)
									link.contentPath = txtLinks[0];
							}
						}

						#region Line Break
						if (libraryFile.LineBreakProperties != null)
						{
							link.lineBreakProperties = new LineBreak();
							link.lineBreakProperties.id = libraryFile.LineBreakProperties.Identifier.ToString();
							link.lineBreakProperties.libraryId = Parent.Identifier.ToString();
							link.lineBreakProperties.note = libraryFile.LineBreakProperties.Note;
							link.lineBreakProperties.foreColor = ColorTranslator.ToHtml(libraryFile.LineBreakProperties.ForeColor);
							link.lineBreakProperties.font = new Font();
							link.lineBreakProperties.font.name = libraryFile.LineBreakProperties.Font.Name;
							link.lineBreakProperties.font.size = (int)Math.Round(libraryFile.LineBreakProperties.Font.Size, 0);
							link.lineBreakProperties.font.isBold = libraryFile.LineBreakProperties.Font.Bold;
							link.lineBreakProperties.font.isItalic = libraryFile.LineBreakProperties.Font.Italic;
							link.lineBreakProperties.dateModify = libraryFile.LineBreakProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
						}
						#endregion

						#region Banner
						link.banner = new Banner();
						link.banner.id = libraryFile.BannerProperties.Identifier.ToString();
						link.banner.libraryId = Parent.Identifier.ToString();
						link.banner.isEnabled = libraryFile.BannerProperties.Enable;
						link.banner.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFile.BannerProperties.Image, typeof(byte[])));
						link.banner.showText = libraryFile.BannerProperties.ShowText;
						link.banner.imageAlignment = libraryFile.BannerProperties.ImageAlignement.ToString().ToLower();
						link.banner.text = libraryFile.BannerProperties.Text;
						link.banner.foreColor = ColorTranslator.ToHtml(libraryFile.BannerProperties.ForeColor);
						link.banner.font = new Font();
						link.banner.font.name = libraryFile.BannerProperties.Font.Name;
						link.banner.font.size = (int)Math.Round(libraryFile.BannerProperties.Font.Size, 0);
						link.banner.font.isBold = libraryFile.BannerProperties.Font.Bold;
						link.banner.font.isItalic = libraryFile.BannerProperties.Font.Italic;
						link.banner.dateModify = libraryFile.BannerProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
						#endregion

						#region Categories
						var categories = new List<LinkCategory>();
						foreach (SearchGroup searchGroup in libraryFile.SearchTags.SearchGroups)
							foreach (string tag in searchGroup.Tags)
							{
								var category = new LinkCategory();
								category.libraryId = Parent.Identifier.ToString();
								category.linkId = libraryFile.Identifier.ToString();
								category.category = searchGroup.Name;
								category.tag = tag;
								categories.Add(category);
							}
						if (categories.Count > 0)
							link.categories = categories.ToArray();
						#endregion

						#region File Card
						link.enableFileCard = libraryFile.FileCard.Enable;
						link.fileCard = new ContentManagmentService.FileCard();
						link.fileCard.id = libraryFile.FileCard.Identifier.ToString();
						link.fileCard.libraryId = Parent.Identifier.ToString();
						link.fileCard.title = libraryFile.FileCard.Title;
						link.fileCard.advertiser = libraryFile.FileCard.Advertiser;
						link.fileCard.dateSold = libraryFile.FileCard.DateSold.HasValue ? libraryFile.FileCard.DateSold.Value.ToString("MM/dd/yyyy hh:mm:ss tt") : null;
						link.fileCard.broadcastClosed = libraryFile.FileCard.BroadcastClosed.HasValue ? (float)libraryFile.FileCard.BroadcastClosed.Value : 0;
						link.fileCard.digitalClosed = libraryFile.FileCard.DigitalClosed.HasValue ? (float)libraryFile.FileCard.DigitalClosed.Value : 0;
						link.fileCard.publishingClosed = libraryFile.FileCard.PublishingClosed.HasValue ? (float)libraryFile.FileCard.PublishingClosed.Value : 0;
						link.fileCard.salesName = libraryFile.FileCard.SalesName;
						link.fileCard.salesEmail = libraryFile.FileCard.SalesEmail;
						link.fileCard.salesPhone = libraryFile.FileCard.SalesPhone;
						link.fileCard.salesStation = libraryFile.FileCard.SalesStation;
						if (libraryFile.FileCard.Notes.Count > 0)
							link.fileCard.notes = libraryFile.FileCard.Notes.ToArray();
						#endregion

						#region Attachments
						link.enableAttachments = libraryFile.AttachmentProperties.Enable;
						var attachments = new List<Attachment>();
						foreach (LinkAttachment linkAttachment in libraryFile.AttachmentProperties.FilesAttachments)
						{
							var attachment = new Attachment();
							attachment.linkId = libraryFile.Identifier.ToString();
							attachment.libraryId = Parent.Identifier.ToString();
							attachment.name = linkAttachment.Name;
							attachment.originalFormat = linkAttachment.Format;
							attachment.path = linkAttachment.DestinationRelativePath;

							IPreviewContainer previewContainer = Parent.GetPreviewContainer(linkAttachment.OriginalPath);
							if (previewContainer != null)
								attachment.previewId = previewContainer.Identifier;

							attachments.Add(attachment);
						}
						foreach (LinkAttachment linkAttachment in libraryFile.AttachmentProperties.WebAttachments)
						{
							var attachment = new Attachment();
							attachment.linkId = libraryFile.Identifier.ToString();
							attachment.libraryId = Parent.Identifier.ToString();
							attachment.name = linkAttachment.Name;
							attachment.originalFormat = "url";
							attachment.path = linkAttachment.DestinationRelativePath;

							attachments.Add(attachment);
						}
						if (attachments.Count > 0)
							link.attachments = attachments.ToArray();
						#endregion

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

			var autoWidgets = new List<ContentManagmentService.AutoWidget>();
			foreach (AutoWidget libraryAutoWidget in Parent.AutoWidgets)
			{
				var autoWidget = new ContentManagmentService.AutoWidget();
				autoWidget.libraryId = Parent.Identifier.ToString();
				autoWidget.extension = libraryAutoWidget.Extension;
				autoWidget.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryAutoWidget.Widget, typeof(byte[])));
				autoWidgets.Add(autoWidget);
			}
			library.autoWidgets = autoWidgets.ToArray();

			var previewContainers = new List<ContentManagmentService.UniversalPreviewContainer>();
			foreach (IPreviewContainer libraryPreviewContainer in Parent.PreviewContainers)
			{
				var previewContainer = new ContentManagmentService.UniversalPreviewContainer();
				previewContainer.id = libraryPreviewContainer.Identifier;
				previewContainer.libraryId = Parent.Identifier.ToString();

				Size thumbSize = libraryPreviewContainer.GetThumbSize();
				previewContainer.thumbsWidth = thumbSize.Width;
				previewContainer.thumbsHeight = thumbSize.Height;

				string[] pngLinks = libraryPreviewContainer.GetPreviewLinks("png");
				if (pngLinks != null && pngLinks.Length > 0)
					previewContainer.pngLinks = pngLinks;

				string[] pngPhoneLinks = libraryPreviewContainer.GetPreviewLinks("png_phone");
				if (pngPhoneLinks != null && pngPhoneLinks.Length > 0)
					previewContainer.pngPhoneLinks = pngPhoneLinks;

				string[] jpegLinks = libraryPreviewContainer.GetPreviewLinks("jpg");
				if (jpegLinks != null && jpegLinks.Length > 0)
					previewContainer.jpegLinks = jpegLinks;

				string[] jpegPhoneLinks = libraryPreviewContainer.GetPreviewLinks("jpg_phone");
				if (jpegPhoneLinks != null && jpegPhoneLinks.Length > 0)
					previewContainer.jpegPhoneLinks = jpegPhoneLinks;

				string[] pdfLinks = libraryPreviewContainer.GetPreviewLinks("pdf");
				if (pdfLinks != null && pdfLinks.Length > 0)
					previewContainer.pdfLinks = pdfLinks;

				string[] mp4Links = libraryPreviewContainer.GetPreviewLinks("mp4");
				if (mp4Links != null && mp4Links.Length > 0)
					previewContainer.mp4Links = mp4Links;

				string[] ogvLinks = libraryPreviewContainer.GetPreviewLinks("ogv");
				if (ogvLinks != null && ogvLinks.Length > 0)
					previewContainer.ogvLinks = ogvLinks;

				string[] oldOfficeLinks = libraryPreviewContainer.GetPreviewLinks("old office");
				if (oldOfficeLinks != null && oldOfficeLinks.Length > 0)
					previewContainer.oldOfficeFormatLinks = oldOfficeLinks;

				string[] newOfficeLinks = libraryPreviewContainer.GetPreviewLinks("new office");
				if (newOfficeLinks != null && newOfficeLinks.Length > 0)
					previewContainer.newOfficeFormatLinks = newOfficeLinks;

				string[] thumbsLinks = libraryPreviewContainer.GetPreviewLinks("thumbs");
				if (thumbsLinks != null && thumbsLinks.Length > 0)
					previewContainer.thumbsLinks = thumbsLinks;

				string[] thumbsPhoneLinks = libraryPreviewContainer.GetPreviewLinks("thumbs_phone");
				if (thumbsPhoneLinks != null && thumbsPhoneLinks.Length > 0)
					previewContainer.thumbsPhoneLinks = thumbsPhoneLinks;

				previewContainers.Add(previewContainer);
			}
			library.previewContainers = previewContainers.ToArray();

			return library;
		}

		private Category[] PrepareCategories()
		{
			var searchTags = new SearchTags();
			var result = new List<Category>();
			foreach (SearchGroup group in searchTags.SearchGroups)
			{
				foreach (string tag in group.Tags)
				{
					var category = new Category();
					category.category = group.Name;
					category.tag = tag;
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
			return _sitePermissionsManager.GetUsers(out message);
		}

		public void SetUser(string login, string password, string firstName, string lastName, string email, GroupRecord[] groups, IPadAdminService.LibraryPage[] pages, out string message)
		{
			_sitePermissionsManager.SetUser(login, password, firstName, lastName, email, groups, pages, out message);
		}

		public void DeleteUser(string login, out string message)
		{
			_sitePermissionsManager.DeleteUser(login, out message);
		}
		#endregion

		#region Groups
		public GroupRecord[] GetGroups(out string message)
		{
			return _sitePermissionsManager.GetGroups(out message);
		}

		public void SetGroup(string id, string name, UserRecord[] users, IPadAdminService.LibraryPage[] pages, out string message)
		{
			_sitePermissionsManager.SetGroup(id, name, users, pages, out message);
		}

		public void DeleteGroup(string id, out string message)
		{
			_sitePermissionsManager.DeleteGroup(id, out message);
		}

		public string[] GetGroupTemplates(out string message)
		{
			return _sitePermissionsManager.GetGroupTemplates(out message);
		}
		#endregion

		#region Libraraies
		public IPadAdminService.Library[] GetLibraries(out string message)
		{
			return _sitePermissionsManager.GetLibraries(out message);
		}

		public void SetPage(string id, UserRecord[] users, GroupRecord[] groups, out string message)
		{
			_sitePermissionsManager.SetPage(id, users, groups, out message);
		}
		#endregion
		#endregion

		#region Video Management
		public VideoInfo[] VideoFiles
		{
			get
			{
				var videoFiles = new List<VideoInfo>();
				int i = 1;
				foreach (IPreviewContainer previewContainer in Parent.PreviewContainers.Where(x => !string.IsNullOrEmpty(x.OriginalPath) && x.Type == FileTypes.MediaPlayerVideo || x.Type == FileTypes.QuickTimeVideo))
				{
					var videoFile = new VideoInfo(previewContainer);
					videoFile.Index = i.ToString();
					videoFile.SourceFileName = Path.GetFileName(previewContainer.OriginalPath);
					videoFile.SourceFilePath = previewContainer.OriginalPath;
					if (Directory.Exists(previewContainer.ContainerPath))
						videoFile.IPadFolderPath = previewContainer.ContainerPath;
					else
						videoFile.IPadFolderPath = null;

					string mp4Path = Path.Combine(previewContainer.ContainerPath, "mp4", Path.GetFileName(Path.ChangeExtension(previewContainer.OriginalPath, ".mp4")));
					if (File.Exists(mp4Path))
					{
						videoFile.Mp4FileName = Path.GetFileName(mp4Path);
						videoFile.Mp4FilePath = mp4Path;
						videoFile.IPadCompatible = "YES!";
					}
					else
					{
						videoFile.Mp4FileName = null;
						videoFile.Mp4FilePath = null;
						videoFile.IPadCompatible = "NO!";
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
		public string SourceFileName { get; set; }
		public string SourceFilePath { get; set; }
		public string IPadFolderPath { get; set; }
		public string Mp4FileName { get; set; }
		public string Mp4FilePath { get; set; }
		public string IPadCompatible { get; set; }
		public string OgvFileName { get; set; }
		public string OgvFilePath { get; set; }
	}

	public class SitePermissionsManager
	{

		private string _login;
		private string _password;

		public string _website;
		public string Website
		{
			get { return _website; }
		}

		public SitePermissionsManager(XmlNode node)
		{
			Deserialize(node);
		}

		public SitePermissionsManager(string website, string login, string password)
		{
			_website = website;
			_login = login;
			_password = password;
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
					string sessionKey = client.getSessionKey(_login, _password);
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

		public void SetUser(string login, string password, string firstName, string lastName, string email, GroupRecord[] groups, IPadAdminService.LibraryPage[] pages, out string message)
		{
			message = string.Empty;
			AdminControllerService client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						client.setUser(sessionKey, login, password, firstName, lastName, email, groups, pages);
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
			AdminControllerService client = GetAdminClient();
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
			AdminControllerService client = GetAdminClient();
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

		public void SetGroup(string id, string name, UserRecord[] users, IPadAdminService.LibraryPage[] pages, out string message)
		{
			message = string.Empty;
			AdminControllerService client = GetAdminClient();
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
			AdminControllerService client = GetAdminClient();
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
		public IPadAdminService.Library[] GetLibraries(out string message)
		{
			message = string.Empty;
			var libraries = new List<IPadAdminService.Library>();
			var client = GetAdminClient();
			if (client != null)
			{
				try
				{
					string sessionKey = client.getSessionKey(_login, _password);
					if (!string.IsNullOrEmpty(sessionKey))
						libraries.AddRange(client.getLibraries(sessionKey) ?? new IPadAdminService.Library[] { });
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
	}
}

namespace SalesDepot.CoreObjects.IPadAdminService
{
	public partial class UserRecord
	{
		public string FullName
		{
			get { return (firstName + " " + lastName).Trim(); }
		}

		public string LoginWithName
		{
			get { return string.Format("{0} ({1})", login, FullName); }
		}

		public string AssignedObjects
		{
			get
			{
				string result = string.Empty;
				result += "Assigned Groups: ";
				if (groups != null)
				{
					if (groups.Any(x => !x.selected))
					{
						if (groups.Any(x => x.selected))
							result += string.Join(", ", groups.Where(x => x.selected).Select(x => x.name).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "None";
				result += Environment.NewLine;
				result += "Assigned Libraries: ";
				if (libraries != null)
				{
					if (libraries.Any(x => !x.selected))
					{
						if (libraries.Any(x => x.selected))
							result += string.Join(", ", libraries.Where(x => x.selected).Select(x => x.name).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "None";
				return result;
			}
		}
	}

	public partial class GroupRecord
	{
		public string AssignedObjects
		{
			get
			{
				string result = string.Empty;
				result += "Assigned Users: ";
				if (users != null)
				{
					if (users.Any(x => !x.selected))
					{
						if (users.Any(x => x.selected))
							result += string.Join(", ", users.Where(x => x.selected).Select(x => x.login).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "None";
				result += Environment.NewLine;
				result += "Assigned Libraries: ";
				if (libraries != null)
				{
					if (libraries.Any(x => !x.selected))
					{
						if (libraries.Any(x => x.selected))
							result += string.Join(", ", libraries.Where(x => x.selected).Select(x => x.name).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "ALL";
				return result;
			}
		}
	}

	public partial class LibraryPage
	{
		public string AssignedObjects
		{
			get
			{
				string result = string.Empty;
				result += "Assigned Users: ";
				if (users != null)
				{
					if (users.Any(x => !x.selected))
					{
						if (users.Any(x => x.selected))
							result += string.Join(", ", users.Where(x => x.selected).Select(x => x.login).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "None";
				result += Environment.NewLine;
				result += "Assigned Groups: ";
				if (groups != null)
				{
					if (groups.Any(x => !x.selected))
					{
						if (groups.Any(x => x.selected))
							result += string.Join(", ", groups.Where(x => x.selected).Select(x => x.name).ToArray());
						else
							result += "None";
					}
					else
						result += "ALL";
				}
				else
					result += "None";
				return result;
			}
		}
	}
}