using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class WebLinkPreviewContainer : HyperlinkPreviewContainer
	{
		#region Nonpersistent Properties

		[NotMapped, JsonIgnore]
		public bool IsQuickSite => SourcePath.Contains("qpage");

		#endregion
	}
}
