using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Layout;
using DevExpress.XtraGrid.Views.Layout.ViewInfo;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;
using SalesDepot.ToolForms.Gallery;
using Vintasoft.Imaging;
using Vintasoft.Imaging.VisualTools;

namespace SalesDepot.PresentationClasses.Gallery
{
	public partial class FavoriteImagesControl : UserControl
	{
		private FavoriteImagesManager _manager;
		private LayoutViewHitInfo _menuHitInfo;
		private ImageViewer _imageContainer;
		private int _zoomIndex;

		public FavoriteImagesControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			ViewMode.Click += ViewMode_Click;
			ViewMode.CheckedChanged += ViewMode_CheckedChanged;
			EditMode.Click += ViewMode_Click;
			EditMode.CheckedChanged += ViewMode_CheckedChanged;
			ImageSelect.Click += ImageSelect_Click;
			ImageCrop.Click += ImageCrop_Click;
			ZoomIn.Click += ZoomIn_Click;
			ZoomOut.Click += ZoomOut_Click;
			Copy.Click += Copy_Click;
		}

		#region Buttons
		public RibbonBar ImageBar
		{
			get { return FormMain.Instance.ribbonBarFavoritesImage; }
		}
		public RibbonBar ZoomBar
		{
			get { return FormMain.Instance.ribbonBarFavoritesZoom; }
		}
		public RibbonBar CopyBar
		{
			get { return FormMain.Instance.ribbonBarFavoritesCopy; }
		}
		public ButtonItem ViewMode
		{
			get { return FormMain.Instance.buttonItemFavoritesView; }
		}
		public ButtonItem EditMode
		{
			get { return FormMain.Instance.buttonItemFavoritesEdit; }
		}
		public ButtonItem ImageSelect
		{
			get { return FormMain.Instance.buttonItemFavoritesImageSelect; }
		}
		public ButtonItem ImageCrop
		{
			get { return FormMain.Instance.buttonItemFavoritesImageCrop; }
		}
		public ButtonItem ZoomIn
		{
			get { return FormMain.Instance.buttonItemFavoritesZoomIn; }
		}
		public ButtonItem ZoomOut
		{
			get { return FormMain.Instance.buttonItemFavoritesZoomOut; }
		}
		public ButtonItem Copy
		{
			get { return FormMain.Instance.buttonItemFavoritesCopy; }
		}
		#endregion

		public void Init()
		{
			_manager = FavoriteImagesManager.Instance;
			_manager.CollectionChanged += (o, e) =>
			{
				gridControlLogoGallery.DataSource = _manager.Images;
				gridControlLogoGallery.RefreshDataSource();
			};
			gridControlLogoGallery.DataSource = _manager.Images;

			_imageContainer = new ImageViewer();
			Controls.Add(_imageContainer);
			_imageContainer.Dock = DockStyle.Fill;
			var menuItemCopy = new MenuItem("Copy to Clipboard");
			menuItemCopy.Click += Copy_Click;
			var menuItemFavorites = new MenuItem("Save to my Favorites");
			menuItemFavorites.Click += Favorites_Click;
			_imageContainer.ContextMenu = new ContextMenu(new[] { menuItemCopy, menuItemFavorites });

			ViewMode_CheckedChanged(null, EventArgs.Empty);
			ViewMode_Click(ViewMode, EventArgs.Empty);
		}

