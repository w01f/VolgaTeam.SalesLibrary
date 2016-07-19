using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Mappers.Wallbin;
using Library = SalesLibraries.Business.Entities.Wallbin.Persistent.Library;

namespace SalesLibraries.Business.Contexts.Wallbin
{
	public abstract class LibraryContext : SqLiteContext
	{
		public string LibraryName { get; }
		public string DataSourceFolderPath { get; }
		public string DataSourceFilePath { get; }

		public DbSet<Library> Libraries { get; set; }
		public DbSet<DBVersion> Versions { get; set; }

		public event EventHandler<EventArgs> BeforeSave;

		public Library Library => Libraries.Single();

		protected LibraryContext(string libraryName, string libraryPath, string libraryFileName)
			: base(Path.Combine(libraryPath, libraryFileName))
		{
			LibraryName = libraryName;
			DataSourceFolderPath = libraryPath;
			DataSourceFilePath = Path.Combine(libraryPath, libraryFileName);
		}

		public override int SaveChanges()
		{
			try
			{
				ChangeTracker.DetectChanges();
				foreach (var library in Libraries)
				{
					library.BeforeSave();
				}
				BeforeSave?.Invoke(this, EventArgs.Empty);
				var result = base.SaveChanges();
				Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "VACUUM;");
				return result;
			}
			catch (DbEntityValidationException e)
			{
				throw e;
			}
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new PreviewContainerMap());
			modelBuilder.Configurations.Add(new BaseLibraryLinkMap());
			modelBuilder.Configurations.Add(new LibraryFileLinkMap());
			base.OnModelCreating(modelBuilder);
		}

		protected void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
		{
			if (e.Entity is Library)
			{
				var library = ((Library)e.Entity);
				library.Context = this;
				library.Name = LibraryName;
				library.Path = DataSourceFolderPath;
				library.Pages.Sort();
			}
			if (e.Entity is WallbinEntity)
			{
				((WallbinEntity) e.Entity).AllowToHandleChanges = true;
			}
		}
	}
}
