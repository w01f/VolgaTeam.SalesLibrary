using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.BusinessClasses
{
	public class SuperFilterList
	{
		public List<SuperFilter> Items { get; private set; }

		public SuperFilterList()
		{
			Items = new List<SuperFilter>();
		}

		public void Load()
		{
			if (AppModeManager.Instance.AppMode == AppModeEnum.Local)
				LoadLocal();
			else
				LoadCloud();
		}

		private void LoadLocal()
		{
			Items.AddRange(SuperFilter.LoadSuperFilters());
		}

		private void LoadCloud()
		{
			string message;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.SuperFiltersDataTag);
			if (localMetaData != null)
			{
				DateTime? cloudLastUpdate = null;
				DateTime tempDate;
				var dateStr = ServiceConnector.Instance.GetMetaData(MetaDataConst.SuperFiltersDataTag, MetaDataConst.LastUpdatePropertyName, out message);
				if (DateTime.TryParse(dateStr, out tempDate))
					cloudLastUpdate = tempDate;
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					Items.AddRange(localMetaData.GetData<List<SuperFilter>>());
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.SuperFiltersDataTag);

			Items.AddRange(ServiceConnector.Instance.GetSuperFilters(out message).Select(cloudSuperFilter => new SuperFilter() { Name = cloudSuperFilter.value }));

			localMetaData.Content = JsonConvert.SerializeObject(Items);
			localMetaData.Save();
		}
	}
}
