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
		void ExportData();
	}

	public enum ViewType
	{
		RawData = 0,
		MainUserReport = 1,
		MainGroupReport = 2,
		NavigationUserReport = 3,
		NavigationGroupReport = 4,
		AccessGroupReport = 5,
		AccessAllReport = 6,
		QuizPassUserReport = 7,
		QuizPassGroupReport = 8,
		QuizStatusUserReport = 9,
		None = 999
	}
}
