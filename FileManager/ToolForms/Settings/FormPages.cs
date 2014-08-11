using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraEditors.Controls;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormPages : MetroForm
	{
		public FormPages()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laDeletePageWarning.Font = new Font(laDeletePageWarning.Font.FontFamily, laDeletePageWarning.Font.Size - 2, laDeletePageWarning.Font.Style);
			}
		}

		public Library Library { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			LoadPages();
		}

		private void LoadPages()
		{
			gridControlPages.DataSource = new BindingList<LibraryPage>(Library.Pages);
		}

		private void FormPages_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			if (DialogResult != DialogResult.OK) return;
			gridViewPages.CloseEditor();
			if (Library.Pages.Any(x => string.IsNullOrEmpty(x.Name.Trim())))
			{
				AppManager.Instance.ShowWarning("You need to name all Pages before closing.");
				e.Cancel = true;
			}
		}

		private void FormPages_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (Library.Pages.Count > 0)
			{
				Library.IsConfigured = true;
				Library.Save();
			}
			else
				AppManager.Instance.ShowWarning("You need to have at least 1 Page in Library.");
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					Library.UpPage(gridViewPages.GetDataSourceRowIndex(gridViewPages.FocusedRowHandle));
					if (gridViewPages.FocusedRowHandle > 0)
						gridViewPages.FocusedRowHandle--;
					break;
				case 1:
					Library.DownPage(gridViewPages.GetDataSourceRowIndex(gridViewPages.FocusedRowHandle));
					if (gridViewPages.FocusedRowHandle < gridViewPages.RowCount - 1)
						gridViewPages.FocusedRowHandle++;
					break;
			}
		}

		private void buttonXAdd_Click(object sender, EventArgs e)
		{
			Library.AddPage();
			((BindingList<LibraryPage>)gridControlPages.DataSource).ResetBindings();
			gridViewPages.FocusedRowHandle = gridViewPages.RowCount - 1;
		}

		private void buttonXRemove_Click(object sender, EventArgs e)
		{
			if (gridViewPages.FocusedRowHandle < 0) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete selected page?") != DialogResult.Yes) return;
			gridViewPages.DeleteSelectedRows();
			Library.RebuildPagesOrder();
		}
	}
}