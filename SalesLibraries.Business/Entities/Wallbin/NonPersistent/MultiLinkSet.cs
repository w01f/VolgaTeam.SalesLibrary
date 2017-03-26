using System;
using System.Collections.Generic;
using System.Linq;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent
{
	public class MultiLinkSet : ILinksGroup
	{
		public string Name => "All Files";
		public ILinkGroupSettingsContainer LinkGroupSettingsContainer { get; } = null;
		public IEnumerable<BaseLibraryLink> AllGroupLinks { get; }

		public MultiLinkSet(IEnumerable<BaseLibraryLink> links)
		{
			AllGroupLinks = links.ToList();
		}

		public string LinkGroupName => null;
	}
}
