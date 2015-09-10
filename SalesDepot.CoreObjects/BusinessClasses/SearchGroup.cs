﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Font = System.Drawing.Font;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class SearchGroup
	{
		public const string TagName = "Category";

		public string AllTags
		{
			get
			{
				return string.Join(", ", Tags.Where(t => !String.IsNullOrEmpty(t.Name)).Select(x => x.Name));
			}
		}

		public SearchGroup()
		{
			Name = string.Empty;
			Description = string.Empty;
			Tags = new List<SearchTag>();
			TagNameObject = TagName;
		}

		public string TagNameObject { get; protected set; }

		public string Name { get; set; }

		[JsonIgnore]
		public bool Selected { get; set; }
		public string Description { get; set; }
		public List<SearchTag> Tags { get; private set; }

		[JsonIgnore]
		public CheckedListBoxControl ListBox { get; private set; }
		[JsonIgnore]
		public ButtonX ToggleButton { get; private set; }

		public bool Compare(SearchGroup anotherGroup)
		{
			return Tags.All(tag => anotherGroup.Tags.Select(x => x.Name).Contains(tag.Name)) && Tags.Count == anotherGroup.Tags.Count;
		}

		public void InitGroupControls()
		{
			if (ListBox != null)
				ListBox.Dispose();
			ListBox = new CheckedListBoxControl();
			ListBox.Appearance.Font = new Font("Arial", 9.75F, FontStyle.Regular);
			ListBox.Appearance.Options.UseFont = true;
			ListBox.CheckOnClick = true;
			ListBox.ItemHeight = 30;
			ListBox.SelectionMode = SelectionMode.None;
			ListBox.Dock = DockStyle.Fill;
			ListBox.Items.AddRange(Tags.ToArray());

			if (ToggleButton != null)
				ToggleButton.Dispose();
			ToggleButton = new ButtonX();
			ToggleButton.AccessibleRole = AccessibleRole.PushButton;
			ToggleButton.ColorTable = eButtonColor.OrangeWithBackground;
			ToggleButton.Size = new Size(250, 30);
			ToggleButton.Style = eDotNetBarStyle.StyleManagerControlled;
			ToggleButton.Text = Description.Replace("&", "&&");
			ToggleButton.TextColor = Color.Black;
			ToggleButton.TextAlignment = eButtonTextAlignment.Left;
			ToggleButton.Tag = ListBox;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.Append(@"<" + TagNameObject + " ");
			result.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
			result.AppendLine(@">");
			foreach (var tag in Tags)
			{
				result.Append(@"<Tag ");
				result.Append("Value = \"" + tag.Name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + "\" ");
				result.AppendLine(@"/>");
			}
			result.AppendLine(@"</" + TagNameObject + ">");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
				}
			}
			foreach (XmlNode tagNode in node.ChildNodes)
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
										Tags.Add(new SearchTag(Name) { Name = attribute.Value });
									break;
							}
						}
						break;
				}
			}
		}

		public virtual SearchGroup Clone()
		{
			var result = new SearchGroup();
			result.Name = Name;
			result.Description = Description;
			result.Selected = Selected;
			result.Tags.AddRange(Tags.Select(t => new SearchTag(result.Name) { Name = t.Name, Selected = t.Selected }));
			return result;
		}

		public static IEnumerable<SearchGroup> LoadFromCloudData(IEnumerable<SalesDepot.Services.FileManagerDataService.Category> cloudCategories)
		{
			return cloudCategories.GroupBy(cat => cat.category).Select(group => new SearchGroup()
			{
				Name = group.Key,
				Description = group.Select(g => g.description).FirstOrDefault(),
				Tags = new List<SearchTag>(group.Select(g => new SearchTag(group.Key) { Name = g.tag }))
			});
		}
	}

	public class CustomKeywords : SearchGroup
	{
		public new const string TagName = "CustomKeywords";

		public CustomKeywords()
		{
			Name = "Custom Keywords";
			TagNameObject = TagName;
		}

		public override SearchGroup Clone()
		{
			var result = new CustomKeywords();
			result.Name = Name;
			result.Description = Description;
			result.Selected = Selected;
			result.Tags.AddRange(Tags.Select(t => new SearchTag(result.Name) { Name = t.Name, Selected = t.Selected }));
			return result;
		}
	}

	public class SearchTag
	{
		public string Name { get; set; }
		public string Parent { get; private set; }

		[JsonIgnore]
		public bool Selected { get; set; }

		public SearchTag(string parentGroup)
		{
			Parent = parentGroup;
		}

		public override string ToString()
		{
			return Name;
		}
	}
}