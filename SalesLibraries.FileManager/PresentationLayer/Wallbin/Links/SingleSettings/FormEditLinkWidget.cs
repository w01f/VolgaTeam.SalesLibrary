using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.Graphics;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.ImageGallery;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	public partial class FormEditLinkWidget : MetroForm, ILinkSetSettingsEditForm
	{
		private const string ImageTitleFormat = "<size=+4>{0}</size><br><color=lightgray>{1}</color>";

		private bool _allowHandleEvents;
		private Image _originalImage;
		private string _originalImageName;
		private readonly BaseLibraryLink _sourceLink;
		private readonly ILinksGroup _sourceLinkGroup;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Widget,
		};

		private string WidgetTitle
			=>
				_sourceLinkGroup != null
					? String.Format("Library links ({0})", _sourceLinkGroup.AllGroupLinks.Count())
					: _sourceLink.LinkInfoDisplayName;

		public FormEditLinkWidget()
		{
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
			layoutControlGroupColorize.Enabled = false;

			layoutControlItemTitle.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTitle.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemTitle.MinSize = RectangleHelper.ScaleSize(layoutControlItemTitle.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utils.GetScaleFactor(CreateGraphics().DpiX));
		}

		public FormEditLinkWidget(BaseLibraryLink sourceLink) : this()
		{
			_sourceLink = sourceLink;
			labelControlTitle.Text = String.Format(ImageTitleFormat, WidgetTitle, String.Empty);
		}

		public FormEditLinkWidget(ILinksGroup linkGroup, LinkType? defaultLinkType = null) : this()
		{
			_sourceLinkGroup = linkGroup;
			_sourceLink = _sourceLinkGroup.AllGroupLinks
				.FirstOrDefault(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value);
			labelControlTitle.Text = String.Format(ImageTitleFormat, WidgetTitle, String.Empty);
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			FormStateHelper.Init(this, RemoteResourceManager.Instance.AppAliasSettingsFolder, "Site Admin-Link-Widget", false, false);
			Text = String.Format(Text,
				_sourceLinkGroup != null ?
					String.Format("{0} links", _sourceLinkGroup.AllGroupLinks.Count()) :
					_sourceLink.ToString());
			StartPosition = FormStartPosition.CenterParent;
			LoadData();
		}

		private void LoadData()
		{
			_allowHandleEvents = false;

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
					_allowHandleEvents = false;
					treeViewGallery.SelectedNode = galleryNode;
					_allowHandleEvents = true;
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
				if (!_allowHandleEvents) return;
				xtraTabControlGallery.SelectedTabPage = galleryPage;
			};

			LibraryObjectLink objectLink;
			if (_sourceLinkGroup != null)
			{
				objectLink = _sourceLinkGroup.AllGroupLinks.All(link => link.Type == _sourceLink.Type)
					? _sourceLink as LibraryObjectLink
					: null;
			}
			else
				objectLink = _sourceLink as LibraryObjectLink;
			layoutControlItemWidgetAuto.Visibility = !String.IsNullOrEmpty(objectLink?.AutoWidgetKey) ?
				LayoutVisibility.Always :
				LayoutVisibility.Never;

			checkEditInvert.Checked = _sourceLink.Widget.Inverted;
			colorEditInversionColor.EditValue = _sourceLink.Widget.InversionColor;
			_originalImage = _sourceLink.Widget.WidgetType == WidgetType.CustomWidget ? _sourceLink.Widget.Image : null;
			_originalImageName = _sourceLink.Widget.WidgetType == WidgetType.CustomWidget ? _sourceLink.Widget.ImageName : null;
			switch (_sourceLink.Widget.WidgetType)
			{
				case WidgetType.AutoWidget:
					checkEditWidgetAuto.Checked = true;
					checkEditWidgetCustom.Checked = false;
					checkEditWidgetNone.Checked = false;
					break;
				case WidgetType.CustomWidget:
					checkEditWidgetAuto.Checked = false;
					checkEditWidgetCustom.Checked = true;
					checkEditWidgetNone.Checked = false;
					break;
				case WidgetType.NoWidget:
					checkEditWidgetAuto.Checked = false;
					checkEditWidgetCustom.Checked = false;
					checkEditWidgetNone.Checked = true;
					break;
			}

			UpdateCustomDisplayImage();

			_allowHandleEvents = true;
		}

		private void SaveData()
		{
			foreach (var link in (_sourceLinkGroup?.AllGroupLinks ?? new[] { _sourceLink }).ToList())
			{
				if (checkEditWidgetAuto.Checked)
				{
					link.Widget.WidgetType = WidgetType.AutoWidget;
					link.Widget.Image = null;
					link.Widget.ImageName = null;
				}
				else if (checkEditWidgetCustom.Checked)
				{
					link.Widget.WidgetType = WidgetType.CustomWidget;
					link.Widget.Inverted = checkEditInvert.Checked;
					link.Widget.InversionColor = checkEditInvert.Checked
						? colorEditInversionColor.Color
						: GraphicObjectExtensions.DefaultReplaceColor;
					link.Widget.Image = _originalImage;
					link.Widget.ImageName = _originalImageName;
				}
				else if (checkEditWidgetNone.Checked)
				{
					link.Widget.WidgetType = WidgetType.NoWidget;
					link.Widget.Image = null;
					link.Widget.ImageName = null;
				}
				link.Banner.Enable = !link.Widget.Enabled && link.Banner.Enable;
				link.Thumbnail.Enable = !link.Widget.Enabled && link.Thumbnail.Enable;
			}
		}

		private void UpdateCustomDisplayImage()
		{
			Image displayImage = null;
			var displayImageName = String.Empty;
			if (checkEditWidgetCustom.Checked)
			{
				if (_originalImage != null && checkEditInvert.Checked)
				{
					var imageClone = (Image)_originalImage.Clone();
					displayImage = imageClone.ReplaceColor(colorEditInversionColor.Color);
				}
				else
					displayImage = _originalImage;
				displayImageName = _originalImageName;
			}
			else if (checkEditWidgetAuto.Checked)
			{
				LibraryObjectLink objectLink;
				if (_sourceLinkGroup != null)
				{
					objectLink = _sourceLinkGroup.AllGroupLinks.All(link => link.Type == _sourceLink.Type)
						? _sourceLink as LibraryObjectLink
						: null;
				}
				else
					objectLink = _sourceLink as LibraryObjectLink;
				if (!String.IsNullOrEmpty(objectLink?.AutoWidgetKey) && objectLink.Widget.HasAutoWidget)
				{
					displayImage = objectLink.Widget.AutoWidget;
					displayImageName = objectLink?.AutoWidgetKey;
				}
			}
			labelControlTitle.Appearance.Image = displayImage;
			labelControlTitle.Text = String.Format(ImageTitleFormat, WidgetTitle, displayImageName);
			labelControlTitle.ForeColor = checkEditWidgetCustom.Checked && displayImage != null && checkEditInvert.Checked ? colorEditInversionColor.Color : Color.Black;
		}

		private void FormEditLinkSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
		}

		private void OnWidgetTypeChanged(object sender, EventArgs e)
		{
			layoutControlItemGallery.Enabled = checkEditWidgetCustom.Checked;
			layoutControlGroupSearch.Enabled = checkEditWidgetCustom.Checked;
			layoutControlGroupColorize.Enabled = checkEditWidgetCustom.Checked;

			colorEditInversionColor.Enabled = checkEditInvert.Checked;

			if (_allowHandleEvents)
				UpdateCustomDisplayImage();
		}

		private void OnSelectedWidgetChanged(object sender, LinkImageEventArgs e)
		{
			if (_allowHandleEvents)
			{
				_originalImage = e.Image;
				_originalImageName = e.Text;
				UpdateCustomDisplayImage();
			}
		}

		private void colorEditInversionColor_EditValueChanged(object sender, EventArgs e)
		{
			if (_allowHandleEvents)
				UpdateCustomDisplayImage();
		}

		private void OnImageDoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
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

		private void labelControlTitle_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right) return;
			var viewInfo = labelControlTitle.GetViewInfo() as LabelControlViewInfo;
			if (viewInfo == null) return;
			if (viewInfo.ImageBounds.Contains(e.Location) && checkEditInvert.Checked && labelControlTitle.Appearance.Image != null && colorEditInversionColor.Color != Color.White)
				contextMenuStripImage.Show(Cursor.Position);
		}

		private void toolStripMenuItemImageAddToFavorites_Click(object sender, EventArgs e)
		{
			if (labelControlTitle.Appearance.Image == null) return;
			var favoritesContainer = xtraTabControlGallery.TabPages.OfType<FavoritesImagesContainer>().FirstOrDefault();
			((FavoriteImageGroup)favoritesContainer?.ParentImageGroup)?.AddImage<Banner>(labelControlTitle.Appearance.Image, String.Format("{0}_{1}", _originalImageName, colorEditInversionColor.Color.ToHex()));
		}

		private void contextMenuStripImage_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = !checkEditInvert.Checked || labelControlTitle.Appearance.Image == null || colorEditInversionColor.Color == Color.White;
		}
	}
}