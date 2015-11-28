using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesLibraries.Common.Extensions
{
	public static class CollectionExtension
	{
		public static bool Compare<TCollection>(this IEnumerable<TCollection> source, IEnumerable<TCollection> target) where TCollection : IEquatable<TCollection>
		{
			return source != null &&
				target != null &&
				source.Count() == target.Count() &&
				source.All(sourceItem => target.Any(targetItem => targetItem.Equals(sourceItem)));
		}

		public static T[] Merge<T>(this T[] first, IEnumerable<T> second) where T : class
		{
			var mergedList = new List<T>(first);
			mergedList.AddRange(second);
			return mergedList.ToArray();
		}

		public static T[] Merge<T>(this T[] first, T second) where T : class
		{
			var mergedList = new List<T>(first) { second };
			return mergedList.ToArray();
		}

		public static T[] ExtractPlainCollection<T>(this object[] target) where T : class
		{
			var plain = new List<T>();
			foreach (var item in target)
			{
				if (item is T)
					plain.Add((T)item);
				else if (item.GetType().IsArray)
					plain.AddRange(((object[])item).ExtractPlainCollection<T>());
			}
			return plain.ToArray();
		}
	}
}
