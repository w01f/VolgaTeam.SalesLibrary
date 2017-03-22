using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class PreviewableLink : LibraryFileLink
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public string PreviewContainerPath => GetPreviewContainer().ContainerPath;
		public string PreviewContainerName => GetPreviewContainer().ExtId.ToString("D");
		#endregion

		protected override void AfterCreate()
		{
			GetPreviewContainer();
			base.AfterCreate();
		}

		public BasePreviewContainer GetPreviewContainer()
		{
			return ParentLibrary.GetPreviewContainerBySourcePath(FullPath);
		}

		public void ClearPreviewContainer()
		{
			MarkAsModified();
			GetPreviewContainer().ClearContent();
		}

		public void UpddatePreviewContainer(IPreviewGenerator generator, CancellationToken cancelationToken)
		{
			ClearPreviewContainer();
			GetPreviewContainer().UpdateContent(new[] { this }, generator, cancelationToken);
		}
	}
}
