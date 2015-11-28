using System.Drawing;
using SalesDepot.BusinessClasses;

namespace SalesDepot.PresentationClasses.Viewers
{
	internal interface IFileViewer
	{
		LibraryLink File { get; }
		string DisplayName { get; }
		string CriteriaOverlap { get; }
		Image Widget { get; }

		void ReleaseResources();
		void Open();
		void Save();
		void Email();
		void Print();
		void EmailLinkToQuickSite();
		void AddLinkToQuickSite();
	}
}