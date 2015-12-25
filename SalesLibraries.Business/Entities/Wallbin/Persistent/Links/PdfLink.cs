using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class PdfLink : DocumentLink
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get { return WebFormats.Pdf; }
		}
		#endregion

		public PdfLink()
		{
			Type = FileTypes.Pdf;
		}
	}
}
