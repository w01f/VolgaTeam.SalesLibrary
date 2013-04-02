using SalesDepot.Services.TickerService;

namespace SalesDepot.SiteManager.PresentationClasses.Ticker.LinkEditors
{
	interface ITickerEditor
	{
		string LinkType { get; set; }
		KeyValuePair[] Details { get; set; }
	}
}
