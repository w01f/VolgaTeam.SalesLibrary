using System;
using System.Drawing;
using System.Linq;

namespace SalesLibraries.Common.Extensions
{
	public static class CommonExtensions
	{
		public static string GetName(this WebDAVClient.Model.Item item)
		{
			return item.Href.Split(@"/"[0]).LastOrDefault(part => !String.IsNullOrEmpty(part));
		}

		public static string ToHex(this Color target)
		{
			return String.Format("#{0}{1}{2}",
				target.R.ToString("X2"),
				target.G.ToString("X2"),
				target.B.ToString("X2"));
		}
	}
}
