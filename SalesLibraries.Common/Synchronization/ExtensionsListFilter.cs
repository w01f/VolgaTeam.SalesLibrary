using System;
using System.Collections.Generic;
using System.IO;

namespace SalesLibraries.Common.Synchronization
{
	class ExtensionsListFilter : SyncFilterList
	{
		private HashSet<string> _extensions;

		public ExtensionsListFilter(IEnumerable<string> items, SyncFilterType filterType) : base(items, filterType) { }

		protected override void Init()
		{
			_extensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (var extension in _sourceItems)
				_extensions.Add(extension);
		}

		public override bool IsMatchFile(string targetItem)
		{
			var extension = Path.GetExtension(targetItem);
			return !String.IsNullOrEmpty(extension) && _extensions.Contains(extension);
		}

		public override bool IsMatchDirectory(string targetItem)
		{
			return true;
		}
	}
}
