using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class InternalLibraryObjectLink : InternalLink, IThumbnailSettingsHolder
	{
		#region Nonpersistent Properties
		private InternalLibraryObjectLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<InternalLibraryObjectLinkSettings>(this, SettingsEncoded));
			set => _settings = value as InternalLibraryObjectLinkSettings;
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.InternalLibraryLink;

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				if (!((LibraryObjectLinkSettings)Settings).ShowOnlyCustomHoverNote)
				{
					lines.Add(String.Format("Target Library: {0}", TargetLibrary));
					lines.Add(String.Format("Target Page: {0}", TargetPage));
					lines.Add(String.Format("Target Window: {0}", TargetFolder));
					lines.Add(String.Format("Target Link: {0}", TargetLink));
					lines.Add(base.Hint);
				}
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string TargetLibrary => ((InternalLibraryObjectLinkSettings)Settings).LibraryName;

		[NotMapped, JsonIgnore]
		public string TargetPage => ((InternalLibraryObjectLinkSettings)Settings).PageName;

		[NotMapped, JsonIgnore]
		public string TargetFolder => ((InternalLibraryObjectLinkSettings)Settings).WindowName;

		[NotMapped, JsonIgnore]
		public string TargetLink => ((InternalLibraryObjectLinkSettings)Settings).LinkName;

		[NotMapped, JsonIgnore]
		public override string AutoWidgetKey => "internal link";

		[NotMapped, JsonIgnore]
		public Color ThumbnailBackColor => ParentFolder.Settings.BackgroundWindowColor;

		[NotMapped, JsonIgnore]
		public bool ShowSourceFilesList => true;
		#endregion

		public static InternalLibraryObjectLink Create(InternalLibraryObjectLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = CreateEntity<InternalLibraryObjectLink>();
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((InternalLibraryObjectLinkSettings)link.Settings).LibraryName = linkInfo.LibraryName;
			((InternalLibraryObjectLinkSettings)link.Settings).PageName = linkInfo.PageName;
			((InternalLibraryObjectLinkSettings)link.Settings).WindowName = linkInfo.WindowName;
			((InternalLibraryObjectLinkSettings)link.Settings).LinkName = linkInfo.LinkName;
			((InternalLibraryObjectLinkSettings)link.Settings).ThumbnailUrls = linkInfo.ThumbnailUrls.ToArray();
			if (linkInfo.FormatAsBluelink)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Underline;
				link.Settings.ForeColor = Color.Blue;
			}
			if (linkInfo.FormatBold)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Bold;
			}
			link.AfterCreate();
			return link;
		}

		public IList<string> GetThumbnailSourceUrl()
		{
			var internalLibraryObjectSettings = (InternalLibraryObjectLinkSettings)Settings;
			return internalLibraryObjectSettings.ThumbnailUrls;
		}

		public IList<string> GetThumbnailSourceFiles(string sessionKey)
		{
			var previewFiles = new List<string>();

			var tempFolderName = $"{sessionKey}_{ExtId.ToString()}_thumbnails";
			var tempPath = Path.Combine(Path.GetTempPath(), tempFolderName);
			if (Directory.Exists(tempPath))
				previewFiles.AddRange(Directory.GetFiles(tempPath, "*.png").ToList());
			else
			{
				Directory.CreateDirectory(tempPath);

				var thumbnailUrls = GetThumbnailSourceUrl();
				foreach (var thumbnailUrl in thumbnailUrls)
				{
					var uri = new Uri(thumbnailUrl);
					var fileName = uri.Segments[uri.Segments.Length - 1];

					var request = System.Net.WebRequest.Create(thumbnailUrl);
					var response = request.GetResponse();
					var responseStream = response.GetResponseStream();
					if (responseStream != null)
					{
						var fileStream = File.Create(Path.Combine(tempPath, fileName));
						responseStream.CopyTo(fileStream);
						fileStream.Close();
						fileStream.Dispose();
						responseStream.Dispose();
					}
				}
				previewFiles.AddRange(Directory.GetFiles(tempPath, "*.png").ToList());
			}
			previewFiles.Sort(WinAPIHelper.StrCmpLogicalW);
			return previewFiles;
		}
	}
}
