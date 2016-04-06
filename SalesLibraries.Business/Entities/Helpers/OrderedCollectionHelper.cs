using System;
using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Interfaces;

namespace SalesLibraries.Business.Entities.Helpers
{
	public static class OrderedCollectionHelper
	{
		public static void UpItem<TItem>(this ICollection<TItem> targetCollection, TItem targetItem, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			if (targetCollection is List<TItem>)
				((List<TItem>)targetCollection).UpListItem(targetItem, filterCondition);
		}

		private static void UpListItem<TItem>(this IList<TItem> targetList, TItem targetItem, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			var filteredCollection = (filterCondition != null ? targetList.Where(filterCondition) : targetList).ToList();
			var nextItem = filteredCollection.Where(i => i.CollectionOrder < targetItem.CollectionOrder).OrderByDescending(i => i.CollectionOrder).FirstOrDefault();
			if (nextItem == null) return;
			nextItem.CollectionOrder++;
			targetItem.CollectionOrder--;
			targetList.Sort();
			targetItem.Parent.MarkAsModified();
		}

		public static void DownItem<TItem>(this ICollection<TItem> targetCollection, TItem targetItem, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			if (targetCollection is List<TItem>)
				((List<TItem>)targetCollection).DownListItem(targetItem, filterCondition);
		}

		private static void DownListItem<TItem>(this IList<TItem> targetList, TItem targetItem, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			var filteredCollection = (filterCondition != null ? targetList.Where(filterCondition) : targetList).ToList();
			var nextItem = filteredCollection.Where(i => i.CollectionOrder > targetItem.CollectionOrder).OrderBy(i => i.CollectionOrder).FirstOrDefault();
			if (nextItem == null) return;
			nextItem.CollectionOrder--;
			targetItem.CollectionOrder++;
			targetList.Sort();
			targetItem.Parent.MarkAsModified();
		}

		public static void ResetItemsOrder<TItem>(this ICollection<TItem> targetCollection, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			var filteredCollection = (filterCondition != null ? targetCollection.Where(filterCondition) : targetCollection).ToList();
			var order = 0;
			foreach (var item in filteredCollection.OrderBy(i => i.CollectionOrder))
			{
				item.CollectionOrder = order;
				order++;
			}
			targetCollection.Sort(filterCondition);
		}

		public static void Sort<TItem>(this ICollection<TItem> targetCollection, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			var filteredCollection = (filterCondition != null ? targetCollection.Where(filterCondition) : targetCollection).ToList();
			foreach (var item in filteredCollection)
				targetCollection.Remove(item);
			foreach (var item in filteredCollection.OrderBy(i => i.CollectionOrder))
				targetCollection.Add(item);
		}

		public static void AddItem<TItem>(this ICollection<TItem> targetCollection, TItem targetItem, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			if (targetCollection.Any())
				targetItem.CollectionOrder = targetCollection.Max(item => item.CollectionOrder) + 1;
			targetCollection.Add(targetItem);
			targetCollection.ResetItemsOrder(filterCondition);
			targetItem.Parent.MarkAsModified();
		}

		public static void InsertItem<TItem>(this IList<TItem> targetCollection, TItem targetItem, int position, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			for (var i = position; i < targetCollection.Count; i++)
				targetCollection[i].CollectionOrder += 1;
			targetItem.CollectionOrder = position;
			targetCollection.Insert(position, targetItem);
			targetCollection.ResetItemsOrder(filterCondition);
			targetItem.Parent.MarkAsModified();
		}

		public static void ChangeItemPosition<TItem>(this IList<TItem> targetCollection, TItem targetItem, int newPosition, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			for (var i = newPosition; i < targetCollection.Count; i++)
				targetCollection[i].CollectionOrder += 1;
			targetItem.CollectionOrder = newPosition;
			targetCollection.ResetItemsOrder(filterCondition);
			targetItem.Parent.MarkAsModified();
		}

		public static void RemoveItem<TItem>(this ICollection<TItem> targetCollection, TItem targetItem, Func<TItem, bool> filterCondition = null)
			where TItem : class, ICollectionItem
		{
			if (targetItem.Parent != null)
				targetItem.Parent.MarkAsModified();
			targetCollection.Remove(targetItem);
			targetCollection.ResetItemsOrder(filterCondition);
		}
	}
}
