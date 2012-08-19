using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace FileManager.BusinessClasses
{
    public class IPadManager
    {
        public Library Parent { get; private set; }
        public string SyncDestinationPath { get; set; }
        public string Website { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public IPadManager(Library parent)
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
            result.AppendLine(@"<SyncDestinationPath>" + this.SyncDestinationPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</SyncDestinationPath>");
            result.AppendLine(@"<Website>" + this.Website.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Website>");
            result.AppendLine(@"<User>" + this.Login.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</User>");
            result.AppendLine(@"<Password>" + this.Password.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Password>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
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

        #region User Management
        private IPadAdminService.AdminControllerService GetClient()
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
            IPadAdminService.AdminControllerService client = GetClient();
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

        public void SetUser(string login, string password, string firstName, string lastName, string email,out string message)
        {
            message = string.Empty;
            IPadAdminService.AdminControllerService client = GetClient();
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
            IPadAdminService.AdminControllerService client = GetClient();
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
                catch(Exception ex)
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
                List<LibraryFile> videoLinks = new List<LibraryFile>();
                foreach (LibraryPage page in this.Parent.Pages)
                    foreach (LibraryFolder folder in page.Folders)
                        videoLinks.AddRange(folder.Files.Where(x => x.Type == FileTypes.MediaPlayerVideo || x.Type == FileTypes.QuickTimeVideo));
                videoLinks.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(Path.GetFileName(x.FullPath), Path.GetFileName(y.FullPath)));

                List<VideoInfo> videoFiles = new List<VideoInfo>();
                int i = 1;
                foreach (LibraryFile videoLink in videoLinks)
                {
                    VideoInfo videoFile = new VideoInfo(videoLink);
                    videoFile.Index = i.ToString();
                    videoFile.SourceFileName = Path.GetFileName(videoLink.FullPath);
                    videoFile.SourceFilePath = videoLink.FullPath;
                    if (videoLink.UniversalPreviewContainer != null)
                    {
                        if (Directory.Exists(videoLink.UniversalPreviewContainer.ContainerPath))
                            videoFile.IPadFolderPath = videoLink.UniversalPreviewContainer.ContainerPath;
                        else
                            videoFile.IPadFolderPath = null;

                        string mp4Path = Path.Combine(videoLink.UniversalPreviewContainer.ContainerPath, "mp4", Path.GetFileName(Path.ChangeExtension(videoLink.FullPath, ".mp4")));
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
                        string ogvPath = Path.Combine(videoLink.UniversalPreviewContainer.ContainerPath, "ogv", Path.GetFileName(Path.ChangeExtension(videoLink.FullPath, ".ogv")));
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
        public LibraryFile Parent { get; private set; }
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

        public VideoInfo(LibraryFile parent)
        {
            this.Parent = parent;
        }
    }
}
