using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;

namespace SalesLibraries.Business.Contexts
{
	public class SqLiteContext : DbContext
	{
		public ObjectContext ObjectContext
		{
			get { return ((IObjectContextAdapter)this).ObjectContext; }
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		public SqLiteContext(string dataSourcePath)
			: base(new SQLiteConnection(new SQLiteConnectionStringBuilder
			{
				DataSource = dataSourcePath.StartsWith(@"\\") ? String.Format(@"\\{0}", dataSourcePath) : dataSourcePath
			}.ConnectionString, true), true)
		{ }
	}
}
