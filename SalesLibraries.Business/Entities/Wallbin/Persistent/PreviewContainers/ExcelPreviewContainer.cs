using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class ExcelPreviewContainer : FilePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder => DocumentSubFolderName;

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats => new[] { PreviewFormats.Text }.Union(ImagePreviewFormats).ToArray();

		[NotMapped, JsonIgnore]
		public bool GenerateText { get; private set; }

		[NotMapped, JsonIgnore]
		private IEnumerable<string> ImagePreviewFormats => new[]
		{
			PreviewFormats.Thumbnails,
			PreviewFormats.ThumbnailsForDatatable
		};
		#endregion

		protected override void UpdateState(IList<IPreviewableLink> associatedLinks)
		{
			GenerateText = associatedLinks
				.OfType<PreviewableFileLink>()
				.Select(link => link.Settings)
				.OfType<ExcelLinkSettings>()
				.Any(set => set.GenerateContentText);
			base.UpdateState(associatedLinks);
			if (!IsUpToDate)
				return;
			IsUpToDate = IsAlive &&
				ImagePreviewFormats.All(previewFormat =>
				{
					var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
					return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
				})
				&&
				((!GenerateText && !Directory.Exists(Path.Combine(ContainerPath, PreviewFormats.Text)))
					||
					(GenerateText && new[] { PreviewFormats.Text }.All(previewFormat =>
					{
						var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
						return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
					}))
				);

		}
	}
}
