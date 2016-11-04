using System;
using System.Linq;

namespace SalesLibraries.Common.Extensions
{
	public static class CommonExtensions
	{
		public static string GetName(this WebDAVClient.Model.Item item)
		{
			return item.Href.Split(@"/"[0]).LastOrDefault(part => !String.IsNullOrEmpty(part));
		}
	}
}
