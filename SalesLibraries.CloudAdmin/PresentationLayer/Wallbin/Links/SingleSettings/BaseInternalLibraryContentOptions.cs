using DevExpress.XtraTab;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.SingleSettings
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
