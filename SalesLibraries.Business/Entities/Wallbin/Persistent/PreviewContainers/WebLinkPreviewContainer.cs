using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class WebLinkPreviewContainer : BasePreviewContainer
	{
		#region Nonpersistent Properties
		private CommonPreviewContainerSettings _settings;
		[NotMapped, JsonIgnore]
		public override BasePreviewContainerSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<CommonPreviewContainerSettings>(this, SettingsEncoded)); }
			set { _settings = value as CommonPreviewContainerSettings; }
		}

		[NotMapped, JsonIgnore]
		public override String SourcePath => RelativePath;

		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder => DocumentSubFolderName;

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats => new[]
		{
			PreviewFormats.Thumbnails,
			PreviewFormats.ThumbnailsForDatatable,
		};
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
