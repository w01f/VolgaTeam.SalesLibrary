using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;

namespace SalesLibraries.Common.JsonConverters
{
	public class ImageConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Image) || objectType.IsSubclassOf(typeof(Image));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return !String.IsNullOrEmpty(reader.Value as String) ? Image.FromStream(new MemoryStream(Convert.FromBase64String((String)reader.Value))) : null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var bmp = value as Bitmap;
			if (bmp == null) return;
			var m = new MemoryStream();
			try
			{
				bmp.Save(m, GetImageFormat(bmp));
			}
			catch
			{
				bmp.Save(m, ImageFormat.Png);
			}
			writer.WriteValue(Convert.ToBase64String(m.ToArray()));
		}

		private static ImageFormat GetImageFormat(Image image)
		{
			var img = image.RawFormat;
			if (img.Equals(ImageFormat.Jpeg))
				return ImageFormat.Jpeg;
			if (img.Equals(ImageFormat.Bmp))
				return ImageFormat.Bmp;
			if (img.Equals(ImageFormat.Png))
				return ImageFormat.Png;
			if (img.Equals(ImageFormat.Emf))
				return ImageFormat.Emf;
			if (img.Equals(ImageFormat.Exif))
				return ImageFormat.Exif;
			if (img.Equals(ImageFormat.Gif))
				return ImageFormat.Gif;
			if (img.Equals(ImageFormat.Icon))
				return ImageFormat.Icon;
			if (img.Equals(ImageFormat.MemoryBmp))
				return ImageFormat.MemoryBmp;
			if (img.Equals(ImageFormat.Tiff))
				return ImageFormat.Tiff;
			return ImageFormat.Wmf;
		}
	}
}
