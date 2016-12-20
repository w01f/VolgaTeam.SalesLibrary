using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraTab;
using SalesLibraries.Common.Extensions;
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
					var tabPage = BaseLinkImagesContainer.Create(imageGroup);
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

			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				laWidgetDescription.Font = new Font(laWidgetDescription.Font.FontFamily, laWidgetDescription.Font.Size - 2, laWidgetDescription.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}

			_loading = false;
		}

		private void UpdateDisplayImage()
		{
			if (OriginalImage != null && checkEditInvert.Checked)
			{
				var imageClone = (Image)OriginalImage.Clone();
				pbSelectedWidget.Image = colorEditInversionColor.Color != GraphicObjectExtensions.DefaultInversionColor
					? imageClone.ReplaceColor(colorEditInversionColor.Color)
					: imageClone.Invert();
			}
			else
				pbSelectedWidget.Image = OriginalImage;
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			Close();
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			OriginalImage= e.Image;
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

		}

		private void contextMenuStripImage_Opening(object sender, CancelEventArgs e)
		{

		}
	}
}