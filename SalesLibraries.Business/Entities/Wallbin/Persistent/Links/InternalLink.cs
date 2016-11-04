using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class InternalLink : LibraryObjectLink
	{
		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override string FullPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string WebPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.InternalLink;

		[NotMapped, JsonIgnore]
		public override string LinkInfoDisplayName => Settings.TextWordWrap ? "Internal Link" : Name;
		#endregion

		protected InternalLink()
		{
			Type = FileTypes.InternalLink;
		}
	}
}
