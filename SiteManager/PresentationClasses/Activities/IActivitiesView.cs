using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalesDepot.SiteManager.PresentationClasses.Activities
{
	public interface IActivitiesView
	{
		DateTime StartDate { get; set; }
		DateTime EndDate { get; set; }
		bool Active { get; set; }
		IEnumerable<Control> FilterControls { get; }
		void ShowView();
		void UpdateData(bool showMessages, ref string updateMessage);
		void ClearData();
		void ExportData();
	}

	public enum ViewType
	{
		RawData = 0,
		MainUserReport = 1,
		NavigationUserReport = 2,
		AccessGroupReport = 3,
		QuizPassUserReport = 4,
		QuizStatusUserReport = 5,
		QuizUnitedReport = 6,
		FileActivityReport = 7,
		VideoInfoReport = 8,
		None = 999
	}
}
