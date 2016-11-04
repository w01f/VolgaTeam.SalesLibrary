using System;
using System.Drawing;

namespace SalesLibraries.Common.Helpers
{
	public static class ColorHelper
	{
		public static string ToHex(this Color target)
		{
			return String.Format("#{0}{1}{2}",
				target.R.ToString("X2"),
				target.G.ToString("X2"),
				target.B.ToString("X2"));
		}

		public static Color GetNegativeColor(this Color target)
		{
			var a = 1 - (0.299 * target.R + 0.587 * target.G + 0.114 * target.B) / 255;
			var d = a < 0.5 ? 0 : 255;
			return Color.FromArgb(d, d, d);
		}
	}
}
