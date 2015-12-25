using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Mappers.Wallbin;
using SalesLibraries.Business.Schema.Wallbin;
using Constants = SalesLibraries.Common.Configuration.Constants;
using Library = SalesLibraries.Business.Entities.Wallbin.Persistent.Library;

namespace SalesLibraries.Business.Contexts.Wallbin
{
	public class LibraryContext : SqLiteContext
	{
		public string LibraryName { get; private set; }
		public string DataSourcePath { get; private set; }

		public DbSet<Library> Libraries { get; set; }
		public DbSet<DBVersion> Versions { get; set; }

		public Library Library
		{
			get { return Libraries.Single(); }
		}

		public LibraryContext(string libraryName, string libraryPath)
			: base(Path.Combine(libraryPath, Constants.StorageFileName))
		{
			LibraryName = libraryName;
			DataSourcePath = libraryPath;
			Database.SetInitializer(new WallbinInitializer());
			Database.Initialize(true);
			ObjectContext.ObjectMaterialized += OnObjectMaterialized;
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
				return base.SaveChanges();
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

		private void OnObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
		{
			if (e.Entity is Library)
			{
				var library = ((Library)e.Entity);
				library.Context = this;
				library.Name = LibraryName;
				library.Path = DataSourcePath;
				library.Pages.Sort();
			}
		}
	}
}
