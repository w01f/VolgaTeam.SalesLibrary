using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class PreviewableFileLink : LibraryFileLink, IPreviewableLink
	{
		#region Nonpersistent Properties

		[NotMapped, JsonIgnore]
		public string PreviewName => NameWithExtension;
		[NotMapped, JsonIgnore]
		public string PreviewSourcePath => FullPath;
		[NotMapped, JsonIgnore]
		public string PreviewContainerPath => GetPreviewContainer().ContainerPath;
		[NotMapped, JsonIgnore]
		public string PreviewContainerName => GetPreviewContainer().ExtId.ToString("D");
		[NotMapped, JsonIgnore]
		public OneDrivePreviewSettings OneDriveSettings => ((FilePreviewContainerSettings)GetPreviewContainer().Settings).OneDriveSettings;
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

		public void UpdatePreviewContainer(IPreviewContentGenerator generator, CancellationToken cancelationToken)
		{
			ClearPreviewContainer();
			GetPreviewContainer().UpdatePreviewContent(new[] { this }, generator, cancelationToken);
		}
	}
}
