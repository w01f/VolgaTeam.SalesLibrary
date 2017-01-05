using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class TabbedWallbin : BaseWallbin
	{
		public TabbedWallbin(LibraryContext dataStorage)
			: base(dataStorage)
		{
			InitializeComponent();
		}

		public override void DisposeView()
		{
			xtraTabControl.SelectedPageChanging -= OnSelectedPageChanging;
			xtraTabControl.SelectedPageChanged -= OnSelectedPageChanged;
			base.DisposeView();
		}

		protected override void InitControls()
		{
			base.InitControls();
			xtraTabControl.TabPages.Clear();
			xtraTabControl.TabPages.AddRange(Pages.Cast<XtraTabPage>().ToArray());
			xtraTabControl.SelectedTabPage = (XtraTabPage)ActivePage;
			xtraTabControl.SelectedPageChanging += OnSelectedPageChanging;
			xtraTabControl.SelectedPageChanged += OnSelectedPageChanged;
		}

		private void OnSelectedPageChanging(object sender, TabPageChangingEventArgs e)
		{
			var newPage = e.Page as IPageView;
			if (newPage == null) return;
			MainController.Instance.ProcessManager.Run("Loading Page...",
				(cancelationToken, formProgress) => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					if (e.PrevPage != null)
						WinAPIHelper.SendMessage(e.PrevPage.Handle, 11, IntPtr.Zero, IntPtr.Zero);
					newPage.Suspend();
					WinAPIHelper.SendMessage(e.Page.Handle, 11, new IntPtr(0), IntPtr.Zero);
					SetActivePage(newPage);
				})));
		}

		private void OnSelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			var newPage = e.Page as IPageView;
			if (newPage == null) return;
			newPage.ShowPage();
			WinAPIHelper.SendMessage(e.Page.Handle, 11, new IntPtr(1), IntPtr.Zero);
			newPage.Resume();
		}
	}
}
