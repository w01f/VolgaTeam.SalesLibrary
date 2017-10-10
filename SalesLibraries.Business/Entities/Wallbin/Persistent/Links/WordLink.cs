using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class WordLink : DocumentLink
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.Word;

		#endregion

		public WordLink()
		{
			Type = LinkType.Word;
		}
	}
}
