using System.IO;
using ceTe.DynamicPDF.Rasterizer;

namespace FileManager.ToolClasses
{
    class PdfHelper
    {
        private static PdfHelper _instance;

        public static PdfHelper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PdfHelper();
                return _instance;
            }
        }

        private PdfHelper()
        {
            PdfRasterizer.AddLicense("RST47N0DNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN");
        }

        public void ExportPdf(string sourceFilePath, string destinationPngPath, string destinationJpgPath, string destinationThumbsPath)
        {
            var rasterizer = new PdfRasterizer(sourceFilePath);
            // Save the image.
            var pngImageFormat = new PngImageFormat(PngColorFormat.Rgb);
            var jpgImageFormat = new JpegImageFormat(90);
            var imageSizeFormat = new PercentageImageSize(100);
            var thumbsSizeFormat = new PercentageImageSize(10);
            for (var i = 0; i < rasterizer.Pages.Count; i++)
            {
                rasterizer.Pages[i].Draw(Path.Combine(destinationPngPath, "Page" + (i + 1) + ".png"), pngImageFormat, imageSizeFormat);
                rasterizer.Pages[i].Draw(Path.Combine(destinationJpgPath, "Page" + (i + 1) + ".JPG"), jpgImageFormat, imageSizeFormat);
                rasterizer.Pages[i].Draw(Path.Combine(destinationThumbsPath, "Page" + (i + 1) + ".png"), pngImageFormat, thumbsSizeFormat);
            }

            rasterizer.Dispose();
        }
    }
}
