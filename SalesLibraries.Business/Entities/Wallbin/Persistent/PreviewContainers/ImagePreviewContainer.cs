using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class ImagePreviewContainer : FilePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder => DocumentSubFolderName;

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats => new[]
		{
			PreviewFormats.Thumbnails,
			PreviewFormats.ThumbnailsForDatatable
		};
		#endregion

		protected override void UpdateState(IList<IPreviewableLink> associatedLinks)
		{
			base.UpdateState(associatedLinks);
			if (!IsUpToDate)
				return;
			IsUpToDate = IsAlive &&
				AvailablePreviewFormats.All(previewFormat =>
				{
					var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
					return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
				});
		}
	}
}
