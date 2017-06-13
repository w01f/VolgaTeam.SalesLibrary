using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(WebLinkPreviewContainer))]
	class WebPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var thumbsDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());

			if (!updateThumbs) return;

			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
			{
				var siteCatcher = new WebLinkThumbnailMaker();
				siteCatcher.MakeThumbnail(previewContainer.SourcePath, thumbsDestination);
			}));

			previewContainer.MarkAsModified();
		}
	}
}
