using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class LibraryFileLink : LibraryObjectLink
	{
		#region Persistent Properties
		public Guid DataSourceId { get; set; }

		private bool _isDead;
		public bool IsDead
		{
			get => _isDead;
			set
			{
				if (_isDead != value)
					MarkAsModified();
				_isDead = value;
			}
		}

		public string OneDriveEncoded { get; set; }

		public virtual LibraryFolderLink FolderLink { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent => (IChangable)FolderLink ?? Folder;

		[NotMapped, JsonIgnore]
		public override Library ParentLibrary => FolderLink != null ? FolderLink.ParentLibrary : base.ParentLibrary;

		[NotMapped, JsonIgnore]
		public override LibraryPage ParentPage => FolderLink != null ? FolderLink.ParentPage : base.ParentPage;

		[NotMapped, JsonIgnore]
		public override LibraryFolder ParentFolder => FolderLink != null ? FolderLink.ParentFolder : base.ParentFolder;

		[NotMapped, JsonIgnore]
		public override BaseLibraryLink TopLevelLink => FolderLink ?? base.TopLevelLink;

		private LibraryFileLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<LibraryFileLinkSettings>(this, SettingsEncoded));
			set => _settings = value as LibraryFileLinkSettings;
		}

		[NotMapped, JsonIgnore]
		public override LinkFileType SubType
		{
			get
			{
				var subTypeKey = Extension.Replace(".", String.Empty).ToLower();
				switch (subTypeKey)
				{
					case FileTypes.Ppt:
						return LinkFileType.Ppt;
					case FileTypes.Pptx:
						return LinkFileType.Pptx;
					case FileTypes.Pps:
						return LinkFileType.Pps;
					case FileTypes.Pdf:
						return LinkFileType.Pdf;
					case FileTypes.Doc:
						return LinkFileType.Doc;
					case FileTypes.Docx:
						return LinkFileType.Docx;
					case FileTypes.Xls:
						return LinkFileType.Xls;
					case FileTypes.Xlsx:
						return LinkFileType.Xlsx;
					case FileTypes.Txt:
						return LinkFileType.Txt;
					case FileTypes.Jpg:
						return LinkFileType.Jpg;
					case FileTypes.Jpeg:
						return LinkFileType.Jpeg;
					case FileTypes.Png:
						return LinkFileType.Png;
					case FileTypes.Svg:
						return LinkFileType.Svg;
					case FileTypes.Mp4:
						return LinkFileType.Mp4;
					case FileTypes.M4v:
						return LinkFileType.M4v;
					case FileTypes.Mov:
						return LinkFileType.Mov;
					case FileTypes.Wmv:
						return LinkFileType.Wmv;
					case FileTypes.Key:
						return LinkFileType.Key;
					case FileTypes.Mp3:
						return LinkFileType.Mp3;
					case FileTypes.Xml:
						return LinkFileType.Xml;
					case FileTypes.Eps:
						return LinkFileType.Eps;
					case FileTypes.Ait:
						return LinkFileType.Ait;
					case FileTypes.Ai:
						return LinkFileType.Ai;
					case FileTypes.Psd:
						return LinkFileType.Psd;
					case FileTypes.Pdd:
						return LinkFileType.Pdd;
					case FileTypes.Aep:
						return LinkFileType.Aep;
					case FileTypes.Aet:
						return LinkFileType.Aet;
					case FileTypes.Zip:
						return LinkFileType.Zip;
					case FileTypes.SevenZip:
						return LinkFileType.SevenZip;
					case FileTypes.Rar:
						return LinkFileType.Rar;
					default:
						return LinkFileType.Other;
				}
			}
		}

		[NotMapped, JsonIgnore]
		public override string FullPath
		{
			get
			{
				var dataSourcePath = ParentLibrary.GetDataSources()
					.Where(ds => ds.DataSourceId.Equals(DataSourceId))
					.Select(ds => ds.GetRootPath()).FirstOrDefault();
				return String.IsNullOrEmpty(dataSourcePath) ?
					null :
					Path.Combine(dataSourcePath, RelativePath);
			}
		}

		[NotMapped, JsonIgnore]
		public override string WebPath => String.Format("{1}{0}", RelativePath, Path.DirectorySeparatorChar);

		[NotMapped, JsonIgnore]
		public override string LinkInfoDisplayName => Settings.TextWordWrap ? NameWithExtension : Name;

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
					lines.Add(String.Format("{0}: {1}", HintTitle, NameWithExtension));
					if (!String.Equals(NameWithExtension, RelativePath, StringComparison.OrdinalIgnoreCase))
						lines.Add(WebPath?.Replace(NameWithExtension, String.Empty));
					lines.Add(base.Hint);
				}
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public override Color DisplayColor
		{
			get
			{
				if (Banner.Enable && Banner.TextEnabled)
					return Banner.ForeColor;
				if (Thumbnail.Enable && Thumbnail.TextEnabled)
					return Thumbnail.ForeColor;
				if (Settings.ForeColor.HasValue)
					return Settings.ForeColor.Value;
				return ParentFolder.Settings.ForeWindowColor;
			}
		}

		[NotMapped, JsonIgnore]
		public virtual string HintTitle => "File";

		[NotMapped, JsonIgnore]
		public string NameWithExtension => Path.GetFileName(FullPath);

		[NotMapped, JsonIgnore]
		public string NameWithoutExtension => Path.GetFileNameWithoutExtension(FullPath);

		[NotMapped, JsonIgnore]
		public string Extension => Path.GetExtension(FullPath);

		[NotMapped, JsonIgnore]
		public string LocationPath => Path.GetDirectoryName(FullPath);

		[NotMapped, JsonIgnore]
		public string RootPath => ParentLibrary.Path;

		[NotMapped, JsonIgnore]
		public virtual bool IsFolder => false;

		[NotMapped, JsonIgnore]
		public override string AutoWidgetKey => Extension.Replace(".", String.Empty);
		#endregion

		public override string ToString()
		{
			return NameWithExtension;
		}

		public override void DeleteLink()
		{
			if (this.IsLinkExternal())
				this.DeleteExternalLink();
			FolderLink?.Links.RemoveItem(this);
			base.DeleteLink();
		}

		public override void ResetParent()
		{
			FolderLink = null;
			base.ResetParent();
		}

		public virtual bool CheckIfDead()
		{
			return !File.Exists(FullPath);
		}

		public override void ApplyValues(BaseLibraryLink link)
		{
			IsDead = ((LibraryFileLink)link).IsDead;
			DataSourceId = ((LibraryFileLink)link).DataSourceId;
			OneDriveEncoded = ((LibraryFileLink)link).OneDriveEncoded;
			base.ApplyValues(link);
		}

		public override BaseLibraryLink Copy()
		{
			var link = (LibraryFileLink)base.Copy();
			link.IsDead = IsDead;
			link.DataSourceId = DataSourceId;
			link.OneDriveEncoded = OneDriveEncoded;

			if (this.IsLinkExternal() && FolderLink == null)
			{
				var destinationPath = ExternalLinksHelper.CopyExternalFiles(FullPath, ParentLibrary.Path, link.ExtId);
				var relativePath = destinationPath.Replace(ParentLibrary.Path, String.Empty);
				relativePath = relativePath.StartsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture))
					? relativePath.Substring(1)
					: relativePath;
				link.RelativePath = relativePath;
			}

			return link;
		}

		public static LibraryFileLink Create(SourceLink sourceLink, LibraryFolder parentFolder)
		{
			var link = Create(sourceLink, parentFolder.Page.Library);
			link.Folder = parentFolder;
			link.AfterCreate();
			return link;
		}

		public static LibraryFileLink Create(SourceLink sourceLink, LibraryFolderLink parentFolderLink)
		{
			var link = Create(sourceLink, parentFolderLink.ParentLibrary);
			link.FolderLink = parentFolderLink;
			link.AfterCreate();
			return link;
		}

		private static LibraryFileLink Create(SourceLink sourceLink, Library parentLibrary)
		{
			LibraryFileLink link;
			if (sourceLink is FileLink)
				link = Create(sourceLink.Path);
			else
				link = CreateEntity<LibraryFolderLink>();
			link.Name = sourceLink.NameWithoutExtension;
			link.DataSourceId = sourceLink.RootId;

			var dataSourcePath = parentLibrary.GetDataSources().Where(ds => ds.DataSourceId == sourceLink.RootId).Select(ds => ds.Path).FirstOrDefault();

			if (dataSourcePath != null)
			{
				var sourcePath = sourceLink.Path;
				if (sourceLink.IsExternal)
					if (sourceLink is FileLink)
						sourcePath = ExternalLinksHelper.CopyExternalFile(sourceLink.Path, dataSourcePath, link.ExtId);
					else
						sourcePath = ExternalLinksHelper.CopyExternalFolder(sourceLink.Path, dataSourcePath, link.ExtId, ((FolderLink)sourceLink).SelectedFiles.Select(fileInfo => fileInfo.FullName).ToList());
				var relativePath = sourcePath.Replace(dataSourcePath, String.Empty);
				relativePath = relativePath.StartsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)) ? relativePath.Substring(1) : relativePath;
				link.RelativePath = relativePath;
			}
			return link;
		}

		public static LibraryFileLink Create(string filePath)
		{
			if (FileFormatHelper.IsPowerPointFile(filePath))
				return CreateEntity<PowerPointLink>();
			if (FileFormatHelper.IsWordFile(filePath))
				return CreateEntity<WordLink>();
			if (FileFormatHelper.IsPdfFile(filePath))
				return CreateEntity<PdfLink>();
			if (FileFormatHelper.IsExcelFile(filePath))
				return CreateEntity<ExcelLink>();
			if (FileFormatHelper.IsVideoFile(filePath))
				return CreateEntity<VideoLink>();
			if (FileFormatHelper.IsPngFile(filePath) ||
				FileFormatHelper.IsJpegFile(filePath) ||
				FileFormatHelper.IsGifFile(filePath))
				return CreateEntity<ImageLink>();
			return CreateEntity<CommonFileLink>();
		}

		public void ApplyOriginalFileStateChangesOnAssociatedLink()
		{
			if (IsFolder) return;
			if (!File.Exists(FullPath))
			{
				IsDead = true;
				return;
			}
			var currentDate = new FileInfo(FullPath).LastWriteTime;
			var savedDate = ((LibraryFileLinkSettings)Settings).FileDate;
			if (savedDate.HasValue && savedDate < currentDate)
				MarkAsModified();
			((LibraryFileLinkSettings)Settings).FileDate = currentDate;
			if (!savedDate.HasValue && AddDate < currentDate)
				MarkAsModified();
		}
	}
}
