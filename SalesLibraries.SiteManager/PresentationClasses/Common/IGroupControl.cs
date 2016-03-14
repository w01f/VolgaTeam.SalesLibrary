using DevExpress.XtraPrinting;

namespace SalesLibraries.SiteManager.PresentationClasses.Common
{
	public interface IGroupControl
	{
		string GroupName { get; }
		PrintableComponentLink GetPrintLink();
	}
}
