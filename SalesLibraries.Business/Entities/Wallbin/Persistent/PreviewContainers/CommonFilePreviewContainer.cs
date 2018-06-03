using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers
{
	public class CommonFilePreviewContainer : FilePreviewContainer
	{
		#region Nonpersistent Properties

		[NotMapped, JsonIgnore]
		protected override string PreviewSubFolder => DocumentSubFolderName;

		[NotMapped, JsonIgnore]
		public override string[] AvailablePreviewFormats => new string[] { };
		#endregion
	}
}
