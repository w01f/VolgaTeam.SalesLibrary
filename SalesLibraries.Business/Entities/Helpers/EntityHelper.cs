﻿using System;
using System.Data.Entity;
using Newtonsoft.Json;
using SalesLibraries.Common.JsonConverters;

namespace SalesLibraries.Business.Entities.Helpers
{
	public static class EntityHelper
	{
		public static TEntity Clone<TContext, TEntity>(this TEntity original)
			where TContext : DbContext
			where TEntity : BaseEntity<TContext>
		{
			var serializerSettings = new DefaultSerializeSettings();
			original.BeforeSave();
			var originalEncoded = JsonConvert.SerializeObject(original, Formatting.Indented, serializerSettings);
			var copy = JsonConvert.DeserializeObject<TEntity>(originalEncoded, serializerSettings);
			return copy;
		}

		public static bool PerformTransaction<TContext, TEntity>(this TEntity target, TContext context, Func<TEntity, bool> transtactionMethod, Action<Action> copyWrapMethod, Action<TContext, TEntity, TEntity> commitMethod)
			where TContext : DbContext
			where TEntity : BaseEntity<TContext>
		{
			var copyInnerMethod = new Func<TEntity>(target.Clone<TContext, TEntity>);

			TEntity targetCopy = null;
			if (copyWrapMethod != null)
				copyWrapMethod(() => { targetCopy = copyInnerMethod(); });
			else
				targetCopy = copyInnerMethod();

			if (!transtactionMethod(targetCopy)) return false;

			commitMethod(context, target, targetCopy);
			return true;
		}
	}
}