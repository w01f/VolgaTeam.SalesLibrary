using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Layout;
using SalesDepot.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.Services.QBuilderService;
using SalesDepot.ToolForms;
using SalesDepot.ToolForms.QBuilderForms;

namespace SalesDepot.PresentationClasses.QBuilderControls
{
	[ToolboxItem(false)]
	public partial class PageContentControl : UserControl
	{
		public PageContentControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			hyperLinkEditUrl.OpenLink += hyperLinkEditUrl_OpenLink;
		}

		public void UpdateContent()
		{
			var pageAvailable = true;
			pageAvailable &= QBuilder.Instance.Connected;
			var page = QBuilder.Instance.SelectedPage;
			pageAvailable &= page != null;

			if (pageAvailable)
			{
				textEditTitleValue.EditValue = page.title;
				labelControlCreateDate.Text = page.CreateDate.HasValue ? String.Format("Creared: {0}", page.CreateDate.Value.ToString("MM/dd/yy hh:mm tt")) : null;
				hyperLinkEditUrl.EditValue = page.url;

				UpdateLinks(page);

				checkEditTitleEnabled.Checked = !String.IsNullOrEmpty(page.subtitle);
				htmlEditorControlTitle.HtmlText = page.subtitle;
				checkEditHeaderEnabled.Checked = !String.IsNullOrEmpty(page.header);
				htmlEditorControlHeader.HtmlText = page.header;
				checkEditFooterEnabled.Checked = !String.IsNullOrEmpty(page.footer);
				htmlEditorControlFooter.HtmlText = page.footer;

				checkEditExpirationDate.Checked = page.ExpirationDate.HasValue;
				dateEditExpirationDate.EditValue = page.ExpirationDate;
				checkEditRestricted.Checked = page.isRestricted;
				checkEditPinCode.Checked = !String.IsNullOrEmpty(page.pinCode);
				textEditPinCode.EditValue = page.pinCode;
				checkEditDisableBanners.Checked = page.disableBanners;
				checkEditDisableWidgets.Checked = page.disableWidgets;
				checkEditShowLinksAsUrl.Checked = page.showLinksAsUrl;
				checkEditRecordActivity.Checked = page.recordActivity;
				textEditActivityEmailCopy.EditValue = page.activityEmailCopy;

				gridControlLogoGallery.DataSource = QBuilder.Instance.Logos;
				var selectedLogo = QBuilder.Instance.Logos.FirstOrDefault(l => l.EncodedImage.Equals(page.logo));
				if (selectedLogo != null)
				{
					var index = QBuilder.Instance.Logos.IndexOf(selectedLogo);
					layoutViewLogoGallery.FocusedRowHandle = layoutViewLogoGallery.GetRowHandle(index);
				}
				else
					layoutViewLogoGallery.FocusedRowHandle = 0;

				Enabled = true;
			}
			else
			{
				textEditTitleValue.EditValue = null;
				labelControlCreateDate.Text = null;
				hyperLinkEditUrl.EditValue = null;

				gridControlLinks.DataSource = null;

				checkEditTitleEnabled.Checked = false;
				htmlEditorControlTitle.HtmlText = String.Empty;
				checkEditHeaderEnabled.Checked = false;
				htmlEditorControlHeader.HtmlText = String.Empty;
				checkEditFooterEnabled.Checked = false;
				htmlEditorControlFooter.HtmlText = String.Empty;

				checkEditExpirationDate.Checked = false;
				dateEditExpirationDate.EditValue = null;
				checkEditRestricted.Checked = false;

				gridControlLogoGallery.DataSource = null;

				Enabled = false;
			}
		}

		private void UpdateLinks(QPageRecord page)
		{
			labelControlLinksCount.Text = String.Format("Shared Links: {0}", page.links != null ? page.links.Count() : 0);
			gridControlLinks.DataSource = page.links;
		}

