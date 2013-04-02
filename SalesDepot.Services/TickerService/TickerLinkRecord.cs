using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalesDepot.Services.TickerService
{
	public partial class TickerLink
	{
		public static string[] AvailableTypes
		{
			get { return new[] { "Simple text", "Web link", "Library Link", "Video Link", "File Link" }; }
		}

		public static string GetTagTextByKey(string type, string key)
		{
			switch (type)
			{
				case "url":
					switch (key)
					{
						case "path":
							return "URL";
						default:
							return String.Empty;
					}
				case "link":
					switch (key)
					{
						case "library":
							return "Library";
						case "page":
							return "Page";
						case "link":
							return "Link";
						default:
							return String.Empty;
					}
				case "video":
					switch (key)
					{
						case "path":
							return "Video Relative Path";
						default:
							return String.Empty;
					}
				case "file":
					switch (key)
					{
						case "path":
							return "File Relative Path";
						default:
							return String.Empty;
					}
				default:
					return String.Empty;
			}
		}

		public string GetDataByTag(string key)
		{
			return details.Where(x => x.tag.Equals(key)).Select(x => x.data).FirstOrDefault();
		}

		public string TypeString
		{
			get
			{
				switch (type)
				{
					case "text":
						return "Simple text";
					case "url":
						return "Web link";
					case "link":
						return "Library Link";
					case "video":
						return "Video Link";
					case "file":
						return "File Link";
					default:
						return null;
				}
			}
			set
			{
				switch (value)
				{
					case "Simple text":
						type = "text";
						break;
					case "Web link":
						type = "url";
						break;
					case "Library Link":
						type = "link";
						break;
					case "Video Link":
						type = "video";
						break;
					case "File Link":
						type = "file";
						break;
					default:
						type = null;
						break;
				}
			}
		}

		public string DetailString
		{
			get
			{
				if (details == null) return null;
				var detailString = new StringBuilder();
				foreach (var detail in details)
					detailString.AppendLine(string.Format("{0}: {1}", GetTagTextByKey(type, detail.tag), detail.data));
				return detailString.ToString();
			}
		}

		public int UserOrder { get; set; }

		public void AddDetail(string tag, string data)
		{
			var detailList = new List<KeyValuePair>(details);
			var detail = detailList.FirstOrDefault(x => x.tag.Equals(tag));
			if (detail == null)
			{
				detail = new KeyValuePair();
				detailList.Add(detail);
			}
			detail.tag = tag;
			detail.data = data;
			details = detailList.ToArray();
		}
	}
}
