using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using Manina.Windows.Forms;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.ImageGallery
{
	[ToolboxItem(false)]
	//public partial class LinkImagesContainer : UserControl
	public abstract partial class BaseLinkImagesContainer : XtraTabPage
	{
		protected ImageListView.HitInfo _menuHitInfo;
		private bool _initialized;

		public ImageSourceGroup ParentImageGroup { get; private set; }

		public event EventHandler<LinkImageEventArgs> SelectedImageChanged;
		public event EventHandler<EventArgs> OnImageDoubleClick;

		public BaseImageSource SelectedImageSource
		{
			get
			{
				return imageListView.SelectedItems.Count > 0 ?
					imageListView.SelectedItems.Select(item => (BaseImageSource)item.Tag).FirstOrDefault() :
					null;
			}
		}

		protected BaseLinkImagesContainer(ImageSourceGroup parent)
		{
			InitializeComponent();
			ParentImageGroup = parent;
			Text = ParentImageGroup.Name;
			ParentImageGroup.DataChanged += OnParentImageGroupChanged;
			imageListView.ItemClick += OnGalleryItemClick;
			imageListView.ItemDoubleClick += OnGalleryItemDoubleClick;
			imageListView.ItemHover += OnGalleryItemHover;
			imageListView.MouseDown += OnGalleryMouseDown;
			Disposed += OnDisposed;
		}

		public static BaseLinkImagesContainer Create(ImageSourceGroup parent)
		{
			if (parent is FavoriteImageGroup)
				return new FavoritesImagesContainer(parent);
			if (parent is RegularImageGroup)
				return new RegularImagesContainer(parent);
			if (parent is SearchResultsImageGroup)
				return new SearchResultsImagesContainer(parent);
			throw new ArgumentOutOfRangeException("There is no container control for image group");
		}

		public void Init()
		{
			if(_initialized) return;
			Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			InitPopupMenu();
			LoadImages();
			_initialized = true;
			Cursor = Cursors.Default;
		}

		public void Release()
		{
			imageListView.ClearSelection();
			imageListView.Items.Clear();
			ParentImageGroup = null;
		}

		protected abstract void InitPopupMenu();

		protected virtual void LoadImages()
		{
			imageListView.Items.Clear();
			imageListView.Items.AddRange(ParentImageGroup.Images.Select(ims => new ImageListViewItem(ims.FilePath, ims.FileName) { Tag = ims }).ToArray());
		}

		private void OnDisposed(object sender, EventArgs e)
		{
			ParentImageGroup.DataChanged -= OnParentImageGroupChanged;
		}

		private void OnParentImageGroupChanged(Object sender, EventArgs e)
		{
			LoadImages();
		}

		private void OnGalleryItemClick(object sender, ItemClickEventArgs e)
		{
			imageListView.ClearSelection();
			e.Item.Selected = true;
			var linkImage = SelectedImageSource;
			SelectedImageChanged?.Invoke(this, new LinkImageEventArgs { Image = Image.FromFile(linkImage.FilePath), Text = linkImage.FileName });
		}

		private void OnGalleryItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			OnGalleryItemClick(sender, e);
			OnImageDoubleClick?.Invoke(this, EventArgs.Empty);
		}

		private void OnGalleryItemHover(object sender, ItemHoverEventArgs e)
		{
			toolTip.RemoveAll();
			var sourceItem = e.Item?.Tag as BaseImageSource;
			if (sourceItem == null) return;
			var toolTipText = sourceItem.FileName;
			toolTip.SetToolTip(imageListView, toolTipText);
		}

		private void OnGalleryMouseDown(object sender, MouseEventArgs e)
		{
			_menuHitInfo = null;
			ImageListView.HitInfo hitInfo;
			imageListView.HitTest(new Point(e.X, e.Y), out hitInfo);
			if (ModifierKeys != Keys.None) return;
			if (!hitInfo.InItemArea) return;
			switch (e.Button)
			{
				case MouseButtons.Right:
					_menuHitInfo = hitInfo;
					contextMenuStrip.Show(MousePosition);
					break;
			}
		}
	}

	public class LinkImageEventArgs : EventArgs
	{
		public Image Image { get; set; }
		public string Text { get; set; }
	}
}
