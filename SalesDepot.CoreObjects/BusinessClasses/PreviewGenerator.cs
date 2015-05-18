using System;
using System.IO;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public interface IPreviewGenerator
	{
		IPreviewContainer Parent { get; }
		void GeneratePreview(bool generateImages, bool generateText);
	}

	public class PowerPointPreviewGenerator : IPreviewGenerator
	{
		#region IPreviewGenerator Members
		public PowerPointPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
		}

		public IPreviewContainer Parent { get; private set; }

		public void GeneratePreview(bool generateImages, bool generateText)
		{
			bool update;
			PowerPointHelper.Instance.ExportPresentationAllFormats(Parent.OriginalPath, Parent.ContainerPath, generateImages, generateText, out update);
			if (update)
				Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}

	public class WordPreviewGenerator : IPreviewGenerator
	{
		#region IPreviewGenerator Members
		public WordPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
		}

		public IPreviewContainer Parent { get; private set; }

		public void GeneratePreview(bool generateImages, bool generateText)
		{
			bool update;
			WordHelper.Instance.ExportDocumentAllFormats(Parent.OriginalPath, Parent.ContainerPath, generateImages, generateText, out update);
			if (update)
				Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}

	public class ExcelPreviewGenerator : IPreviewGenerator
	{
		#region IPreviewGenerator Members
		public ExcelPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
		}

		public IPreviewContainer Parent { get; private set; }

		public void GeneratePreview(bool generateImages, bool generateText)
		{
			bool update;
			ExcelHelper.Instance.ExportBookAllFormats(Parent.OriginalPath, Parent.ContainerPath, out update);
			if (update)
				Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}

	public class PdfPreviewGenerator : IPreviewGenerator
	{
		#region IPreviewGenerator Members
		public PdfPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
		}

		public bool Update { get; set; }
		public IPreviewContainer Parent { get; private set; }

		public void GeneratePreview(bool generateImages, bool generateText)
		{
			var pngDestination = Path.Combine(Parent.ContainerPath, "png");
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0) && generateImages;
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);
			var pngPhoneDestination = Path.Combine(Parent.ContainerPath, "png_phone");
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination, "*.png").Length > 0) && generateImages;
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);
			var jpgDestination = Path.Combine(Parent.ContainerPath, "jpg");
			var updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0) && generateImages;
			if (updateJpg && !Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);
			var jpgPhoneDestination = Path.Combine(Parent.ContainerPath, "jpg_phone");
			var updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination, "*.jpg").Length > 0) && generateImages;
			if (updateJpgPhone && !Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);
			var thumbsDestination = Path.Combine(Parent.ContainerPath, "thumbs");
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination, "*.png").Length > 0) && generateImages;
			if (updateThumbs && !Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);
			var thumbsPhoneDestination = Path.Combine(Parent.ContainerPath, "thumbs_phone");
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination, "*.png").Length > 0) && generateImages;
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);
			
			if (updatePng || updateJpg || updateThumbs)
				PdfHelper.Instance.ExportPdf(Parent.OriginalPath, pngDestination, jpgDestination, thumbsDestination);
			
			if (updatePngPhone || updateJpgPhone || updateThumbsPhone)
				PdfHelper.Instance.ExportPdfPhone(Parent.OriginalPath, pngPhoneDestination, jpgPhoneDestination, thumbsPhoneDestination);

			var txtDestination = Path.Combine(Parent.ContainerPath, "txt");
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0) && generateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);
			if (updateTxt)
				PdfHelper.Instance.ExtractText(Parent.OriginalPath, txtDestination);

			if (updatePng || updateJpg || updateThumbs || updateTxt)
				Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}
}