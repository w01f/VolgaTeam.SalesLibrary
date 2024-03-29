﻿using System.Collections.Generic;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public interface ILinkSetSettingsEditControl : ILinkSettingsEditControl
	{
		void LoadData(IEnumerable<BaseLibraryLink> sourceLinks);
	}
}
