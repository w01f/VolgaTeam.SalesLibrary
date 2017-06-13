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
	public abstract class DocumentPreviewContainer : FilePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder => DocumentSubFolderName;

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats => BasePreviewFormats.Union(ImagePreviewFormats.Union(TextPreviewFormats)).ToArray();

		[NotMapped, JsonIgnore]
		protected abstract IEnumerable<string> BasePreviewFormats { get; }

		[NotMapped, JsonIgnore]
		private IEnumerable<string> ImagePreviewFormats => new[]
		{
			PreviewFormats.Png,
			PreviewFormats.PngForMobile,
			PreviewFormats.Thumbnails,
			PreviewFormats.ThumbnailsForMobile,
		};

		[NotMapped, JsonIgnore]
		private IEnumerable<string> TextPreviewFormats => new[] { PreviewFormats.Text };

		[NotMapped, JsonIgnore]
		public bool GenerateImages { get; private set; }

		[NotMapped, JsonIgnore]
		public bool GenerateText { get; private set; }
		#endregion

		protected override void UpdateState(IEnumerable<IPreviewableLink> associatedLinks)
		{
			var associatedLinksList = associatedLinks.ToList();
			GenerateImages = associatedLinksList
				.OfType<PreviewableFileLink>()
				.Select(link => link.Settings)
				.OfType<DocumentLinkSettings>()
				.Any(set => set.GeneratePreviewImages);
			GenerateText = associatedLinksList
				.OfType<PreviewableFileLink>()
				.Select(link => link.Settings)
				.OfType<DocumentLinkSettings>()
				.Any(set => set.GenerateContentText);
			base.UpdateState(associatedLinksList);
			if (!IsUpToDate)
				return;
			IsUpToDate = BasePreviewFormats.All(previewFormat =>
				{
					var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
					return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
				})
				&&
				(!GenerateImages && ImagePreviewFormats.All(previewFormat => !(Directory.Exists(Path.Combine(ContainerPath, previewFormat))))
					||
					(GenerateImages && ImagePreviewFormats.All(previewFormat =>
						{
							var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
							return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
						}))
				)
				&&
				((!GenerateText && TextPreviewFormats.All(previewFormat => !Directory.Exists(Path.Combine(ContainerPath, previewFormat))))
					||
					(GenerateText && TextPreviewFormats.All(previewFormat =>
						{
							var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
							return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Any();
						}))
				);
		}
	}
}
