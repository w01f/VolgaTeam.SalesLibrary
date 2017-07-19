using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Configuration;

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
			PreviewFormats.ThumbnailsForDatatable
		};

		[NotMapped, JsonIgnore]
		private IEnumerable<string> TextPreviewFormats => new[] { PreviewFormats.Text };

		[NotMapped, JsonIgnore]
		public bool GenerateFullImages { get; private set; }

		[NotMapped, JsonIgnore]
		public bool GenerateSingleImage { get; private set; }

		[NotMapped, JsonIgnore]
		public bool GenerateText { get; private set; }
		#endregion

		protected override void UpdateState(IEnumerable<IPreviewableLink> associatedLinks)
		{
			var associatedLinksList = associatedLinks.ToList();
			GenerateFullImages = associatedLinksList
				.OfType<PreviewableFileLink>()
				.Select(link => link.Settings)
				.OfType<DocumentLinkSettings>()
				.Any(set => set.GeneratePreviewImages);
			GenerateSingleImage = associatedLinksList
				.OfType<PreviewableFileLink>()
				.Select(link => link.Settings)
				.OfType<DocumentLinkSettings>()
				.All(set => !set.GeneratePreviewImages);
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
				(
					GenerateSingleImage && ImagePreviewFormats.All(previewFormat =>
					 {
						 var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
						 return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Count(filePath => !String.IsNullOrEmpty(filePath) && Path.GetFileName(filePath).StartsWith(Constants.SinglePreviewFilePrefixName)) > 0;
					 })
					||
					GenerateFullImages && ImagePreviewFormats.All(previewFormat =>
					{
						var previewFolderPath = Path.Combine(ContainerPath, previewFormat);
						return Directory.Exists(previewFolderPath) && Directory.GetFiles(previewFolderPath).Count(filePath => !String.IsNullOrEmpty(filePath) && !Path.GetFileName(filePath).StartsWith(Constants.SinglePreviewFilePrefixName)) > 0;
					})
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
