using SalesLibraries.Browser.Controls.BusinessClasses.Enums;
using SalesLibraries.Browser.Controls.BusinessClasses.Interfaces;

namespace SalesLibraries.Browser.Controls.BusinessClasses.Objects.LinkViewContent
{
	class ExcelContent : ViewContent, IPrintableContent
	{
		public override LinkContentType ContentType=>LinkContentType.Excel;
		public string PrintableFileUrl => OriginalFileUrl;
		public int? CurrentPage => null;
	}
}
