using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Xml;
using SalesLibraries.Common.Objects.PowerPoint;

namespace SalesLibraries.Common.Helpers
{
	public static class SlideFormatParser
	{
		private static readonly List<SlideFormat> _availableFormats = new List<SlideFormat>();

		public static SlideFormatEnum GetFormatBySlideSize(SlideSize slideSize)
		{
			return _availableFormats
				.Where(f => f.SupportedSizes
					.Any(supportedSize => supportedSize.Width == slideSize.Width &&
						supportedSize.Height == slideSize.Height))
				.Select(f => f.Id)
				.FirstOrDefault();
		}

		public static void LoadAvailableFormats()
		{
			if (!RemoteResourceManager.Instance.SlideSizeSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(RemoteResourceManager.Instance.SlideSizeSettingsFile.LocalPath);
			foreach (var formatNode in document.SelectNodes(@"/Settings/Format").OfType<XmlNode>())
			{
				var slideFormat = new SlideFormat();
				var slideFormatAttributes = formatNode.Attributes.OfType<XmlAttribute>().ToList();
				var idAttribute = slideFormatAttributes.FirstOrDefault(a => a.Name == "Name");
				if (idAttribute == null) continue;

				SlideFormatEnum slideFormatId;
				if (!Enum.TryParse(String.Format("Format{0}", idAttribute.Value), out slideFormatId)) continue;
				slideFormat.Id = slideFormatId;

				foreach (var sizeNode in formatNode.ChildNodes.OfType<XmlNode>())
				{
					var slideSize = new SlideSize();
					var slideSizeAttributes = sizeNode.Attributes.OfType<XmlAttribute>().ToList();

					decimal temp;

					var widthAttribute = slideSizeAttributes.FirstOrDefault(a => a.Name == "Width");
					if (widthAttribute == null) continue;
					if (!Decimal.TryParse(widthAttribute.Value, out temp)) continue;
					slideSize.Width = temp;

					var heightAttribute = slideSizeAttributes.FirstOrDefault(a => a.Name == "Height");
					if (heightAttribute == null) continue;
					if (!Decimal.TryParse(heightAttribute.Value, out temp)) continue;
					slideSize.Height = temp;

					slideFormat.SupportedSizes.Add(slideSize);

					var defaultAttribute = slideSizeAttributes.FirstOrDefault(a => a.Name == "IsDefault");
					bool isDefault;
					if (defaultAttribute != null && Boolean.TryParse(defaultAttribute.Value, out isDefault) && isDefault)
						slideFormat.DefaultSize = slideSize;
				}

				if (slideFormat.DefaultSize == null)
					slideFormat.DefaultSize = slideFormat.SupportedSizes.FirstOrDefault();

				_availableFormats.Add(slideFormat);
			}
		}
	}
}
