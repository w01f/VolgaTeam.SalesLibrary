using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class VimeoPreviewContainer : HyperlinkPreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public string VimeoInfoUrl
		{
			get
			{
				var regEx = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)");
				var match = regEx.Match(SourcePath);
				if (match.Success && match.Groups.Count > 1)
					return String.Format("http://vimeo.com/api/v2/video/{0}.json", match.Groups[1].Value);
				return null;
			}
		}
		#endregion
	}
}