		public void SavePage()
		{
			var page = QBuilder.Instance.SelectedPage;
			page.title = textEditTitleValue.EditValue != null ? textEditTitleValue.EditValue.ToString() : null;
			page.subtitle = checkEditTitleEnabled.Checked && !String.IsNullOrEmpty(htmlEditorControlTitle.SimpleText) ? htmlEditorControlTitle.HtmlText : null;
			page.header = checkEditHeaderEnabled.Checked && !String.IsNullOrEmpty(htmlEditorControlHeader.SimpleText) ? htmlEditorControlHeader.HtmlText : null;
			page.footer = checkEditFooterEnabled.Checked && !String.IsNullOrEmpty(htmlEditorControlFooter.SimpleText) ? htmlEditorControlFooter.HtmlText : null;
			page.expirationDate = checkEditExpirationDate.Checked && dateEditExpirationDate.EditValue != null ? dateEditExpirationDate.DateTime.ToString("MM/dd/yyyy") : null;
			page.isRestricted = checkEditRestricted.Checked;
			page.pinCode = checkEditPinCode.Checked && textEditPinCode.EditValue != null ? textEditPinCode.EditValue.ToString() : null;
			page.disableBanners = checkEditDisableBanners.Checked;
			page.disableWidgets = checkEditDisableWidgets.Checked;
			page.showLinksAsUrl = checkEditShowLinksAsUrl.Checked;
			page.recordActivity = checkEditRecordActivity.Checked;
			page.activityEmailCopy = checkEditRecordActivity.Checked && textEditActivityEmailCopy.EditValue != null ? textEditActivityEmailCopy.EditValue.ToString() : null;

			var selectedLogo = layoutViewLogoGallery.GetFocusedRow() as PageLogo;
			page.logo = selectedLogo != null ? selectedLogo.EncodedImage : null;

			Enabled = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Save quickSITE...";
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.SavePage();
				form.Close();
			}
			Enabled = true;
		}

		private void hyperLinkEditUrl_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			e.Handled = true;
			using (var form = new FormBrowserSelector())
			{
				if (form.ShowDialog() == DialogResult.OK)
				{
					SavePage();
					var browser = String.Empty;
					switch (form.SelectedBrowser)
					{
						case FormBrowserSelector.Browser.Chrome:
							browser = "chrome.exe";
							break;
						case FormBrowserSelector.Browser.Firefox:
							browser = "firefox.exe";
							break;
						case FormBrowserSelector.Browser.IE:
							browser = "iexplore.exe";
							break;
						case FormBrowserSelector.Browser.Opera:
							browser = "opera.exe";
							break;
					}
					try
					{
						var process = new Process
						{
							StartInfo =
							{
								FileName = browser,
								Arguments = e.EditValue.ToString()
							}
						};
						process.Start();
					}
					catch { }
				}
			}
		}

		private void layoutViewLogoGallery_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
		{
			var view = sender as LayoutView;
			if (view.FocusedRowHandle == e.RowHandle)
			{
				e.Appearance.BackColor = Color.Orange;
				e.Appearance.BackColor2 = Color.Orange;
			}
		}

		private void repositoryItemButtonEditLinksActions_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			var link = advBandedGridViewLinks.GetFocusedRow() as QPageLinkRecord;
			if (link == null) return;
			var currentLinkIndex = advBandedGridViewLinks.FocusedRowHandle;
			switch (e.Button.Index)
			{
				case 0:
					if (currentLinkIndex > 0)
					{
						if (!QBuilder.Instance.Connected) return;
						Enabled = false;
						using (var form = new FormProgress())
						{
							form.laProgress.Text = "Changing Links Order...";
							form.TopMost = true;
							form.Show();
							QBuilder.Instance.SetLinkIndex(link.id, currentLinkIndex - 1);
							form.Close();
						}
						Enabled = true;
					}
					break;
				case 1:
					if (currentLinkIndex < (advBandedGridViewLinks.RowCount - 1))
					{
						var nextLink = advBandedGridViewLinks.GetRow(currentLinkIndex + 1) as QPageLinkRecord;
						if (nextLink == null) return;
						if (!QBuilder.Instance.Connected) return;
						Enabled = false;
						using (var form = new FormProgress())
						{
							form.laProgress.Text = "Changing Links Order...";
							form.TopMost = true;
							form.Show();
							QBuilder.Instance.SetLinkIndex(nextLink.id, currentLinkIndex);
							form.Close();
						}
						Enabled = true;
					}
					break;
				case 2:
					if (AppManager.Instance.ShowWarningQuestion("Do you want to delete link from quickSITE?") == DialogResult.Yes)
					{
						if (!QBuilder.Instance.Connected) return;
						var result = false;
						Enabled = false;
						using (var form = new FormProgress())
						{
							form.laProgress.Text = "Deleting Link from quickSITE...";
							form.TopMost = true;
							form.Show();
							result = QBuilder.Instance.DeleteLinkFromPage(link.id);
							form.Close();
						}
						Enabled = true;
						if (result)
						{
							QBuilder.Instance.SelectedPage.RemoveLink(link);
							UpdateLinks(QBuilder.Instance.SelectedPage);
						}
					}
					break;
			}
		}

		private void checkEditTitleEnabled_CheckedChanged(object sender, EventArgs e)
		{
			htmlEditorControlTitle.Enabled = checkEditTitleEnabled.Checked;
			if (!checkEditTitleEnabled.Checked)
				htmlEditorControlTitle.HtmlText = String.Empty;
		}

		private void checkEditHeaderEnabled_CheckedChanged(object sender, EventArgs e)
		{
			htmlEditorControlHeader.Enabled = checkEditHeaderEnabled.Checked;
			if (!checkEditHeaderEnabled.Checked)
				htmlEditorControlHeader.HtmlText = String.Empty;
		}

		private void checkEditFooterEnabled_CheckedChanged(object sender, EventArgs e)
		{
			htmlEditorControlFooter.Enabled = checkEditFooterEnabled.Checked;
			if (!checkEditFooterEnabled.Checked)
				htmlEditorControlFooter.HtmlText = String.Empty;
		}

		private void checkEditExpirationDate_CheckedChanged(object sender, EventArgs e)
		{
			dateEditExpirationDate.Enabled = checkEditExpirationDate.Checked;
			if (!checkEditExpirationDate.Checked)
				dateEditExpirationDate.EditValue = null;
			else if (dateEditExpirationDate.EditValue == null)
				dateEditExpirationDate.DateTime = DateTime.Now.AddDays(7);
		}

		private void checkEditPinCode_CheckedChanged(object sender, EventArgs e)
		{
			textEditPinCode.Enabled = checkEditPinCode.Checked;
			if (!checkEditPinCode.Checked)
				textEditPinCode.EditValue = null;
		}

		private void checkEditRecordActivity_CheckedChanged(object sender, EventArgs e)
		{
			textEditActivityEmailCopy.Enabled = checkEditRecordActivity.Checked;
			if (!checkEditRecordActivity.Checked)
				textEditActivityEmailCopy.EditValue = null;
		}

		private void dateEditExpirationDate_EditValueChanged(object sender, EventArgs e)
		{
			var today = DateTime.Today;
			var expirationDate = dateEditExpirationDate.EditValue != null ? dateEditExpirationDate.DateTime : DateTime.MaxValue;
			if (expirationDate < today)
				dateEditExpirationDate.ForeColor = Color.Red;
			else
				dateEditExpirationDate.ForeColor = Color.Black;
		}

		private void advBandedGridViewLinks_MouseMove(object sender, MouseEventArgs e)
		{
			GridHitInfo hitInfo = advBandedGridViewLinks.CalcHitInfo(e.Location);
			Cursor = hitInfo.InRowCell && (hitInfo.Column == bandedGridColumnLinksName || hitInfo.Column == bandedGridColumnLinksType) ? Cursors.Hand : Cursor = Cursors.Default;
		}

		private void advBandedGridViewLinks_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (e.Column != bandedGridColumnLinksName && e.Column != bandedGridColumnLinksType) return;
			var link = advBandedGridViewLinks.GetFocusedRow() as QPageLinkRecord;
			if (link == null) return;
			var libraryLink = LibraryManager.Instance.GetLibraryLink(link.libraryId, link.linkId);
			if (libraryLink == null)
			{
				AppManager.Instance.ShowWarning("Link was not found in local libraries");
				return;
			}
			LinkManager.Instance.OpenLink(libraryLink);
		}

		#region DnD Processing
		private int _dropTargetRowHandle = GridControl.InvalidRowHandle;
		private int DropTargetRowHandle
		{
			get { return _dropTargetRowHandle; }
			set
			{
				if (_dropTargetRowHandle != value)
				{
					_dropTargetRowHandle = value;
					gridControlLinks.Refresh();
				}
			}
		}

		private void gridControlLinks_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(typeof(QPageLinkRecord)) ? DragDropEffects.Move : DragDropEffects.None;
			var grid = sender as GridControl;
			if (grid == null) return;
			var view = grid.MainView as GridView;
			if (view == null) return;
			var hitInfo = view.CalcHitInfo(grid.PointToClient(new Point(e.X, e.Y)));
			DropTargetRowHandle = hitInfo.RowHandle;
		}

		private void gridControlLinks_DragDrop(object sender, DragEventArgs e)
		{
			var link = e.Data.GetData(typeof(QPageLinkRecord)) as QPageLinkRecord;
			if (link == null) return;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Adding Link to quickSITE...";
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.AddLinksToPage(new[] { link.id }, DropTargetRowHandle == GridControl.InvalidRowHandle ? -1 : DropTargetRowHandle);
				form.Close();
				DropTargetRowHandle = GridControl.InvalidRowHandle;
			}
		}

		private void gridControlLinks_MouseLeave(object sender, EventArgs e)
		{
			DropTargetRowHandle = GridControl.InvalidRowHandle;
		}

		private void gridControlLinks_Paint(object sender, PaintEventArgs e)
		{
			if (DropTargetRowHandle == GridControl.InvalidRowHandle) return;
			var grid = sender as GridControl;
			if (grid == null) return;
			var view = grid.MainView as GridView;
			if (view == null) return;
			bool isBottomLine = DropTargetRowHandle == view.DataRowCount;
			var viewInfo = view.GetViewInfo() as GridViewInfo;
			var rowInfo = viewInfo.GetGridRowInfo(isBottomLine ? DropTargetRowHandle - 1 : DropTargetRowHandle);
			if (rowInfo == null) return;
			Point p1, p2;
			if (isBottomLine)
			{
				p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Bottom - 2);
				p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Bottom - 2);
			}
			else
			{
				p1 = new Point(rowInfo.Bounds.Left, rowInfo.Bounds.Top - 2);
				p2 = new Point(rowInfo.Bounds.Right, rowInfo.Bounds.Top - 2);
			}
			e.Graphics.DrawLine(Pens.Orange, p1, p2);
		}
		#endregion
	}
}
