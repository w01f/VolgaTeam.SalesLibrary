using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Views
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
			if (_pageSelector != null)
				_pageSelector.EditValueChanged -= OnSelectedPageChanged;
			pnContainer.Controls.Clear();
			base.DisposeView();
		}

		protected override void InitControls()
		{
			base.InitControls();
			MainController.Instance.MainForm.itemContainerHomeWallbin.Visible = Pages.Any();
			_pageSelector = MainController.Instance.MainForm.comboBoxEditHomePage;
			_pageSelector.Enabled = Pages.Count > 1;
			_pageSelector.Properties.Items.Clear();
			_pageSelector.Properties.Items.AddRange(Pages);
			_pageSelector.EditValue = ActivePage;
			_pageSelector.EditValueChanged += OnSelectedPageChanged;
			MainController.Instance.MainForm.ribbonBarHomeWallbin.RecalcLayout();
			MainController.Instance.MainForm.ribbonPanelHome.PerformLayout();
		}

		public override void ShowView()
		{
			Application.DoEvents();
			if (!pnContainer.Controls.Contains((Control)ActivePage))
				pnContainer.Controls.Add((Control)ActivePage);
			base.ShowView();
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
