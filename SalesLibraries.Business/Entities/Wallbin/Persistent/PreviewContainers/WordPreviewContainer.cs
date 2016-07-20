using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class WordPreviewContainer : DocumentPreviewContainer
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		protected override IEnumerable<string> BasePreviewFormats => new[]
		{
			PreviewFormats.Word,
			PreviewFormats.Pdf
		};

		#endregion
	}
}
