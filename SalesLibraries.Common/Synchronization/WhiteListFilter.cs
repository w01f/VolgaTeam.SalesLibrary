using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalesLibraries.Common.Synchronization
{
	class WhiteListFilter : SyncFilterList
	{
		private HashSet<string> _filePaths;
		private HashSet<string> _folderPaths;

		public WhiteListFilter(IEnumerable<string> items, SyncFilterType filterType) : base(items, filterType) { }

		protected override void Init()
		{
			_filePaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			_folderPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (var filePath in _sourceItems)
			{
				_filePaths.Add(filePath);
				var directory = Path.GetDirectoryName(filePath);
				if (!String.IsNullOrEmpty(directory) && !_folderPaths.Contains(directory))
					_folderPaths.Add(directory);
			}
		}

		public override bool IsMatchFile(string targetItem)
		{
			return _filePaths.Contains(targetItem);
		}

		public override bool IsMatchDirectory(string targetItem)
		{
			return _folderPaths.Any(item => item.Contains(targetItem));
		}
	}
}
