using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class DocumentLink : PreviewableLink, IThumbnailSettingsHolder
	{
		#region Nonpersistent Properties
		private DocumentLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<DocumentLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as DocumentLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public Color ThumbnailBackColor => Folder.Settings.BackgroundWindowColor;

		[NotMapped, JsonIgnore]
		public bool ShowSourceFilesList => true;
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

		public IList<string> GetThumbnailSourceFiles()
		{
			var previewFiles = new List<string>();
			var sourceFilesPath = Path.Combine(PreviewContainerPath, PreviewFormats.Png);
			if (Directory.Exists(sourceFilesPath))
				previewFiles.AddRange(Directory.GetFiles(sourceFilesPath));
			previewFiles.Sort(WinAPIHelper.StrCmpLogicalW);
			return previewFiles;
		}
	}
}
