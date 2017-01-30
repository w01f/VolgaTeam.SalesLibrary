using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class LibraryFolderLink : LibraryFileLink
	{
		#region Persistent Properties
		[InverseProperty("FolderLink")]
		public virtual ICollection<LibraryFileLink> Links { get; set; }
		#endregion

		#region Nonpersistent Properties
		private LibraryFolderLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LibraryFolderLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as LibraryFolderLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.Folder;

		[NotMapped, JsonIgnore]
		public IEnumerable<LibraryFileLink> AllLinks
		{
			get { return Links.Union(Links.OfType<LibraryFolderLink>().SelectMany(lf => lf.AllLinks)); }
		}

		[NotMapped, JsonIgnore]
		public override string HintTitle => "Folder";

		[NotMapped, JsonIgnore]
		public override bool IsFolder => true;

		[NotMapped, JsonIgnore]
		public override string AutoWidgetKey => "folder_closed";
		#endregion

		public LibraryFolderLink()
		{
			Type = FileTypes.Folder;
			Links = new List<LibraryFileLink>();
		}

		public override void BeforeSave()
		{
			if (NeedToSave)
			{
				foreach (var link in Links)
					link.BeforeSave();
			}
			base.BeforeSave();
		}

		public override void Save(LibraryContext context, IDbEntity<LibraryContext> current, bool withCommit = true)
		{
			var currentFolderLink = (LibraryFolderLink)current;
			Links.Save(currentFolderLink.Links, context);
			base.Save(context, current, withCommit);
		}

		public override void Delete(LibraryContext context)
		{
			foreach (var link in Links.ToList())
				link.Delete(context);
			Links.Clear();
			base.Delete(context);
		}

		protected override void AfterCreate()
		{
			UpdateContent();
			base.AfterCreate();
		}

		public override bool CheckIfDead()
		{
			return !Directory.Exists(FullPath);
		}

		public override BaseLibraryLink Copy()
		{
			var folderLink = (LibraryFolderLink)base.Copy();

			foreach (var libraryLink in Links.OfType<LibraryFileLink>())
			{
				var newLink = (LibraryFileLink)libraryLink.Copy();
				newLink.FolderLink = folderLink;
				folderLink.Links.Add(newLink);
			}

			return folderLink;
		}

		public void UpdateContent()
		{
			var existedPaths = new List<string>();
			if (Directory.Exists(FullPath))
			{
				var newLinks = new List<LibraryFileLink>();
				foreach (var folderPath in Directory.GetDirectories(FullPath))
				{
					existedPaths.Add(folderPath);
					if (Links.Any(link => link.FullPath.ToLower().Equals(folderPath.ToLower()))) continue;
					var folderLink = (LibraryFolderLink)Create(new FolderLink { RootId = DataSourceId, Path = folderPath }, this);
					Links.AddItem(folderLink);
					newLinks.Add(folderLink);
				}
				foreach (var filePath in Directory.GetFiles(FullPath)
					.Where(filePath => filePath.ToUpper().Contains(Constants.ExternalFilesRootFolderName.ToUpper()) || 
						GlobalSettings.HiddenObjects.All(item => !filePath.ToUpper().Contains(item.ToUpper()))))
				{
					existedPaths.Add(filePath);
					if (Links.Any(link => link.FullPath.ToLower().Equals(filePath.ToLower()))) continue;
					var fileLink = Create(new FileLink { RootId = DataSourceId, Path = filePath }, this);
					Links.AddItem(fileLink);
					newLinks.Add(fileLink);
				}

				((LibraryFolderLinkSettings)Settings).ApplyUniverslaLinkSettings(newLinks);
			}
			var linksToRemove = Links.Where(link => !existedPaths.Any(path => path.ToLower().Equals(link.FullPath.ToLower()))).ToList();
			foreach (var link in linksToRemove)
			{
				Links.RemoveItem(link);
				link.Delete(ParentLibrary.Context);
			}
		}
	}
}
