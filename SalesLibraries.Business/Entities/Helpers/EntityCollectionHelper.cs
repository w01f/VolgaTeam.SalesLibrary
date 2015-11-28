using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.Business.Entities.Helpers
{
	static class EntityCollectionHelper
	{
		public static void Save<TContext, TEntity>(this ICollection<TEntity> originalCollection, ICollection<TEntity> currentCollection, TContext context)
			where TContext : DbContext
			where TEntity : BaseEntity<TContext>, IExtKeyHolder, IChangable, ICollectionItem
		{
			foreach (var originalItem in originalCollection.ToList())
			{
				var currentItem = currentCollection.FirstOrDefault(item => item.CompareByKey(originalItem));
				if (currentItem == null)
				{
					originalItem.Delete(context);
					originalCollection.Remove(originalItem);
				}
				else if (originalItem.IsModified(currentItem))
					originalItem.Save(context, currentItem, false);
			}
			foreach (var newItem in currentCollection.Where(currentItem => originalCollection.All(originalItem => !originalItem.CompareByKey(currentItem))).ToList())
			{
				newItem.ResetParent();
				originalCollection.Add(newItem);
				newItem.Add(context);
			}
		}
	}
}
