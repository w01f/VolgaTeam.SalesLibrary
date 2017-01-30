using System;
using System.Linq;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Common.Extensions
{
	public static class CommonExtensions
	{
		public static string GetName(this WebDAVClient.Model.Item item)
		{
			return item.Href.Split(@"/"[0]).LastOrDefault(part => !String.IsNullOrEmpty(part));
		}

		public static string ToFileSize(this long l)
		{
			return String.Format(new FileSizeFormatProvider(), "{0:fs}", l);
		}
	}
}
