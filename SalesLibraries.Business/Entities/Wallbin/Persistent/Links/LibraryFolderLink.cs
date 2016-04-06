using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
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
		[NotMapped, JsonIgnore]
		public IEnumerable<LibraryFileLink> AllLinks
		{
			get { return Links.Union(Links.OfType<LibraryFolderLink>().SelectMany(lf => lf.AllLinks)); }
		}

		[NotMapped, JsonIgnore]
		public override string HintTitle
		{
			get { return "Folder"; }
		}

		[NotMapped, JsonIgnore]
		public override bool IsFolder
		{
			get { return true; }
		}
		#endregion

		public LibraryFolderLink()
		{
			Type = FileTypes.Folder;
			Links = new List<LibraryFileLink>();
		}

		public override void BeforeSave()
		{
			foreach (var link in Links)
				link.BeforeSave();
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
				foreach (var folderPath in Directory.GetDirectories(FullPath))
				{
					existedPaths.Add(folderPath);
					if (Links.Any(link => link.FullPath.ToLower().Equals(folderPath.ToLower()))) continue;
					var folderLink = (LibraryFolderLink)Create(new FolderLink { RootId = DataSourceId, Path = folderPath }, this);
					Links.AddItem(folderLink);
				}
				foreach (var filePath in Directory.GetFiles(FullPath).Where(filePath => GlobalSettings.HiddenObjects.All(item => filePath.ToLower() != item.ToLower())))
				{
					existedPaths.Add(filePath);
					if (Links.Any(link => link.FullPath.ToLower().Equals(filePath.ToLower()))) continue;
					var fileLink = Create(new FileLink { RootId = DataSourceId, Path = filePath }, this);
					Links.AddItem(fileLink);
				}
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
