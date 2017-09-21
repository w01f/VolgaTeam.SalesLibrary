using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.FileManager.Business.Models.Connection;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Business.Synchronization;
using SalesLibraries.FileManager.PresentationLayer.Sync;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.GroupSettings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;
using SalesLibraries.FileManager.Properties;

namespace SalesLibraries.FileManager.Controllers
{
	[ToolboxItem(false)]
	public partial class WallbinPageOld : UserControl, IPageController
	{
		private bool _isLoading;
		private GroupSettingsManager _groupSettingsManager;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public WallbinPageOld()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			pnContainer.Dock = DockStyle.Fill;
			pnEmpty.BringToFront();
			NeedToUpdate = true;

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

				pnTagInfoContainer.Width = RectangleHelper.ScaleHorizontal(pnTagInfoContainer.Width, comboBoxEditPages.ScaleFactor.Width);
			}
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
			MainController.Instance.MainForm.buttonItemVideoSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemTagsSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemSecuritySync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemSettingsSync.Click += OnSyncClick;
			MainController.Instance.MainForm.buttonItemBundlesSync.Click += OnSyncClick;

			#region Link Operations
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesTags.Visible = MainController.Instance.Settings.EditorSettings.EnableTagsEdit;

			MainController.Instance.MainForm.buttonItemHomeAddUrl.Click += buttonItemHomeAddUrl_Click;
			MainController.Instance.MainForm.buttonItemHomeAddLineBreak.Click += buttonItemHomeAddLineBreak_Click;

			MainController.Instance.MainForm.buttonItemHomeLinkOpen.Click += buttonItemHomeLinkOpen_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkDelete.Click += buttonItemHomeLinkDelete_Click;

			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesNotes.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesTags.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesWidget.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesBanner.Click += buttonItemHomeLinkSettings_Click;
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesThumbnail.Click += buttonItemHomeLinkSettings_Click;
			#endregion

			#region General Settings
			MainController.Instance.MainForm.buttonItemPreferencesPages.Click += buttonItemPreferencesPages_Click;
			MainController.Instance.MainForm.buttonItemPreferencesColumns.Click += buttonItemPreferencesColumns_Click;
			MainController.Instance.MainForm.buttonItemPreferencesAutoWidgets.Click += buttonItemPreferencesAutoWidgets_Click;
			MainController.Instance.MainForm.buttonItemSettingsLibraries.Click += buttonItemSettingsLibraries_Click;
			MainController.Instance.MainForm.buttonItemSettingsSyncSettings.Click += buttonItemSettingsSync_Click;
			MainController.Instance.MainForm.buttonItemSettingsAdvanced.Click += buttonItemSettingsAdvanced_Click;
			MainController.Instance.MainForm.buttonItemSettingsWallbin.Click += OnWallbinSettingsClick;
			#endregion

			#region Tags
			MainController.Instance.MainForm.ribbonBarTagsSuperFilters.Visible =
				MainController.Instance.Lists.SuperFilters.Items.Any();

			MainController.Instance.MainForm.buttonItemTagsCategories.Click += buttonItemTagsSync_Click;
			MainController.Instance.MainForm.buttonItemTagsCategories.CheckedChanged += buttonItemTagsCategories_CheckedChanged;
			MainController.Instance.MainForm.buttonItemTagsKeywords.Click += buttonItemTagsSync_Click;
			MainController.Instance.MainForm.buttonItemTagsKeywords.CheckedChanged += buttonItemTagsKeywords_CheckedChanged;
			MainController.Instance.MainForm.buttonItemTagsSuperFilters.Click += buttonItemTagsSync_Click;
			MainController.Instance.MainForm.buttonItemTagsSuperFilters.CheckedChanged += buttonItemTagsSuperFilters_CheckedChanged;
			#endregion

			#region Security
			MainController.Instance.MainForm.buttonItemSecuritySelect.Click += buttonItemSecuritySelect_Click;
			MainController.Instance.MainForm.buttonItemSecurityReset.Click += buttonItemSecurityReset_Click;
			#endregion

			#region Link Bundles
			MainController.Instance.MainForm.buttonItemBundlesNew.Click += OnBundlesNewClick;
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

			UpdateLinkButtons();

