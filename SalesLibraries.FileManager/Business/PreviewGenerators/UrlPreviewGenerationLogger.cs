using System;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	class UrlPreviewGenerationLogger : PreviewGenerationLogger
	{
		public UrlPreviewGenerationLogger(BasePreviewContainer previewContainer) : base(previewContainer) { }

		protected override void AddSourceObjectInfo()
		{
			base.AddSourceObjectInfo();
			_log.AppendLine(String.Format("Url - {0}", _previewContainer.SourcePath));
		}
	}
}
