using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
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

		protected override void AfterCreate()
		{
			base.AfterCreate();

			var settingsTemplate = ParentFolder.Settings.GetSettingsTemplate<DocumentLinkSettings>(
				LinkSettingsGroupType.AdminSettings,
				Type);

			if (settingsTemplate != null)
			{
				((DocumentLinkSettings)Settings).GeneratePreviewImages = settingsTemplate.GeneratePreviewImages;
				((DocumentLinkSettings)Settings).GenerateContentText = settingsTemplate.GenerateContentText;
				((DocumentLinkSettings)Settings).ForcePreview = settingsTemplate.ForcePreview;
				((DocumentLinkSettings)Settings).IsArchiveResource = settingsTemplate.IsArchiveResource;
			}
		}
	}
}
