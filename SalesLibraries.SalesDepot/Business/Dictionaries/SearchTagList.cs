using System;
using System.Xml;
using SalesLibraries.Common.Dictionaries;
using SalesLibraries.Common.Objects.SearchTags;

namespace SalesLibraries.SalesDepot.Business.Dictionaries
{
	class SearchTagList : BaseSearchTagList
	{
		public override void Load()
		{
			if (!Configuration.RemoteResourceManager.Instance.SDSearchFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(Configuration.RemoteResourceManager.Instance.SDSearchFile.LocalPath);

			var node = document.SelectSingleNode(@"/SDSearch");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "MaxTags":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								MaxTags = temp;
						}
						break;
					case "TagCount":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								TagCount = temp;
						}
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
												if (!String.IsNullOrEmpty(attribute.Value))
													@group.Tags.Add(new SearchTag() { Name = attribute.Value });
												break;
										}
									}
									break;
							}
						}
						if (!String.IsNullOrEmpty(@group.Name) && @group.Tags.Count > 0)
							SearchGroups.Add(@group);
						break;
				}
			}
		}
	}
}
