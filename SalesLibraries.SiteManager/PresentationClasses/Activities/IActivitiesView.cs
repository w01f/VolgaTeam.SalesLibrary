using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities
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
		AccessGroupReport = 2,
		QuizPassUserReport = 3,
		QuizStatusUserReport = 4,
		QuizUnitedReport = 5,
		FileActivityReport = 6,
		VideoInfoReport = 7,
		FileActivityReportLegacy = 8,
		None = 999
	}
}
