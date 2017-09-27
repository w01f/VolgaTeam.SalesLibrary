using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class YoutubePreviewContainer : HyperlinkPreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public string ThumnailUrl
		{
			get
			{
				var regEx = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");
				var match = regEx.Match(SourcePath);
				if (match.Success && match.Groups.Count > 1)
					return String.Format("https://img.youtube.com/vi/{0}/0.jpg", match.Groups[1].Value);
				return null;
			}
		}
		#endregion
	}
}
