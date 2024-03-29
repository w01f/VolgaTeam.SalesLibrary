﻿using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormPages : MetroForm
	{
		public FormPages()
		{
			InitializeComponent();

			layoutControlItemAdd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAdd.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRemove.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRemove.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemRemove.MinSize = RectangleHelper.ScaleSize(layoutControlItemRemove.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public Library Library { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			LoadPages();
		}

		private void LoadPages()
		{
			gridControlPages.DataSource = Library.Pages;
			gridViewPages.RefreshData();
		}

		private void FormPages_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = false;
			if (DialogResult != DialogResult.OK) return;
			gridViewPages.CloseEditor();
			if (!Library.Pages.Any())
			{
				MainController.Instance.PopupMessages.ShowWarning("You need to have at least 1 Page in Library.");
				e.Cancel = true;
			}
			else if (Library.Pages.Any(x => string.IsNullOrEmpty(x.Name.Trim())))
			{
				MainController.Instance.PopupMessages.ShowWarning("You need to name all Pages before closing.");
				e.Cancel = true;
			}
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var page = gridViewPages.GetFocusedRow() as LibraryPage;
			if (page == null) return;
			var focusedRowHandle = gridViewPages.FocusedRowHandle;
			switch (e.Button.Index)
			{
				case 0:
					focusedRowHandle = gridViewPages.FocusedRowHandle > 0 ? gridViewPages.FocusedRowHandle - 1 : 0;
					Library.Pages.UpItem(page);
					break;
				case 1:
					focusedRowHandle = gridViewPages.FocusedRowHandle < gridViewPages.RowCount - 1 ? gridViewPages.FocusedRowHandle + 1 : gridViewPages.RowCount - 1;
					Library.Pages.DownItem(page);
					break;
			}
			LoadPages();
			gridViewPages.FocusedRowHandle = focusedRowHandle;
		}

		private void buttonXAdd_Click(object sender, EventArgs e)
		{
			Library.AddPage();
			LoadPages();
			gridViewPages.FocusedRowHandle = gridViewPages.RowCount - 1;
		}

		private void buttonXRemove_Click(object sender, EventArgs e)
		{
			var page = gridViewPages.GetFocusedRow() as LibraryPage;
			if (page == null) return;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure you want to delete selected page?") != DialogResult.Yes) return;
			var focusedRowHandle = gridViewPages.FocusedRowHandle;
			Library.Pages.RemoveItem(page);
			LoadPages();
			if (gridViewPages.RowCount > 0 && gridViewPages.RowCount > focusedRowHandle)
				gridViewPages.FocusedRowHandle = focusedRowHandle;
		}
	}
}