			_isLoading = false;
		}

		public void ProcessChanges()
		{
			if (!IsActive) return;
			MainController.Instance.WallbinViews.SaveActiveWallbin(false);
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
			var selectedLinks = MainController.Instance.WallbinViews.Selection.SelectedLinks.ToList();

			MainController.Instance.MainForm.buttonItemHomeAddUrl.Enabled =
			MainController.Instance.MainForm.buttonItemHomeAddLineBreak.Enabled = selectedFolder != null;

			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesNotes.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkOpen.Enabled = selectedLinks.Count == 1 && selectedFolder?.SelectedLinkRow != null && !selectedFolder.SelectedLinkRow.Inaccessable;

			MainController.Instance.MainForm.buttonItemHomeLinkDelete.Enabled = selectedLinks.Any();

			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesImage.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesWidget.Enabled =
			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesBanner.Enabled = selectedLinks.Any(l => !(l is LibraryFileLink) || !((LibraryFileLink)l).IsDead);

			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesThumbnail.Enabled = selectedLinks.Any(l => l is IThumbnailSettingsHolder && (!(l is LibraryFileLink) || !((LibraryFileLink)l).IsDead));

			MainController.Instance.MainForm.buttonItemHomeLinkPropertiesTags.Enabled =
				selectedLinks.Any(l => !(l is LineBreak) && (!(l is LibraryFileLink) || !((LibraryFileLink)l).IsDead));
		}

		public void OnLibraryChanged(object sender, EventArgs e)
		{
			if (!IsActive)
			{
				NeedToUpdate = true;
				return;
			}
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(() =>
			{
				_isLoading = true;
				pnEmpty.BringToFront();
				var activeWallbin = MainController.Instance.WallbinViews.ActiveWallbin;
				if (activeWallbin == null) return;
				if (!pnContainer.Controls.Contains((Control)activeWallbin))
					pnContainer.Controls.Add((Control)activeWallbin);

				activeWallbin.ShowView();
				UpdateRetractableBarContent();
				((Control)activeWallbin).BringToFront();
				pnContainer.BringToFront();
				_isLoading = false;
			})));
		}

		private void OnLibraryDataChanged(object sender, EventArgs e)
		{
			linkInfoControl.UpdateData();
			linkTagsInfoControl.UpdateData();
		}

		private void OnFormatStateChanged(object sender, EventArgs e)
		{
			UpdateRetractableBarContent();
			superFilterControl.UpdateData();
			linkInfoControl.UpdateData();
			linkTagsInfoControl.UpdateData();
		}

		private void OnSelectionChanged(object sender, SelectionEventArgs e)
		{
			UpdateLinkButtons();
			linkInfoControl.UpdateData();
			linkTagsInfoControl.UpdateData();
			switch (e.SelectionType)
			{
				case SelectionEventType.SelectionReset:
				case SelectionEventType.FolderSelected:
					superFilterControl.Reset();
					break;
				case SelectionEventType.LinkSelected:
					superFilterControl.UpdateData();
					break;
			}
		}

		private void OnSyncClick(object sender, EventArgs e)
		{
			MainController.Instance.ProcessChanges();

			var targetContext = MainController.Instance.WallbinViews.ActiveWallbin.DataStorage;
			var targetLibrary = targetContext.Library;
			if ((MainController.Instance.Settings.NetworkPaths.Any() && MainController.Instance.Settings.NetworkPaths.Any(p => !Directory.Exists(p))) ||
				MainController.Instance.Settings.WebPaths.Any() && MainController.Instance.Settings.WebPaths.Any(p => !Directory.Exists(p)))
				MainController.Instance.PopupMessages.ShowWarning("Some of your Upload Directories are Not connected. Your changes will still be saved in your Source Directory.");

			MainController.Instance.MainForm.ribbonControl.Enabled = false;
			var savedState = MainController.Instance.MainForm.WindowState;
			if (targetLibrary.SyncSettings.MinimizeOnSync)
				MainController.Instance.MainForm.WindowState = FormWindowState.Minimized;

			using (var formProgressSync = new FormProgressSync())
			{
				var successfullSync = false;
				MainController.Instance.ProcessManager.RunWithProgress(
					formProgressSync,
					false,
					(cancellationToken, formProgress) =>
					{
						SyncManager.SyncRegular(cancellationToken);
						successfullSync = true;
					},
					null,
					exception =>
					{
						MainController.Instance.MainForm.WindowState = savedState;
						MainController.Instance.MainForm.ribbonControl.Enabled = true;
						Application.DoEvents();

						MainController.Instance.ActivateApplication();
						Application.DoEvents();

						SyncManager.ProcessSyncException(exception);
					}
					);

				if (successfullSync)
				{
					if (targetLibrary.SyncSettings.CloseAfterSync)
						MainController.Instance.MainForm.Close();
					else
					{
						MainController.Instance.MainForm.WindowState = savedState;
						MainController.Instance.MainForm.ribbonControl.Enabled = true;
						MainController.Instance.TabWallbin.UpdateWallbin();
						MainController.Instance.ActivateApplication();
					}
				}
			}
		}

		#region Link Operations Processing
		private void buttonItemHomeAddUrl_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			selectedFolder?.AddHyperLink();
		}

		private void buttonItemHomeAddLineBreak_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			selectedFolder?.AddLineBreak();
		}

		private void buttonItemHomeLinkOpen_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			selectedFolder?.OpenLink();
		}

		private void buttonItemHomeLinkDelete_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			var selectedLinks = MainController.Instance.WallbinViews.Selection.SelectedLinks.ToList();
			if (selectedLinks.Count > 1)
				selectedFolder?.DeleteMultiLinks(selectedLinks);
			else
				selectedFolder.DeleteSingleLink();
		}

		private void buttonItemHomeLinkSettings_Click(object sender, EventArgs e)
		{
			var selectedFolder = MainController.Instance.WallbinViews.Selection.SelectedFolder;
			if (selectedFolder == null) return;
			var button = sender as ButtonItem;
			if (button?.Tag == null) return;
			var propertiesType = (LinkSettingsType)Enum.Parse(typeof(LinkSettingsType), (String)button.Tag);

			var selectedLinks = MainController.Instance.WallbinViews.Selection.SelectedLinks.ToList();
			if (selectedLinks.Count > 1)
				selectedFolder.EditLinksGroupSettings(selectedLinks.ToMultiLinkSet(), propertiesType);
			else
				selectedFolder.EditSingleLinkSettings(propertiesType);
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
						if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
						{
							libraryCopy.MarkAsModified();
							return true;
						}
						return false;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", (cancelationToken, formProgess) => original.Save(context, current)));
			if (!resut) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(UpdateWallbin)));
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
						if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
						{
							libraryCopy.MarkAsModified();
							return true;
						}
						return false;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", (cancelationToken, formProgess) => original.Save(context, current)));
			if (!resut) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(UpdateWallbin)));
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
						if (form.ShowDialog(MainController.Instance.MainForm) == DialogResult.OK)
						{
							libraryCopy.MarkAsModified();
							return true;
						}
						return false;
					}
				},
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
				(context, original, current) => MainController.Instance.ProcessManager.Run("Saving Changes...", (cancelationToken, formProgess) => original.Save(context, current)));
			if (!resut) return;
			MainController.Instance.ProcessManager.RunInQueue("Loading Library...", () => MainController.Instance.MainForm.ActiveForm.Invoke(new MethodInvoker(UpdateWallbin)));
		}

		private void buttonItemSettingsLibraries_Click(object sender, EventArgs e)
		{
			ProcessChanges();
			using (var form = new FormPaths())
			{
				var oldBackupPath = MainController.Instance.Settings.BackupPath;
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				MainController.Instance.Settings.Save();
				var backupChanged = !String.Equals(
					MainController.Instance.Settings.BackupPath,
					oldBackupPath,
					StringComparison.InvariantCultureIgnoreCase);

				if (backupChanged)
				{
					DatabaseConnectionHelper.Disconnect(oldBackupPath);

					var connectionState = DatabaseConnectionHelper.GetConnectionState(MainController.Instance.Settings.BackupPath);
					if (connectionState.Type == ConnectionStateType.Busy)
					{
						MainController.Instance.PopupMessages.ShowWarning(
							String.Format(
								"{0} is currently updating the site.{1}Please try back again later, or ask the user to hurry up and finish…",
								connectionState.User,
								Environment.NewLine));
						return;
					}

					NeedToUpdate = true;
					MainController.Instance.ReloadData();
				}
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
				copyMethod => MainController.Instance.ProcessManager.Run("Preparing Data...", (cancelationToken, formProgess) => copyMethod()),
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

		#region Retractable Bar
		private void UpdateRetractableBarContent()
		{
			if (MainController.Instance.WallbinViews.ActiveWallbin == null)
			{
				retractableBar.Visible = false;
				return;
			}
			retractableBar.Visible = true;
			laEditorTitle.Text = String.Empty;
			if (MainController.Instance.WallbinViews.FormatState.ShowFiles)
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
			else if (MainController.Instance.WallbinViews.FormatState.ShowLinkBundles)
			{
				retractableBar.AddButtons(new[]
				{
					new ButtonInfo
					{
						Logo = Resources.RetractableLogoBundles,
						Tooltip = "Expand link bundle list"
					}
				});
				laEditorTitle.Text = "Saved Link Bundles";
				if (!retractableBar.Content.Controls.Contains(MainController.Instance.WallbinViews.ActiveWallbin.LinkBundleListControl))
					retractableBar.Content.Controls.Add(MainController.Instance.WallbinViews.ActiveWallbin.LinkBundleListControl);
				MainController.Instance.WallbinViews.ActiveWallbin.LinkBundleListControl.BringToFront();
			}
			else if (MainController.Instance.WallbinViews.FormatState.ShowTagsEditor)
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
				retractableBar.Visible = false;
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

		#region Link Bundles Processing
		private void OnBundlesNewClick(object sender, EventArgs eventArgs)
		{
			MainController.Instance.WallbinViews.ActiveWallbin.LinkBundleListControl.AddBundle();
		}
		#endregion
	}
}
