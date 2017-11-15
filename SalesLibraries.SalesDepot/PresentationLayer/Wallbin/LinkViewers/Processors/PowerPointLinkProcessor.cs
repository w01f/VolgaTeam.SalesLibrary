using System.IO;
using DevExpress.XtraLayout.Utils;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(PowerPointLink))]
	class PowerPointLinkProcessor : PreviewableFileLinkProcessor
	{
		private readonly PowerPointLink _powerPointLink;

		public PowerPointLinkProcessor(LibraryFileLink fileLink)
			: base(fileLink)
		{
			_powerPointLink = (PowerPointLink)fileLink;
		}

		protected override LinkLaunchOptionsEnum GetLaunchOptions()
		{
			return MainController.Instance.Settings.LinkLaunchSettings.PowerPoint;
		}

		protected override void OpenViewer()
		{
			if (!MainController.Instance.Settings.LinkLaunchSettings.OldStyleQuickView)
				LinkManager.ViewPresentation(_powerPointLink);
			else
				LinkManager.ViewPresentationOld(_powerPointLink);
		}

		protected override void EmailLink()
		{
			PowerPointSingleton.Instance.OpenSlideSourcePresentation(new FileInfo(_fileLink.FullPath));
			using (var form = new FormEmailPresentation())
			{
				form.PowerPointLink = _powerPointLink;
				form.ActiveSlide = 1;
				form.layoutControlItemActiveSlide.Visibility = LayoutVisibility.Never;
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}
	}
}
