﻿using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(ExcelLink))]
	class ExcelLinkProcessor : DocumentLinkProcessor
	{
		public ExcelLinkProcessor(LibraryFileLink fileLink) : base(fileLink) { }
		protected override LinkLaunchOptionsEnum GetLaunchOptions()
		{
			return MainController.Instance.Settings.LinkLaunchSettings.Excel;
		}
	}
}
