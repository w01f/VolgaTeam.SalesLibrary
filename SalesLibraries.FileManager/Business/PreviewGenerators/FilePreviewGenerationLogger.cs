using System;
using System.IO;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	class FilePreviewGenerationLogger : PreviewGenerationLogger
	{
		public FilePreviewGenerationLogger(BasePreviewContainer previewContainer) : base(previewContainer) { }

		protected override void AddSourceObjectInfo()
		{
			base.AddSourceObjectInfo();
			var fileInfo = new FileInfo(_previewContainer.SourcePath);
			_log.AppendLine(String.Format("File - {0} ({1})", fileInfo.Name, Utils.FormatFileSize(fileInfo.Length)));
		}
	}
}
