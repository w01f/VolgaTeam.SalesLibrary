﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public class IPadManager
    {
        public ILibrary Parent { get; private set; }
        public bool Enabled { get; set; }
        public string SyncDestinationPath { get; set; }
        public string Website { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public IPadManager(ILibrary parent)
        {
            this.Parent = parent;
            this.SyncDestinationPath = string.Empty;
            this.Website = string.Empty;
            this.Login = string.Empty;
            this.Password = string.Empty;
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Enabled>" + this.Enabled.ToString() + @"</Enabled>");
            result.AppendLine(@"<SyncDestinationPath>" + this.SyncDestinationPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SyncDestinationPath>");
            result.AppendLine(@"<Website>" + this.Website.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Website>");
            result.AppendLine(@"<User>" + this.Login.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</User>");
            result.AppendLine(@"<Password>" + this.Password.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Password>");
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
                            this.Enabled = tempBool;
                        break;
                    case "SyncDestinationPath":
                        this.SyncDestinationPath = childNode.InnerText;
                        break;
                    case "Website":
                        this.Website = childNode.InnerText;
                        break;
                    case "User":
                        this.Login = childNode.InnerText;
                        break;
                    case "Password":
                        this.Password = childNode.InnerText;
                        break;
                }
            }
        }

        #region Data Managment
        public void SaveJson()
        {
            ContentManagmentService.Library serverLibrary = PrepareServerLibrary();
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(serverLibrary);
            using (StreamWriter sw = new StreamWriter(Path.Combine(this.Parent.Folder.FullName, Constants.JsonFileName), false))
            {
                sw.Write(jsonString);
                sw.Flush();
            }
        }

        private ContentManagmentService.Library PrepareServerLibrary()
        {
            TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));

            ContentManagmentService.Library library = new ContentManagmentService.Library();
            library.id = this.Parent.Identifier.ToString();
            library.name = this.Parent.Name;

            #region Pages
            List<ContentManagmentService.LibraryPage> pages = new List<ContentManagmentService.LibraryPage>();
            foreach (LibraryPage libraryPage in this.Parent.Pages)
            {
                ContentManagmentService.LibraryPage page = new ContentManagmentService.LibraryPage();
                page.id = libraryPage.Identifier.ToString();
                page.libraryId = this.Parent.Identifier.ToString();
                page.name = libraryPage.Name;
                page.order = libraryPage.Order;
                page.enableColumns = libraryPage.EnableColumnTitles;
                page.dateModify = libraryPage.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

                #region Columns
                List<ContentManagmentService.Column> columns = new List<ContentManagmentService.Column>();
                foreach (ColumnTitle columnTitle in libraryPage.ColumnTitles)
                {
                    ContentManagmentService.Column column = new ContentManagmentService.Column();
                    column.pageId = libraryPage.Identifier.ToString();
                    column.libraryId = this.Parent.Identifier.ToString();
                    column.name = columnTitle.Name;
                    column.order = columnTitle.ColumnOrder;
                    column.backColor = ColorTranslator.ToHtml(columnTitle.BackgroundColor);
                    column.foreColor = ColorTranslator.ToHtml(columnTitle.ForeColor);
                    column.showText = columnTitle.EnableText;
                    column.alignment = columnTitle.HeaderAlignment.ToString().ToLower();
                    column.enableWidget = columnTitle.EnableWidget;
                    column.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(columnTitle.Widget, typeof(byte[])));
                    column.font = new ContentManagmentService.Font();
                    column.font.name = columnTitle.HeaderFont.Name;
                    column.font.size = (int)Math.Round(columnTitle.HeaderFont.Size, 0);
                    column.font.isBold = columnTitle.HeaderFont.Bold;
                    column.font.isItalic = columnTitle.HeaderFont.Italic;
                    column.dateModify = columnTitle.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

                    #region Banner
                    column.banner = new ContentManagmentService.Banner();
                    column.banner.id = columnTitle.BannerProperties.Identifier.ToString();
                    column.banner.libraryId = this.Parent.Identifier.ToString();
                    column.banner.isEnabled = columnTitle.BannerProperties.Enable;
                    column.banner.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(columnTitle.BannerProperties.Image, typeof(byte[])));
                    column.banner.showText = columnTitle.BannerProperties.ShowText;
                    column.banner.imageAlignment = columnTitle.BannerProperties.ImageAlignement.ToString().ToLower();
                    column.banner.text = columnTitle.BannerProperties.Text;
                    column.banner.foreColor = ColorTranslator.ToHtml(columnTitle.BannerProperties.ForeColor);
                    column.banner.font = new ContentManagmentService.Font();
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
                List<ContentManagmentService.LibraryFolder> folders = new List<ContentManagmentService.LibraryFolder>();
                foreach (LibraryFolder libraryFolder in libraryPage.Folders)
                {
                    ContentManagmentService.LibraryFolder folder = new ContentManagmentService.LibraryFolder();
                    folder.id = libraryFolder.Identifier.ToString();
                    folder.pageId = libraryPage.Identifier.ToString();
                    folder.libraryId = this.Parent.Identifier.ToString();
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
                    folder.windowFont = new ContentManagmentService.Font();
                    folder.windowFont.name = libraryFolder.WindowFont.Name;
                    folder.windowFont.size = (int)Math.Round(libraryFolder.WindowFont.Size, 0);
                    folder.windowFont.isBold = libraryFolder.WindowFont.Bold;
                    folder.windowFont.isItalic = libraryFolder.WindowFont.Italic;
                    folder.headerFont = new ContentManagmentService.Font();
                    folder.headerFont.name = libraryFolder.HeaderFont.Name;
                    folder.headerFont.size = (int)Math.Round(libraryFolder.HeaderFont.Size, 0);
                    folder.headerFont.isBold = libraryFolder.HeaderFont.Bold;
                    folder.headerFont.isItalic = libraryFolder.HeaderFont.Italic;
                    folder.dateAdd = libraryFolder.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                    folder.dateModify = libraryFolder.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

                    #region Banner
                    folder.banner = new ContentManagmentService.Banner();
                    folder.banner.id = libraryFolder.BannerProperties.Identifier.ToString();
                    folder.banner.libraryId = this.Parent.Identifier.ToString();
                    folder.banner.isEnabled = libraryFolder.BannerProperties.Enable;
                    folder.banner.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFolder.BannerProperties.Image, typeof(byte[])));
                    folder.banner.showText = libraryFolder.BannerProperties.ShowText;
                    folder.banner.imageAlignment = libraryFolder.BannerProperties.ImageAlignement.ToString().ToLower();
                    folder.banner.text = libraryFolder.BannerProperties.Text;
                    folder.banner.foreColor = ColorTranslator.ToHtml(libraryFolder.BannerProperties.ForeColor);
                    folder.banner.font = new ContentManagmentService.Font();
                    folder.banner.font.name = libraryFolder.BannerProperties.Font.Name;
                    folder.banner.font.size = (int)Math.Round(libraryFolder.BannerProperties.Font.Size, 0);
                    folder.banner.font.isBold = libraryFolder.BannerProperties.Font.Bold;
                    folder.banner.font.isItalic = libraryFolder.BannerProperties.Font.Italic;
                    folder.banner.dateModify = libraryFolder.BannerProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
                    #endregion

                    #region Files
                    List<ContentManagmentService.LibraryLink> links = new List<ContentManagmentService.LibraryLink>();
                    foreach (ILibraryFile libraryFile in libraryFolder.Files)
                    {
                        ContentManagmentService.LibraryLink link = new ContentManagmentService.LibraryLink();
                        link.id = libraryFile.Identifier.ToString();
                        link.folderId = libraryFolder.Identifier.ToString();
                        link.libraryId = this.Parent.Identifier.ToString();
                        link.name = libraryFile.Name;
                        link.fileRelativePath = libraryFile.RelativePath;
                        if (File.Exists(libraryFile.OriginalPath))
                        {
                            link.fileName = Path.GetFileName(libraryFile.OriginalPath);
                            link.fileExtension = Path.GetExtension(libraryFile.OriginalPath).Replace(".", string.Empty).ToLower();
                            link.fileDate = File.GetLastWriteTime(libraryFile.OriginalPath).ToString("MM/dd/yyyy hh:mm:ss tt");
                        }
                        else
                        {
                            link.fileName = string.Empty;
                            link.fileExtension = string.Empty;
                        }
                        link.note = libraryFile.Note;
                        link.isBold = libraryFile.IsBold;
                        link.order = libraryFile.Order;
                        link.type = (int)libraryFile.Type;
                        link.enableWidget = libraryFile.EnableWidget;
                        link.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFile.Widget, typeof(byte[])));
                        link.tags = string.Empty;
                        link.dateAdd = libraryFile.AddDate.ToString("MM/dd/yyyy hh:mm:ss tt");
                        link.dateModify = libraryFile.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");

                        #region Line Break
                        if (libraryFile.LineBreakProperties != null)
                        {
                            link.lineBreakProperties = new ContentManagmentService.LineBreak();
                            link.lineBreakProperties.id = libraryFile.LineBreakProperties.Identifier.ToString();
                            link.lineBreakProperties.libraryId = this.Parent.Identifier.ToString();
                            link.lineBreakProperties.note = libraryFile.LineBreakProperties.Note;
                            link.lineBreakProperties.foreColor = ColorTranslator.ToHtml(libraryFile.LineBreakProperties.ForeColor);
                            link.lineBreakProperties.font = new ContentManagmentService.Font();
                            link.lineBreakProperties.font.name = libraryFile.LineBreakProperties.Font.Name;
                            link.lineBreakProperties.font.size = (int)Math.Round(libraryFile.LineBreakProperties.Font.Size, 0);
                            link.lineBreakProperties.font.isBold = libraryFile.LineBreakProperties.Font.Bold;
                            link.lineBreakProperties.font.isItalic = libraryFile.LineBreakProperties.Font.Italic;
                            link.lineBreakProperties.dateModify = libraryFile.LineBreakProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
                        }
                        #endregion

                        #region Banner
                        link.banner = new ContentManagmentService.Banner();
                        link.banner.id = libraryFile.BannerProperties.Identifier.ToString();
                        link.banner.libraryId = this.Parent.Identifier.ToString();
                        link.banner.isEnabled = libraryFile.BannerProperties.Enable;
                        link.banner.image = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryFile.BannerProperties.Image, typeof(byte[])));
                        link.banner.showText = libraryFile.BannerProperties.ShowText;
                        link.banner.imageAlignment = libraryFile.BannerProperties.ImageAlignement.ToString().ToLower();
                        link.banner.text = libraryFile.BannerProperties.Text;
                        link.banner.foreColor = ColorTranslator.ToHtml(libraryFile.BannerProperties.ForeColor);
                        link.banner.font = new ContentManagmentService.Font();
                        link.banner.font.name = libraryFile.BannerProperties.Font.Name;
                        link.banner.font.size = (int)Math.Round(libraryFile.BannerProperties.Font.Size, 0);
                        link.banner.font.isBold = libraryFile.BannerProperties.Font.Bold;
                        link.banner.font.isItalic = libraryFile.BannerProperties.Font.Italic;
                        link.banner.dateModify = libraryFile.BannerProperties.LastChanged.ToString("MM/dd/yyyy hh:mm:ss tt");
                        #endregion

                        #region Preview Links
                        if (libraryFile.UniversalPreviewContainer != null)
                        {
                            link.universalPreview = new ContentManagmentService.UniversalPreviewContainer();
                            link.universalPreview.linkId = libraryFile.Identifier.ToString();
                            link.universalPreview.libraryId = this.Parent.Identifier.ToString();

                            Size thumbSize = libraryFile.UniversalPreviewContainer.GetThumbSize();
                            link.universalPreview.thumbsWidth = thumbSize.Width;
                            link.universalPreview.thumbsHeight = thumbSize.Height;

                            string[] pngLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("png");
                            if (pngLinks != null && pngLinks.Length > 0)
                                link.universalPreview.pngLinks = pngLinks;

                            string[] jpegLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("jpg");
                            if (jpegLinks != null && jpegLinks.Length > 0)
                                link.universalPreview.jpegLinks = jpegLinks;

                            string[] pdfLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("pdf");
                            if (pdfLinks != null && pdfLinks.Length > 0)
                                link.universalPreview.pdfLinks = pdfLinks;

                            string[] mp4Links = libraryFile.UniversalPreviewContainer.GetPreviewLinks("mp4");
                            if (mp4Links != null && mp4Links.Length > 0)
                                link.universalPreview.mp4Links = mp4Links;

                            string[] ogvLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("ogv");
                            if (ogvLinks != null && ogvLinks.Length > 0)
                                link.universalPreview.ogvLinks = ogvLinks;

                            string[] oldOfficeLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("old office");
                            if (oldOfficeLinks != null && oldOfficeLinks.Length > 0)
                                link.universalPreview.oldOfficeFormatLinks = oldOfficeLinks;

                            string[] newOfficeLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("new office");
                            if (newOfficeLinks != null && newOfficeLinks.Length > 0)
                                link.universalPreview.newOfficeFormatLinks = newOfficeLinks;

                            string[] txtLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("txt");
                            if (txtLinks != null && txtLinks.Length > 0)
                                link.universalPreview.txtLinks = txtLinks;

                            string[] thumbsLinks = libraryFile.UniversalPreviewContainer.GetPreviewLinks("thumbs");
                            if (thumbsLinks != null && thumbsLinks.Length > 0)
                            {
                                link.universalPreview.thumbsLinks = thumbsLinks;
                            }
                        }
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

            List<ContentManagmentService.AutoWidget> autoWidgets = new List<ContentManagmentService.AutoWidget>();
            foreach (AutoWidget libraryAutoWidget in this.Parent.AutoWidgets)
            {
                ContentManagmentService.AutoWidget autoWidget = new ContentManagmentService.AutoWidget();
                autoWidget.libraryId = this.Parent.Identifier.ToString();
                autoWidget.extension = libraryAutoWidget.Extension;
                autoWidget.widget = Convert.ToBase64String((byte[])imageConverter.ConvertTo(libraryAutoWidget.Widget, typeof(byte[])));
                autoWidgets.Add(autoWidget);
            }
            library.autoWidgets = autoWidgets.ToArray();

            return library;
        }
        #endregion

        #region User Administration
        private IPadAdminService.AdminControllerService GetAdminClient()
        {
            try
            {
                IPadAdminService.AdminControllerService client = new IPadAdminService.AdminControllerService();
                client.Url = string.Format("{0}/admin/quote?ws=1", this.Website);
                return client;
            }
            catch
            {
                return null;
            }
        }

        public IPadAdminService.UserRecord[] GetUsers(out string message)
        {
            message = string.Empty;
            List<IPadAdminService.UserRecord> users = new List<IPadAdminService.UserRecord>();
            IPadAdminService.AdminControllerService client = GetAdminClient();
            if (client != null)
            {
                try
                {
                    string sessionKey = client.getSessionKey(this.Login, this.Password);
                    if (!string.IsNullOrEmpty(sessionKey))
                        users.AddRange(client.getUsers(sessionKey));
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

        public void SetUser(string login, string password, string firstName, string lastName, string email, out string message)
        {
            message = string.Empty;
            IPadAdminService.AdminControllerService client = GetAdminClient();
            if (client != null)
            {
                try
                {
                    string sessionKey = client.getSessionKey(this.Login, this.Password);
                    if (!string.IsNullOrEmpty(sessionKey))
                        client.setUser(sessionKey, login, password, firstName, lastName, email);
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
            IPadAdminService.AdminControllerService client = GetAdminClient();
            if (client != null)
            {
                try
                {
                    string sessionKey = client.getSessionKey(this.Login, this.Password);
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

        #region Video Management
        public VideoInfo[] VideoFiles
        {
            get
            {
                List<ILibraryFile> videoLinks = new List<ILibraryFile>();
                foreach (LibraryPage page in this.Parent.Pages)
                    foreach (LibraryFolder folder in page.Folders)
                        videoLinks.AddRange(folder.Files.Where(x => x.Type == FileTypes.MediaPlayerVideo || x.Type == FileTypes.QuickTimeVideo));
                videoLinks.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(Path.GetFileName(x.OriginalPath), Path.GetFileName(y.OriginalPath)));

                List<VideoInfo> videoFiles = new List<VideoInfo>();
                int i = 1;
                foreach (ILibraryFile videoLink in videoLinks)
                {
                    VideoInfo videoFile = new VideoInfo(videoLink);
                    videoFile.Index = i.ToString();
                    videoFile.SourceFileName = Path.GetFileName(videoLink.OriginalPath);
                    videoFile.SourceFilePath = videoLink.OriginalPath;
                    if (videoLink.UniversalPreviewContainer != null)
                    {
                        if (Directory.Exists(videoLink.UniversalPreviewContainer.ContainerPath))
                            videoFile.IPadFolderPath = videoLink.UniversalPreviewContainer.ContainerPath;
                        else
                            videoFile.IPadFolderPath = null;

                        string mp4Path = Path.Combine(videoLink.UniversalPreviewContainer.ContainerPath, "mp4", Path.GetFileName(Path.ChangeExtension(videoLink.OriginalPath, ".mp4")));
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
                        string ogvPath = Path.Combine(videoLink.UniversalPreviewContainer.ContainerPath, "ogv", Path.GetFileName(Path.ChangeExtension(videoLink.OriginalPath, ".ogv")));
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
                    }
                    else
                        videoFile.IPadCompatible = "NO!";
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
        public ILibraryFile Parent { get; private set; }
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

        public VideoInfo(ILibraryFile parent)
        {
            this.Parent = parent;
        }
    }
}