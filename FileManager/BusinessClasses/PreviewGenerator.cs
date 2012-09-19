﻿using System.IO;

namespace FileManager.BusinessClasses
{
    interface IPreviewGenerator
    {
        string SourceFile { get; set; }
        string ContainerPath { get; set; }
        void GeneratePreview();
    }

    public class PowerPointPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public string SourceFile { get; set; }
        public string ContainerPath { get; set; }

        public void GeneratePreview()
        {
            InteropClasses.PowerPointHelper.Instance.ExportPresentationAllFormats(this.SourceFile, this.ContainerPath);
        }
        #endregion
    }

    public class WordPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public string SourceFile { get; set; }
        public string ContainerPath { get; set; }

        public void GeneratePreview()
        {
            InteropClasses.WordHelper.Instance.ExportDocumentAllFormats(this.SourceFile, this.ContainerPath);
        }
        #endregion
    }

    public class ExcelPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public string SourceFile { get; set; }
        public string ContainerPath { get; set; }

        public void GeneratePreview()
        {
            InteropClasses.ExcelHelper.Instance.ExportBookAllFormats(this.SourceFile, this.ContainerPath);
        }
        #endregion
    }

    public class PdfPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public string SourceFile { get; set; }
        public string ContainerPath { get; set; }
        public bool Update { get; set; }

        public void GeneratePreview()
        {
            string pngDestination = Path.Combine(this.ContainerPath, "png");
            bool updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0);
            if (!Directory.Exists(pngDestination))
                Directory.CreateDirectory(pngDestination);
            string jpgDestination = Path.Combine(this.ContainerPath, "jpg");
            bool updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0);
            if (!Directory.Exists(jpgDestination))
                Directory.CreateDirectory(jpgDestination);
            string thumbsDestination = Path.Combine(this.ContainerPath, "thumbs");
            bool updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination, "*.png").Length > 0);
            if (!Directory.Exists(thumbsDestination))
                Directory.CreateDirectory(thumbsDestination);
            if (updatePng || updateJpg || updateThumbs)
                ToolClasses.PdfHelper.Instance.ExportPdf(this.SourceFile, pngDestination, jpgDestination, thumbsDestination);

            string txtDestination = Path.Combine(this.ContainerPath, "txt");
            bool updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0);
            if (!Directory.Exists(txtDestination))
                Directory.CreateDirectory(txtDestination);
            if (updateTxt)
                ToolClasses.PdfHelper.Instance.ExtractText(this.SourceFile, txtDestination);
        }
        #endregion
    }

    public class VideoPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public string SourceFile { get; set; }
        public string ContainerPath { get; set; }

        public void GeneratePreview()
        {
            if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
            {
                string mp4Destination = Path.Combine(this.ContainerPath, "mp4");
                bool updateMp4 = !(Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination, "*.mp4").Length > 0);
                if (!Directory.Exists(mp4Destination))
                    Directory.CreateDirectory(mp4Destination);
                if (updateMp4)
                    ToolClasses.VideoHelper.Instance.ExportMp4(this.SourceFile, mp4Destination);
                if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
                    ToolClasses.SyncManager.DeleteFolder(new DirectoryInfo(mp4Destination));
            }

            if ((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive)
            {
                string ogvDestination = Path.Combine(this.ContainerPath, "ogv");
                bool updateOgv = !(Directory.Exists(ogvDestination) && Directory.GetFiles(ogvDestination, "*.ogv").Length > 0);
                if (!Directory.Exists(ogvDestination))
                    Directory.CreateDirectory(ogvDestination);
                if (updateOgv)
                    ToolClasses.VideoHelper.Instance.ExportOgv(this.SourceFile, ogvDestination);
                if (!((AppManager.Instance.ThreadActive && !AppManager.Instance.ThreadAborted) || !AppManager.Instance.ThreadActive))
                    ToolClasses.SyncManager.DeleteFolder(new DirectoryInfo(ogvDestination));

            }
        }
        #endregion
    }
}
