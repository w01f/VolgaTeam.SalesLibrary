using System;
using System.IO;

namespace SalesDepot.CoreObjects.BusinessClasses
{
    public interface IPreviewGenerator
    {
        IPreviewContainer Parent { get; }
        void GeneratePreview();
    }

    public class PowerPointPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public IPreviewContainer Parent { get; private set; }

        public PowerPointPreviewGenerator(IPreviewContainer parent)
        {
            this.Parent = parent;
        }

        public void GeneratePreview()
        {
            bool update = false;
            InteropClasses.PowerPointHelper.Instance.ExportPresentationAllFormats(this.Parent.OriginalPath, this.Parent.ContainerPath, out update);
            if (update)
                this.Parent.LastChanged = DateTime.Now;
        }
        #endregion
    }

    public class WordPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public IPreviewContainer Parent { get; private set; }

        public WordPreviewGenerator(IPreviewContainer parent)
        {
            this.Parent = parent;
        }

        public void GeneratePreview()
        {
            bool update = false;
            InteropClasses.WordHelper.Instance.ExportDocumentAllFormats(this.Parent.OriginalPath, this.Parent.ContainerPath, out update);
            if (update)
                this.Parent.LastChanged = DateTime.Now;
        }
        #endregion
    }

    public class ExcelPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public IPreviewContainer Parent { get; private set; }

        public ExcelPreviewGenerator(IPreviewContainer parent)
        {
            this.Parent = parent;
        }

        public void GeneratePreview()
        {
            bool update = false;
            InteropClasses.ExcelHelper.Instance.ExportBookAllFormats(this.Parent.OriginalPath, this.Parent.ContainerPath, out update);
            if (update)
                this.Parent.LastChanged = DateTime.Now;
        }
        #endregion
    }

    public class PdfPreviewGenerator : IPreviewGenerator
    {
        #region IPreviewGenerator Members
        public IPreviewContainer Parent { get; private set; }
        public bool Update { get; set; }

        public PdfPreviewGenerator(IPreviewContainer parent)
        {
            this.Parent = parent;
        }

        public void GeneratePreview()
        {
            string pngDestination = Path.Combine(this.Parent.ContainerPath, "png");
            bool updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0);
            if (!Directory.Exists(pngDestination))
                Directory.CreateDirectory(pngDestination);
            string jpgDestination = Path.Combine(this.Parent.ContainerPath, "jpg");
            bool updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0);
            if (!Directory.Exists(jpgDestination))
                Directory.CreateDirectory(jpgDestination);
            string thumbsDestination = Path.Combine(this.Parent.ContainerPath, "thumbs");
            bool updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination, "*.png").Length > 0);
            if (!Directory.Exists(thumbsDestination))
                Directory.CreateDirectory(thumbsDestination);
            if (updatePng || updateJpg || updateThumbs)
                ToolClasses.PdfHelper.Instance.ExportPdf(this.Parent.OriginalPath, pngDestination, jpgDestination, thumbsDestination);

            string txtDestination = Path.Combine(this.Parent.ContainerPath, "txt");
            bool updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0);
            if (!Directory.Exists(txtDestination))
                Directory.CreateDirectory(txtDestination);
            if (updateTxt)
                ToolClasses.PdfHelper.Instance.ExtractText(this.Parent.OriginalPath, txtDestination);

            if (updatePng || updateJpg || updateThumbs || updateTxt)
                this.Parent.LastChanged = DateTime.Now;
        }
        #endregion
    }
}
