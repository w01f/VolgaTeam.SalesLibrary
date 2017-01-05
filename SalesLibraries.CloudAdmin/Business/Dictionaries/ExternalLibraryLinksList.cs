using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SalesLibraries.CloudAdmin.Business.Models.ExternalLibraryLinks;
using SalesLibraries.CloudAdmin.Configuration;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.ServiceConnector.Models.Rest.AppMetaData;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Dictionaries;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.CloudAdmin.Business.Dictionaries
{
	class ExternalLibraryLinksList
	{
		public bool IsLoaded { get; set; }

		public List<Library> Libraries { get; set; }

		public ExternalLibraryLinksList()
		{
			Libraries = new List<Library>();
		}

		public void Load()
		{
			RestResponse response;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.ExternalLibraryLinksDataTag);
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
					Libraries.AddRange(localMetaData.GetData<List<Library>>());
					IsLoaded = true;
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.ExternalLibraryLinksDataTag);

			response = MainController.Instance.RestServiceConnection.DoRequest(new LibraryLinksGetRequestData(), "Error loading dictionaries updates from server");
			Libraries.AddRange(response.GetData<Library[]>());

			localMetaData.Content = JsonConvert.SerializeObject(Libraries);
			localMetaData.Save();
			IsLoaded = true;
		}
	}
}
