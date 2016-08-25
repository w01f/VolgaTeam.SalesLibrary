using System;
using System.Collections.Generic;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.BusinessClasses;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.FileActivityData
{
	public class ContainerControl : BaseContainerControl
	{
		protected override bool ShowLibrary => true;
		public override IEnumerable<FileActivityReportModel> GetFileActivityReport(DateTime startDate, DateTime endDate, out String message)
		{
			return WebSiteManager.Instance.SelectedSite.GetFileActivityReport(StartDate, EndDate, out message);
		}
	}
}
