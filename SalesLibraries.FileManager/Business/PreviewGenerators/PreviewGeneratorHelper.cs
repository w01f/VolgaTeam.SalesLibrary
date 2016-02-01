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
		public static IPreviewGenerator GetPreviewGenerator(this BasePreviewContainer previewContainer)
		{
			var previewGenerators = ObjectIntendHelper.GetObjectInstances(
				typeof(IPreviewGenerator),
				EntitySettingsResolver.ExtractObjectTypeFromProxy(previewContainer.GetType()))
				.OfType<IPreviewGenerator>()
				.ToList();
			if (!previewGenerators.Any())
				throw new NotImplementedException("Preview Generator was not found for that type");
			if (previewGenerators.Count > 1)
				throw new NotImplementedException("Several Preview Generators was not found for that type");
			return previewGenerators.Single();
		}
	}
}
