using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;

namespace FileManager.PresentationClasses.WallBin
{
	//public partial class LinkImagesContainer : UserControl
	public sealed partial class LinkImagesContainer : XtraTabPage
	{
		private readonly LinkImageGroup _parent;
		private LayoutViewHitInfo _hitInfo;
		private bool _allowToSave;

		public event EventHandler<LinkImageEventArgs> SelectedImageChanged;
		public event EventHandler<EventArgs> OnImageDoubleClick;

		public LinkImagesContainer(LinkImageGroup parent)
		{
			InitializeComponent();
			_parent = parent;
			_allowToSave = false;
			Text = _parent.Name;
			gridControlGallery.DataSource = _parent.Images;
			_parent.OnDataChanged += (o, e) =>
			{
				_allowToSave = false;
				gridControlGallery.DataSource = _parent.Images;
				layoutViewGallery.RefreshData();
				_allowToSave = true;
			};
			_allowToSave = true;
			gridControlGallery.MouseUp += gridControlGallery_MouseUp;
			gridControlGallery.MouseMove += gridControlGallery_MouseMove;
			layoutViewGallery.FocusedRowChanged += layoutViewGalery_FocusedRowChanged;
			layoutViewGallery.Click += gridViewGallery_Click;
			layoutViewGallery.DoubleClick += gridViewGallery_DoubleClick;
			if (_parent is FavoriteImageGroup)
				laFavoritesHint.Visible = false;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laFavoritesHint.Font = new Font(laFavoritesHint.Font.FontFamily, laFavoritesHint.Font.Size - 4, laFavoritesHint.Font.Style);
			}
		}

		private void gridControlGallery_MouseMove(object sender, MouseEventArgs e)
		{
			layoutViewGallery.Focus();
		}

		private void layoutViewGalery_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			if (!_allowToSave) return;
			var imageSource = layoutViewGallery.GetFocusedRow() as LinkImageSource;
			if (imageSource == null) return;
			if (e.PrevFocusedRowHandle == GridControl.InvalidRowHandle) return;
			if (SelectedImageChanged != null)
				SelectedImageChanged(this, new LinkImageEventArgs { Image = imageSource.Image, Text = imageSource.FileName });
		}

		private void gridViewGallery_Click(object sender, EventArgs e)
		{
			var pt = gridControlGallery.PointToClient(MousePosition);
			var hitInfo = layoutViewGallery.CalcHitInfo(pt);
			if (hitInfo.RowHandle == layoutViewGallery.FocusedRowHandle)
				layoutViewGalery_FocusedRowChanged(sender, new FocusedRowChangedEventArgs(hitInfo.RowHandle, hitInfo.RowHandle));
		}

		private void gridViewGallery_DoubleClick(object sender, EventArgs e)
		{
			var pt = gridControlGallery.PointToClient(MousePosition);
			if (!layoutViewGallery.CalcHitInfo(pt).InField) return;
			gridViewGallery_Click(sender, e);
			if (OnImageDoubleClick != null)
				OnImageDoubleClick(this, EventArgs.Empty);
		}

		private void gridControlGallery_MouseUp(object sender, MouseEventArgs e)
		{
			_hitInfo = layoutViewGallery.CalcHitInfo(e.Location);
			if (_hitInfo.InField && e.Button == MouseButtons.Right && !(_parent is FavoriteImageGroup))
				contextMenuStrip.Show(MousePosition);
		}

		private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_hitInfo == null) return;
			var selectedImage = layoutViewGallery.GetRow(_hitInfo.RowHandle) as LinkImageSource;
			if (selectedImage == null) return;
			selectedImage.CopyToFavs();
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			ToolTipControlInfo info = null;
			try
			{
				var view = gridControlGallery.GetViewAt(e.ControlMousePosition) as LayoutView;
				if (view == null) return;
				var hi = view.CalcHitInfo(e.ControlMousePosition);
				if (!hi.InFieldValue) return;
				var imageSource = view.GetRow(hi.RowHandle) as LinkImageSource;
				if (imageSource == null) return;
				info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), imageSource.FileName);
			}
			finally
			{
				e.Info = info;
			}
		}
	}

	public class LinkImageEventArgs : EventArgs
	{
		public Image Image { get; set; }
		public string Text { get; set; }
	}
}
