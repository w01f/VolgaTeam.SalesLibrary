using System;
using System.Linq;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.JsonConverters;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	static class PreviewGeneratorHelper
	{
		public static IPreviewContentGenerator GetPreviewContentGenerator(this BasePreviewContainer previewContainer)
		{
			var previewContentGenerators = ObjectIntendHelper.GetObjectInstances(
				typeof(IPreviewContentGenerator),
				EntitySettingsResolver.ExtractObjectTypeFromProxy(previewContainer.GetType()))
				.OfType<IPreviewContentGenerator>()
				.ToList();
			if (!previewContentGenerators.Any())
				throw new NotImplementedException("Preview Generator was not found for that type");
			if (previewContentGenerators.Count > 1)
				throw new NotImplementedException("Several Preview Generators were found for that type");
			return previewContentGenerators.Single();
		}

		public static IOneDriveContentGenerator GetOneDriveContentGenerator(this FilePreviewContainer previewContainer)
		{
			var oneDriveContentGenerators = ObjectIntendHelper.GetObjectInstances(
					typeof(IOneDriveContentGenerator),
					EntitySettingsResolver.ExtractObjectTypeFromProxy(previewContainer.GetType()))
				.OfType<IOneDriveContentGenerator>()
				.ToList();
			if (oneDriveContentGenerators.Count > 1)
				throw new NotImplementedException("Several Preview Generators were found for that type");
			return oneDriveContentGenerators.FirstOrDefault() ?? new CommonFilePreviewGenerator();
		}
	}
}
