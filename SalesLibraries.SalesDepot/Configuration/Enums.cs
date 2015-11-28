using System;

namespace SalesLibraries.SalesDepot.Configuration
{
	public enum LinkLaunchOptionsEnum
	{
		Viewer = 0,
		Menu,
		Launch
	}

	[Flags]
	public enum EmailButtonsDisplayOptionsEnum
	{
		DisplayNone = 0x00,
		DisplayEmailBin = 0x02,
		DisplayQuickView = 0x04,
		DisplayViewOptions = 0x08
	}

	public enum BrowseType
	{
		Day = 0,
		Week,
		Month
	}
}
