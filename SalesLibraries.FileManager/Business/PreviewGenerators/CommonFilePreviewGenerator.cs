using System;
using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(CommonFilePreviewContainer))]
	class CommonFilePreviewGenerator : FilePreviewGenerator
	{
		public override void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var filePreviewContainer = (CommonFilePreviewContainer)previewContainer;

			var logger = new FilePreviewGenerationLogger(filePreviewContainer);
			logger.StartLogging();

			if (MainController.Instance.Settings.OneDriveSettings.Enabled)
			{
				GenerateOneDriveContent(filePreviewContainer, cancellationToken);
				logger.LogStage(PreviewFormats.OneDrive);
			}

			logger.FinishLogging();
		}
	}
}
