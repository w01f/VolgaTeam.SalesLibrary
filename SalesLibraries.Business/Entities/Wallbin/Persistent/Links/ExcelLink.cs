using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class ExcelLink : PreviewableFileLink
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
		public override string WebFormat => WebFormats.Excel;

		#endregion

		public ExcelLink()
		{
			Type = FileTypes.Excel;
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();

			var settingsTemplate = ParentFolder.Settings.GetSettingsTemplate<ExcelLinkSettings>(
				LinkSettingsGroupType.AdminSettings,
				Type);

			if (settingsTemplate != null)
			{
				((ExcelLinkSettings)Settings).GenerateContentText = settingsTemplate.GenerateContentText;
				((ExcelLinkSettings)Settings).ForceDownload = settingsTemplate.ForceDownload;
				((ExcelLinkSettings)Settings).IsArchiveResource = settingsTemplate.IsArchiveResource;
			}
		}
	}
}
