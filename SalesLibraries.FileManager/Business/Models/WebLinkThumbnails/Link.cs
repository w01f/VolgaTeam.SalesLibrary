using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SalesLibraries.FileManager.Business.Models.WebLinkThumbnails
{
	class Link
	{
		private readonly Regex _linkTemplate = new Regex(@"^http(s?):\/\/((w){3}.)?");

		public string Url { get; set; }
		public string Description { get; set; }
		public bool Checked { get; set; }
		public bool Loaded { get; set; }

		public string ImageName
		{
			get
			{
				var list = Path.GetInvalidFileNameChars();
				return list.Aggregate(Description, (current, t) => current.Replace(t, '_'));
			}
		}

		public bool IsUrlEqual(string url)
		{
			return _linkTemplate.Replace(Url, String.Empty).Equals(_linkTemplate.Replace(url, String.Empty));
		}
	}
}
