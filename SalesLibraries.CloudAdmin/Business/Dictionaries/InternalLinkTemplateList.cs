using System.Collections.Generic;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.RemoteStorage;
using SalesLibraries.CloudAdmin.Configuration;

namespace SalesLibraries.CloudAdmin.Business.Dictionaries
{
	class InternalLinkTemplateList
	{
		public List<InternalLinkTemplate> Templates { get; } = new List<InternalLinkTemplate>();

		public void Load()
		{
			var libraryTemplatesFolder = new StorageDirectory(RemoteResourceManager.Instance.InternalLinkTemplatesFolder.RelativePathParts.Merge("Library"));
			if (libraryTemplatesFolder.ExistsLocal())
				Templates.AddRange(Directory.GetFiles(libraryTemplatesFolder.LocalPath, "*.xml")
					.Select(path => new InternalLinkTemplate
					{
						Type = InternlalLinkTemplateType.Library,
						SourcePath = path
					}));

			var libraryPageTemplatesFolder = new StorageDirectory(RemoteResourceManager.Instance.InternalLinkTemplatesFolder.RelativePathParts.Merge("Page"));
			if (libraryPageTemplatesFolder.ExistsLocal())
				Templates.AddRange(Directory.GetFiles(libraryPageTemplatesFolder.LocalPath, "*.xml")
					.Select(path => new InternalLinkTemplate
					{
						Type = InternlalLinkTemplateType.Page,
						SourcePath = path
					}));

			var libraryFolderTemplatesFolder = new StorageDirectory(RemoteResourceManager.Instance.InternalLinkTemplatesFolder.RelativePathParts.Merge("Window"));
			if (libraryFolderTemplatesFolder.ExistsLocal())
				Templates.AddRange(Directory.GetFiles(libraryFolderTemplatesFolder.LocalPath, "*.xml")
					.Select(path => new InternalLinkTemplate
					{
						Type = InternlalLinkTemplateType.Folder,
						SourcePath = path
					}));
		}
	}
}
