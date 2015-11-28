using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.SalesDepot.Controllers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class SimpleWallbin : BaseWallbin
	{
		private ComboBoxEdit _pageSelector;
		public SimpleWallbin(LibraryContext dataStorage)
			: base(dataStorage)
		{
			InitializeComponent();
		}

		public override void DisposeView()
		{
			pnContainer.Controls.Clear();
			base.DisposeView();
		}

		public override void UnloadView()
		{
			if (_pageSelector != null)
				_pageSelector.EditValueChanged -= OnSelectedPageChanged;
			base.UnloadView();
		}

		public override void ShowView()
		{
			Application.DoEvents();
			if (!pnContainer.Controls.Contains((Control)ActivePage))
				pnContainer.Controls.Add((Control)ActivePage);
			InitPageCombo();
			base.ShowView();
		}

		private void InitPageCombo()
		{
			MainController.Instance.MainForm.itemContainerHomeWallbinPage.Visible = Pages.Any();
			_pageSelector = MainController.Instance.MainForm.comboBoxEditHomePage;
			_pageSelector.Enabled = Pages.Count > 1;
			_pageSelector.Properties.Items.Clear();
			_pageSelector.Properties.Items.AddRange(Pages);
			_pageSelector.EditValue = ActivePage;
			_pageSelector.EditValueChanged -= OnSelectedPageChanged;
			_pageSelector.EditValueChanged += OnSelectedPageChanged;
			MainController.Instance.MainForm.ribbonBarHomeWallbin.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelHome.PerformLayout();
		}

		private void OnSelectedPageChanged(object sender, EventArgs eventArgs)
		{
			var editor = sender as ComboBoxEdit;
			if (editor == null) return;
			var selectedPage = editor.EditValue as IPageView;
			if (selectedPage == null) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Page...",
				() => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					pnEmpty.BringToFront();
					Application.DoEvents();
					SetActivePage(selectedPage);
					ShowView();
				})));
		}
	}
}
