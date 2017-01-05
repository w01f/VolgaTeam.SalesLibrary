using DevExpress.XtraTab;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public class BaseInternalLibraryContentOptions : XtraTabPage
	{
		public virtual void LoadData()
		{
			if (!MainController.Instance.Lists.ExternalLibraryLinks.IsLoaded)
			{
				MainController.Instance.ProcessManager.Run(
					"Loading Site Links...",
					(cancelationToken, formProgess) =>
					{
						MainController.Instance.Lists.ExternalLibraryLinks.Load();
					});
			}
		}
	}
}
