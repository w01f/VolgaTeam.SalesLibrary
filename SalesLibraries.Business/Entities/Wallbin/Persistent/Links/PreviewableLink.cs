using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class PreviewableLink : LibraryFileLink
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public string PreviewContainerPath => GetPreviewContainer().ContainerPath;
		#endregion
		
		protected override void AfterCreate()
		{
			GetPreviewContainer();
			base.AfterCreate();
		}

		private BasePreviewContainer GetPreviewContainer()
		{
			return ParentLibrary.GetPreviewContainerBySourcePath(FullPath);
		}

		public void ClearPreviewContainer()
		{
			MarkAsModified();
			GetPreviewContainer().ClearContent();
		}
	}
}
