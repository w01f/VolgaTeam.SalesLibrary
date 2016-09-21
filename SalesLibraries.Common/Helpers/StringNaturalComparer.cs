using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SalesLibraries.Common.Helpers
{
	public class StringNaturalComparer : IComparer<string>, IDisposable
	{
		private Dictionary<string, string[]> _table = new Dictionary<string, string[]>();
		private readonly bool _isAscending;

		public StringNaturalComparer(bool inAscendingOrder = true)
		{
			_isAscending = inAscendingOrder;
		}

		#region IComparer<string> Members

		public int Compare(string x, string y)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IComparer<string> Members

		int IComparer<string>.Compare(string x, string y)
		{
			if (x == y)
				return 0;

			string[] x1, y1;

			if (!_table.TryGetValue(x, out x1))
			{
				x1 = Regex.Split(x.Replace(" ", ""), "([0-9]+)");
				_table.Add(x, x1);
			}

			if (!_table.TryGetValue(y, out y1))
			{
				y1 = Regex.Split(y.Replace(" ", ""), "([0-9]+)");
				_table.Add(y, y1);
			}

			int returnVal;

			for (var i = 0; i < x1.Length && i < y1.Length; i++)
			{
				if (x1[i] != y1[i])
				{
					returnVal = PartCompare(x1[i], y1[i]);
					return _isAscending ? returnVal : -returnVal;
				}
			}

			if (y1.Length > x1.Length)
			{
				returnVal = 1;
			}
			else if (x1.Length > y1.Length)
			{
				returnVal = -1;
			}
			else
			{
				returnVal = 0;
			}

			return _isAscending ? returnVal : -returnVal;
		}

		private static int PartCompare(string left, string right)
		{
			int x, y;
			if (!int.TryParse(left, out x))
				return String.Compare(left, right, StringComparison.Ordinal);

			return !int.TryParse(right, out y) ? String.Compare(left, right, StringComparison.Ordinal) : x.CompareTo(y);
		}

		#endregion

		public void Dispose()
		{
			_table.Clear();
			_table = null;
		}
	}
}
