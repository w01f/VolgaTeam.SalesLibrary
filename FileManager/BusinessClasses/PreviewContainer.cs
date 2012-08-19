using System;
using System.IO;
using System.Text;
using System.Xml;

namespace FileManager.BusinessClasses
{
    public interface IPreviewContainer
    {
        LibraryFile Parent { get; }
        string ContainerID { get; }
        string ContainerPath { get; }
        string Serialize();
        void Deserialize(XmlNode node);
        void UpdateContent();
        void ClearContent();
    }

    public class UniversalPreviewContainer : IPreviewContainer
    {
        public UniversalPreviewContainer(LibraryFile parent)
        {
            this.Parent = parent;
            this.ContainerID = Guid.NewGuid().ToString();
            switch (Path.GetExtension(this.Parent.FullPath).ToUpper())
            {
                case ".PPT":
                case ".PPTX":
                case ".DOC":
                case ".DOCX":
                case ".XLS":
                case ".XLSX":
                case ".PDF":
                    this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.FtpPreviewContainersRootFolderName, "files", this.ContainerID);
                    break;
                case ".MPEG":
                case ".WMV":
                case ".AVI":
                case ".WMZ":
                case ".MPG":
                case ".ASF":
                case ".MOV":
                case ".MP4":
                case ".M4V":
                case ".FLV":
                case ".OGV":
                case ".OGM":
                case ".OGX":
                    this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.FtpPreviewContainersRootFolderName, "video", this.ContainerID);
                    break;
            }
        }

        #region IPreviewContainer Members
        public LibraryFile Parent { get; private set; }
        public string ContainerID { get; private set; }
        public string ContainerPath { get; private set; }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<FolderName>" + this.ContainerID + @"</FolderName>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FolderName":
                        this.ContainerID = childNode.InnerText;
                        switch (Path.GetExtension(this.Parent.FullPath).ToUpper())
                        {
                            case ".PPT":
                            case ".PPTX":
                            case ".DOC":
                            case ".DOCX":
                            case ".XLS":
                            case ".XLSX":
                            case ".PDF":
                                this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.FtpPreviewContainersRootFolderName, "files", this.ContainerID);
                                break;
                            case ".MPEG":
                            case ".WMV":
                            case ".AVI":
                            case ".WMZ":
                            case ".MPG":
                            case ".ASF":
                            case ".MOV":
                            case ".MP4":
                            case ".M4V":
                            case ".FLV":
                            case ".OGV":
                            case ".OGM":
                            case ".OGX":
                                this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.FtpPreviewContainersRootFolderName, "video", this.ContainerID);
                                break;
                        }
                        break;
                }
            }
        }

        public void UpdateContent()
        {
            FileInfo parentFile = new FileInfo(this.Parent.FullPath);
            if (!string.IsNullOrEmpty(this.ContainerPath))
            {
                DirectoryInfo previewFolder = new DirectoryInfo(this.ContainerPath);
                bool update = false;
                if (!previewFolder.Exists)
                    update = true;
                else if (parentFile.LastWriteTime > previewFolder.CreationTime)
                    update = true;
                else if (!parentFile.Exists)
                    update = true;
                else
                    update = false;
                if (previewFolder.Exists && update)
                    ToolClasses.SyncManager.DeleteFolder(previewFolder);
                if (parentFile.Exists)
                {
                    IPreviewGenerator previewGenerator = null;
                    switch (parentFile.Extension.ToUpper())
                    {
                        case ".PPT":
                        case ".PPTX":
                            previewGenerator = new PowerPointPreviewGenerator();
                            break;
                        case ".DOC":
                        case ".DOCX":
                            previewGenerator = new WordPreviewGenerator();
                            break;
                        case ".XLS":
                        case ".XLSX":
                            break;
                        case ".PDF":
                            previewGenerator = new PdfPreviewGenerator();
                            break;
                        case ".MPEG":
                        case ".WMV":
                        case ".AVI":
                        case ".WMZ":
                        case ".MPG":
                        case ".ASF":
                        case ".MOV":
                        case ".MP4":
                        case ".M4V":
                        case ".FLV":
                        case ".OGV":
                        case ".OGM":
                        case ".OGX":
                            previewGenerator = new VideoPreviewGenerator();
                            break;
                    }
                    if (previewGenerator != null)
                    {
                        previewGenerator.SourceFile = this.Parent.FullPath;
                        previewGenerator.ContainerPath = this.ContainerPath;
                        previewGenerator.GeneratePreview();
                    }
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
    public class PresentationPreviewContainer : IPreviewContainer
    {
        public PresentationPreviewContainer(LibraryFile parent)
        {
            this.Parent = parent;
            this.ContainerID = Guid.NewGuid().ToString();
            this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.RegularPreviewContainersRootFolderName, this.ContainerID);
        }

        #region PreviewContainer Members
        public LibraryFile Parent { get; private set; }
        public string ContainerID { get; private set; }
        public string ContainerPath { get; private set; }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<FolderName>" + this.ContainerID + @"</FolderName>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FolderName":
                        this.ContainerID = childNode.InnerText;
                        this.ContainerPath = Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.RegularPreviewContainersRootFolderName, this.ContainerID);
                        break;
                }
            }
        }

        public void UpdateContent()
        {
            ClearOldPreviewImages();

            FileInfo parentFile = new FileInfo(this.Parent.FullPath);
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
                if (!Directory.Exists(Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.RegularPreviewContainersRootFolderName)))
                    Directory.CreateDirectory(Path.Combine(this.Parent.Parent.Parent.Parent.Folder.FullName, ConfigurationClasses.SettingsManager.RegularPreviewContainersRootFolderName));
                if (previewFolder.Exists)
                    ToolClasses.SyncManager.DeleteFolder(previewFolder);
                Directory.CreateDirectory(this.ContainerPath);
                InteropClasses.PowerPointHelper.Instance.ExportPresentationAsImages(this.Parent.FullPath, this.ContainerPath);
            }
        }

        public void ClearContent()
        {
            DirectoryInfo previewFolder = new DirectoryInfo(this.ContainerPath);
            if (previewFolder.Exists)
                ToolClasses.SyncManager.DeleteFolder(previewFolder);
        }
        #endregion

        private void ClearOldPreviewImages()
        {
            if (this.Parent.Parent != null)
            {
                DirectoryInfo folder = new FileInfo(this.Parent.FullPath).Directory;
                if (folder.Exists && folder.GetDirectories("*" + ConfigurationClasses.SettingsManager.OldPreviewFolderPrefix + "*").Length > 0)
                    ToolClasses.SyncManager.DeleteFolder(folder, ConfigurationClasses.SettingsManager.OldPreviewFolderPrefix);
            }
        }
    }
    #endregion
}
