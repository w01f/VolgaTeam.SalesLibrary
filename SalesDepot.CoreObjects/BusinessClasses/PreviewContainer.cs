using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public interface IPreviewStorage
    {
        string StoragePath { get; }
        List<IPreviewContainer> PreviewContainers { get; }
        IPreviewContainer GetPreviewContainer(string originalPath);
        IPreviewGenerator GetPreviewGenerator(IPreviewContainer previewContainer);
        void UpdatePreviewableObject(string originalPath, DateTime lastChanged);
        void UpdatePreviewContainers();
    }

    public interface IPreviewContainer
    {
        IPreviewStorage Parent { get; }
        string Identifier { get; }
        string OriginalPath { get; set; }
        FileTypes Type { get; set; }
        string Extension { get; }
        DateTime LastChanged { set; }
        string ContainerPath { get; }
        string Serialize();
        void Deserialize(XmlNode node);
        string GetTextContent();
        string[] GetPreviewLinks(string format);
        void UpdateContent();
        void ClearContent();
        Size GetThumbSize();
    }

    public class UniversalPreviewContainer : IPreviewContainer
    {
        private string _originalPath = string.Empty;

        public UniversalPreviewContainer(IPreviewStorage parent)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid().ToString();

        }

        #region IPreviewContainer Members
        public IPreviewStorage Parent { get; private set; }
        public string Identifier { get; private set; }
        public string ContainerPath { get; private set; }
        public FileTypes Type { get; set; }

        public string OriginalPath
        {
            get
            {
                return _originalPath;
            }
            set
            {
                _originalPath = value;
                switch (this.Extension.ToUpper())
                {
                    case ".PPT":
                    case ".PPTX":
                        this.ContainerPath = Path.Combine(this.Parent.StoragePath, Constants.FtpPreviewContainersRootFolderName, "files", this.Identifier);
                        this.Type = FileTypes.Presentation;
                        break;
                    case ".DOC":
                    case ".DOCX":
                    case ".XLS":
                    case ".XLSX":
                    case ".PDF":
                        this.Type = FileTypes.Other;
                        this.ContainerPath = Path.Combine(this.Parent.StoragePath, Constants.FtpPreviewContainersRootFolderName, "files", this.Identifier);
                        break;
                    case ".MPEG":
                    case ".WMV":
                    case ".AVI":
                    case ".WMZ":
                        this.Type = FileTypes.MediaPlayerVideo;
                        this.ContainerPath = Path.Combine(this.Parent.StoragePath, Constants.FtpPreviewContainersRootFolderName, "video", this.Identifier);
                        break;
                    case ".MPG":
                    case ".ASF":
                    case ".MOV":
                    case ".MP4":
                    case ".M4V":
                    case ".FLV":
                    case ".OGV":
                    case ".OGM":
                    case ".OGX":
                        this.Type = FileTypes.QuickTimeVideo;
                        this.ContainerPath = Path.Combine(this.Parent.StoragePath, Constants.FtpPreviewContainersRootFolderName, "video", this.Identifier);
                        break;
                    default:
                        this.Type = FileTypes.Url;
                        break;
                }
            }
        }

        public string Extension
        {
            get
            {
                return Path.GetExtension(_originalPath);
            }
        }

        public DateTime LastChanged
        {
            set
            {
                this.Parent.UpdatePreviewableObject(this.OriginalPath, value);
            }
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<Identifier>" + this.Identifier + @"</Identifier>");
            result.AppendLine(@"<OriginalPath>" + this.OriginalPath.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</OriginalPath>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FolderName":
                    case "Identifier":
                        this.Identifier = childNode.InnerText;
                        break;
                    case "OriginalPath":
                        this.OriginalPath = childNode.InnerText;
                        break;

                }
            }
        }

        public string[] GetPreviewLinks(string format)
        {
            List<string> result = new List<string>();

            if (!string.IsNullOrEmpty(this.ContainerPath))
            {
                if (format.Equals("old office"))
                {
                }
                else if (format.Equals("new office"))
                {
                }
                else
                {
                    string previewFolder = Path.Combine(this.ContainerPath, format);
                    if (Directory.Exists(previewFolder))
                        result.AddRange(Directory.GetFiles(previewFolder).Select(x => x.Replace(this.Parent.StoragePath, string.Empty)));
                }
            }

            return result.ToArray();
        }

        public Size GetThumbSize()
        {
            Size size = new Size(0, 0);
            if (!string.IsNullOrEmpty(this.ContainerPath))
            {
                string thumbsFolder = Path.Combine(this.ContainerPath, "thumbs");
                if (Directory.Exists(thumbsFolder))
                    foreach (string thumbFile in Directory.GetFiles(thumbsFolder, "*.png"))
                    {
                        try
                        {
                            size = Image.FromFile(thumbFile).Size;
                        }
                        catch { }
                    }
            }
            return size;
        }

        public string GetTextContent()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(this.ContainerPath))
            {
                string textContentFolder = Path.Combine(this.ContainerPath, "txt");
                if (Directory.Exists(textContentFolder))
                    foreach (string textContentFile in Directory.GetFiles(textContentFolder, "*.txt"))
                    {
                        using (System.IO.StreamReader textContentStream = new System.IO.StreamReader(textContentFile))
                        {
                            result = textContentStream.ReadToEnd();
                            result = result.Replace(Environment.NewLine, " ");
                            StringBuilder sb = new StringBuilder();
                            foreach (char c in result)
                            {
                                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                                    sb.Append(c);
                            }
                            result = sb.ToString();
                            textContentStream.Close();
                            break;
                        }
                    }
            }
            return result;
        }

        public void UpdateContent()
        {
            FileInfo parentFile = new FileInfo(this.OriginalPath);
            if (!string.IsNullOrEmpty(this.ContainerPath))
            {
                DirectoryInfo previewFolder = new DirectoryInfo(this.ContainerPath);
                bool update = false;
                if (!previewFolder.Exists)
                    update = true;
                else
                {
                    TimeSpan time = parentFile.LastWriteTime.Subtract(previewFolder.CreationTime);
                    if (time.Minutes > 0)
                        update = true;
                    else if (!parentFile.Exists)
                        update = true;
                    else
                        update = false;
                }
                if (previewFolder.Exists && update)
                    ToolClasses.SyncManager.DeleteFolder(previewFolder);
                if (parentFile.Exists)
                {
                    IPreviewGenerator previewGenerator = this.Parent.GetPreviewGenerator(this);
                    if (previewGenerator != null)
                        previewGenerator.GeneratePreview();
                }
            }
        }

        public void ClearContent()
        {
            if (!string.IsNullOrEmpty(this.ContainerPath))
            {
                DirectoryInfo previewFolder = new DirectoryInfo(this.ContainerPath);
                if (previewFolder.Exists)
                    ToolClasses.SyncManager.DeleteFolder(previewFolder);
            }
        }
        #endregion
    }

    #region Compatibility with desktop version of Sales Depot
    public class PresentationPreviewContainer
    {
        public PresentationPreviewContainer(ILibraryFile parent)
        {
            this.Parent = parent;
            this.Identifier = Guid.NewGuid().ToString();
            this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName, this.Identifier);
        }

        #region PreviewContainer Members
        public ILibraryFile Parent { get; private set; }
        public string Identifier { get; private set; }
        public string ContainerPath { get; private set; }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<FolderName>" + this.Identifier + @"</FolderName>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FolderName":
                        this.Identifier = childNode.InnerText;
                        this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName, this.Identifier);
                        break;
                }
            }
        }

        public string[] GetPreviewLinks(string format)
        {
            return null;
        }

        public string GetTextContent()
        {
            string result = string.Empty;
            return result;
        }

        public void UpdateContent()
        {
            FileInfo parentFile = new FileInfo(this.Parent.OriginalPath);
            DirectoryInfo previewFolder = new DirectoryInfo(this.ContainerPath);
            bool needToUpdate = false;
            if (!previewFolder.Exists)
                needToUpdate = true;
            else if (parentFile.LastWriteTime > previewFolder.CreationTime)
                needToUpdate = true;
            else
                needToUpdate = false;
            if (needToUpdate)
            {
                if (!Directory.Exists(Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName)))
                    Directory.CreateDirectory(Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, Constants.RegularPreviewContainersRootFolderName));
                if (previewFolder.Exists)
                    ToolClasses.SyncManager.DeleteFolder(previewFolder);
                Directory.CreateDirectory(this.ContainerPath);
                InteropClasses.PowerPointHelper.Instance.ExportPresentationAsImages(this.Parent.OriginalPath, this.ContainerPath);
            }
        }

        public Size GetThumbSize()
        {
            return new Size(0, 0);
        }

        public void ClearContent()
        {
            DirectoryInfo previewFolder = new DirectoryInfo(this.ContainerPath);
            if (previewFolder.Exists)
                ToolClasses.SyncManager.DeleteFolder(previewFolder);
        }
        #endregion
    }
    #endregion
}
