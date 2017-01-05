using System.Windows.Forms;
using SalesLibraries.CloudAdmin.Controllers;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Links.HyperlinkEdit
{
	public class BaseInternalLibraryContentEditControl : UserControl
	{
		protected bool IsListsLoaded { get; private set; }

		public virtual void InitControl()
		{
			if (IsListsLoaded) return;
			if (!MainController.Instance.Lists.ExternalLibraryLinks.IsLoaded)
			{
				MainController.Instance.ProcessManager.Run(
					"Loading Site Links...",
					(cancelationToken, formProgess) =>
					{
						MainController.Instance.Lists.ExternalLibraryLinks.Load();
					});
				IsListsLoaded = true;
			}
		}
	}
}
