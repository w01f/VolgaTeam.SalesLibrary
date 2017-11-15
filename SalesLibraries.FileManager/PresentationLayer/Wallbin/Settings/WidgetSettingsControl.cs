using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings
{
	public partial class WidgetSettingsControl : UserControl
	{
		private bool _loading;
		private readonly WidgetSettings _data;
		private Image _originalImage;
		private string _originalImageName;

		public event EventHandler<CheckedChangedEventArgs> StateChanged;
		public event EventHandler<EventArgs> DoubleClicked;
		public event EventHandler<EventArgs> ControlClicked;

		public WidgetSettingsControl(WidgetSettings data)
		{
			_data = data;
			InitializeComponent();

			retractableBarGallery.AddButtons(new[]
				{
					new ButtonInfo
					{
						Logo = Resources.RetractableLogoGallery,
						Tooltip = "Expand gallery"
					}
				});

			retractableBarGallery.ContentSize = retractableBarGallery.Width;

			layoutControlGroupSearch.Enabled = false;
			layoutControlItemGallery.Enabled = false;
			layoutControlGroupCustomWidget.Enabled = false;

			layoutControlItemWidgetCustom.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWidgetCustom.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemWidgetNone.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWidgetNone.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemWidgetColorizeEditor.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWidgetColorizeEditor.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemWidgetColorizeEditor.MinSize = RectangleHelper.ScaleSize(layoutControlItemWidgetColorizeEditor.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemWidgetColorizeToggle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWidgetColorizeToggle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemWidgetColorizeToggle.MinSize = RectangleHelper.ScaleSize(layoutControlItemWidgetColorizeToggle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public void LoadData()
		{
			_loading = true;
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

			checkEditInvert.Checked = _data.Inverted;
			checkEditUseTextColor.Checked = _data.WidgetHolder != null && _data.WidgetHolder.UseTextColorForWidget;
			checkEditUseTextColor.Enabled = checkEditWidgetCustom.Checked && checkEditInvert.Checked;
			colorEditInversionColor.Enabled = checkEditWidgetCustom.Checked && checkEditInvert.Checked && !checkEditUseTextColor.Checked;
			colorEditInversionColor.EditValue = _data.InversionColor;
			_originalImage = _data.WidgetType == WidgetType.CustomWidget ? _data.Image : null;
			_originalImageName = _data.WidgetType == WidgetType.CustomWidget ? _data.ImageName : null;
			switch (_data.WidgetType)
			{
				case WidgetType.CustomWidget:
					checkEditWidgetCustom.Checked = true;
					checkEditWidgetNone.Checked = false;
					break;
				default:
					checkEditWidgetCustom.Checked = false;
					checkEditWidgetNone.Checked = true;
					break;
			}

			UpdateCustomDisplayImage();

			_loading = false;
		}

		public void SaveData()
		{
			if (_data == null) return;
			if (checkEditWidgetCustom.Checked)
			{
				_data.WidgetType = WidgetType.CustomWidget;
				_data.Inverted = checkEditInvert.Checked;
				if (_data.WidgetHolder != null)
					_data.WidgetHolder.UseTextColorForWidget = checkEditUseTextColor.Checked;
				_data.InversionColor = checkEditInvert.Checked
					? colorEditInversionColor.Color
					: GraphicObjectExtensions.DefaultReplaceColor;
				_data.Image = _originalImage;
				_data.ImageName = _originalImageName;
			}
			else if (checkEditWidgetNone.Checked)
			{
				_data.WidgetType = WidgetType.NoWidget;
				_data.Image = null;
				_data.ImageName = null;
			}
		}

		public void ChangeState(bool enable)
		{
			_loading = true;
			checkEditWidgetCustom.Checked = enable;
			checkEditWidgetNone.Checked = !enable;
			_loading = false;
		}

		public void UpdateColor(Color? textColor)
		{
			checkEditUseTextColor.Checked = textColor.HasValue;
			colorEditInversionColor.Enabled = checkEditWidgetCustom.Checked && checkEditInvert.Checked && !checkEditUseTextColor.Checked;
			colorEditInversionColor.EditValue = checkEditUseTextColor.Checked ? textColor : _data.InversionColor;
		}

		private void UpdateCustomDisplayImage()
		{
			if (_originalImage != null && checkEditInvert.Checked)
			{
				var imageClone = (Image)_originalImage.Clone();
				pictureEditCustomWidget.Image = imageClone.ReplaceColor(colorEditInversionColor.Color);
			}
			else
				pictureEditCustomWidget.Image = _originalImage;
		}

		private void OnWidgetTypeChanged(object sender, EventArgs e)
		{
			layoutControlItemGallery.Enabled = checkEditWidgetCustom.Checked;
			layoutControlGroupCustomWidget.Enabled = checkEditWidgetCustom.Checked;
			layoutControlGroupSearch.Enabled = checkEditWidgetCustom.Checked;

			colorEditInversionColor.Enabled = checkEditInvert.Checked;

			layoutControlItemUseTextColor.Enabled = checkEditWidgetCustom.Checked && checkEditInvert.Checked;

			if (!_loading)
			{
				UpdateCustomDisplayImage();
				StateChanged?.Invoke(this, new CheckedChangedEventArgs(checkEditWidgetCustom.Checked));
			}
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			if (!_loading)
			{
				_originalImage = e.Image;
				_originalImageName = e.Text;
				UpdateCustomDisplayImage();
			}
		}

		private void colorEditInversionColor_EditValueChanged(object sender, EventArgs e)
		{
			if (!_loading)
				UpdateCustomDisplayImage();
		}

		private void checkEditUseTextColor_CheckedChanged(object sender, EventArgs e)
		{
			if (!_loading)
				StateChanged?.Invoke(this, new CheckedChangedEventArgs(checkEditWidgetCustom.Checked));
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			DoubleClicked?.Invoke(this, EventArgs.Empty);
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

		private void OnFormClick(Object sender, EventArgs e)
		{
			ControlClicked?.Invoke(sender, e);
		}

		private void OnWidgetPictureMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var pictureEdit = sender as PictureEdit;
			if (checkEditInvert.Checked && pictureEdit?.Image != null && colorEditInversionColor.Color != Color.White)
				contextMenuStripImage.Show(Cursor.Position);
		}

		private void toolStripMenuItemImageAddToFavorites_Click(object sender, EventArgs e)
		{
			if (pictureEditCustomWidget.Image == null) return;
			var favoritesContainer = xtraTabControlGallery.TabPages.OfType<FavoritesImagesContainer>().FirstOrDefault();
			((FavoriteImageGroup)favoritesContainer?.ParentImageGroup)?.AddImage<Widget>(pictureEditCustomWidget.Image, String.Format("{0}_{1}", _originalImageName, colorEditInversionColor.Color.ToHex()));
		}

		private void contextMenuStripImage_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = !checkEditInvert.Checked || pictureEditCustomWidget.Image == null || colorEditInversionColor.Color == Color.White;
		}
	}
}