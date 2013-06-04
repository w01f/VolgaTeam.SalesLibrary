using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Layout;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.Services.QBuilderService;
using SalesDepot.ToolForms;

namespace SalesDepot.PresentationClasses.QBuilderControls
{
	[ToolboxItem(false)]
	public partial class PageContentControl : UserControl
	{
		public PageContentControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			hyperLinkEditUrl.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(hyperLinkEditUrl_OpenLink);
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
				checkEditShowLinkToMainSite.Checked = page.showLinkMainSite;
				checkEditShowTicker.Checked = page.showTicker;

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
				checkEditShowLinkToMainSite.Checked = false;
				checkEditShowTicker.Checked = false;

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
			page.showLinkMainSite = checkEditShowLinkToMainSite.Checked;
			page.showTicker = checkEditShowTicker.Checked;

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
			SavePage();
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

		private void dateEditExpirationDate_EditValueChanged(object sender, EventArgs e)
		{
			var today = DateTime.Today;
			var expirationDate = dateEditExpirationDate.EditValue != null ? dateEditExpirationDate.DateTime : DateTime.MaxValue;
			if (expirationDate < today)
				dateEditExpirationDate.ForeColor = Color.Red;
			else
				dateEditExpirationDate.ForeColor = Color.Black;
		}

		#region DnD Processing
		private void gridControlLinks_DragOver(object sender, DragEventArgs e)
		{
			e.Effect = e.Data.GetDataPresent(typeof(QPageLinkRecord)) ? DragDropEffects.Move : DragDropEffects.None;
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
				QBuilder.Instance.AddLinksToPage(new[] { link.id });
				form.Close();
			}
		}
		#endregion
	}
}
