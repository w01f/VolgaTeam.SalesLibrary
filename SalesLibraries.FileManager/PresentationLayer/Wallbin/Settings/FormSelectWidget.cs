using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraTab;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class FormSelectWidget : MetroForm
	{
		private bool _loading;
		public Image OriginalImage { get; set; }
		public string OriginalImageName { get; set; }

		public FormSelectWidget()
		{
			InitializeComponent();

			_loading = true;

			retractableBarGallery.AddButtons(new[]
				{
					new ButtonInfo
					{
						Logo = Resources.RetractableLogoGallery,
						Tooltip = "Expand gallery"
					}
				});

			xtraTabControlGallery.TabPages.Clear();
			xtraTabControlGallery.TabPages.AddRange(
				MainController.Instance.Lists.Widgets.Items.Select(imageGroup =>
				{
					var tabPage = BaseLinkImagesContainer.Create<Widget>(imageGroup);
					tabPage.SelectedImageChanged += OnSelectedWidgetChanged;
					tabPage.OnImageDoubleClick += OnImageDoubleClick;
					return (XtraTabPage)tabPage;
				}).ToArray()
			);
			xtraTabControlGallery.SelectedPageChanged += (o, e) =>
			{
				((BaseLinkImagesContainer)e.Page).Init();
				var galleryNode = e.Page.Tag as TreeNode;
				if (galleryNode == null)
				{
					galleryNode = treeViewGallery.Nodes.Insert(xtraTabControlGallery.TabPages.IndexOf(e.Page), e.Page.Text);
					galleryNode.Tag = e.Page;
					e.Page.Tag = galleryNode;
					_loading = true;
					treeViewGallery.SelectedNode = galleryNode;
					_loading = false;
				}
			};
			((BaseLinkImagesContainer)xtraTabControlGallery.SelectedTabPage).Init();
			foreach (var galleryPage in xtraTabControlGallery.TabPages.OfType<BaseLinkImagesContainer>().ToList())
			{
				if (!galleryPage.PageVisible) continue;
				var galleryNode = treeViewGallery.Nodes.Add(galleryPage.Text);
				galleryNode.Tag = galleryPage;
				galleryPage.Tag = galleryNode;
				if (xtraTabControlGallery.SelectedTabPage == galleryPage)
				{
					treeViewGallery.SelectedNode = galleryNode;
					labelControlSelectedGalleryName.Text = galleryPage.Text;
				}
			}
			treeViewGallery.AfterSelect += (o, e) =>
			{
				var galleryPage = treeViewGallery.SelectedNode?.Tag as XtraTabPage;
				labelControlSelectedGalleryName.Text = galleryPage?.Text;
				if (_loading) return;
				xtraTabControlGallery.SelectedTabPage = galleryPage;
			};

			_loading = false;

			retractableBarGallery.ContentSize = retractableBarGallery.Width;

			simpleLabelItemTitle.MaxSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			simpleLabelItemTitle.MinSize = RectangleHelper.ScaleSize(simpleLabelItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExtension.MaxSize = RectangleHelper.ScaleSize(layoutControlItemExtension.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExtension.MinSize = RectangleHelper.ScaleSize(layoutControlItemExtension.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void UpdateDisplayImage()
		{
			if (OriginalImage != null && checkEditInvert.Checked)
			{
				var imageClone = (Image)OriginalImage.Clone();
				labelControlExtension.Appearance.Image = imageClone.ReplaceColor(colorEditInversionColor.Color);
			}
			else
				labelControlExtension.Appearance.Image = OriginalImage;
			simpleLabelItemTitle.AppearanceItemCaption.ForeColor = OriginalImage != null && checkEditInvert.Checked
				? colorEditInversionColor.Color
				: Color.Black;
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			OriginalImage = e.Image;
			OriginalImageName = e.Text;
			UpdateDisplayImage();
		}

		private void OnSearchButtonClick(object sender, EventArgs e)
		{
			var keyword = textEditSearch.EditValue as String;
			if (String.IsNullOrEmpty(keyword)) return;
			MainController.Instance.Lists.Widgets.SearchResults.LoadImages(keyword);
		}

		private void OnSearchEditValueChanged(object sender, EventArgs e)
		{
			buttonXSearch.Enabled = !String.IsNullOrEmpty(textEditSearch.EditValue as String);
		}

		private void OnSearchKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				OnSearchButtonClick(sender, EventArgs.Empty);
		}

		private void OnFormClick(object sender, EventArgs e)
		{
			buttonXOK.Focus();
		}

		private void colorEditInversionColor_EditValueChanged(object sender, EventArgs e)
		{
			UpdateDisplayImage();
		}

		private void checkEditInvert_CheckedChanged(object sender, EventArgs e)
		{
			UpdateDisplayImage();
			colorEditInversionColor.Enabled = checkEditInvert.Checked;
		}

		private void FormSelectWidget_Load(object sender, EventArgs e)
		{
			UpdateDisplayImage();
		}

		private void toolStripMenuItemImageAddToFavorites_Click(object sender, EventArgs e)
		{
			if (labelControlExtension.Appearance.Image == null) return;
			var favoritesContainer = xtraTabControlGallery.TabPages.OfType<FavoritesImagesContainer>().FirstOrDefault();
			((FavoriteImageGroup)favoritesContainer?.ParentImageGroup)?.AddImage<Widget>(labelControlExtension.Appearance.Image, String.Format("{0}_{1}", OriginalImageName, colorEditInversionColor.Color.ToHex()));
		}

		private void labelControlExtension_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var viewInfo = labelControlExtension.GetViewInfo() as LabelControlViewInfo;
			if (viewInfo == null) return;
			if (viewInfo.ImageBounds.Contains(e.Location) && checkEditInvert.Checked && labelControlExtension.Appearance.Image != null && colorEditInversionColor.Color != Color.White)
				contextMenuStripImage.Show(Cursor.Position);
		}
	}
}