using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public abstract class DocumentPreviewContainer : BasePreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder
		{
			get { return DocumentSubFolderName; }
		}

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats
		{
			get { return BasePreviewFormats.Union(ImagePreviewFormats.Union(TextPreviewFormats)).ToArray(); }
		}

		[NotMapped, JsonIgnore]
		protected abstract IEnumerable<string> BasePreviewFormats { get; }

		[NotMapped, JsonIgnore]
		private IEnumerable<string> ImagePreviewFormats
		{
			get
			{
				return new[]
				{
					PreviewFormats.Png,
					PreviewFormats.PngForMobile,
					PreviewFormats.Jpeg,
					PreviewFormats.JpegForMobile,
					PreviewFormats.Thumbnails,
					PreviewFormats.ThumbnailsForMobile,
				};
			}
		}

		[NotMapped, JsonIgnore]
		private IEnumerable<string> TextPreviewFormats
		{
			get { return new[] { PreviewFormats.Text }; }
		}

		[NotMapped, JsonIgnore]
		public bool GenerateImages { get; private set; }

		[NotMapped, JsonIgnore]
		public bool GenerateText { get; private set; }
		#endregion

		protected override void UpdateState(IEnumerable<PreviewableLink> associatedLinks)
		{
			var associatedLinksList = associatedLinks.ToList();
			GenerateImages = associatedLinksList
				.Select(link => link.Settings)
				.OfType<DocumentLinkSettings>()
				.Any(set => set.GeneratePreviewImages);
			GenerateText = associatedLinksList
				.Select(link => link.Settings)
				.OfType<DocumentLinkSettings>()
				.Any(set => set.GenerateContentText);
			base.UpdateState(associatedLinksList);
			if (!IsUpToDate)
				return;
			IsUpToDate = BasePreviewFormats.All(previewFormat => Directory.Exists(Path.Combine(ContainerPath, previewFormat)))
				&&
				((!GenerateImages && ImagePreviewFormats.All(previewFormat => !Directory.Exists(Path.Combine(ContainerPath, previewFormat)))) ||
				(GenerateImages && ImagePreviewFormats.All(previewFormat => Directory.Exists(Path.Combine(ContainerPath, previewFormat)))))
				&&
				((!GenerateText && TextPreviewFormats.All(previewFormat => !Directory.Exists(Path.Combine(ContainerPath, previewFormat)))) ||
				(GenerateText && TextPreviewFormats.All(previewFormat => Directory.Exists(Path.Combine(ContainerPath, previewFormat)))));
		}
	}
}
