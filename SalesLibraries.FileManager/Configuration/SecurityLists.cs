using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using SalesLibraries.FileManager.Business.Models.Security;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.ServiceConnector.Models.Rest.AppMetaData;
using SalesLibraries.ServiceConnector.Models.Rest.Common;
using SalesLibraries.ServiceConnector.Models.Rest.Dictionaries;
using SalesLibraries.ServiceConnector.Services.Rest;

namespace SalesLibraries.FileManager.Configuration
{
	class SecurityLists
	{
		private readonly List<LibraryGroup> _groups = new List<LibraryGroup>();

		public void Load()
		{
			RestResponse response;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.SecurityGroupsDataTag);
			if (localMetaData != null)
			{
				response = MainController.Instance.RestServiceConnection.DoRequest(new MetaDataGetRequestData
				{
					DataTag = MetaDataConst.SecurityGroupsDataTag,
					PropertyName = MetaDataConst.LastUpdatePropertyName
				},
					"Error loading dictionaries updates from server");
				var cloudLastUpdate = response.GetData<DateTime?>(); if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					_groups.AddRange(localMetaData.GetData<List<LibraryGroup>>());
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.SecurityGroupsDataTag);

			response = MainController.Instance.RestServiceConnection.DoRequest(new SecurityGetRequestData(), "Error loading dictionaries updates from server");
			var securityGroups = response.GetData<SiteUserGroup[]>();
			_groups.AddRange(securityGroups.Select(LibraryGroup.LoadFromCloudData));
			localMetaData.Content = JsonConvert.SerializeObject(_groups);
			localMetaData.Save();
		}

		public List<LibraryGroup> GetGroupsByLibrary(Guid libraryId)
		{
			return _groups.Where(g => g.AssignedLibraryIds.Contains(libraryId.ToString())).ToList();
		}

		public IEnumerable<string> LoadUserLoginsFromFile(string filePath)
		{
			var document = new XmlDocument();
			document.Load(filePath);
			return document.SelectNodes(@"/Users/User").OfType<XmlNode>().Select(node => node.InnerText.ToLower());
		}
	}
}
