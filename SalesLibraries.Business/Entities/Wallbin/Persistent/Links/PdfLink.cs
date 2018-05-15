using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class PdfLink : DocumentLink
	{
		#region Nonpersistent Properties
		private PdfLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<PdfLinkSettings>(this, SettingsEncoded));
			set => _settings = value as PdfLinkSettings;
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.Pdf;
		#endregion

		public PdfLink()
		{
			Type = LinkType.Pdf;
		}

		protected override void AfterCreate()
		{
			var pdfLinkSettings = (PdfLinkSettings)Settings;
			pdfLinkSettings.CheckIfArchiveResouce();

			base.AfterCreate();
		}
	}
}
