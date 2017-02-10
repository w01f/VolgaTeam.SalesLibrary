using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class PowerPointLink : PreviewableLink, IThumbnailSettingsHolder
	{
		#region Nonpersistent Properties
		private PowerPointLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<PowerPointLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as PowerPointLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.PowerPoint;

		[NotMapped, JsonIgnore]
		public override string Hint => String.Format("{0}{2}{1}",
			base.Hint,
			String.Format("Slide Size: {0} W = {1:#.##} H = {2:#.##}",
				((PowerPointLinkSettings)Settings).Orientation,
				((PowerPointLinkSettings)Settings).Width,
				((PowerPointLinkSettings)Settings).Height),
			Environment.NewLine);

		[NotMapped, JsonIgnore]
		public Color ThumbnailBackColor => Folder.Settings.BackgroundWindowColor;

		[NotMapped, JsonIgnore]
		public bool ShowSourceFilesList => true;
		#endregion

		public PowerPointLink()
		{
			Type = FileTypes.PowerPoint;
		}

		public override void Delete(LibraryContext context)
		{
			var powerPointSettings = (PowerPointLinkSettings)Settings;
			powerPointSettings.ClearQuickViewContent();
			base.Delete(context);
		}

		protected override void AfterCreate()
		{
			var powerPointSettings = (PowerPointLinkSettings)Settings;
			powerPointSettings.UpdateSizeInfo();
			base.AfterCreate();
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
