using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class WebLinkPreviewContainer : BasePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override String SourcePath => RelativePath;

		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder => DocumentSubFolderName;

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats => new[] { PreviewFormats.Thumbnails };
		#endregion

		protected override void UpdateState(IEnumerable<IPreviewableLink> associatedLinks)
		{
			base.UpdateState(associatedLinks);
			IsUpToDate = AvailablePreviewFormats.All(previewFormat =>
			{
				var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
				return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
			});
		}
	}
}
