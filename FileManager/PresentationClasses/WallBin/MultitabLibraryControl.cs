using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using FileManager.Controllers;
using FileManager.PresentationClasses.WallBin.Decorators;

namespace FileManager.PresentationClasses.WallBin
{
	[ToolboxItem(false)]
	public partial class MultitabLibraryControl : UserControl
	{
		private readonly List<PageDecorator> _pages = new List<PageDecorator>();

		public MultitabLibraryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void Init(PageDecorator[] pages)
		{
			_pages.Clear();
			_pages.AddRange(pages);
			xtraTabControl.TabPages.Clear();
			xtraTabControl.TabPages.AddRange(_pages.Select(x => x.TabPage).ToArray());
			if (MainController.Instance.ActiveDecorator != null && MainController.Instance.ActiveDecorator.ActivePage != null)
				xtraTabControl.SelectedTabPage = MainController.Instance.ActiveDecorator.ActivePage.TabPage;
			xtraTabControl.SelectedPageChanged += xtraTabControl_SelectedPageChanged;
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.Page != null)
			{
				var pageDecorator = e.Page.Tag as PageDecorator;
				if (pageDecorator != null)
					MainController.Instance.WallbinController.SelectPage(pageDecorator.Page);
			}
		}
	}
}