﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class PreviewableFileLink : LibraryFileLink, IPreviewableLink
	{
		#region Nonpersistent Properties

		[NotMapped, JsonIgnore]
		public string PreviewName => NameWithExtension;
		[NotMapped, JsonIgnore]
		public string PreviewContainerPath => GetPreviewContainer().ContainerPath;
		[NotMapped, JsonIgnore]
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

		public void UpdatePreviewContainer(IPreviewGenerator generator, CancellationToken cancelationToken)
		{
			ClearPreviewContainer();
			GetPreviewContainer().UpdateContent(new[] { this }, generator, cancelationToken);
		}
	}
}