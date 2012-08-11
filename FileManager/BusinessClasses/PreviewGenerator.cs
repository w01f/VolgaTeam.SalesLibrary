using System.IO;

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
            string mp4Destination = Path.Combine(this.ContainerPath, "mp4");
            bool updateMp4 = !(Directory.Exists(mp4Destination) && Directory.GetFiles(mp4Destination, "*.mp4").Length > 0);
            if (!Directory.Exists(mp4Destination))
                Directory.CreateDirectory(mp4Destination);
            if (updateMp4)
                ToolClasses.VideoHelper.Instance.ExportMp4(this.SourceFile, mp4Destination);

            string ogvDestination = Path.Combine(this.ContainerPath, "ogv");
            bool updateOgv = !(Directory.Exists(ogvDestination) && Directory.GetFiles(ogvDestination, "*.ogv").Length > 0);
            if (!Directory.Exists(ogvDestination))
                Directory.CreateDirectory(ogvDestination);
            if (updateOgv)
                ToolClasses.VideoHelper.Instance.ExportOgv(this.SourceFile, ogvDestination);
        }
        #endregion
    }
}
