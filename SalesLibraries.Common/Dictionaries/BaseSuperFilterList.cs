using System.Collections.Generic;

namespace SalesLibraries.Common.Dictionaries
{
	public abstract class BaseSuperFilterList
	{
		public List<string> Items { get; private set; }

		protected BaseSuperFilterList()
		{
			Items = new List<string>();
		}

		public abstract void Load();
	}
}
