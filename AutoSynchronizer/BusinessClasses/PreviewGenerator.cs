using System.IO;

namespace AutoSynchronizer.BusinessClasses
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
            if (InteropClasses.PowerPointHelper.Instance.Connect())
            {
                //InteropClasses.PowerPointHelper.Instance.ExportPresentationAllFormats(this.SourceFile, this.ContainerPath);
                InteropClasses.PowerPointHelper.Instance.Disconnect();
            }
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
            //if (InteropClasses.WordHelper.Instance.Connect())
            //{
            //    InteropClasses.WordHelper.Instance.ExportDocumentAllFormats(this.SourceFile, this.ContainerPath);
            //    InteropClasses.WordHelper.Instance.Disconnect();
            //}
        }
        #endregion
    }

    public class PdfPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public string SourceFile { get; set; }
        public string ContainerPath { get; set; }

        public void GeneratePreview()
        {
            string pngDestination = Path.Combine(this.ContainerPath, "png");
            if (!Directory.Exists(pngDestination))
                Directory.CreateDirectory(pngDestination);
            string jpgDestination = Path.Combine(this.ContainerPath, "jpg");
            if (!Directory.Exists(jpgDestination))
                Directory.CreateDirectory(jpgDestination);
            //ToolClasses.PdfHelper.Instance.ExportPdf(this.SourceFile, pngDestination, jpgDestination);
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
        }
        #endregion
    }
}
