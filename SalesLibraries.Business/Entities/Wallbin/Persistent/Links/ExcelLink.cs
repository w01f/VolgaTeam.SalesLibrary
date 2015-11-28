using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class ExcelLink : PreviewableLink
	{
		#region Nonpersistent Properties
		private ExcelLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<ExcelLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as ExcelLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get { return WebFormats.Excel; }
		}
		#endregion

		public ExcelLink()
		{
			Type = FileTypes.Excel;
		}
	}
}
