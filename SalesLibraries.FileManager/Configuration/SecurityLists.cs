using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;
using SalesLibraries.FileManager.Business.Models;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Configuration
{
	class SecurityLists
	{
		private readonly List<LibraryGroup> _groups = new List<LibraryGroup>();

		public void Load()
		{
			string message;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.SecurityGroupsDataTag);
			if (localMetaData != null)
			{
				DateTime? cloudLastUpdate = null;
				DateTime tempDate;
				var dateStr = MainController.Instance.ServiceConnection.GetMetaData(MetaDataConst.SecurityGroupsDataTag, MetaDataConst.LastUpdatePropertyName, out message);
				if (DateTime.TryParse(dateStr, out tempDate))
					cloudLastUpdate = tempDate;
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					_groups.AddRange(localMetaData.GetData<List<LibraryGroup>>());
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.SecurityGroupsDataTag);
			_groups.AddRange(MainController.Instance.ServiceConnection.GetSecurityGroups(out message).Select(LibraryGroup.LoadFromCloudData));
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
