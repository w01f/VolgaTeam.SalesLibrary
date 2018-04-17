using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace SalesLibraries.Business.Entities
{
	public abstract class BaseEntity<TContext> : IDbEntity<TContext> where TContext : DbContext
	{
		[Key]
		public Int64 Id { get; set; }

		public abstract void BeforeSave();

		public virtual void AfterSave() { }

		public virtual void AfterClone(BaseEntity<TContext> original) { }

		public virtual void Save(TContext context, IDbEntity<TContext> current, bool withCommit = true)
		{
			current.BeforeSave();
			var entityEntry = context.Entry(this);
			if (entityEntry.State == EntityState.Unchanged)
				entityEntry.State = EntityState.Modified;
			entityEntry.CurrentValues.SetValues(current);
			AfterSave();
			if (withCommit)
				context.SaveChanges();
		}

		public virtual void Delete(TContext context)
		{
			var entityEntry = context.Entry(this);
			switch (entityEntry.State)
			{
				case EntityState.Added:
					entityEntry.State = EntityState.Detached;
					break;
				case EntityState.Detached:
					break;
				default:
					entityEntry.State = EntityState.Deleted;
					break;
			}
		}

		public virtual void Add(TContext context)
		{
			context.Entry(this).State = EntityState.Added;
		}

		public abstract void ResetParent();
	}
}
