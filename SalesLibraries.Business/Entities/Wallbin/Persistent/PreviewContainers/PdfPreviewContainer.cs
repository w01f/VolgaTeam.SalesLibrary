using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class PdfPreviewContainer : DocumentPreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		protected override IEnumerable<string> BasePreviewFormats
		{
			get { return new string[] { }; }
		}
		#endregion
	}
}
