using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.BusinessClasses
{
    public class PresentationPreviewContainer
    {
        private LibraryFile _parent = null;
        private string _folderName = Guid.NewGuid().ToString();
        private string _remotePreviewStorageFolder = string.Empty;
        private string LocalPreviewStorageFolder { get; set; }
        public List<PresentationPreviewSlide> Slides { get; set; }
        public int SelectedIndex { get; set; }

        public PresentationPreviewContainer(LibraryFile parent)
        {
            _parent = parent;
            if (_parent.Parent != null)
            {
                _remotePreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, CoreObjects.Constants.RegularPreviewContainersRootFolderName, _folderName);
                if (ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection)
                    this.LocalPreviewStorageFolder = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LocalLibraryCacheFolder, _folderName);
                else
                    this.LocalPreviewStorageFolder = _remotePreviewStorageFolder;
            }
            Slides = new List<PresentationPreviewSlide>();
        }

        public string Serialize()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(@"<FolderName>" + _folderName + @"</FolderName>");
            return result.ToString();
        }

        public void Deserialize(XmlNode node)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "FolderName":
                        _folderName = childNode.InnerText;
                        if (_parent.Parent != null)
                        {
                            _remotePreviewStorageFolder = Path.Combine(_parent.Parent.Parent.Parent.Folder.FullName, CoreObjects.Constants.RegularPreviewContainersRootFolderName, _folderName);
                            if (ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection)
                                this.LocalPreviewStorageFolder = Path.Combine(ConfigurationClasses.SettingsManager.Instance.LocalLibraryCacheFolder, _folderName);
                            else
                                this.LocalPreviewStorageFolder = _remotePreviewStorageFolder;
                        }
                        break;
                }
            }
        }

        public void ReleasePreviewImages()
        {
            foreach (PresentationPreviewSlide slide in this.Slides)
                slide.PreviewImage.Dispose();
            this.Slides.Clear();
        }

        public Image SelectedSlide
        {
            get
            {
                if (this.SelectedIndex >= 0 && this.SelectedIndex < this.Slides.Count)
                    return this.Slides[this.SelectedIndex].PreviewImage;
                else
                    return null;
            }
        }

        public void GetPreviewImages()
        {
            if (Directory.Exists(_remotePreviewStorageFolder))
            {
                this.Slides.Clear();
                List<string> localPreviewImages = new List<string>();
                if (ConfigurationClasses.SettingsManager.Instance.UseRemoteConnection)
                {
                    if (!Directory.Exists(this.LocalPreviewStorageFolder))
                    {
                        Directory.CreateDirectory(this.LocalPreviewStorageFolder);
                        foreach (string imagePath in Directory.GetFiles(_remotePreviewStorageFolder, "*.png"))
                        {
                            try
                            {
                                File.Copy(imagePath, Path.Combine(this.LocalPreviewStorageFolder, Path.GetFileName(imagePath)), true);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                localPreviewImages.AddRange(Directory.GetFiles(this.LocalPreviewStorageFolder, "*.png"));
                localPreviewImages.Sort((x, y) => InteropClasses.WinAPIHelper.StrCmpLogicalW(x, y));
                for (int i = 0; i < localPreviewImages.Count; i++)
                {
                    PresentationPreviewSlide slide = new PresentationPreviewSlide();
                    slide.Index = i;
                    slide.PreviewImage = new System.Drawing.Bitmap(localPreviewImages[i], true);
                    this.Slides.Add(slide);
                }
            }
        }

        public bool CheckPreviewImages()
        {
            bool result = false;
            if (Directory.Exists(_remotePreviewStorageFolder))
                result = Directory.GetFiles(_remotePreviewStorageFolder, "*.png").Length > 0;
            return result;
        }
    }

    public class PresentationPreviewSlide
    {
        public int Index { get; set; }
        public Bitmap PreviewImage { get; set; }
    }
}
