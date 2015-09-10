using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FileManager.BusinessClasses
{
	public class SecurityGroups
	{
		public List<LibraryGroup> Groups { get; private set; }

		public SecurityGroups()
		{
			Groups = new List<LibraryGroup>();
		}

		public void Load()
		{
			string message;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.SecurityGroupsDataTag);
			if (localMetaData != null)
			{
				DateTime? cloudLastUpdate = null;
				DateTime tempDate;
				var dateStr = ServiceConnector.Instance.GetMetaData(MetaDataConst.SecurityGroupsDataTag, MetaDataConst.LastUpdatePropertyName, out message);
				if (DateTime.TryParse(dateStr, out tempDate))
					cloudLastUpdate = tempDate;
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					Groups.AddRange(localMetaData.GetData<List<LibraryGroup>>());
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.SecurityGroupsDataTag);
			Groups.AddRange(ServiceConnector.Instance.GetSecurityGroups(out message).Select(LibraryGroup.LoadFromCloudData));
			localMetaData.Content = JsonConvert.SerializeObject(Groups);
			localMetaData.Save();
		}

		public List<LibraryGroup> GetGroupsByLibrary(string libraryId)
		{
			return Groups.Where(g => g.AssignedLibraryIds.Contains(libraryId)).ToList();
		}
	}
}
