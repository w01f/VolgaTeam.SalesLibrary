using System.Drawing;
using OutlookSalesDepotAddIn.BusinessClasses;

namespace OutlookSalesDepotAddIn.Controls.Viewers
{
	internal interface IFileViewer
	{
		LibraryLink File { get; }
		string DisplayName { get; }
		string CriteriaOverlap { get; }
		Image Widget { get; }

		void Attach();
		void ReleaseResources();
	}
}