using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using FileManager.PresentationClasses.Tags;
using FileManager.PresentationClasses.WallBin;
using FileManager.PresentationClasses.WallBin.Decorators;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.TabPages
{
	[ToolboxItem(false)]
	public partial class TabHomeControl : UserControl
	{
		private readonly WallBinOptions _wallBinOptions = new WallBinOptions();
		private WallBinTreeListControl _treeList;
		private readonly CategoriesEditor _categoriesEditor = new CategoriesEditor();
		private readonly KeywordsEditor _keywordsEditor = new KeywordsEditor();
		private readonly FileCardsEditor _fileCardsEditor = new FileCardsEditor();
		private readonly AttachmentsEditor _attachmentsEditor = new AttachmentsEditor();
		private readonly TagsCleaner _tagsCleaner = new TagsCleaner();

		public TabHomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnMain.Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
		}

		#region Page Initialization
		public void InitPage()
		{
			InitLibrariesSelector();
			InitPageSelector();

			LoadTreeView();

			LoadTagsEditors();

			UpdateControlsState();

			splitContainerControl.Panel2.Resize += (sender, args) => MainController.Instance.WallbinController.ResizeActivePage();
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
			FormMain.Instance.buttonItemSettingsMultitab.Checked = SettingsManager.Instance.MultitabView;
			FormMain.Instance.buttonItemHomeFileTreeView.Checked = SettingsManager.Instance.TreeViewVisible;
			var activeLibrary = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.Library : null;
			if (activeLibrary != null)
			{
				if (!activeLibrary.UseDirectAccess)
				{
					btSetupWallBin.Visible = !activeLibrary.IsConfigured;
					btSetupWallBin.BringToFront();

					FormMain.Instance.buttonItemHomeFileTreeView.Enabled = true;
					FormMain.Instance.ribbonBarHomeAddLink.Enabled = true;
					FormMain.Instance.ribbonBarHomeDelete.Enabled = true;
					FormMain.Instance.ribbonBarHomeFileTreeView.Enabled = true;
					FormMain.Instance.ribbonBarHomeFontSize.Enabled = true;
					FormMain.Instance.ribbonBarHomeLibraries.Enabled = true;
					FormMain.Instance.ribbonBarHomeNudge.Enabled = true;
					FormMain.Instance.ribbonBarHomeOpen.Enabled = true;
					FormMain.Instance.ribbonBarHomeProperties.Enabled = true;
					FormMain.Instance.ribbonBarHomeSave.Enabled = true;
					FormMain.Instance.ribbonBarSettingsAutoWidgets.Enabled = true;
					FormMain.Instance.ribbonBarSettingsBranding.Enabled = true;
					FormMain.Instance.ribbonBarSettingsColumns.Enabled = true;
					FormMain.Instance.ribbonBarSettingsDeadLinks.Enabled = true;
					FormMain.Instance.ribbonBarSettingsEmailList.Enabled = true;
					FormMain.Instance.ribbonBarSettingsMultitab.Enabled = true;
					FormMain.Instance.ribbonBarSettingsPages.Enabled = true;

					ShowDockPanel(_wallBinOptions.ShowFiles && (SettingsManager.Instance.TreeViewVisible || activeLibrary.UseDirectAccess));

					UpdateFontButtonStatus();
				}
				else
				{
					btSetupWallBin.Visible = false;
					FormMain.Instance.buttonItemHomeFileTreeView.Enabled = false;
					FormMain.Instance.ribbonBarHomeAddLink.Enabled = false;
					FormMain.Instance.ribbonBarHomeDelete.Enabled = false;
					FormMain.Instance.ribbonBarHomeFileTreeView.Enabled = false;
					FormMain.Instance.ribbonBarHomeFontSize.Enabled = false;
					FormMain.Instance.ribbonBarHomeLibraries.Enabled = false;
					FormMain.Instance.ribbonBarHomeNudge.Enabled = false;
					FormMain.Instance.ribbonBarHomeOpen.Enabled = false;
					FormMain.Instance.ribbonBarHomeProperties.Enabled = false;
					FormMain.Instance.ribbonBarHomeSave.Enabled = false;
					FormMain.Instance.ribbonBarSettingsAutoWidgets.Enabled = false;
					FormMain.Instance.ribbonBarSettingsBranding.Enabled = false;
					FormMain.Instance.ribbonBarSettingsColumns.Enabled = false;
					FormMain.Instance.ribbonBarSettingsDeadLinks.Enabled = false;
					FormMain.Instance.ribbonBarSettingsEmailList.Enabled = false;
					FormMain.Instance.ribbonBarSettingsMultitab.Enabled = false;
					FormMain.Instance.ribbonBarSettingsPages.Enabled = false;
					ShowDockPanel(false);
				}
				MainController.Instance.WallbinController.ResizeActivePage();
			}
		}

		public void UpdateFontButtonStatus()
		{
			FormMain.Instance.buttonItemHomeFontUp.Enabled = SettingsManager.Instance.FontSize < 20;
			FormMain.Instance.buttonItemHomeFontDown.Enabled = SettingsManager.Instance.FontSize > 8;
		}

		public void ApplyWallBinOptions(WallBinOptions options)
		{
			_wallBinOptions.AllowEdit = options.AllowEdit;
			_wallBinOptions.AllowMultiSelect = options.AllowMultiSelect;
			_wallBinOptions.ShowFiles = options.ShowFiles;
			_wallBinOptions.ShowTagsEditor = options.ShowTagsEditor;
			_wallBinOptions.ShowCategoryTags = options.ShowCategoryTags;
			_wallBinOptions.ShowKeywordTags = options.ShowKeywordTags;
			_wallBinOptions.ShowFileCardTags = options.ShowFileCardTags;
			_wallBinOptions.ShowAttachmentTags = options.ShowAttachmentTags;

			var activeLibrary = MainController.Instance.ActiveDecorator != null ? MainController.Instance.ActiveDecorator.Library : null;
			ShowDockPanel(_wallBinOptions.ShowFiles && (SettingsManager.Instance.TreeViewVisible || (activeLibrary != null && activeLibrary.UseDirectAccess)));
			ShowTagsEditor(_wallBinOptions.ShowTagsEditor);
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
				dockPanelTreeView_Container.Controls.Add(_treeList);
		}

		public void ShowDockPanel(bool show)
		{
			dockPanelTreeView.DockChanged -= dockPanelTreeView_DockChanged;
			if (show)
				dockPanelTreeView.Show();
			else
				dockPanelTreeView.Hide();
			dockPanelTreeView.Dock = SettingsManager.Instance.TreeViewDocked ? DockingStyle.Left : DockingStyle.Float;
			dockPanelTreeView.FloatLocation = new Point(200, 200);
			if (show)
				dockPanelTreeView.DockChanged += dockPanelTreeView_DockChanged;
		}

		private void dockPanelTreeView_DockChanged(object sender, EventArgs e)
		{
			MainController.Instance.WallbinController.ResizeActivePage();
			SettingsManager.Instance.TreeViewDocked = dockPanelTreeView.Dock == DockingStyle.Left;
			SettingsManager.Instance.Save();
		}

		private void dockManager_Sizing(object sender, SizingEventArgs e)
		{
			if (e.Panel.Name.Equals("dockPanelTreeView") && (e.NewSize.Width < 350 || e.NewSize.Height < 450))
				e.Cancel = true;
		}

		private void dockPanelTreeView_ClosedPanel(object sender, DockPanelEventArgs e)
		{
			FormMain.Instance.buttonItemHomeFileTreeView.Checked = false;
		}

		private void dockPanelTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			dockPanelTreeView.Dock = DockingStyle.Left;
		}
		#endregion

		#region Tags Manager Processing
		public ITagsEditor ActiveTagsEditor { get; private set; }

		private void LoadTagsEditors()
		{
			if (!splitContainerControl.Panel1.Controls.Contains(_categoriesEditor))
				splitContainerControl.Panel1.Controls.Add(_categoriesEditor);
			if (!splitContainerControl.Panel1.Controls.Contains(_keywordsEditor))
				splitContainerControl.Panel1.Controls.Add(_keywordsEditor);
			if (!splitContainerControl.Panel1.Controls.Contains(_fileCardsEditor))
				splitContainerControl.Panel1.Controls.Add(_fileCardsEditor);
			if (!splitContainerControl.Panel1.Controls.Contains(_attachmentsEditor))
				splitContainerControl.Panel1.Controls.Add(_attachmentsEditor);
			if (!splitContainerControl.Panel1.Controls.Contains(_tagsCleaner))
				splitContainerControl.Panel1.Controls.Add(_tagsCleaner);
			SwitchTagsEditor();
		}

		private void ShowTagsEditor(bool show)
		{
			if (show)
			{
				SwitchTagsEditor();
				splitContainerControl.PanelVisibility = SplitPanelVisibility.Both;
			}
			else
				splitContainerControl.PanelVisibility = SplitPanelVisibility.Panel2;
		}

		public void SwitchTagsEditor()
		{
			if (SettingsManager.Instance.EnableTagsCategories)
				ActiveTagsEditor = _categoriesEditor;
			else if (SettingsManager.Instance.EnableTagsKeywords)
				ActiveTagsEditor = _keywordsEditor;
			else if (SettingsManager.Instance.EnableTagsFileCards)
				ActiveTagsEditor = _fileCardsEditor;
			else if (SettingsManager.Instance.EnableTagsAttachments)
				ActiveTagsEditor = _attachmentsEditor;
			else if (SettingsManager.Instance.EnableTagsCleaner)
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
	}
}