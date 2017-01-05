using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Common.Dictionaries;
using SalesLibraries.CloudAdmin.Configuration;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.ServiceConnector.Models.Rest.AppMetaData;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Dictionaries;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.CloudAdmin.Business.Dictionaries
{
	class SuperFilterList : BaseSuperFilterList
	{
		public override void Load()
		{
			RestResponse response;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.SuperFiltersDataTag);
			if (localMetaData != null)
			{
				response = MainController.Instance.RestServiceConnection.DoRequest(new MetaDataGetRequestData
				{
					DataTag = MetaDataConst.SuperFiltersDataTag,
					PropertyName = MetaDataConst.LastUpdatePropertyName
				},
					"Error loading dictionaries updates from server");
				var cloudLastUpdate = response.GetData<DateTime?>();
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					Items.AddRange(localMetaData.GetData<List<string>>());
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.SuperFiltersDataTag);

			response = MainController.Instance.RestServiceConnection.DoRequest(new SuperFiltersGetRequestData(), "Error loading dictionaries updates from server");
			Items.AddRange(response.GetData<SuperFilter[]>().Select(cloudSuperFilter => cloudSuperFilter.Value));

			localMetaData.Content = JsonConvert.SerializeObject(Items);
			localMetaData.Save();
		}
	}
}
