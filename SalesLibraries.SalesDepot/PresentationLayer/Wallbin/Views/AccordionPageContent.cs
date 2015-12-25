using System;
using System.Linq;
using SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Folders;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	class AccordionPageContent : ColumnsPageContent
	{
		public AccordionPageContent(IPageView pageContainer) : base(pageContainer) { }

		protected override void LoadFolders()
		{
			base.LoadFolders();
			foreach (var folderBox in _folderBoxes.OfType<AccordionFolderBox>())
				folderBox.ContentExpanded += OnFolderExpanded;
		}

		private void OnFolderExpanded(object sender, EventArgs e)
		{
			foreach (var folderBox in _folderBoxes.OfType<AccordionFolderBox>().Where(fb => fb.IsExpanded && fb != sender))
				folderBox.IsExpanded = false;
		}
	}
}
