using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class PreviewableHyperLink : HyperLink, IPreviewableLink, IThumbnailSettingsHolder
	{
		#region Nonpersistent Properties

		[NotMapped, JsonIgnore]
		public virtual string PreviewSourcePath => FullPath;
		[NotMapped, JsonIgnore]
		public bool IsDead => false;
		[NotMapped, JsonIgnore]
		public string PreviewName => Name;
		[NotMapped, JsonIgnore]
		public string PreviewContainerPath => GetPreviewContainer().ContainerPath;
		[NotMapped, JsonIgnore]
		public string PreviewContainerName => GetPreviewContainer().ExtId.ToString("D");
		[NotMapped, JsonIgnore]
		public Color ThumbnailBackColor => ParentFolder.Settings.BackgroundWindowColor;
		[NotMapped, JsonIgnore]
		public bool ShowSourceFilesList => true;
		#endregion

		protected override void AfterCreate()
		{
			GetPreviewContainer();
			base.AfterCreate();
		}

		public BasePreviewContainer GetPreviewContainer()
		{
			return ParentLibrary.GetPreviewContainerBySourcePath(PreviewSourcePath);
		}

		public void ClearPreviewContainer()
		{
			MarkAsModified();
			GetPreviewContainer().ClearContent();
		}

		public void UpdatePreviewContainer(IPreviewGenerator generator, CancellationToken cancelationToken)
		{
			ClearPreviewContainer();
			GetPreviewContainer().UpdateContent(new[] { this }, generator, cancelationToken);
		}

		public IList<string> GetThumbnailSourceFiles()
		{
			var previewFiles = new List<string>();
			var sourceFilesPath = Path.Combine(PreviewContainerPath, PreviewFormats.Thumbnails);
			if (Directory.Exists(sourceFilesPath))
				previewFiles.AddRange(Directory.GetFiles(sourceFilesPath));
			previewFiles.Sort(WinAPIHelper.StrCmpLogicalW);
			return previewFiles;
		}
	}
}
