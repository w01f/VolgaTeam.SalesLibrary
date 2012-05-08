using System.Drawing;
using System.Drawing.Drawing2D;

namespace FileManager.ToolClasses
{
    class ImageHelper
    {
        public static Image GetThumbnail(Image source, int newHeight, int newWidth)
        {
            Bitmap newImage = new Bitmap(newWidth, newHeight);
            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(source, new Rectangle(0, 0, newWidth, newHeight));
            }
            return newImage;
        }
    }
}
