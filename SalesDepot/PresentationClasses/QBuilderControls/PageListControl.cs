using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.Services.QBuilderService;
using SalesDepot.ToolForms;
using SalesDepot.ToolForms.QBuilderForms;

namespace SalesDepot.PresentationClasses.QBuilderControls
{
	[ToolboxItem(false)]
	public partial class PageListControl : UserControl
	{
		private bool _updating = false;
		private readonly FormAddPage _formAddPage;
		private readonly FormDeletePages _formDeletePages;

		public QPageRecord SelectedPage
		{
			get { return advBandedGridView.GetFocusedRow() as QPageRecord; }
		}

		public PageListControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_formAddPage = new FormAddPage();
			_formDeletePages = new FormDeletePages();
		}

		public void UpdateContent()
		{
			_updating = true;
			gridControl.DataSource = null;
			Enabled = false;
			if (!QBuilder.Instance.Connected) return;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading Site List...";
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.GetPageList();
				form.Close();
			}
			gridControl.DataSource = QBuilder.Instance.Pages;
			if (!string.IsNullOrEmpty(QBuilder.Instance.SelectedPageId))
			{
				if (QBuilder.Instance.SelectedPage != null)
				{
					var index = QBuilder.Instance.Pages.IndexOf(QBuilder.Instance.SelectedPage);
					advBandedGridView.FocusedRowHandle = advBandedGridView.GetRowHandle(index);
				}
			}
			LoadPage();
			Enabled = true;
			_updating = false;
		}

		public void LoadPage()
		{
			if (!QBuilder.Instance.Connected) return;
			if (SelectedPage == null) return;
			Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading quickSITE...";
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.LoadPage(SelectedPage.id);
				form.Close();
			}
			advBandedGridView.RefreshData();
			Enabled = true;
		}

		public void AddPage()
		{
			if (!QBuilder.Instance.Connected) return;
			var pageTitle = string.Empty;
			_formAddPage.Init(false);
			if (_formAddPage.ShowDialog() == DialogResult.OK)
				pageTitle = _formAddPage.PageTitle;
			if (String.IsNullOrEmpty(pageTitle)) return;
			Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Adding quickSITE...";
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.AddPage(pageTitle);
				form.Close();
			}
			Enabled = true;
		}

		public void DeletePage()
		{
			if (SelectedPage == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Do you want to delete quickSITE?") == DialogResult.Yes)
			{
				if (!QBuilder.Instance.Connected) return;
				Enabled = false;
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Deleting quickSITE...";
					form.TopMost = true;
					form.Show();
					QBuilder.Instance.DeletePages(new[] { SelectedPage.id });
					form.Close();
				}
				Enabled = true;
			}
		}

		public void PreviewPage()
		{
			if (SelectedPage == null) return;
			try
			{
				Process.Start(SelectedPage.url);
			}
			catch { }
		}

		public void EmailPage()
		{
			if (SelectedPage == null) return;
			try
			{
				if (String.IsNullOrEmpty(SelectedPage.title))
					Process.Start("mailto: ?body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + SelectedPage.url + (!String.IsNullOrEmpty(SelectedPage.pinCode) ? ("%0D%0APin-code: " + SelectedPage.pinCode) : String.Empty));
				else
					Process.Start("mailto: ?subject=" + SelectedPage.title + "&body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + SelectedPage.url + (!String.IsNullOrEmpty(SelectedPage.pinCode) ? ("%0D%0APin-code: " + SelectedPage.pinCode) : String.Empty));
			}
			catch { }
		}

		public void ClonePage()
		{
			if (!QBuilder.Instance.Connected || SelectedPage == null) return;
			var pageTitle = string.Empty;
			_formAddPage.Init(true);
			if (_formAddPage.ShowDialog() == DialogResult.OK)
				pageTitle = _formAddPage.PageTitle;
			if (String.IsNullOrEmpty(pageTitle)) return;
			Enabled = false;
			using (var form = new FormProgress())
			{
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.ClonePage(SelectedPage.id, pageTitle);
				form.Close();
			}
			Enabled = true;
		}

		private void simpleButtonDelete_Click(object sender, System.EventArgs e)
		{
			if (!QBuilder.Instance.Connected) return;
			var pageIds = new List<string>();
			_formDeletePages.Init(QBuilder.Instance.Pages);
			if (_formDeletePages.ShowDialog() == DialogResult.OK)
				pageIds.AddRange(_formDeletePages.SelectedPageIds);
			if (!pageIds.Any()) return;
			Enabled = false;
			using (var form = new FormProgress())
			{
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.DeletePages(pageIds.ToArray());
				form.Close();
			}
			Enabled = true;
		}

		private void repositoryItemButtonEditActions_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (e.Button.Index == 0) { ClonePage(); }
			else if (e.Button.Index == 1)
			{
				DeletePage();
			}
		}

		private void advBandedGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if (!_updating)
				LoadPage();
		}
	}
}