		public void AddToFavorites(Image image, string defaultName)
		{
			using (var form = new FormAddFavoriteImage(image, defaultName, _manager.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Add Image to Favorites";
				form.laTitle.Text = "Save this Image in your Favorites folder for future presentations";
				if (form.ShowDialog() != DialogResult.OK) return;
				_manager.SaveImage(image, form.ImageName);
			}
		}

		private void Control_Resize(object sender, EventArgs e)
		{
			var p = ClientRectangle.GetCenter();
			var tmp = circularProgressWebpage.ClientRectangle.GetCenter();
			p.Offset(-tmp.X, -tmp.Y);
			circularProgressWebpage.Location = p;
		}

		#region Editor Processing
		private void LoadImage(Image image)
		{
			_zoomIndex = 100;
			ChangeTool(null);
			_imageContainer.Image = null;
			if (image != null)
				_imageContainer.Image = new VintasoftImage(image, true);
			EditMode.Enabled = _imageContainer.Image != null;
		}

		private void ChangeTool(VisualTool tool)
		{
			_imageContainer.VisualTool = _imageContainer.VisualTool != null && tool != null && tool.GetType() == _imageContainer.VisualTool.GetType() ? null : tool;
			ImageCrop.Checked = (_imageContainer.VisualTool as CropSelectionTool) != null;
			ImageSelect.Checked = (_imageContainer.VisualTool as RectangularSelectionToolWithCopyPaste) != null;
		}

		private void ShowProgress()
		{
			circularProgressWebpage.BringToFront();
			circularProgressWebpage.Visible = true;
			circularProgressWebpage.IsRunning = true;
			circularProgressWebpage.BringToFront();
		}

		private void HideProgress()
		{
			circularProgressWebpage.IsRunning = false;
			circularProgressWebpage.Visible = false;
		}

		private void ViewMode_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null) return;
			if (button.Checked) return;
			ViewMode.Checked = false;
			EditMode.Checked = false;
			button.Checked = true;
		}

		private void ViewMode_CheckedChanged(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null) return;
			if (!button.Checked) return;
			CopyBar.Enabled = false;
			ImageBar.Enabled = false;
			ZoomBar.Enabled = false;
			if (ViewMode.Checked)
			{
				gridControlLogoGallery.BringToFront();
			}
			else if (EditMode.Checked)
			{
				CopyBar.Enabled = true;
				ImageBar.Enabled = true;
				ZoomBar.Enabled = true;
				_imageContainer.BringToFront();
			}
		}

		private void ImageSelect_Click(object sender, EventArgs e)
		{
			ChangeTool(new RectangularSelectionToolWithCopyPaste { Cursor = Cursors.Cross });
		}

		private void ImageCrop_Click(object sender, EventArgs e)
		{
			ChangeTool(new CropSelectionTool { Cursor = Cursors.Cross });
		}

		private void ZoomIn_Click(object sender, EventArgs e)
		{
			_imageContainer.ChangeZoom(_zoomIndex += 10, _imageContainer.ClientRectangle.GetCenter());
		}

		private void ZoomOut_Click(object sender, EventArgs e)
		{
			_imageContainer.ChangeZoom((_zoomIndex > 10 ? (_zoomIndex -= 10) : 10), _imageContainer.ClientRectangle.GetCenter());
		}

		private void Copy_Click(object sender, EventArgs e)
		{
			_imageContainer.DoCopy();
		}

		private void Favorites_Click(object sender, EventArgs e)
		{
			_imageContainer.DoCopy();
			var image = Clipboard.GetImage();
			if (image != null)
				AddToFavorites(image, null);
		}
		#endregion

		#region Grid Processing
		private void layoutViewLogoGallery_MouseDown(object sender, MouseEventArgs e)
		{
			var view = sender as LayoutView;
			if (view == null) return;
			var hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
			if (ModifierKeys != Keys.None)
				return;
			if (!hitInfo.InCard) return;
			switch (e.Button)
			{
				case MouseButtons.Right:
					_menuHitInfo = hitInfo;
					contextMenuStrip.Show(MousePosition);
					break;
			}
		}

		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			var imageSource = layoutViewLogoGallery.GetRow(_menuHitInfo.RowHandle) as ImageSource;
			if (imageSource == null || !imageSource.ContainsData) return;
			Clipboard.SetImage(imageSource.BigImage);
		}

		private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
		{
			var imageSource = layoutViewLogoGallery.GetRow(_menuHitInfo.RowHandle) as ImageSource;
			if (imageSource == null) return;
			var image = imageSource.BigImage.Clone() as Image;

			ShowProgress();
			var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)(() => LoadImage(image))));
			Application.DoEvents();
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			HideProgress();

			ViewMode_Click(EditMode, EventArgs.Empty);

		}

		private void toolStripMenuItemRename_Click(object sender, EventArgs e)
		{
			var imageSource = layoutViewLogoGallery.GetRow(_menuHitInfo.RowHandle) as ImageSource;
			if (imageSource == null) return;
			var image = imageSource.BigImage.Clone() as Image;
			using (var form = new FormAddFavoriteImage(image, null, _manager.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Rename Favorite Image";
				form.laTitle.Text = "Set new name for your Favorite Image";
				if (form.ShowDialog() != DialogResult.OK) return;
				_manager.DeleteImage(imageSource);
				_manager.SaveImage(image, form.ImageName);
			}
			_menuHitInfo = null;
		}

		private void toolStripMenuItemDelete_Click(object sender, EventArgs e)
		{
			var imageSource = layoutViewLogoGallery.GetRow(_menuHitInfo.RowHandle) as ImageSource;
			if (imageSource == null) return;
			if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to delete image?") != DialogResult.Yes) return;
			_manager.DeleteImage(imageSource);
			_menuHitInfo = null;
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			var view = gridControlLogoGallery.GetViewAt(e.ControlMousePosition) as LayoutView;
			if (view == null) return;
			var hi = view.CalcHitInfo(e.ControlMousePosition);
			if (!hi.InFieldValue) return;
			var imageSource = view.GetRow(hi.RowHandle) as ImageSource;
			e.Info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), Path.GetFileName(imageSource.FileName));
		}
		#endregion
	}
}
