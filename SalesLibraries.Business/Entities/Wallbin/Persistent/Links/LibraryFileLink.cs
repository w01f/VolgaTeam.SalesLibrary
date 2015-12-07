using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
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
			get { return _isDead; }
			set
			{
				if (_isDead != value)
					MarkAsModified();
				_isDead = value;
			}
		}
		public virtual LibraryFolderLink FolderLink { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return (IChangable)FolderLink ?? Folder; }
		}

		[NotMapped, JsonIgnore]
		public override Library ParentLibrary
		{
			get { return FolderLink != null ? FolderLink.ParentLibrary : base.ParentLibrary; }
		}

		[NotMapped, JsonIgnore]
		public override LibraryPage ParentPage
		{
			get { return FolderLink != null ? FolderLink.ParentPage : base.ParentPage; }
		}

		[NotMapped, JsonIgnore]
		public override LibraryFolder ParentFolder
		{
			get { return FolderLink != null ? FolderLink.ParentFolder : base.ParentFolder; }
		}

		[NotMapped, JsonIgnore]
		public override BaseLibraryLink TopLevelLink
		{
			get { return FolderLink ?? base.TopLevelLink; }
		}

		private LibraryFileLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LibraryFileLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as LibraryFileLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string FullPath
		{
			get
			{
				var dataSourcePath = ParentLibrary.GetDataSources()
					.Where(ds => ds.DataSourceId.Equals(DataSourceId))
					.Select(ds => ds.GetFilePath()).FirstOrDefault();
				return String.IsNullOrEmpty(dataSourcePath) ?
					null :
					Path.Combine(dataSourcePath, RelativePath);
			}
		}

		[NotMapped, JsonIgnore]
		public override string WebPath
		{
			get { return String.Format("{1}{0}", RelativePath, Path.DirectorySeparatorChar); }
		}

		[NotMapped, JsonIgnore]
		public override string DisplayNameWithoutNote
		{
			get
			{
				if (!IsDead || !ParentLibrary.InactiveLinksSettings.Enable)
					return base.DisplayNameWithoutNote;
				if (!ParentLibrary.InactiveLinksSettings.ShowBoldWarning)
					return ParentLibrary.InactiveLinksSettings.ReplaceInactiveLinksWithLineBreak ? String.Empty : base.DisplayNameWithoutNote;
				return String.Format("INACTIVE! {0}", base.DisplayNameWithoutNote);
			}
		}

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				lines.Add(String.Format("{0}: {1}", HintTitle, NameWithExtension));
				lines.Add(base.Hint);
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public virtual string HintTitle
		{
			get { return "File"; }
		}

		[NotMapped, JsonIgnore]
		public string NameWithExtension
		{
			get { return Path.GetFileName(FullPath); }
		}

		[NotMapped, JsonIgnore]
		public string NameWithoutExtension
		{
			get { return Path.GetFileNameWithoutExtension(FullPath); }
		}

		[NotMapped, JsonIgnore]
		public string Extension
		{
			get { return Path.GetExtension(FullPath); }
		}

		[NotMapped, JsonIgnore]
		public string LocationPath
		{
			get { return Path.GetDirectoryName(FullPath); }
		}

		[NotMapped, JsonIgnore]
		public string RootPath
		{
			get { return ParentLibrary.Path; }
		}

		[NotMapped, JsonIgnore]
		public virtual bool IsFolder
		{
			get { return false; }
		}
		#endregion

		public override string ToString()
		{
			return NameWithExtension;
		}

		public override void DeleteLink(bool fullDelete = false)
		{
			if (FolderLink != null)
				FolderLink.Links.RemoveItem(this);
			base.DeleteLink(fullDelete);
		}

		public override void ResetParent()
		{
			FolderLink = null;
			base.ResetParent();
		}

		protected virtual void AfterCreate() { }

		public virtual bool CheckIfDead()
		{
			return !File.Exists(FullPath);
		}

		public override void ApplyValues(BaseLibraryLink link)
		{
			IsDead = ((LibraryFileLink)link).IsDead;
			DataSourceId = ((LibraryFileLink)link).DataSourceId;
			base.ApplyValues(link);
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
				link = new LibraryFolderLink();
			link.Name = sourceLink.NameWithoutExtension;
			link.DataSourceId = sourceLink.RootId;
			var dataSourcePath = parentLibrary.GetDataSources().Where(ds => ds.DataSourceId == sourceLink.RootId).Select(ds => ds.Path).FirstOrDefault();
			if (dataSourcePath != null)
			{
				var relativePath = sourceLink.Path.Replace(dataSourcePath, String.Empty);
				relativePath = relativePath.StartsWith(Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)) ? relativePath.Substring(1) : relativePath;
				link.RelativePath = relativePath;
			}
			return link;
		}

		public static LibraryFileLink Create(string filePath)
		{
			if (FileFormatHelper.IsPowerPointFile(filePath))
				return new PowerPointLink();
			if (FileFormatHelper.IsWordFile(filePath))
				return new WordLink();
			if (FileFormatHelper.IsPdfFile(filePath))
				return new PdfLink();
			if (FileFormatHelper.IsExcelFile(filePath))
				return new ExcelLink();
			if (FileFormatHelper.IsVideoFile(filePath))
				return new VideoLink();
			return new CommonFileLink();
		}
	}
}
