using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Folders
{
	public class ClassicFolderBox : FolderBoxWithHeader
	{
		public ClassicFolderBox(LibraryFolder dataSource) : base(dataSource)
		{
			new FolderBoxInitializer().Initialize(this);
		}

		#region Public Properties
		public override IWallbinViewFormat FormatState
		{
			get { return MainController.Instance.WallbinViews.FormatState; }
		}
		#endregion
	}
}
