using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using ProgramManager.CoreObjects;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.ConfigurationClasses
{
	internal class ListManager
	{
		private static readonly ListManager _instance = new ListManager();

		private ListManager()
		{
			HeaderFonts = new List<OutputFont>();
			HeaderFonts.Add(new OutputFont("Arial", 12, true));
			HeaderFonts.Add(new OutputFont("Verdana", 12, true));
			HeaderFonts.Add(new OutputFont("Calibri", 12, true));
			HeaderFonts.Add(new OutputFont("Trebuchet MS", 12, true));

			FooterFonts = new List<OutputFont>();
			FooterFonts.Add(new OutputFont("Arial", 11));
			FooterFonts.Add(new OutputFont("Verdana", 11));
			FooterFonts.Add(new OutputFont("Calibri", 11));
			FooterFonts.Add(new OutputFont("Trebuchet MS", 11));

			BodyFonts = new List<OutputFont>();
			BodyFonts.Add(new OutputFont("Arial", 8));
			BodyFonts.Add(new OutputFont("Arial", 9));
			BodyFonts.Add(new OutputFont("Arial", 10));
			BodyFonts.Add(new OutputFont("Verdana", 8));
			BodyFonts.Add(new OutputFont("Calibri", 8));
			BodyFonts.Add(new OutputFont("Trebuchet MS", 8));
			BodyFonts.Add(new OutputFont("Verdana", 9));
			BodyFonts.Add(new OutputFont("Calibri", 9));
			BodyFonts.Add(new OutputFont("Trebuchet MS", 9));
			BodyFonts.Add(new OutputFont("Verdana", 10));
			BodyFonts.Add(new OutputFont("Calibri", 10));
			BodyFonts.Add(new OutputFont("Trebuchet MS", 10));
		}

		public string ListsFolder { get; set; }

		public SearchTags SearchTags { get; set; }

		public List<OutputFont> HeaderFonts { get; private set; }
		public List<OutputFont> FooterFonts { get; private set; }
		public List<OutputFont> BodyFonts { get; private set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public void Init()
		{
			ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			SearchTags = new SearchTags();
		}
	}

	public class SearchTags
	{
		private readonly string _listsFileName;

		public SearchTags()
		{
			_listsFileName = Path.Combine(ListManager.Instance.ListsFolder, "SDSearch.xml");
			SearchGroups = new List<SearchGroup>();
			Load();
		}

		public List<SearchGroup> SearchGroups { get; set; }

		private void Load()
		{
			XmlNode node;
			if (File.Exists(_listsFileName))
			{
				var document = new XmlDocument();
				document.Load(_listsFileName);

				node = document.SelectSingleNode(@"/SDSearch");
				if (node != null)
				{
					int i = 0;
					foreach (XmlNode childNode in node.ChildNodes)
					{
						switch (childNode.Name)
						{
							case "Category":
								var group = new SearchGroup();
								foreach (XmlAttribute attribute in childNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Name":
											group.Name = attribute.Value;
											break;
										case "Description":
											group.Description = attribute.Value;
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
															group.Tags.Add(new SearchTag(group.Name) { Name = attribute.Value });
														break;
												}
											}
											break;
									}
								}
								if (!string.IsNullOrEmpty(group.Name) && group.Tags.Count > 0)
									SearchGroups.Add(group);
								i++;
								break;
						}
					}
				}
			}
		}
	}
}