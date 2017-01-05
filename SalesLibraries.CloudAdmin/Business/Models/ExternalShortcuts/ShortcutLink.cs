using System;

namespace SalesLibraries.CloudAdmin.Business.Models.ExternalShortcuts
{
	class ShortcutLink
	{
		public string Id { get; set; }
		public int Order { get; set; }
		public string Title { get; set; }
		public int GroupOrder { get; set; }
		public string GroupFolder { get; set; }
		public string LinkFolder { get; set; }

		public override string ToString()
		{
			return LinkFolder ?? base.ToString();
		}
	}
}
