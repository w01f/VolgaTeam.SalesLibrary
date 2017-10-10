using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public abstract class FilePreviewContainer : BasePreviewContainer
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
		public override string SourcePath => Path.Combine(Library.Path, RelativePath);

		[NotMapped, JsonIgnore]
		public string SourceSubType => !String.IsNullOrEmpty(SourcePath) && File.Exists(SourcePath) ? Path.GetExtension(SourcePath).Replace(".", String.Empty).ToLower() : null;
		#endregion

		protected override void UpdateState(IList<IPreviewableLink> associatedLinks)
		{
			base.UpdateState(associatedLinks);
			if (!File.Exists(SourcePath) || associatedLinks.All(link => link.IsDead))
			{
				IsUpToDate = false;
				IsAlive = false;
				return;
			}
			var sourceFileActualDate = File.GetLastWriteTime(SourcePath);
			var containerActualDate = Directory.GetLastWriteTime(ContainerPath);
			var time = sourceFileActualDate.Subtract(containerActualDate);
			IsUpToDate = time.Minutes <= 0;
		}
	}
}
