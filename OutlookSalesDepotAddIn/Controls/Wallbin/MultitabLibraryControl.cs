using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraTab;
using OutlookSalesDepotAddIn.BusinessClasses;
using OutlookSalesDepotAddIn.Forms;

namespace OutlookSalesDepotAddIn.Controls.Wallbin
{
	[ToolboxItem(false)]
	public partial class MultitabLibraryControl : UserControl
	{
		private readonly List<PageDecorator> _pages = new List<PageDecorator>();

		public PageDecorator SelectedPage
		{
			get { return xtraTabControl.SelectedTabPage != null ? xtraTabControl.SelectedTabPage.Tag as PageDecorator : null; }
		}

		public MultitabLibraryControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Resize += MultitabLibraryControl_Resize;
		}

		private void FillPages()
		{
			pnEmpty.BringToFront();
			xtraTabControl.SelectedPageChanged -= xtraTabControl_SelectedPageChanged;
			xtraTabControl.TabPages.Clear();
			xtraTabControl.TabPages.AddRange(_pages.Select(x => x.TabPage).ToArray());
			var selectedPage = xtraTabControl.TabPages.FirstOrDefault(x => String.IsNullOrEmpty(SettingsManager.Instance.SelectedPage) || x.Text.Equals(SettingsManager.Instance.SelectedPage.Replace("&", "&&")));
			if (selectedPage != null)
				xtraTabControl.SelectedTabPage = selectedPage;
			xtraTabControl_SelectedPageChanged(null, new TabPageChangedEventArgs(null, xtraTabControl.SelectedTabPage));
			xtraTabControl.SelectedPageChanged += xtraTabControl_SelectedPageChanged;
			pnEmpty.SendToBack();
			new Thread(delegate()
			{
				foreach (var page in _pages.Where(p => !p.ReadyToShow))
					FormMain.Instance.Invoke((MethodInvoker)(() => page.Apply(true)));
			}).Start();
		}

		private void MultitabLibraryControl_Resize(object sender, EventArgs e)
		{
			if (xtraTabControl.SelectedTabPage == null) return;
			var pageDecorator = xtraTabControl.SelectedTabPage.Tag as PageDecorator;
			if (pageDecorator == null) return;
			pageDecorator.UpdatePage();
		}

		private void xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			if (e.Page == null) return;
			var pageDecorator = e.Page.Tag as PageDecorator;
			if (pageDecorator == null) return;
			pageDecorator.Apply();
			SettingsManager.Instance.SelectedPage = pageDecorator.Page.Name;
			SettingsManager.Instance.SaveSettings();
		}

		public void AddPages(IEnumerable<PageDecorator> pages)
		{
			_pages.Clear();
			_pages.AddRange(pages);
			FillPages();
		}
	}
}