using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.BusinessClasses
{
	public class CategoryList
	{
		public CategoryList()
		{
			SearchGroups = new List<SearchGroup>();
		}

		public List<SearchGroup> SearchGroups { get; private set; }
		public int MaxTags { get; set; }
		public bool TagCount { get; set; }

		public void Load()
		{
			if (AppModeManager.Instance.AppMode == AppModeEnum.Local)
				LoadLocal();
			else
				LoadCloud();
		}

		private void LoadCloud()
		{
			string message;
			var localMetaData = MetaDataContainer.Load(MetaDataConst.CategoriesDataTag);
			if (localMetaData != null)
			{
				DateTime? cloudLastUpdate = null;
				DateTime tempDate;
				var dateStr = ServiceConnector.Instance.GetMetaData(MetaDataConst.CategoriesDataTag, MetaDataConst.LastUpdatePropertyName, out message);
				if (DateTime.TryParse(dateStr, out tempDate))
					cloudLastUpdate = tempDate;
				if (!cloudLastUpdate.HasValue || cloudLastUpdate <= localMetaData.LastUpdate)
				{
					var localData = localMetaData.GetData<CategoryList>();
					SearchGroups.AddRange(localData.SearchGroups);
					MaxTags = localData.MaxTags;
					TagCount = localData.TagCount;
					return;
				}
			}
			else
				localMetaData = new MetaDataContainer(MetaDataConst.CategoriesDataTag);

			SearchGroups.AddRange(SearchGroup.LoadFromCloudData(ServiceConnector.Instance.GetCategories(out message)));

			int tempInt;
			if (Int32.TryParse(ServiceConnector.Instance.GetMetaData(MetaDataConst.CategoriesDataTag, MetaDataConst.MaxTagsPropertyName, out message), out tempInt))
				MaxTags = tempInt;

			bool tempBool;
			if (Boolean.TryParse(ServiceConnector.Instance.GetMetaData(MetaDataConst.CategoriesDataTag, MetaDataConst.CountTagsPropertyName, out message), out tempBool))
				TagCount = tempBool;

			localMetaData.Content = JsonConvert.SerializeObject(this);
			localMetaData.Save();
		}

		private void LoadLocal()
		{
			var listsFileName = Path.Combine(string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "SDSearch.xml");
			if (!File.Exists(listsFileName)) return;
			var document = new XmlDocument();
			document.Load(listsFileName);

			var node = document.SelectSingleNode(@"/SDSearch");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "MaxTags":
						int tempInt;
						if (int.TryParse(childNode.InnerText, out tempInt))
							MaxTags = tempInt;
						break;
					case "TagCount":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							TagCount = tempBool;
						break;
					case "Category":
						var group = new SearchGroup();
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Name":
									@group.Name = attribute.Value;
									break;
								case "Description":
									@group.Description = attribute.Value;
									break;
							}
						}
						foreach (XmlNode tagNode in childNode.ChildNodes)
						{
							switch (tagNode.Name)
							{
								case "Tag":
									foreach (XmlAttribute attribute in tagNode.Attributes)
									{
										switch (attribute.Name)
										{
											case "Value":
												if (!string.IsNullOrEmpty(attribute.Value))
													@group.Tags.Add(new SearchTag(@group.Name) { Name = attribute.Value });
												break;
										}
									}
									break;
							}
						}
						if (!string.IsNullOrEmpty(@group.Name) && @group.Tags.Count > 0)
							SearchGroups.Add(@group);
						break;
				}
			}
		}
	}
}
