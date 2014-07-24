using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class GalleryManager
	{
		private bool _isConfigured;

		public bool AutoLoad { get; private set; }
		public List<SourceUrl> SourceUrls { get; private set; }

		public GalleryManager(string settingPath)
		{
			SourceUrls = new List<SourceUrl>();
			Init(settingPath);
		}

		private void Init(string settingsPath)
		{
			if (!File.Exists(settingsPath)) return;
			var settingsDoc = XDocument.Load(settingsPath);
			foreach (var element in settingsDoc.Descendants("SourceUrl"))
			{
				var name = element.Attribute("Name") != null ? element.Attribute("Name").Value : String.Empty;
				var url = element.Attribute("Url") != null ? element.Attribute("Url").Value : String.Empty;
				var groupId = element.Attribute("GroupId") != null ? element.Attribute("GroupId").Value : String.Empty;
				if (!url.EndsWith("/")) url += "/";
				var sourceUrl = new SourceUrl { Url = url, Name = name, GroupId = groupId };
				SourceUrls.Add(sourceUrl);
			}
			var node = settingsDoc.Descendants("AutoLoad").FirstOrDefault();
			bool temp;
			AutoLoad = node != null && Boolean.TryParse(node.Value, out temp) && temp;

			if (!SourceUrls.Any()) return;
			_isConfigured = true;
		}

		public IEnumerable<SnapshotCollection> GetSnapshots(SourceUrl sourceUrl)
		{
			var result = new List<SnapshotCollection>();
			if (!_isConfigured) return result;
			foreach (var item in (IEnumerable)JsonConvert.DeserializeObject(new WebClient().DownloadString(String.Format("{0}{1}{2}",
				sourceUrl.Url,
				"contents.php",
				!String.IsNullOrEmpty(sourceUrl.GroupId) ? String.Format("?gid={0}", sourceUrl.GroupId) : String.Empty))))
			{
				var i = 0;
				JProperty tmp = null;
				foreach (var m in (IEnumerable)item)
				{
					switch (i)
					{
						case 0:
							tmp = (m as JProperty); i++;
							break;

						case 1:
							var snapshotName = tmp != null ? (String)tmp.Value : String.Empty;
							var s = new SnapshotCollection(snapshotName);
							if (m != null)
								foreach (var property in ((IEnumerable)m).OfType<JArray>())
								{
									foreach (var ss in property.Select(prop => (String)prop).Where(ss => !String.IsNullOrEmpty(ss)))
										s.Screenshots.Add(new SnapshotItem(ss.Replace(s.Name, String.Empty), String.Format("{0}{1}/{2}/", sourceUrl.Url, s.Name, ss)));
									break;
								}

							if (s.Screenshots.Any())
								result.Add(s);
							break;
					}
					if (i > 1)
						break; // Next
				}
			}
			return result;
		}

		public class SourceUrl
		{
			public string Name { get; set; }
			public string Url { get; set; }
			public string GroupId { get; set; }
		}
	}

	public class SnapshotCollection
	{
		public string Name { get; private set; }

		public SnapshotCollection(string name)
		{
			Name = name;
			Screenshots = new List<SnapshotItem>();
		}

		public override String ToString()
		{
			return Name;
		}

		public List<SnapshotItem> Screenshots { get; private set; }
	}

	public class SnapshotItem
	{
		public string Name { get; private set; }
		public string Url { get; private set; }

		public SnapshotItem(string name, string url)
		{
			Url = url;
			Name = name.Trim().TrimStart(new[] { '-' }).Replace(" - ", "-").Replace("-", " - ");
		}

		public override String ToString()
		{
			return Name;
		}
	}
}
