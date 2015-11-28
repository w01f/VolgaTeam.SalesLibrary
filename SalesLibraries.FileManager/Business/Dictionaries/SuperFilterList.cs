using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Common.Dictionaries;
using SalesLibraries.FileManager.Configuration;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.Dictionaries
{
	class SuperFilterList : BaseSuperFilterList
	{
		public override void Load()
		{
			string message;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.SuperFiltersDataTag);
			if (localMetaData != null)
			{
				DateTime? cloudLastUpdate = null;
				DateTime tempDate;
				var dateStr = MainController.Instance.ServiceConnection.GetMetaData(MetaDataConst.SuperFiltersDataTag, MetaDataConst.LastUpdatePropertyName, out message);
				if (DateTime.TryParse(dateStr, out tempDate))
					cloudLastUpdate = tempDate;
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					Items.AddRange(localMetaData.GetData<List<String>>());
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.SuperFiltersDataTag);

			Items.AddRange(MainController.Instance.ServiceConnection.GetSuperFilters(out message).Select(cloudSuperFilter => cloudSuperFilter.value));

			localMetaData.Content = JsonConvert.SerializeObject(Items);
			localMetaData.Save();
		}
	}
}
