using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.Services.QBuilderService;
using SalesDepot.ToolForms;

namespace SalesDepot.PresentationClasses.QBuilderControls
{
	[ToolboxItem(false)]
	public partial class LinkCartControl : UserControl
	{
		private readonly List<QPageLinkRecord> _links = new List<QPageLinkRecord>();

		public QPageLinkRecord SelectedLink
		{
			get { return advBandedGridView.GetFocusedRow() as QPageLinkRecord; }
		}

		public LinkCartControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void UpdateContent()
		{
			gridControl.DataSource = null;
			_links.Clear();
			Enabled = false;
			if (!QBuilder.Instance.Connected) return;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading Link Cart...";
				form.TopMost = true;
				form.Show();
				_links.AddRange(QBuilder.Instance.GetLinkCart());
				form.Close();
			}
			gridControl.DataSource = _links;
			Enabled = true;
		}

		private void repositoryItemButtonEditActions_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (SelectedLink == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Do you want to delete link from Cart?") == DialogResult.Yes)
			{
				if (!QBuilder.Instance.Connected) return;
				var result = false;
				Enabled = false;
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Deleting Link from Cart...";
					form.TopMost = true;
					form.Show();
					result = QBuilder.Instance.DeleteLinkFromCart(SelectedLink.id);
					form.Close();
				}
				Enabled = true;
				if (result)
				{
					_links.Remove(SelectedLink);
					advBandedGridView.RefreshData();
				}
			}
		}

		private void simpleButtonRefresh_Click(object sender, System.EventArgs e)
		{
			UpdateContent();
		}

		private void simpleButtonClear_Click(object sender, System.EventArgs e)
		{
			gridControl.DataSource = null;
			_links.Clear();
			Enabled = false;
			if (!QBuilder.Instance.Connected) return;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Cleaning Link Cart...";
				form.TopMost = true;
				form.Show();
				QBuilder.Instance.DeleteAllFromCart();
				form.Close();
			}
			gridControl.DataSource = _links;
			Enabled = true;
		}

		#region DnD Processing
		private GridHitInfo _downHitInfo;
		private void advBandedGridView_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			_downHitInfo = null;
			var hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
			if (ModifierKeys != Keys.None) return;
			if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
				_downHitInfo = hitInfo;
		}

		private void advBandedGridView_MouseMove(object sender, MouseEventArgs e)
		{
			var view = sender as AdvBandedGridView;
			if (view == null) return;
			if (e.Button == MouseButtons.Left && _downHitInfo != null)
			{
				var dragSize = SystemInformation.DragSize;
				var dragRect = new Rectangle(new Point(_downHitInfo.HitPoint.X - dragSize.Width / 2,
					_downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);
				if (!dragRect.Contains(new Point(e.X, e.Y)))
				{
					view.GridControl.DoDragDrop(SelectedLink, DragDropEffects.Move);
					_downHitInfo = null;
					DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
				}
			}
		}
		#endregion
	}
}
