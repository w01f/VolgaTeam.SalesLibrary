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
			bool update;
			InteropClasses.PowerPointHelper.Instance.ExportPresentationAllFormats(this.Parent.OriginalPath, this.Parent.ContainerPath, out update);
			if (update)
				Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}

	public class WordPreviewGenerator : IPreviewGenerator
	{
		#region IPreviewGenerator Members
		public IPreviewContainer Parent { get; private set; }

		public WordPreviewGenerator(IPreviewContainer parent)
		{
			Parent = parent;
		}

		public void GeneratePreview()
		{
			bool update;
			InteropClasses.WordHelper.Instance.ExportDocumentAllFormats(this.Parent.OriginalPath, this.Parent.ContainerPath, out update);
			if (update)
				Parent.LastChanged = DateTime.Now;
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

		public void GeneratePreview() { }
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
			var pngDestination = Path.Combine(this.Parent.ContainerPath, "png");
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination, "*.png").Length > 0);
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);
			var pngPhoneDestination = Path.Combine(this.Parent.ContainerPath, "png_phone");
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination, "*.png").Length > 0);
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);
			var jpgDestination = Path.Combine(this.Parent.ContainerPath, "jpg");
			var updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination, "*.jpg").Length > 0);
			if (updateJpg && !Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);
			var jpgPhoneDestination = Path.Combine(this.Parent.ContainerPath, "jpg_phone");
			var updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination, "*.jpg").Length > 0);
			if (updateJpgPhone && !Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);
			var thumbsDestination = Path.Combine(this.Parent.ContainerPath, "thumbs");
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination, "*.png").Length > 0);
			if (updateThumbs && !Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);
			var thumbsPhoneDestination = Path.Combine(this.Parent.ContainerPath, "thumbs_phone");
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination, "*.png").Length > 0);
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);
			if (updatePng || updateJpg || updateThumbs)
				ToolClasses.PdfHelper.Instance.ExportPdf(this.Parent.OriginalPath, pngDestination, jpgDestination, thumbsDestination);
			if (updatePngPhone || updateJpgPhone || updateThumbsPhone)
				ToolClasses.PdfHelper.Instance.ExportPdfPhone(this.Parent.OriginalPath, pngPhoneDestination, jpgPhoneDestination, thumbsPhoneDestination);
			if (updatePng || updateJpg || updateThumbs)
				this.Parent.LastChanged = DateTime.Now;
		}
		#endregion
	}
}
