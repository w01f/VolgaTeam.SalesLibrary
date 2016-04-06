using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Business.Synchronization;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.Controllers
{
	[ToolboxItem(false)]
	public partial class WallbinPage : UserControl, IPageController
	{
		private bool _isLoading;
		private GroupSettingsManager _groupSettingsManager;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public WallbinPage()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			pnContainer.Dock = DockStyle.Fill;
			pnEmpty.BringToFront();
			NeedToUpdate = true;
		}

		public void InitController()
		{
			DataStateObserver.Instance.DataChanged += (o, e) =>
			{
				switch (e.ChangeType)
				{
					case DataChangeType.LibrarySelected:
						OnLibraryChanged(o, e);
						break;
					case DataChangeType.LinksDeleted:
						MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
						break;
				}
			};

			MainController.Instance.MainForm.buttonItemHomeSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemPreferencesSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemCalendarSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemProgramManagerSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemVideoSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemTagsSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemSecuritySync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemSettingsSync.Click += OnSyncClick;

			#region Link Operations
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesTags.Visible = MainController.Instance.Settings.EditorSettings.EnableTagsEdit;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesSecurity.Visible = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit;

			MainController.Instance.MainForm.buttonItemHomeAddUrl.Click += buttonItemHomeAddUrl_Click;
			MainController.Instance.MainForm.buttonItemHomeAddLineBreak.Click += buttonItemHomeAddLineBreak_Click;

			MainController.Instance.MainForm.buttonItemHomeLinkOpen.Click += buttonItemHomeLinkOpen_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkDelete.Click += buttonItemHomeLinkDelete_Click;

			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesNotes.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesTags.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesExpirationDate.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesSecurity.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesWidget.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesBanner.Click += buttonItemHomeLinkSettings_Click;
			#endregion

			#region Wallbin Settings
			MainController.Instance.MainForm.buttonItemHomeSettings.Click += OnWallbinSettingsClick;
			MainController.Instance.MainForm.buttonItemHomeZoomIn.Click += OnWallbinFontUpClick;
			MainController.Instance.MainForm.buttonItemHomeZoomOut.Click += OnWallbinFontDownClick;
			UpdateFontButtons();
			#endregion

			#region General Settings
			MainController.Instance.MainForm.buttonItemPreferencesPages.Click += buttonItemPreferencesPages_Click;
			MainController.Instance.MainForm.buttonItemPreferencesColumns.Click += buttonItemPreferencesColumns_Click;
			MainController.Instance.MainForm.buttonItemPreferencesAutoWidgets.Click += buttonItemPreferencesAutoWidgets_Click;
			MainController.Instance.MainForm.buttonItemPreferencesDeadLinks.Click += buttonItemPreferencesDeadLinks_Click;
			MainController.Instance.MainForm.buttonItemPreferencesEmailList.Click += buttonItemPreferencesEmailList_Click;
			MainController.Instance.MainForm.buttonItemSettingsLibraries.Click += buttonItemSettingsLibraries_Click;
			MainController.Instance.MainForm.buttonItemSettingsSyncSettings.Click += buttonItemSettingsSync_Click;
			MainController.Instance.MainForm.buttonItemSettingsAdvanced.Click += buttonItemSettingsAdvanced_Click;
			#endregion

			#region Tags
			MainController.Instance.MainForm.buttonItemTagsCategories.Click += buttonItemTagsSync_Click;
			MainController.Instance.MainForm.buttonItemTagsCategories.CheckedChanged += buttonItemTagsCategories_CheckedChanged;
			MainController.Instance.MainForm.buttonItemTagsKeywords.Click += buttonItemTagsSync_Click;
			MainController.Instance.MainForm.buttonItemTagsKeywords.CheckedChanged += buttonItemTagsKeywords_CheckedChanged;
			MainController.Instance.MainForm.buttonItemTagsSuperFilters.Click += buttonItemTagsSync_Click;
			MainController.Instance.MainForm.buttonItemTagsSuperFilters.CheckedChanged += buttonItemTagsSuperFilters_CheckedChanged;
			MainController.Instance.MainForm.buttonItemTagsClear.Click += buttonItemTagsSync_Click;
			MainController.Instance.MainForm.buttonItemTagsClear.CheckedChanged += buttonItemTagsClear_CheckedChanged;
			#endregion

			#region Security
			MainController.Instance.MainForm.buttonItemSecuritySelect.Click += buttonItemSecuritySelect_Click;
			MainController.Instance.MainForm.buttonItemSecurityReset.Click += buttonItemSecurityReset_Click;
			#endregion

			#region Program Data
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsEnabled.Click += buttonItemProgramManagerSync_Click;
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsDisabled.Click += buttonItemProgramManagerSync_Click;
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsEnabled.CheckedChanged += buttonItemProgramManagerSync_CheckedChanged;
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsDisabled.CheckedChanged += buttonItemProgramManagerSync_CheckedChanged;
			MainController.Instance.MainForm.buttonEditProgramManagerLocation.ButtonClick += buttonEditProgramManagerLocation_ButtonClick;
			#endregion

			MainController.Instance.WallbinViews.FormatState.StateChanged += OnFormatStateChanged;
			MainController.Instance.WallbinViews.Selection.SelectionChanged += OnSelectionChanged;
			MainController.Instance.WallbinViews.DataChanged += OnLibraryDataChanged;

			if (MainController.Instance.Settings.TreeViewVisible)
				retractableBar.Expand(true);
			else
				retractableBar.Collapse(true);
			retractableBar.StateChanged += OnRetractableBarStateChanged;

			_groupSettingsManager = new GroupSettingsManager();
			_groupSettingsManager.ChangesMade += OnLinkGroupChangesMade;

			superFilterControl.Init();
			superFilterControl.EditorChanged += OnLinkSuperFilterChanged;

			pnTagInfoContainer.Visible = MainController.Instance.Lists.SearchTags.TagCount;
		}

		public void ShowPage(TabPageEnum pageType)
		{
			IsActive = true;

			if (!MainController.Instance.MainForm.pnContainer.Controls.Contains(this))
				MainController.Instance.MainForm.pnContainer.Controls.Add(this);
			pnContainer.BringToFront();
			BringToFront();

			if (NeedToUpdate)
			{
				NeedToUpdate = false;
				OnLibraryChanged(this, EventArgs.Empty);
			}

			_isLoading = true;
			MainController.Instance.WallbinViews.Selection.Reset();
			MainController.Instance.WallbinViews.FormatState.SwitchAccordingPage(pageType);
			MainController.Instance.MainForm.buttonItemTagsCategories.Checked = MainController.Instance.WallbinViews.FormatState.ShowCategoryTags;
			MainController.Instance.MainForm.buttonItemTagsKeywords.Checked = MainController.Instance.WallbinViews.FormatState.ShowKeywordTags;
			MainController.Instance.MainForm.buttonItemTagsSuperFilters.Checked = MainController.Instance.WallbinViews.FormatState.ShowSuperFilterTags;
			MainController.Instance.MainForm.buttonItemTagsClear.Checked = MainController.Instance.WallbinViews.FormatState.ShowTagsCleaner;

			UpdateLinkButtons();

			_isLoading = false;
		}

		public void ProcessChanges()
		{
			if (!IsActive) return;
			MainController.Instance.WallbinViews.SaveActiveWallbin();
		}

		public void UpdateWallbin()
		{
			pnEmpty.BringToFront();
			MainController.Instance.WallbinViews.ActiveWallbin.LoadView(true);
			OnLibraryChanged(this, EventArgs.Empty);
			pnContainer.BringToFront();
		}

		private void UpdateLinkButtons()
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			var selectedLink = selectedFolder != null ?
				MainController.Instance.WallbinViews.Selection.SelectedFolder.SelectedLinkRow :
				null;

			MainController.Instance.MainForm.buttonItemHomeAddUrl.Enabled =
			MainController.Instance.MainForm.buttonItemHomeAddLineBreak.Enabled = selectedFolder != null;

			MainController.Instance.MainForm.buttonItemHomeLinkDelete.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesNotes.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesTags.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesSecurity.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesWidget.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesBanner.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkOpen.Enabled = selectedLink != null;
			MainController.Instance.MainForm.buttonItemHomeLinkOpen.Enabled = selectedLink != null && selectedLink.IsOpenable;

			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesExpirationDate.Enabled =
				selectedLink != null &&
				!(selectedLink.Source is LineBreak);
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			if (!IsActive)
			{
				NeedToUpdate = true;
				return;
			}
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
			{
				_isLoading = true;
				pnEmpty.BringToFront();
				var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
				if (activeWallbin == null) return;
				if (!pnContainer.Controls.Contains((Control)activeWallbin))
					pnContainer.Controls.Add((Control)activeWallbin);

				if (!pnTagInfoContainer.Controls.Contains(activeWallbin.TagInfoControl))
					pnTagInfoContainer.Controls.Add(activeWallbin.TagInfoControl);

				activeWallbin.ShowView();
				UpdateRetractableBarContent();
				UpdateProgramDataSettings();
				((Control)activeWallbin).BringToFront();
				pnContainer.BringToFront();
				_isLoading = false;
			})));
		}

		private void OnLibraryDataChanged(object sender, EventArgs e)
		{
			linkInfoControl.UpdateData();
		}

		private void OnFormatStateChanged(object sender, EventArgs e)
		{
			UpdateRetractableBarContent();
			superFilterControl.UpdateData();
			linkInfoControl.UpdateData();
		}

		private void OnSelectionChanged(object sender, SelectionEventArgs e)
		{
			UpdateLinkButtons();
			linkInfoControl.UpdateData();
			switch (e.SelectionType)
			{
				case SelectionEventType.SelectionReset:
					superFilterControl.Reset();
					break;
				case SelectionEventType.LinkSelected:
					superFilterControl.UpdateData();
					break;
			}
		}

		private void OnSyncClick(object sender, EventArgs e)
		{
			SyncManager.SyncRegular();
		}

		#region Link Operations Processing
		private void buttonItemHomeAddUrl_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			if (selectedFolder == null) return;
			selectedFolder.AddHyperLink();
		}

		private void buttonItemHomeAddLineBreak_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			if (selectedFolder == null) return;
			selectedFolder.AddLineBreak();
		}

		private void buttonItemHomeLinkOpen_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			if (selectedFolder == null) return;
			selectedFolder.OpenLink();
		}

		private void buttonItemHomeLinkDelete_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			if (selectedFolder == null) return;
			selectedFolder.DeleteLink();
		}

		private void buttonItemHomeLinkSettings_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			if (selectedFolder == null) return;
			var button = sender as ButtonItem;
			if (button == null || button.Tag == null) return;
			var propertiesType = (LinkSettingsType)Enum.Parse(typeof(LinkSettingsType), (String)button.Tag);
			selectedFolder.EditLinkSettings(propertiesType);
		}
		#endregion

		#region Settings Processing
		private void buttonItemPreferencesPages_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			var resut = library.PerformTransaction(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage,
				libraryCopy =>
				{
					using (var form = new FormPages())
					{
						form.Library = libraryCopy;
						return form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", cancelationToken => original.Save(context, current)));
			if (!resut) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateWallbin)));
		}

		private void buttonItemPreferencesColumns_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			var resut = library.PerformTransaction(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage,
				libraryCopy =>
				{
					using (var form = new FormColumns())
					{
						form.Library = libraryCopy;
						return form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", cancelationToken => original.Save(context, current)));
			if (!resut) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateWallbin)));
		}

		private void buttonItemPreferencesAutoWidgets_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			var resut = library.PerformTransaction(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage,
				libraryCopy =>
				{
					using (var form = new FormAutoWidgets())
					{
						form.Library = libraryCopy;
						return form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", cancelationToken => original.Save(context, current)));
			if (!resut) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateWallbin)));
		}

		private void buttonItemPreferencesDeadLinks_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			var resut = library.PerformTransaction(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage,
				libraryCopy =>
				{
					using (var form = new FormDeadLinks())
					{
						form.Library = libraryCopy;
						return form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", cancelationToken => original.Save(context, current)));
			if (!resut) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateWallbin)));
		}

		private void buttonItemPreferencesEmailList_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			library.PerformTransaction(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage,
				libraryCopy =>
				{
					using (var form = new FormEmailList())
					{
						form.Library = libraryCopy;
						return form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", cancelationToken => original.Save(context, current)));
		}

		private void buttonItemSettingsLibraries_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			using (var form = new FormPaths())
			{
				form.BackupPath = MainController.Instance.Settings.BackupPath;
				form.LocalSyncPath = MainController.Instance.Settings.NetworkPath;
				form.WebSyncPath = MainController.Instance.Settings.WebPath;
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				var backupChanged = !String.Equals(MainController.Instance.Settings.BackupPath, form.BackupPath, StringComparison.InvariantCultureIgnoreCase);
				MainController.Instance.Settings.BackupPath = form.BackupPath;
				MainController.Instance.Settings.NetworkPath = form.LocalSyncPath;
				MainController.Instance.Settings.WebPath = form.WebSyncPath;
				MainController.Instance.Settings.Save();
				if (!backupChanged) return;
				NeedToUpdate = true;
				MainController.Instance.ReloadData();
			}
		}

		private void buttonItemSettingsSync_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			library.PerformTransaction(MainController.Instance.WallbinViews.ActiveWallbin.DataStorage,
				libraryCopy =>
				{
					using (var form = new FormSync())
					{
						form.Library = libraryCopy;
						return form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", cancelationToken => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.RunInQueue("Saving Changes...", () => original.Save(context, current)));
		}

		private void buttonItemSettingsAdvanced_Click(object sender, EventArgs e)
		{
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			using (var form = new FormResetLibraryContent(library.Path))
			{
				form.ShowDialog(MainController.Instance.MainForm);
			}
		}
		#endregion

		#region Link Settings
		private void OnLinkGroupChangesMade(object sender, EventArgs e)
		{
			if (MainController.Instance.WallbinViews.ActiveWallbin == null) return;
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			if (MainController.Instance.WallbinViews.ActiveWallbin.ActivePage != null)
				MainController.Instance.WallbinViews.ActiveWallbin.ActivePage.UpdateView();
		}

		private void OnLinkSuperFilterChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.WallbinViews.ActiveWallbin == null) return;
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
		}
		#endregion

		#region Wallbin Settings
		private void UpdateFontButtons()
		{
			MainController.Instance.MainForm.buttonItemHomeZoomIn.Enabled = MainController.Instance.WallbinViews.FormatState.FontSize < 20;
			MainController.Instance.MainForm.buttonItemHomeZoomOut.Enabled = MainController.Instance.WallbinViews.FormatState.FontSize > 8;
		}

		private void OnWallbinFontUpClick(object sender, EventArgs e)
		{
			MainController.Instance.WallbinViews.FormatState.FontSize += 2;
			UpdateFontButtons();
		}

		private void OnWallbinFontDownClick(object sender, EventArgs e)
		{
			MainController.Instance.WallbinViews.FormatState.FontSize -= 2;
			UpdateFontButtons();
		}

		private void OnWallbinSettingsClick(object sender, EventArgs e)
		{
			using (var form = new FormWallbinSettings())
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				ProcessChanges();
				NeedToUpdate = true;
				MainController.Instance.ReloadWallbinViews();
			}
		}
		#endregion

		#region Retractable Bar
		private void UpdateRetractableBarContent()
		{
			retractableBar.Visible = true;
			laEditorTitle.Text = String.Empty;
			if (MainController.Instance.WallbinViews.FormatState.ShowTagsEditor)
			{
				retractableBar.AddButtons(MainController.Instance.WallbinViews.FormatState.ShowSecurityTags ?
					new[]
					{
						new ButtonInfo
						{
							Logo = Resources.RetractableLogoSecurity,
							Tooltip = "Expand security settings"
						}
					} :
					new[]
					{
						new ButtonInfo
						{
							Logo = Resources.RetractableLogoTags,
							Tooltip = "Expand tags settings"
						}
					});
				_groupSettingsManager.SwitchEditor();
				if (_groupSettingsManager.ActiveEditor != null)
				{
					laEditorTitle.Text = _groupSettingsManager.ActiveEditor.Title;
					if (!retractableBar.Content.Controls.Contains((Control)_groupSettingsManager.ActiveEditor))
						retractableBar.Content.Controls.Add((Control)_groupSettingsManager.ActiveEditor);
					((Control)_groupSettingsManager.ActiveEditor).BringToFront();

				}
			}
			else
			{
				retractableBar.AddButtons(new[]
				{
					new ButtonInfo
					{
						Logo = Resources.RetractableLogoFiles,
						Tooltip = "Expand file list"
					}
				});
				if (!retractableBar.Content.Controls.Contains(MainController.Instance.WallbinViews.ActiveWallbin.DataSourcesControl))
					retractableBar.Content.Controls.Add(MainController.Instance.WallbinViews.ActiveWallbin.DataSourcesControl);
				MainController.Instance.WallbinViews.ActiveWallbin.DataSourcesControl.BringToFront();
			}
		}

		private void OnRetractableBarStateChanged(object sender, StateChangedEventArgs e)
		{
			MainController.Instance.Settings.TreeViewVisible = e.Expaned;
			MainController.Instance.Settings.Save();
		}
		#endregion

		#region Tags Processing
		private void buttonItemTagsSync_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null || button.Checked) return;
			MainController.Instance.MainForm.buttonItemTagsCategories.Checked = false;
			MainController.Instance.MainForm.buttonItemTagsKeywords.Checked = false;
			MainController.Instance.MainForm.buttonItemTagsSuperFilters.Checked = false;
			MainController.Instance.MainForm.buttonItemTagsClear.Checked = false;
			button.Checked = true;
		}

		private void buttonItemTagsCategories_CheckedChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			var button = sender as ButtonItem;
			if (button == null || !button.Checked) return;
			MainController.Instance.WallbinViews.FormatState.ShowCategories();
		}

		private void buttonItemTagsKeywords_CheckedChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			var button = sender as ButtonItem;
			if (button == null || !button.Checked) return;
			MainController.Instance.WallbinViews.FormatState.ShowKeywords();
		}

		private void buttonItemTagsSuperFilters_CheckedChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			var button = sender as ButtonItem;
			if (button == null || !button.Checked) return;
			MainController.Instance.WallbinViews.FormatState.ShowSuperFilters();
		}

		private void buttonItemTagsClear_CheckedChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			var button = sender as ButtonItem;
			if (button == null || !button.Checked) return;
			MainController.Instance.WallbinViews.FormatState.ShowCleaner();
		}
		#endregion

		#region Security Processing
		private void buttonItemSecuritySelect_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.WallbinViews.ActiveWallbin == null) return;
			if (MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == null) return;
			MainController.Instance.WallbinViews.ActiveWallbin.ActivePage.Content.SelectAll();
		}

		private void buttonItemSecurityReset_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.WallbinViews.ActiveWallbin == null) return;
			if (MainController.Instance.WallbinViews.ActiveWallbin.ActivePage == null) return;
			MainController.Instance.WallbinViews.ActiveWallbin.ActivePage.Content.ResetSecurity();
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
		}
		#endregion

		#region Program Data Processing
		private void UpdateProgramDataSettings()
		{
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsEnabled.Checked = library.ProgramData.Enable;
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsDisabled.Checked = !library.ProgramData.Enable;
			MainController.Instance.MainForm.ribbonBarProgramManagerLocation.Enabled = library.ProgramData.Enable;
			MainController.Instance.MainForm.buttonEditProgramManagerLocation.EditValue = library.ProgramData.Path;
		}

		private void buttonItemProgramManagerSync_Click(object sender, EventArgs e)
		{
			var buttonItem = (ButtonItem)sender;
			if (buttonItem.Checked) return;
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsDisabled.Checked = false;
			MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsEnabled.Checked = false;
			buttonItem.Checked = true;
		}

		private void buttonItemProgramManagerSync_CheckedChanged(object sender, EventArgs e)
		{
			if (_isLoading) return;
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			library.ProgramData.Enable = MainController.Instance.MainForm.buttonItemProgramManagerSyncSettingsEnabled.Checked;
			MainController.Instance.MainForm.ribbonBarProgramManagerLocation.Enabled = library.ProgramData.Enable;
			if (!library.ProgramData.Enable)
			{
				MainController.Instance.MainForm.buttonEditProgramManagerLocation.EditValue = null;
				library.ProgramData.Path = null;
			}
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
		}

		private void buttonEditProgramManagerLocation_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var library = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage.Library;
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = !String.IsNullOrEmpty(library.ProgramData.Path) ?
					library.ProgramData.Path :
					Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				if (dialog.ShowDialog() != DialogResult.OK) return;
				if (!Directory.Exists(dialog.SelectedPath)) return;
				MainController.Instance.MainForm.buttonEditProgramManagerLocation.EditValue = dialog.SelectedPath;
				library.ProgramData.Path = dialog.SelectedPath;
				MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
			}
		}
		#endregion
	}
}
