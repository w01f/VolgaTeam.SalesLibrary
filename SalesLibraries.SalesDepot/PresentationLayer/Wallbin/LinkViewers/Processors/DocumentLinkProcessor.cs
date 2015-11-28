using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	abstract class DocumentLinkProcessor : PreviewableFileLinkProcessor
	{
		protected DocumentLinkProcessor(LibraryFileLink fileLink) : base(fileLink) { }
		protected override void EmailLink()
		{
			using (var form = new FormEmailLink())
			{
				form.FileLink = _fileLink;
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}

		protected override void OpenViewer()
		{
			LinkManager.PreviewLink(_fileLink);
		}
	}
}
