using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(ColdFusionLinkPreviewContainer))]
	class ColdFusionPreviewGenerator : IPreviewContentGenerator
	{
		public void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var coldFusionLinkPreviewContainer = (ColdFusionLinkPreviewContainer)previewContainer;

			var logger = new UrlPreviewGenerationLogger(coldFusionLinkPreviewContainer);
			logger.StartLogging();

			var thumbsDestination = Path.Combine(coldFusionLinkPreviewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());

			var thumbsDatatableDestination = Path.Combine(coldFusionLinkPreviewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());

			if (!(updateThumbs || updateThumbsDatatable)) return;

			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			var thumbnailGenerator = new EOBrowserThumbnailGenerator();
			thumbnailGenerator.GenerateThumbnail(coldFusionLinkPreviewContainer.SourcePath, thumbsDestination);
			JpegHelper.ConvertFiles(thumbsDestination, thumbsDatatableDestination);

			logger.LogStage(PreviewFormats.Thumbnails);
			logger.LogStage(PreviewFormats.ThumbnailsForDatatable);

			coldFusionLinkPreviewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
