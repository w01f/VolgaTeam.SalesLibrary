using iTextSharp.text.pdf;
using SalesLibraries.Business.Contexts.Wallbin;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings
{
	public class PdfLinkSettings : DocumentLinkSettings
	{
		public void CheckIfArchiveResouce()
		{
			var sourceFilePath = ParentFileLink.FullPath;
			int numberOfPages;
			using (var pdfReader = new PdfReader(sourceFilePath))
			{
				numberOfPages = pdfReader.NumberOfPages;
				pdfReader.Close();
			}
			if (numberOfPages > WallbinConfiguration.MaxPreviewPdfPagesCount)
				IsArchiveResource = true;
		}
	}
}
