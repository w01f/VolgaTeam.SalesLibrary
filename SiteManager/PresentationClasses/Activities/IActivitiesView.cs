using System;
using System.Windows.Forms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	public interface IActivitiesView
	{
		DateTime StartDate { get; set; }
		DateTime EndDate { get; set; }
		bool Active { get; set; }
		Control FilterControl { get; }
		void ShowView();
		void UpdateData(bool showMessages, ref string updateMessage);
		void ClearData();
	}

	public enum ViewType
	{
		RawData = 0,
		MainUserReport = 1,
		MainGroupReport = 2,
		NavigationUserReport = 3,
		NavigationGroupReport = 4,
		Report5 = 5,
		Report6 = 6,
		None = 999
	}
}
