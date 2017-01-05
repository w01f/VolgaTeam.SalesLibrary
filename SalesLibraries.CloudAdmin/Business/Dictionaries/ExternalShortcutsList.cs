using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SalesLibraries.CloudAdmin.Business.Models.ExternalShortcuts;
using SalesLibraries.CloudAdmin.Configuration;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.ServiceConnector.Models.Rest.AppMetaData;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Dictionaries;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.CloudAdmin.Business.Dictionaries
{
	class ExternalShortcutsList
	{
		public bool IsLoaded { get; set; }

		public List<ShortcutLink> ShortcutLinks { get; set; }

		public ExternalShortcutsList()
		{
			ShortcutLinks = new List<ShortcutLink>();
		}

		public void Load()
		{
			RestResponse response;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.ExternalShortcutsDataTag);
			if (localMetaData != null)
			{
				response = MainController.Instance.RestServiceConnection.DoRequest(new MetaDataGetRequestData
				{
					DataTag = MetaDataConst.ExternalLibraryLinksDataTag,
					PropertyName = MetaDataConst.LastUpdatePropertyName
				},
					"Error loading dictionaries updates from server");
				var cloudLastUpdate = response.GetData<DateTime?>();
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					ShortcutLinks.AddRange(localMetaData.GetData<List<ShortcutLink>>());
					IsLoaded = true;
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.ExternalShortcutsDataTag);

			response = MainController.Instance.RestServiceConnection.DoRequest(new ShortcutLinksGetRequestData(), "Error loading dictionaries updates from server");
			ShortcutLinks.AddRange(response.GetData<ShortcutLink[]>());

			localMetaData.Content = JsonConvert.SerializeObject(ShortcutLinks);
			localMetaData.Save();
			IsLoaded = true;
		}
	}
}
