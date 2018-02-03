namespace SalesLibraries.Browser.Controls.BusinessClasses.Interfaces
{
	public interface IPrintableContent
	{
		string PrintableFileUrl { get; }
		int? CurrentPage { get; }
	}
}
