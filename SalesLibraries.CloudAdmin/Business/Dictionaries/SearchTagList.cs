using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.CloudAdmin.Configuration;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.Common.Dictionaries;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.ServiceConnector.FileManagerResourcesService;

namespace SalesLibraries.CloudAdmin.Business.Dictionaries
{
	class SearchTagList : BaseSearchTagList
	{
		public override void Load()
		{
			string message;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.CategoriesDataTag);
			if (localMetaData != null)
			{
				DateTime? cloudLastUpdate = null;
				DateTime tempDate;
				var dateStr = MainController.Instance.ServiceConnection.GetMetaData(MetaDataConst.CategoriesDataTag, MetaDataConst.LastUpdatePropertyName, out message);
				if (DateTime.TryParse(dateStr, out tempDate))
					cloudLastUpdate = tempDate;
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					var localData = localMetaData.GetData<SearchTagList>();
					SearchGroups.AddRange(localData.SearchGroups);
					MaxTags = localData.MaxTags;
					TagCount = localData.TagCount;
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.CategoriesDataTag);

			SearchGroups.AddRange(LoadFromCloudData(MainController.Instance.ServiceConnection.GetCategories(out message)));

			int tempInt;
			if (Int32.TryParse(MainController.Instance.ServiceConnection.GetMetaData(MetaDataConst.CategoriesDataTag, MetaDataConst.MaxTagsPropertyName, out message), out tempInt))
				MaxTags = tempInt;
			if (Int32.TryParse(MainController.Instance.ServiceConnection.GetMetaData(MetaDataConst.CategoriesDataTag, MetaDataConst.CountTagsPropertyName, out message), out tempInt))
				TagCount = tempInt > 0;

			localMetaData.Content = JsonConvert.SerializeObject(this);
			localMetaData.Save();
		}

		private static IEnumerable<SearchGroup> LoadFromCloudData(IEnumerable<Category> cloudCategories)
		{
			return cloudCategories.GroupBy(cat => cat.category).Select(group =>
			{
				var searchGroup = new SearchGroup()
				{
					Name = group.Key,
					Description = group.Select(g => g.description).FirstOrDefault()
				};
				searchGroup.Tags.AddRange(group.Select(g => new SearchTag { Name = g.tag }));
				return searchGroup;
			});
		}
	}
}
