using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OutlookSalesDepotAddIn.Controls.Wallbin;
using OutlookSalesDepotAddIn.Forms;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	public class LibraryDecorator
	{
		private int _selectedPageIndex = -1;

		public LibraryDecorator(PackageDecorator parent, Library library)
		{
			Parent = parent;
			Pages = new List<PageDecorator>();
			Library = library;
			TabControl = new MultitabLibraryControl();
			Container = new Panel();
			Container.Dock = DockStyle.Fill;
			EmptyPanel = new Panel();
			EmptyPanel.Dock = DockStyle.Fill;
			Container.Controls.Add(EmptyPanel);
			BuildPages();
		}

		public PackageDecorator Parent { get; private set; }
		public Library Library { get; set; }
		public PageDecorator SelectedPage
		{
			get { return TabControl.SelectedPage; }
		}
		public List<PageDecorator> Pages { get; set; }

		public Panel Container { get; private set; }
		public Panel EmptyPanel { get; private set; }

		public MultitabLibraryControl TabControl { get; private set; }
		public bool StateChanged { get; set; }

		private void BuildPages()
		{
			Pages.Clear();
			foreach (var page in Library.Pages)
			{
				Pages.Add(new PageDecorator(this, page));
				Application.DoEvents();
			}
		}

		public void ApplyDecorator(bool firstRun = false)
		{
			FillTabControlWithPages();
			if (!Parent.Container.Controls.Contains(Container))
				Parent.Container.Controls.Add(Container);
			Container.BringToFront();
		}

		private void FillTabControlWithPages()
		{
			foreach (var page in Pages)
			{
				page.Container.Parent = null;
				page.TabPage.Controls.Add(page.Container);
			}
			if (!Container.Controls.Contains(TabControl))
				Container.Controls.Add(TabControl);
			TabControl.AddPages(Pages);
			TabControl.BringToFront();
		}

		public void UpdateView()
		{
			foreach (var page in Pages.Where(p => p.ReadyToShow))
				page.UpdatePage();
		}
	}
}