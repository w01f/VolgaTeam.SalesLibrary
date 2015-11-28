using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class DocumentLink : PreviewableLink
	{
		#region Nonpersistent Properties
		private DocumentLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<DocumentLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as DocumentLinkSettings; }
		}
		#endregion
	}
}
