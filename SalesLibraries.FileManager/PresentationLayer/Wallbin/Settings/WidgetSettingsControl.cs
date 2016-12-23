using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Filtering;
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

				radioButtonWidgetTypeCustom.Font = new Font(radioButtonWidgetTypeCustom.Font.FontFamily, radioButtonWidgetTypeCustom.Font.Size - 2, radioButtonWidgetTypeCustom.Font.Style);
				radioButtonWidgetTypeDisabled.Font = new Font(radioButtonWidgetTypeDisabled.Font.FontFamily, radioButtonWidgetTypeDisabled.Font.Size - 2, radioButtonWidgetTypeDisabled.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}
		}

		public void LoadData()
		{
			_loading = true;
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

			checkEditInvert.Checked = _data.Inverted;
			colorEditInversionColor.EditValue = _data.InversionColor;
			_originalImage = _data.WidgetType == WidgetType.CustomWidget ? _data.Image : null;
			_originalImageName = _data.WidgetType == WidgetType.CustomWidget ? _data.ImageName : null;
			switch (_data.WidgetType)
			{
				case WidgetType.CustomWidget:
					radioButtonWidgetTypeCustom.Checked = true;
					radioButtonWidgetTypeDisabled.Checked = false;
					break;
				case WidgetType.NoWidget:
					radioButtonWidgetTypeCustom.Checked = false;
					radioButtonWidgetTypeDisabled.Checked = true;
					break;
			}

			UpdateCustomDisplayImage();

			_loading = false;
		}

		public void SaveData()
		{
			if (_data == null) return;
			if (radioButtonWidgetTypeCustom.Checked)
			{
				_data.WidgetType = WidgetType.CustomWidget;
				_data.Inverted = checkEditInvert.Checked;
				_data.InversionColor = colorEditInversionColor.Color != GraphicObjectExtensions.DefaultInversionColor
					? colorEditInversionColor.Color
					: GraphicObjectExtensions.DefaultInversionColor;
				_data.Image = _originalImage;
				_data.ImageName = _originalImageName;
			}
			else if (radioButtonWidgetTypeDisabled.Checked)
			{
				_data.WidgetType = WidgetType.NoWidget;
				_data.Image = null;
				_data.ImageName = null;
			}
		}

		public void ChangeState(bool enable)
		{
			_loading = true;
			radioButtonWidgetTypeCustom.Checked = enable;
			radioButtonWidgetTypeDisabled.Checked = !enable;
			_loading = false;
		}

		private void UpdateCustomDisplayImage()
		{
			if (_originalImage != null && checkEditInvert.Checked)
			{
				var imageClone = (Image)_originalImage.Clone();
				pbCustomWidget.Image = colorEditInversionColor.Color != GraphicObjectExtensions.DefaultInversionColor
					? imageClone.ReplaceColor(colorEditInversionColor.Color)
					: imageClone.Invert();
			}
			else
				pbCustomWidget.Image = _originalImage;
		}

		private void OnWidgetTypeChanged(object sender, EventArgs e)
		{
			pbCustomWidget.Enabled = radioButtonWidgetTypeCustom.Checked;
			pnSearch.Enabled = radioButtonWidgetTypeCustom.Checked;
			checkEditInvert.Enabled = radioButtonWidgetTypeCustom.Checked;
			colorEditInversionColor.Enabled = radioButtonWidgetTypeCustom.Checked && checkEditInvert.Checked;
			pnGallery.Enabled = radioButtonWidgetTypeCustom.Checked;
			if (!_loading)
			{
				UpdateCustomDisplayImage();
				StateChanged?.Invoke(this, new CheckedChangedEventArgs(radioButtonWidgetTypeCustom.Checked));
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
			if(!_loading)
				UpdateCustomDisplayImage();
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

		private void toolStripMenuItemImageAddToFavorites_Click(object sender, EventArgs e)
		{
			if (pbCustomWidget.Image == null) return;
			var favoritesContainer = xtraTabControlGallery.TabPages.OfType<FavoritesImagesContainer>().FirstOrDefault();
			((FavoriteImageGroup)favoritesContainer?.ParentImageGroup)?.AddImage<Widget>(pbCustomWidget.Image, String.Format("{0}_{1}", _originalImageName, colorEditInversionColor.Color.ToHex()));
		}

		private void contextMenuStripImage_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = !checkEditInvert.Checked || pbCustomWidget.Image == null || colorEditInversionColor.Color == Color.White;
		}
	}
}