using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using FileManager.PresentationClasses.Tags;
using FileManager.PresentationClasses.WallBin;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.Properties;
using SalesDepot.CommonGUI.RetractableBar;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.TabPages
{
	[ToolboxItem(false)]
	public partial class TabHomeControl : UserControl
	{
		private readonly WallBinOptions _wallBinOptions = new WallBinOptions();
		private WallBinTreeListControl _treeList;
		private readonly CategoriesEditor _categoriesEditor = new CategoriesEditor();
		private readonly SuperFiltersEditor _superFiltersEditor = new SuperFiltersEditor();
		private readonly KeywordsEditor _keywordsEditor = new KeywordsEditor();
		private readonly SecurityEditor _securityEditor = new SecurityEditor();
		private readonly TagsCleaner _tagsCleaner = new TagsCleaner();

		public TabHomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnMain.Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			retractableBar.StateChanged += retractableBar_StateChanged;
		}
		#region Page Initialization
		public void InitPage()
		{
			InitLibrariesSelector();
			InitPageSelector();

			LoadTreeView();

			LoadTagsEditors();

			UpdateControlsState();

			SuperFilterControl.Init();

			pnContainer.Resize += (sender, args) => MainController.Instance.WallbinController.ResizeActivePage();
		}

		private void InitLibrariesSelector()
		{
			LoadLibrariesList();
			FormMain.Instance.comboBoxEditLibraries.EditValueChanging += (sender, e) => { e.Cancel = MainController.Instance.SaveLibraryWarning(); };
			FormMain.Instance.comboBoxEditLibraries.EditValueChanged += (sender, e) => MainController.Instance.WallbinController.SelectLibrary(FormMain.Instance.comboBoxEditLibraries.EditValue as Library);
			MainController.Instance.LibraryChanged += LibraryChanged;
		}

		private void LoadLibrariesList()
		{
			FormMain.Instance.comboBoxEditLibraries.Properties.Items.Clear();
			IEnumerable<Library> libraries = MainController.Instance.WallbinController.GetLibraries();
			if (libraries != null)
			{
				FormMain.Instance.comboBoxEditLibraries.Properties.Items.AddRange(libraries.ToArray());
				FormMain.Instance.comboBoxEditLibraries.EditValue = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.Library : null;
			}
			FormMain.Instance.comboBoxEditLibraries.Enabled = libraries != null && libraries.Count() > 1;
		}

		public void LibraryChanged(object sender, EventArgs e)
		{
			LoadPageList();
			LoadTreeView();
			UpdateControlsState();
		}

		private void InitPageSelector()
		{
			LoadPageList();
			FormMain.Instance.comboBoxEditPages.EditValueChanged += (sender, e) => MainController.Instance.WallbinController.SelectPage(FormMain.Instance.comboBoxEditPages.EditValue as LibraryPage);
		}

		private void LoadPageList()
		{
			FormMain.Instance.comboBoxEditPages.Properties.Items.Clear();
			var pages = MainController.Instance.WallbinController.GetPages();
			if (pages != null)
				FormMain.Instance.comboBoxEditPages.Properties.Items.AddRange(pages.ToArray());
			FormMain.Instance.comboBoxEditPages.EditValue = pages != null && MainController.Instance.ActiveDecorator != null ? (MainController.Instance.ActiveDecorator.ActivePage != null ? MainController.Instance.ActiveDecorator.ActivePage.Page : null) : null;
			FormMain.Instance.comboBoxEditPages.Enabled = pages != null && pages.Count() > 1;
		}

		private void UpdateControlsState()
		{
			barCheckItemTabs.Checked = SettingsManager.Instance.MultitabView;
			var activeLibrary = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.Library : null;
			if (activeLibrary == null) return;
			if (!activeLibrary.UseDirectAccess)
			{
				btSetupWallBin.Visible = !activeLibrary.IsConfigured;
				btSetupWallBin.BringToFront();
				retractableBar.Visible = true;
				FormMain.Instance.ribbonBarHomeAddLink.Enabled = true;
				FormMain.Instance.buttonItemHomeDelete.Enabled = true;
				FormMain.Instance.ribbonBarHomeLibraries.Enabled = true;
				barButtonItemLinkUp.Enabled = true;
				barButtonItemLinkDown.Enabled = true;
				FormMain.Instance.buttonItemHomeOpen.Enabled = true;
				FormMain.Instance.ribbonBarHomeLinkProperties.Enabled = true;
				FormMain.Instance.ribbonBarHomeSave.Enabled = true;
				FormMain.Instance.ribbonBarPreferencesAutoWidgets.Enabled = true;
				FormMain.Instance.ribbonBarSettingsBranding.Enabled = true;
				FormMain.Instance.ribbonBarPreferencesColumns.Enabled = true;
				FormMain.Instance.ribbonBarPreferencesDeadLinks.Enabled = true;
				FormMain.Instance.ribbonBarPreferencesEmailList.Enabled = true;
				FormMain.Instance.ribbonBarPreferencesPages.Enabled = true;
				barButtonItemFontUp.Enabled = true;
				barButtonItemFontDown.Enabled = true;
				barCheckItemTabs.Enabled = true;
				barMinibar.Visible = true;
				if (SettingsManager.Instance.TreeViewVisible)
					retractableBar.Expand(true);
				else
					retractableBar.Collapse(true);
				UpdateFontButtonStatus();
			}
			else
			{
				retractableBar.Visible = false;
				btSetupWallBin.Visible = false;
				FormMain.Instance.ribbonBarHomeAddLink.Enabled = false;
				FormMain.Instance.buttonItemHomeDelete.Enabled = false;
				FormMain.Instance.ribbonBarHomeLibraries.Enabled = false;
				barButtonItemLinkUp.Enabled = false;
				barButtonItemLinkDown.Enabled = false;
				FormMain.Instance.buttonItemHomeOpen.Enabled = false;
				FormMain.Instance.ribbonBarHomeLinkProperties.Enabled = false;
				FormMain.Instance.ribbonBarHomeSave.Enabled = false;
				FormMain.Instance.ribbonBarPreferencesAutoWidgets.Enabled = false;
				FormMain.Instance.ribbonBarSettingsBranding.Enabled = false;
				FormMain.Instance.ribbonBarPreferencesColumns.Enabled = false;
				FormMain.Instance.ribbonBarPreferencesDeadLinks.Enabled = false;
				FormMain.Instance.ribbonBarPreferencesEmailList.Enabled = false;
				FormMain.Instance.ribbonBarPreferencesPages.Enabled = false;
				barButtonItemFontUp.Enabled = false;
				barButtonItemFontDown.Enabled = false;
				barCheckItemTabs.Enabled = false;
				barMinibar.Visible = false;
			}
			MainController.Instance.WallbinController.ResizeActivePage();
		}

		public void UpdateFontButtonStatus()
		{
			barButtonItemFontUp.Enabled = SettingsManager.Instance.FontSize < 20;
			barButtonItemFontDown.Enabled = SettingsManager.Instance.FontSize > 8;
		}

		public void ApplyWallBinOptions(WallBinOptions options)
		{
			_wallBinOptions.AllowEdit = options.AllowEdit;
			_wallBinOptions.AllowMultiSelect = options.AllowMultiSelect;
			_wallBinOptions.ShowFiles = options.ShowFiles;
			_wallBinOptions.ShowTagsEditor = options.ShowTagsEditor;
			_wallBinOptions.ShowCategoryTags = options.ShowCategoryTags;
			_wallBinOptions.ShowSuperFilterTags = options.ShowSuperFilterTags;
			_wallBinOptions.ShowKeywordTags = options.ShowKeywordTags;
			_wallBinOptions.ShowSecurityTags = options.ShowSecurityTags;

			if (_wallBinOptions.ShowTagsEditor)
			{
				if (_wallBinOptions.ShowSecurityTags)
					retractableBar.AddButtons(new[] { new ButtonInfo { Logo = Resources.RetractableLogoSecurity, Tooltip = "Expand security settings" } });
				else
					retractableBar.AddButtons(new[] { new ButtonInfo { Logo = Resources.RetractableLogoTags, Tooltip = "Expand tags settings" } });
				SwitchTagsEditor();
			}
			else
			{
				retractableBar.AddButtons(new[] { new ButtonInfo { Logo = Resources.RetractableLogoFiles, Tooltip = "Expand file list" } });
				if (_treeList != null)
					_treeList.BringToFront();
			}
			MainController.Instance.WallbinController.ResizeActivePage();
		}
		#endregion

		#region TreeView Processing
		private void LoadTreeView()
		{
			if (_treeList != null)
			{
				_treeList.Parent = null;
				_treeList.Dispose();
			}
			var activeLibrary = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.Library : null;
			if (activeLibrary == null) return;
			_treeList = new WallBinTreeListControl();
			_treeList.Init(activeLibrary);
			if (activeLibrary.UseDirectAccess)
			{
				pnMain.Controls.Add(_treeList);
				_treeList.BringToFront();
			}
			else
				retractableBar.Content.Controls.Add(_treeList);
		}

		private void retractableBar_StateChanged(object sender, StateChangedEventArgs e)
		{
			SettingsManager.Instance.TreeViewVisible = e.Expaned;
			SettingsManager.Instance.Save();
			MainController.Instance.ActiveDecorator.ActivePage.ResizePage();
		}
		#endregion

		#region Tags Manager Processing
		public ITagsEditor ActiveTagsEditor { get; set; }

		private void LoadTagsEditors()
		{
			if (!retractableBar.Content.Controls.Contains(_categoriesEditor))
				retractableBar.Content.Controls.Add(_categoriesEditor);
			if (!retractableBar.Content.Controls.Contains(_superFiltersEditor))
				retractableBar.Content.Controls.Add(_superFiltersEditor);
			if (!retractableBar.Content.Controls.Contains(_keywordsEditor))
				retractableBar.Content.Controls.Add(_keywordsEditor);
			if (!retractableBar.Content.Controls.Contains(_securityEditor))
				retractableBar.Content.Controls.Add(_securityEditor);
			if (!retractableBar.Content.Controls.Contains(_tagsCleaner))
				retractableBar.Content.Controls.Add(_tagsCleaner);
			SwitchTagsEditor();
		}

		public void SwitchTagsEditor()
		{
			if (_wallBinOptions.ShowCategoryTags)
				ActiveTagsEditor = _categoriesEditor;
			else if (_wallBinOptions.ShowSuperFilterTags)
				ActiveTagsEditor = _superFiltersEditor;
			else if (_wallBinOptions.ShowKeywordTags)
				ActiveTagsEditor = _keywordsEditor;
			else if (_wallBinOptions.ShowSecurityTags)
				ActiveTagsEditor = _securityEditor;
			else if (_wallBinOptions.ShowTagsEditor)
				ActiveTagsEditor = _tagsCleaner;
			else
				ActiveTagsEditor = null;
			if (ActiveTagsEditor != null)
			{
				ActiveTagsEditor.UpdateData();
				(ActiveTagsEditor as UserControl).BringToFront();
			}
		}
		#endregion

		#region Header Processing
		public void UpdateLinkInfo(LibraryLink link)
		{
			labelControlSelectedLink.Text = link != null ? String.Format("<b>{0}</b>{1}",
				link.Name,
				link.SearchTags != null && !String.IsNullOrEmpty(link.SearchTags.AllTags) ? String.Format("{0}({1})", Environment.NewLine, link.SearchTags.AllTags) : String.Empty) :
				String.Empty;
		}

		public void UpdateTagCountInfo(string infoString, Color color = default(Color))
		{
			if (String.IsNullOrEmpty(infoString))
				labelControlTagCountInfo.Visible = false;
			else
			{
				labelControlTagCountInfo.Visible = true;
				labelControlTagCountInfo.Text = infoString;
				labelControlTagCountInfo.ForeColor = color;
			}
		}
		#endregion

		private void TabHomeControl_Resize(object sender, EventArgs e)
		{
			barMinibar.BeginUpdate();
			barMinibar.Offset = (Width - 115) / 2;
			barMinibar.ApplyDockRowCol();
			barMinibar.EndUpdate();
		}
	}
}