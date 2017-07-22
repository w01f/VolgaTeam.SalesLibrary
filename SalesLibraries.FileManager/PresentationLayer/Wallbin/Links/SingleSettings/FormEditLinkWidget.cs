﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
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
		private bool _allowHandleEvents;
		private Image _originalImage;
		private string _originalImageName;
		private readonly BaseLibraryLink _sourceLink;
		private readonly ILinksGroup _sourceLinkGroup;

		public LinkSettingsType[] EditableSettings => new[]
		{
			LinkSettingsType.Widget,
		};

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

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				radioButtonWidgetTypeAuto.Font = new Font(radioButtonWidgetTypeAuto.Font.FontFamily, radioButtonWidgetTypeAuto.Font.Size - 2,
					radioButtonWidgetTypeAuto.Font.Style);
				radioButtonWidgetTypeCustom.Font = new Font(radioButtonWidgetTypeCustom.Font.FontFamily, radioButtonWidgetTypeCustom.Font.Size - 2,
					radioButtonWidgetTypeCustom.Font.Style);
				radioButtonWidgetTypeDisabled.Font = new Font(radioButtonWidgetTypeDisabled.Font.FontFamily, radioButtonWidgetTypeDisabled.Font.Size - 2,
					radioButtonWidgetTypeDisabled.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXSearch.Font = new Font(buttonXSearch.Font.FontFamily, buttonXSearch.Font.Size - 2, buttonXSearch.Font.Style);
			}
		}

		public FormEditLinkWidget(BaseLibraryLink sourceLink) : this()
		{
			_sourceLink = sourceLink;
		}

		public FormEditLinkWidget(ILinksGroup linkGroup, FileTypes? defaultLinkType = null) : this()
		{
			_sourceLinkGroup = linkGroup;
			_sourceLink = _sourceLinkGroup.AllGroupLinks
				.FirstOrDefault(link => !defaultLinkType.HasValue || link.Type == defaultLinkType.Value);
		}

		public void InitForm<TEditControl>(LinkSettingsType settingsType) where TEditControl : ILinkSettingsEditControl
		{
			Width = 960;
			Height = 590;
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
			if (!String.IsNullOrEmpty(objectLink?.AutoWidgetKey))
			{
				radioButtonWidgetTypeAuto.Visible = true;
				pnAutoWidget.Visible = true;
				if (objectLink.Widget.HasAutoWidget)
				{
					pbAutoWidget.Visible = true;
					pbAutoWidget.Image = objectLink.Widget.AutoWidget;
					laExtension.Text = objectLink.AutoWidgetKey.ToLower();
				}
				else
				{
					pbAutoWidget.Visible = false;
					laExtension.Text = "Not Assigned";
				}
			}
			else
			{
				radioButtonWidgetTypeAuto.Visible = false;
				pnAutoWidget.Visible = false;
			}

			checkEditInvert.Checked = _sourceLink.Widget.Inverted;
			colorEditInversionColor.EditValue = _sourceLink.Widget.InversionColor;
			_originalImage = _sourceLink.Widget.WidgetType == WidgetType.CustomWidget ? _sourceLink.Widget.Image : null;
			_originalImageName = _sourceLink.Widget.WidgetType == WidgetType.CustomWidget ? _sourceLink.Widget.ImageName : null;
			switch (_sourceLink.Widget.WidgetType)
			{
				case WidgetType.AutoWidget:
					radioButtonWidgetTypeAuto.Checked = true;
					radioButtonWidgetTypeCustom.Checked = false;
					radioButtonWidgetTypeDisabled.Checked = false;
					break;
				case WidgetType.CustomWidget:
					radioButtonWidgetTypeAuto.Checked = false;
					radioButtonWidgetTypeCustom.Checked = true;
					radioButtonWidgetTypeDisabled.Checked = false;
					break;
				case WidgetType.NoWidget:
					radioButtonWidgetTypeAuto.Checked = false;
					radioButtonWidgetTypeCustom.Checked = false;
					radioButtonWidgetTypeDisabled.Checked = true;
					break;
			}
			UpdateCustomDisplayImage();

			_allowHandleEvents = true;
		}

		private void SaveData()
		{
			foreach (var link in (_sourceLinkGroup?.AllGroupLinks ?? new[] { _sourceLink }).ToList())
			{
				if (radioButtonWidgetTypeAuto.Checked)
				{
					link.Widget.WidgetType = WidgetType.AutoWidget;
					link.Widget.Image = null;
					link.Widget.ImageName = null;
				}
				else if (radioButtonWidgetTypeCustom.Checked)
				{
					link.Widget.WidgetType = WidgetType.CustomWidget;
					link.Widget.Inverted = checkEditInvert.Checked;
					link.Widget.InversionColor = colorEditInversionColor.Color != GraphicObjectExtensions.DefaultInversionColor
						? colorEditInversionColor.Color
						: GraphicObjectExtensions.DefaultInversionColor;
					link.Widget.Image = _originalImage;
					link.Widget.ImageName = _originalImageName;
				}
				else if (radioButtonWidgetTypeDisabled.Checked)
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

		private void FormEditLinkSettings_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
				SaveData();
		}

		private void OnWidgetTypeChanged(object sender, EventArgs e)
		{
			pbAutoWidget.Enabled = radioButtonWidgetTypeAuto.Checked;

			pbCustomWidget.Enabled = radioButtonWidgetTypeCustom.Checked;
			pnGallery.Enabled = radioButtonWidgetTypeCustom.Checked;
			pnSearch.Enabled = radioButtonWidgetTypeCustom.Checked;
			checkEditInvert.Enabled = radioButtonWidgetTypeCustom.Checked;
			colorEditInversionColor.Enabled = radioButtonWidgetTypeCustom.Checked && checkEditInvert.Checked;

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