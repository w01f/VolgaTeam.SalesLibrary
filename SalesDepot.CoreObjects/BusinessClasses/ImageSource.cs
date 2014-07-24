using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class ImageSource
	{
		public const decimal BigHeight = 146;
		public const decimal SmallHeight = 75;
		public const decimal TinyHeight = 58;
		public const decimal XtraTinyHeight = 41;
		public const decimal BigWidth = 321;
		public const decimal SmallWidth = 164;
		public const decimal TinyWidth = 128;
		public const decimal XtraTinyWidth = 90;

		public bool IsDefault { get; set; }
		public Image BigImage { get; set; }
		public Image SmallImage { get; set; }
		public Image TinyImage { get; set; }
		public Image XtraTinyImage { get; set; }
		public string Name { get; set; }
		public string FileName { get; set; }
		public string OutputFilePath { get; set; }

		public bool ContainsData
		{
			get { return XtraTinyImage != null; }
		}

		private string _encodedBigImage;
		public string EncodedBigImage
		{
			get
			{
				if (!String.IsNullOrEmpty(_encodedBigImage)) return _encodedBigImage;
				var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
				_encodedBigImage = Convert.ToBase64String((byte[])converter.ConvertTo(BigImage, typeof(byte[]))).Trim();
				return _encodedBigImage;
			}
		}

		public string Serialize()
		{
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.Append("<IsDefault>" + IsDefault + "</IsDefault>");
			result.Append("<BigImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(BigImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</BigImage>");
			result.Append("<SmallImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(SmallImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SmallImage>");
			result.Append("<TinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(TinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</TinyImage>");
			result.Append("<XtraTinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(XtraTinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</XtraTinyImage>");
			if (!String.IsNullOrEmpty(Name))
				result.Append("<Name>" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</Name>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsDefault":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsDefault = tempBool;
						break;
					case "BigImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							BigImage = null;
						else
						{
							BigImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
							_encodedBigImage = childNode.InnerText;
						}
						break;
					case "SmallImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							SmallImage = null;
						else
							SmallImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "TinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							TinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "XtraTinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							XtraTinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
				}
			}
		}

		public ImageSource Clone()
		{
			var result = new ImageSource();
			result.IsDefault = IsDefault;
			if (BigImage != null && SmallImage != null && TinyImage != null && XtraTinyImage != null)
			{
				result.BigImage = BigImage.Clone() as Image;
				result.SmallImage = SmallImage.Clone() as Image;
				result.TinyImage = TinyImage.Clone() as Image;
				result.XtraTinyImage = XtraTinyImage.Clone() as Image;
			}
			result.Name = Name;
			return result;
		}

		public void Dispose()
		{
			if (BigImage != null)
				BigImage.Dispose();
			BigImage = null;
			if (SmallImage != null)
				SmallImage.Dispose();
			SmallImage = null;
			if (TinyImage != null)
				TinyImage.Dispose();
			TinyImage = null;
			if (XtraTinyImage != null)
				XtraTinyImage.Dispose();
			XtraTinyImage = null;
		}

		public void PrepareOutputFile()
		{
			OutputFilePath = Path.GetTempFileName();
			BigImage.Save(OutputFilePath);
		}

		public static ImageSource FromImage(Image image)
		{
			var imageSource = new ImageSource();
			if (image != null)
			{
				imageSource.BigImage = image.Resize(new Size((Int32)BigWidth, (Int32)BigHeight));
				imageSource.SmallImage = image.Resize(new Size((Int32)SmallWidth, (Int32)SmallHeight));
				imageSource.TinyImage = image.Resize(new Size((Int32)TinyWidth, (Int32)TinyHeight));
				imageSource.XtraTinyImage = image.Resize(new Size((Int32)XtraTinyWidth, (Int32)XtraTinyHeight));
			};
			return imageSource;
		}
	}
}
