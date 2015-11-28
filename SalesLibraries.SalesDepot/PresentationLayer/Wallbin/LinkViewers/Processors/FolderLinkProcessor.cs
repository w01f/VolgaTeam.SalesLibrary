using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Forms;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Processors
{
	[IntendForClass(typeof(LibraryFolderLink))]
	class FolderLinkProcessor : ILinkViewProcessor
	{
		private readonly LibraryFolderLink _folderLink;

		public FolderLinkProcessor(LibraryFolderLink folderLink)
		{
			_folderLink = folderLink;
		}

		public void Open()
		{
			switch (MainController.Instance.Settings.LinkLaunchSettings.Folder)
			{
				case LinkLaunchOptionsEnum.Menu:
					using (var formViewOptions = new FormFolderViewOptions())
					{
						formViewOptions.Text = string.Format(formViewOptions.Text, _folderLink.Name);
						if (formViewOptions.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
						{
							if (formViewOptions.SelectedOption == ViewOptions.Open)
								LinkManager.OpenFolderLink(_folderLink);
						}
					}
					break;
				case LinkLaunchOptionsEnum.Launch:
					LinkManager.OpenFolderLink(_folderLink);
					return;
				case LinkLaunchOptionsEnum.Viewer:
					LinkManager.PreviewLink(_folderLink);
					break;
			}
		}
	}
}
