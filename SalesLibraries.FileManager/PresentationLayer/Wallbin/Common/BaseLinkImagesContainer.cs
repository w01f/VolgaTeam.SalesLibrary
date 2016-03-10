﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using Manina.Windows.Forms;
using SalesLibraries.Common.Objects.Graphics;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Common
{
	[ToolboxItem(false)]
	//public partial class LinkImagesContainer : UserControl
	public abstract partial class BaseLinkImagesContainer : XtraTabPage
	{
		private readonly LinkImageGroup _parent;
		protected ImageListView.HitInfo _menuHitInfo;
		private bool _initialized;

		public event EventHandler<LinkImageEventArgs> SelectedImageChanged;
		public event EventHandler<EventArgs> OnImageDoubleClick;

		protected LinkImageSource SelectedImageSource
		{
			get
			{
				return imageListView.SelectedItems.Count > 0 ?
					imageListView.SelectedItems.Select(item => (LinkImageSource)item.Tag).FirstOrDefault() :
					null;
			}
		}

		protected BaseLinkImagesContainer(LinkImageGroup parent)
		{
			InitializeComponent();
			_parent = parent;
			Text = _parent.Name;
			_parent.OnDataChanged += (o, e) => LoadImages();
			imageListView.ItemClick += OnGalleryItemClick;
			imageListView.ItemDoubleClick += OnGalleryItemDoubleClick;
			imageListView.ItemHover += OnGalleryItemHover;
			imageListView.MouseDown += OnGalleryMouseDown;
			imageListView.MouseMove += OnGalleryMouseMove;
		}

		public static BaseLinkImagesContainer Create(LinkImageGroup parent)
		{
			if (parent is FavoriteImageGroup)
				return new FavoritesImagesContainer(parent);
			return new RegularImagesContainer(parent);
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

		protected abstract void InitPopupMenu();

		private void LoadImages()
		{
			imageListView.Items.Clear();
			imageListView.Items.AddRange(_parent.Images.Select(ims => new ImageListViewItem(ims.FilePath, ims.FileName) { Tag = ims }).ToArray());
		}

		private void OnGalleryItemClick(object sender, ItemClickEventArgs e)
		{
			imageListView.ClearSelection();
			e.Item.Selected = true;
			var linkImage = SelectedImageSource;
			if (SelectedImageChanged != null)
				SelectedImageChanged(this, new LinkImageEventArgs { Image = Image.FromFile(linkImage.FilePath), Text = linkImage.FileName });
		}

		private void OnGalleryItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			OnGalleryItemClick(sender, e);
			if (OnImageDoubleClick != null)
				OnImageDoubleClick(this, EventArgs.Empty);
		}

		private void OnGalleryItemHover(object sender, ItemHoverEventArgs e)
		{
			toolTip.RemoveAll();
			var sourceItem = e.Item != null ? e.Item.Tag as LinkImageSource : null;
			if (sourceItem == null) return;
			var toolTipText = sourceItem.FileName;
			toolTip.SetToolTip(imageListView, toolTipText);
		}

		private void OnGalleryMouseMove(object sender, MouseEventArgs e)
		{
			imageListView.Focus();
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