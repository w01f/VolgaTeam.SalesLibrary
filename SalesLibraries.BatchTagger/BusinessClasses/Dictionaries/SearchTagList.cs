﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.BatchTagger.Configuration;
using SalesLibraries.Common.Dictionaries;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.ServiceConnector.Models.Rest.AppMetaData;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Dictionaries;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.BatchTagger.BusinessClasses.Dictionaries
{
	public class SearchTagList : BaseSearchTagList
	{
		public override void Load()
		{
			RestResponse response;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.CategoriesDataTag);
			if (localMetaData != null)
			{
				response = AppManager.Instance.Connections.RestConnection.DoRequest(new MetaDataGetRequestData
				{
					DataTag = MetaDataConst.CategoriesDataTag,
					PropertyName = MetaDataConst.LastUpdatePropertyName
				},
					"Error loading dictionaries updates from server");
				var cloudLastUpdate = response.GetData<DateTime?>();
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					var localData = localMetaData.GetData<SearchTagList>();
					SearchSuperGroups.AddRange(localData.SearchSuperGroups);
					MaxTags = localData.MaxTags;
					TagCount = localData.TagCount;
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.CategoriesDataTag);

			response = AppManager.Instance.Connections.RestConnection.DoRequest(new SearchCategoriesGetRequestData(), "Error loading dictionaries updates from server");
			SearchSuperGroups.AddRange(LoadFromCloudData(response.GetData<SearchCategory[]>()));


			response = AppManager.Instance.Connections.RestConnection.DoRequest(new MetaDataGetRequestData
			{
				DataTag = MetaDataConst.CategoriesDataTag,
				PropertyName = MetaDataConst.MaxTagsPropertyName
			},
				"Error loading dictionaries updates from server");
			int tempInt;
			if (Int32.TryParse(response.DataEncoded, out tempInt))
				MaxTags = tempInt;
			response = AppManager.Instance.Connections.RestConnection.DoRequest(new MetaDataGetRequestData
			{
				DataTag = MetaDataConst.CategoriesDataTag,
				PropertyName = MetaDataConst.CountTagsPropertyName
			},
				"Error loading dictionaries updates from server");
			bool tempBool;
			if (Boolean.TryParse(response.DataEncoded, out tempBool))
				TagCount = tempBool;

			localMetaData.Content = JsonConvert.SerializeObject(this);
			localMetaData.Save();
		}

		private static IEnumerable<SearchSuperGroup> LoadFromCloudData(IEnumerable<SearchCategory> cloudCategories)
		{
			return cloudCategories.GroupBy(cat => new { cat.Group }).Select(superGroup =>
			  {
				  var searchSuperGroup = new SearchSuperGroup
				  {
					  Name = superGroup.Key.Group
				  };
				  searchSuperGroup.Groups.AddRange(superGroup.GroupBy(group => new { group.Group, group.Category }).Select(group =>
					 {
						 var searchGroup = new SearchGroup
						 {
							 Name = group.Key.Category,
							 SuperGroup = group.Key.Group,
							 Description = group.Select(g => g.Description).FirstOrDefault()
						 };
						 searchGroup.Tags.AddRange(group.Select(g => new SearchTag { Name = g.Tag }));
						 return searchGroup;
					 }));
				  return searchSuperGroup;
			  });
		}
	}
}
