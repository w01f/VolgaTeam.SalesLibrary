using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class Html5PreviewContainer : HyperlinkPreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public string ThumnailUrl => SourcePath.Replace("index.html", "protected.html");
		#endregion
	}
}
