using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using FileManager.BusinessClasses;
using FileManager.ConfigurationClasses;
using FileManager.PresentationClasses.TabPages;
using FileManager.PresentationClasses.WallBin.Decorators;
using FileManager.Properties;
using FileManager.ToolClasses;
using FileManager.ToolForms;
using FileManager.ToolForms.Settings;
using FileManager.ToolForms.WallBin;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace FileManager.Controllers
{
	public class WallbinController : IPageController
	{
		private FormAutoSync _formAutoSync;
		private FormAutoWidgets _formAutoWidgets;
		private FormBranding _formBranding;
		private FormColumns _formColumns;
		private FormDeadLinks _formDeadLinks;
		private FormEmailList _formEmailList;
		private FormExtraRoots _formExtraRoots;
		private FormPages _formPages;
		private FormSync _formSync;
		private readonly WallBinOptions _wallBinOptions = new WallBinOptions();

		private bool _initialization;
		private TabHomeControl _tabPage;

		public WallbinController()
		{
			FormMain.Instance.buttonItemSettingsPaths.Click += btPathSettings_Click;
			FormMain.Instance.buttonItemSettingsExtraRoots.Click += BtExtraRootClick;
			FormMain.Instance.buttonItemSettingsBranding.Click += buttonItemSettingsBranding_Click;
			FormMain.Instance.buttonItemSettingsSync.Click += buttonItemSettingsSync_Click;
			FormMain.Instance.buttonItemPreferencesPages.Click += buttonItemSettingsPages_Click;
			FormMain.Instance.buttonItemPreferencesColumns.Click += buttonItemSettingsColumns_Click;
			FormMain.Instance.buttonItemPreferencesAutoWidgets.Click += buttonItemSettingsAutoWidgets_Click;
			FormMain.Instance.buttonItemPreferencesDeadLinks.Click += buttonItemSettingsDeadLinks_Click;
			FormMain.Instance.buttonItemPreferencesEmailList.Click += buttonItemSettingsEmailList_Click;
			FormMain.Instance.buttonItemSettingsAutoSync.Click += buttonItemSettingsAutoSync_Click;

			FormMain.Instance.buttonItemHomeAddLineBreak.Click += btLineBreak_Click;
			FormMain.Instance.buttonItemHomeAddNetworkShare.Click += btAddNeworkShare_Click;
			FormMain.Instance.buttonItemHomeAddUrl.Click += btAddUrl_Click;
			FormMain.Instance.buttonItemHomeDelete.Click += btDeleteLink_Click;
			FormMain.Instance.buttonItemHomeOpen.Click += btOpenLink_Click;
			FormMain.Instance.buttonItemHomeLinkPropertiesNotes.Click += buttonItemHomeProperties_Click;
			FormMain.Instance.buttonItemHomeLinkPropertiesTags.Click += buttonItemHomeProperties_Click;
			FormMain.Instance.buttonItemHomeLinkPropertiesExpirationDate.Click += buttonItemHomeProperties_Click;
			FormMain.Instance.buttonItemHomeLinkPropertiesSecurity.Click += buttonItemHomeProperties_Click;
			FormMain.Instance.buttonItemHomeLinkPropertiesWidget.Click += buttonItemHomeProperties_Click;
			FormMain.Instance.buttonItemHomeLinkPropertiesBanner.Click += buttonItemHomeProperties_Click;
			FormMain.Instance.buttonItemHomeSave.Click += btSave_Click;
			FormMain.Instance.buttonItemHomeSync.Click += btSync_Click;

			FormMain.Instance.buttonItemProgramManagerSyncDisabled.Click += buttonItemProgramManagerSync_Click;
			FormMain.Instance.buttonItemProgramManagerSyncEnabled.Click += buttonItemProgramManagerSync_Click;
			FormMain.Instance.buttonItemProgramManagerSyncDisabled.CheckedChanged += buttonItemProgramManagerSync_CheckedChanged;
			FormMain.Instance.buttonItemProgramManagerSyncEnabled.CheckedChanged += buttonItemProgramManagerSync_CheckedChanged;
			FormMain.Instance.buttonEditProgramManagerLocation.ButtonClick += buttonEditProgramManagerLocation_ButtonClick;

			FormMain.Instance.buttonItemTagsCategories.Click += ButtonItemTagsClick;
			FormMain.Instance.buttonItemTagsCategories.CheckedChanged += ButtonItemTagsCheckedChanged;
			FormMain.Instance.buttonItemTagsSuperFilters.Click += ButtonItemTagsClick;
			FormMain.Instance.buttonItemTagsSuperFilters.CheckedChanged += ButtonItemTagsCheckedChanged;
			FormMain.Instance.buttonItemTagsKeywords.Click += ButtonItemTagsClick;
			FormMain.Instance.buttonItemTagsKeywords.CheckedChanged += ButtonItemTagsCheckedChanged;
			FormMain.Instance.buttonItemTagsClear.Click += ButtonItemTagsClick;
			FormMain.Instance.buttonItemTagsClear.CheckedChanged += ButtonItemTagsCheckedChanged;
			FormMain.Instance.buttonItemTagsSave.Click += btTagsSave_Click;

			FormMain.Instance.buttonItemSecuritySelect.Click += SecuritySelect_Click;
			FormMain.Instance.buttonItemSecurityReset.Click += SecurityReset_Click;
			FormMain.Instance.buttonItemSecuritySave.Click += SecuritySave_Click;
		}

		#region IPageController Members
		public void InitController()
		{
			_initialization = true;

			_tabPage = new TabHomeControl();
			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);
			_tabPage.pnEmpty.BringToFront();
			_tabPage.InitPage();
			_tabPage.barCheckItemTabs.CheckedChanged += buttonItemSettingsMultitab_CheckedChanged;
			_tabPage.barCheckItemTabs.CheckedChanged += _tabPage.LibraryChanged;
			_tabPage.barButtonItemFontDown.ItemClick += btFontDown_Click;
			_tabPage.barButtonItemFontUp.ItemClick += btFontUp_Click;
			_tabPage.barButtonItemLinkDown.ItemClick += btDownLink_Click;
			_tabPage.barButtonItemLinkUp.ItemClick += btUpLink_Click;

			var activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator != null && activeDecorator.Library.IsConfigured)
			{
				FormMain.Instance.buttonItemProgramManagerSyncDisabled.Checked = !activeDecorator.Library.EnableProgramManagerSync;
				FormMain.Instance.buttonItemProgramManagerSyncEnabled.Checked = activeDecorator.Library.EnableProgramManagerSync;
				FormMain.Instance.buttonEditProgramManagerLocation.EditValue = activeDecorator.Library.ProgramManagerLocation;
				FormMain.Instance.ribbonBarProgramManagerLocation.Enabled = activeDecorator.Library.EnableProgramManagerSync;
			}
			_tabPage.pnMain.BringToFront();

			UpdateTagsButtons();

			MainController.Instance.LibraryChanged += (sender, args) =>
														  {
															  _initialization = true;
															  ApplyActiveLibrary();
															  _initialization = false;
														  };
			_initialization = false;
		}

		public void PrepareTab(TabPageEnum tabPage)
		{
			PrepareActiveLibrary();
			CustomizeTabPage(tabPage);
		}

		public void ShowTab()
		{
			ApplyActiveLibrary();

			_tabPage.ApplyWallBinOptions(_wallBinOptions);
			_tabPage.BringToFront();
		}
		#endregion

		private void CustomizeTabPage(TabPageEnum tabPage)
		{
			switch (tabPage)
			{
				case TabPageEnum.Tags:
					_wallBinOptions.AllowEdit = false;
					_wallBinOptions.AllowMultiSelect = true;
					_wallBinOptions.ShowFiles = false;
					_wallBinOptions.ShowTagsEditor = true;
					_wallBinOptions.ShowCategoryTags = SettingsManager.Instance.ShowTagsCategories;
					_wallBinOptions.ShowSuperFilterTags = SettingsManager.Instance.ShowTagsSuperFilters;
					_wallBinOptions.ShowKeywordTags = SettingsManager.Instance.ShowTagsKeywords;
					_wallBinOptions.ShowSecurityTags = false;
					break;
				case TabPageEnum.Security:
					_wallBinOptions.AllowEdit = false;
					_wallBinOptions.AllowMultiSelect = true;
					_wallBinOptions.ShowFiles = false;
					_wallBinOptions.ShowTagsEditor = true;
					_wallBinOptions.ShowCategoryTags = false;
					_wallBinOptions.ShowSuperFilterTags = false;
					_wallBinOptions.ShowKeywordTags = false;
					_wallBinOptions.ShowSecurityTags = true;
					break;
				default:
					_tabPage.ActiveTagsEditor = null;
					_wallBinOptions.AllowEdit = true;
					_wallBinOptions.AllowMultiSelect = false;
					_wallBinOptions.ShowFiles = true;
					_wallBinOptions.ShowTagsEditor = false;
					_wallBinOptions.ShowCategoryTags = false;
					_wallBinOptions.ShowSuperFilterTags = false;
					_wallBinOptions.ShowKeywordTags = false;
					_wallBinOptions.ShowSecurityTags = false;
					break;
			}
		}

		#region Libraries Processing
		public IEnumerable<Library> GetLibraries()
		{
			return MainController.Instance.Decorators.Values.Select(x => x.Library);
		}

		public void SelectLibrary(Library library)
		{
			_initialization = true;
			LibraryDecorator newActiveDecorator = MainController.Instance.Decorators[library.Identifier];
			MainController.Instance.ActiveDecorator = newActiveDecorator;
			ApplyActiveLibrary();
			MainController.Instance.RequestChangeLibrary();
			_initialization = false;
		}

		private void ApplyActiveLibrary()
		{
			LibraryDecorator activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator != null && activeDecorator.Library.IsConfigured)
			{
				if (!activeDecorator.Library.UseDirectAccess)
				{
					foreach (PageDecorator page in activeDecorator.Pages)
						page.SwitchMultitab(SettingsManager.Instance.MultitabView);
					if (SettingsManager.Instance.MultitabView)
					{
						if (!_tabPage.pnMain.Controls.Contains(activeDecorator.TabControl))
							_tabPage.pnMain.Controls.Add(activeDecorator.TabControl);
						ResizeActivePage();
						ApplyWallBinOptions();
						UpdateLinkInfo(null);
						activeDecorator.TabControl.BringToFront();
					}
					else
					{
						if (!_tabPage.pnMain.Controls.Contains(activeDecorator.RegularControl))
							_tabPage.pnMain.Controls.Add(activeDecorator.RegularControl);
						activeDecorator.RegularControl.BringToFront();
						ApplyActivePage();
					}
					UpdateTagCountInfo();
				}
			}
		}

		private void PrepareActiveLibrary()
		{
			var activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator == null || !activeDecorator.Library.IsConfigured) return;
			var result = DialogResult.Cancel;
			var library = activeDecorator.Library;
			if (library.DeadLinks.Count > 0 && library.EnableInactiveLinks && library.InactiveLinksMessageAtStartup && activeDecorator.FirstTimeProcess)
				using (var form = new FormIncorrectLinksNotification())
				{
					form.pbLogo.Image = Resources.DeadLinks;
					form.Text = string.Format(form.Text, "INACTIVE");
					form.laTitle.Text = string.Format(form.laTitle.Text, "DEAD");
					result = form.ShowDialog();
					if (result == DialogResult.OK)
						DeleteDeadLinks(library);
				}
			if (library.ExpiredLinks.Count > 0 && activeDecorator.FirstTimeProcess)
				using (var form = new FormIncorrectLinksNotification())
				{
					form.pbLogo.Image = Resources.ExpiredLinks;
					form.Text = string.Format(form.Text, "EXPIRED");
					form.laTitle.Text = string.Format(form.laTitle.Text, "EXPIRED");
					result = form.ShowDialog();
					if (result == DialogResult.OK)
						DeleteExpiredLinks(library);
				}

			activeDecorator.FirstTimeProcess = false;

			if (result == DialogResult.OK)
				MainController.Instance.RequestUpdateLibrary(library);
		}

		private void DeleteDeadLinks(Library library)
		{
			using (var form = new FormDeleteIncorrectLinks())
			{
				form.Text = string.Format(form.Text, "Dead");
				form.ExpiredLinks = false;
				form.IncorrectLinks.Clear();
				form.IncorrectLinks.AddRange(library.DeadLinks.ToArray());
				if (form.ShowDialog() != DialogResult.OK) return;
				library.DeleteDeadLinks(form.LinksForDelete.ToArray());
				library.Save();
			}
		}

		private void DeleteExpiredLinks(Library library)
		{
			using (var form = new FormDeleteIncorrectLinks())
			{
				form.Text = string.Format(form.Text, "Expired");
				form.ExpiredLinks = true;
				form.IncorrectLinks.Clear();
				form.IncorrectLinks.AddRange(library.ExpiredLinks.ToArray());
				if (form.ShowDialog() != DialogResult.OK) return;
				library.DeleteExpiredLinks(form.LinksForDelete.ToArray());
				library.Save();
			}
		}
		#endregion

		#region Page Processing
		public IEnumerable<LibraryPage> GetPages()
		{
			return MainController.Instance.ActiveDecorator != null && !MainController.Instance.ActiveDecorator.Library.UseDirectAccess && !SettingsManager.Instance.MultitabView ? MainController.Instance.ActiveDecorator.Pages.Select(x => x.Page) : null;
		}

		public void SelectPage(LibraryPage page)
		{
			var newPageDecorator = MainController.Instance.ActiveDecorator.Pages.FirstOrDefault(x => x.Page == page);
			MainController.Instance.ActiveDecorator.ActivePage = newPageDecorator;
			ApplyActivePage();
			MainController.Instance.RequestChangePage();
		}

		private void ApplyActivePage()
		{
			var pageDecorator = MainController.Instance.ActiveDecorator != null ? (MainController.Instance.ActiveDecorator.ActivePage) : null;
			if (pageDecorator == null) return;
			ResizeActivePage();
			ApplyWallBinOptions();
			UpdateLinkInfo(null);
			pageDecorator.RegularPage.BringToFront();
		}

		public void ResizeActivePage()
		{
			if (MainController.Instance.ActiveDecorator != null && MainController.Instance.ActiveDecorator.ActivePage != null)
				MainController.Instance.ActiveDecorator.ActivePage.ResizePage();
		}

		public void ApplyWallBinOptions()
		{
			if (MainController.Instance.ActiveDecorator != null && MainController.Instance.ActiveDecorator.ActivePage != null)
				MainController.Instance.ActiveDecorator.ActivePage.ApplyWallBinOptions(_wallBinOptions);
		}

		public void UpdateLinkInfo(LibraryLink link)
		{
			_tabPage.UpdateLinkInfo(link);
			_tabPage.SuperFilterControl.SelectedLink = link;
			_tabPage.SuperFilterControl.UpdateLink();
		}

		public void UpdateTagCountInfo()
		{
			if (ListManager.Instance.SearchTags.TagCount)
			{
				var totalLinks = MainController.Instance.ActiveDecorator.Library.Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).Count(l => l.Type != FileTypes.LineBreak);
				var taggedLinks = MainController.Instance.ActiveDecorator.Library.Pages.SelectMany(p => p.Folders.SelectMany(f => f.Files)).Count(l => l.Type != FileTypes.LineBreak && l.SearchTags != null && !String.IsNullOrEmpty(l.SearchTags.AllTags));
				if (taggedLinks == 0)
				{
					_tabPage.UpdateTagCountInfo("You need to start TAGGING your Links!", Color.Red);
				}
				else if (totalLinks > taggedLinks)
				{
					_tabPage.UpdateTagCountInfo(String.Format("{0} Links Require Tags!", totalLinks - taggedLinks), Color.Red);
				}
				else
					_tabPage.UpdateTagCountInfo("Library 100% TAGGED!", Color.Green);
			}
			else
			{
				_tabPage.UpdateTagCountInfo(String.Empty);
			}
		}
		#endregion

		#region Tags Processing
		private void UpdateTagsButtons()
		{
			FormMain.Instance.buttonItemTagsCategories.Checked = SettingsManager.Instance.ShowTagsCategories;
			FormMain.Instance.buttonItemTagsSuperFilters.Checked = SettingsManager.Instance.ShowTagsSuperFilters;
			FormMain.Instance.buttonItemTagsKeywords.Checked = SettingsManager.Instance.ShowTagsKeywords;
			FormMain.Instance.buttonItemTagsClear.Checked = SettingsManager.Instance.ShowTagsCleaner;
		}

		public void ApplyTagsEditor()
		{
			if (_tabPage.ActiveTagsEditor == null) return;
			if (_tabPage.ActiveTagsEditor.NeedToApply)
				_tabPage.ActiveTagsEditor.ApplyData();
		}

		public void UpdateTagsEditor()
		{
			if (_tabPage.ActiveTagsEditor == null) return;
			_tabPage.ActiveTagsEditor.UpdateData();
		}
		#endregion

		#region Operations
		#region Wallbin
		private void btAddUrl_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.AddUrl();
		}

		private void btAddNeworkShare_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.AddNetworkFolder();
		}

		private void btLineBreak_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.AddLineBreak();
		}

		private void btDownLink_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.DownLink();
		}

		private void btUpLink_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.UpLink();
		}

		private void btOpenLink_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.OpenLink();
		}

		private void btDeleteLink_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.DeleteLink();
		}

		private void btFontUp_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
			{
				SettingsManager.Instance.FontSize += 2;
				SettingsManager.Instance.Save();
				_tabPage.UpdateFontButtonStatus();
				ResizeActivePage();
			}
		}

		private void btFontDown_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
			{
				SettingsManager.Instance.FontSize -= 2;
				SettingsManager.Instance.Save();
				_tabPage.UpdateFontButtonStatus();
				ResizeActivePage();
			}
		}

		private void buttonItemHomeProperties_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null || button.Tag == null) return;
			var propertiesType = (LinkPropertiesType)Enum.Parse(typeof(LinkPropertiesType), (String)button.Tag);
			if (MainController.Instance.ActiveDecorator != null)
				if (MainController.Instance.ActiveDecorator.ActivePage != null)
					if (MainController.Instance.ActiveDecorator.ActivePage.ActiveBox != null)
						MainController.Instance.ActiveDecorator.ActivePage.ActiveBox.ShowLinkProperties(propertiesType);
		}

		private void btSave_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.Save();
			AppManager.Instance.ShowInfo("All Links are saved!");
		}

		private void btSync_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			MainController.Instance.ActiveDecorator.Save();
			if (MainController.Instance.ActiveDecorator.Library.IsSyncLocked()) return;
			FormMain.Instance.ribbonControl.Enabled = false;
			_tabPage.Enabled = false;
			Application.DoEvents();

			if (MainController.Instance.ActiveDecorator.Library.MinimizeOnSync)
				FormMain.Instance.WindowState = FormWindowState.Minimized;

			Globals.ThreadActive = true;
			Globals.ThreadAborted = false;

			var closeAfterSync = MainController.Instance.ActiveDecorator.Library.CloseAfterSync;

			using (var form = new FormProgressSyncFilesRegular())
			{
				form.CloseAfterSync = closeAfterSync;
				form.ProcessAborted += (progressSender, progressE) => { Globals.ThreadAborted = true; };
				var thread = new Thread(delegate()
											{
												if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
													LibraryManager.Instance.SynchronizeLibraries();
											});
				if (MainController.Instance.ActiveDecorator.Library.ShowProgressDuringSync)
					form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				if (MainController.Instance.ActiveDecorator.Library.ShowProgressDuringSync)
					form.Close();
				if (form.CloseAfterSync)
				{
					if (form.CloseAfterSync && !MainController.Instance.ActiveDecorator.Library.FullSync)
						Application.Exit();
					else
						closeAfterSync = form.CloseAfterSync;
				}
			}

			if (MainController.Instance.ActiveDecorator.Library.FullSync && ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive))
			{
				using (var form = new FormProgressSyncFilesIPad())
				{
					form.CloseAfterSync = closeAfterSync;
					form.ProcessAborted += (progressSender, progressE) => { Globals.ThreadAborted = true; };
					var thread = new Thread(delegate()
												{
													AppManager.Instance.KillAutoFM();
													if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
														MainController.Instance.ActiveDecorator.Library.PrepareForIPadSynchronize();
													if ((Globals.ThreadActive && !Globals.ThreadAborted) || !Globals.ThreadActive)
														LibraryManager.Instance.SynchronizeLibraryForIpad(MainController.Instance.ActiveDecorator.Library);
													AppManager.Instance.RunAutoFM();
												});
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();

					if (form.CloseAfterSync)
						Application.Exit();
				}
			}

			Globals.ThreadActive = false;
			Globals.ThreadAborted = false;

			FormMain.Instance.ribbonControl.Enabled = true;
			_tabPage.Enabled = true;
			Application.DoEvents();
		}
		#endregion

		#region Program Manager
		private void buttonItemProgramManagerSync_Click(object sender, EventArgs e)
		{
			FormMain.Instance.buttonItemProgramManagerSyncDisabled.Checked = false;
			FormMain.Instance.buttonItemProgramManagerSyncEnabled.Checked = false;
			(sender as ButtonItem).Checked = true;
		}

		private void buttonItemProgramManagerSync_CheckedChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null && !_initialization)
			{
				MainController.Instance.ActiveDecorator.Library.EnableProgramManagerSync = FormMain.Instance.buttonItemProgramManagerSyncEnabled.Checked;
				MainController.Instance.ActiveDecorator.Library.Save();

				FormMain.Instance.ribbonBarProgramManagerLocation.Enabled = MainController.Instance.ActiveDecorator.Library.EnableProgramManagerSync;

				if (!MainController.Instance.ActiveDecorator.Library.EnableProgramManagerSync)
				{
					FormMain.Instance.buttonEditProgramManagerLocation.EditValue = null;
					MainController.Instance.ActiveDecorator.Library.ProgramManagerLocation = null;
					MainController.Instance.ActiveDecorator.Library.Save();
				}
			}
		}

		private void buttonEditProgramManagerLocation_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			using (var dialog = new FolderBrowserDialog())
			{
				dialog.SelectedPath = MainController.Instance.ActiveDecorator.Library.ProgramManagerLocation;
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					if (Directory.Exists(dialog.SelectedPath))
					{
						FormMain.Instance.buttonEditProgramManagerLocation.EditValue = dialog.SelectedPath;
						MainController.Instance.ActiveDecorator.Library.ProgramManagerLocation = dialog.SelectedPath;
						MainController.Instance.ActiveDecorator.Library.Save();
					}
				}
			}
		}
		#endregion

		#region Settings
		private void btPathSettings_Click(object sender, EventArgs e)
		{
			MainController.Instance.ChangePath();
		}

		private void BtExtraRootClick(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			if (MainController.Instance.SaveLibraryWarning())
			{
				if (_formExtraRoots == null) _formExtraRoots = new FormExtraRoots();
				_formExtraRoots.Library = new Library(MainController.Instance.ActiveDecorator.Library.Name, MainController.Instance.ActiveDecorator.Library.Folder, MainController.Instance.ActiveDecorator.Library.UseDirectAccess, SettingsManager.Instance.DirectAccessFileAgeLimit);
				if (_formExtraRoots.ShowDialog() == DialogResult.OK)
				{
					int libraryIndex = LibraryManager.Instance.LibraryCollection.IndexOf(MainController.Instance.ActiveDecorator.Library);
					LibraryManager.Instance.LibraryCollection.Remove(MainController.Instance.ActiveDecorator.Library);
					LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formExtraRoots.Library);
					MainController.Instance.RequestUpdateLibrary(_formExtraRoots.Library);
				}
			}
		}

		public void buttonItemSettingsBranding_Click(object sender, EventArgs e)
		{
			if (_formBranding == null) _formBranding = new FormBranding();
			if (MainController.Instance.ActiveDecorator != null)
				_formBranding.ShowDialog();
		}

		private void buttonItemSettingsSync_Click(object sender, EventArgs e)
		{
			if (_formSync == null) _formSync = new FormSync();
			if (MainController.Instance.ActiveDecorator != null)
				_formSync.ShowDialog();
		}

		private void buttonItemSettingsPages_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			if (!MainController.Instance.SaveLibraryWarning()) return;
			if (_formPages == null) _formPages = new FormPages();
			_formPages.Library = new Library(MainController.Instance.ActiveDecorator.Library.Name, MainController.Instance.ActiveDecorator.Library.Folder, MainController.Instance.ActiveDecorator.Library.UseDirectAccess, SettingsManager.Instance.DirectAccessFileAgeLimit);
			if (_formPages.ShowDialog() != DialogResult.OK) return;
			var libraryIndex = LibraryManager.Instance.LibraryCollection.IndexOf(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Remove(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formPages.Library);
			MainController.Instance.RequestUpdateLibrary(_formPages.Library);
		}

		private void buttonItemSettingsColumns_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			if (!MainController.Instance.SaveLibraryWarning()) return;
			if (_formColumns == null) _formColumns = new FormColumns();
			_formColumns.Library = new Library(MainController.Instance.ActiveDecorator.Library.Name, MainController.Instance.ActiveDecorator.Library.Folder, MainController.Instance.ActiveDecorator.Library.UseDirectAccess, SettingsManager.Instance.DirectAccessFileAgeLimit);
			if (_formColumns.ShowDialog() != DialogResult.OK) return;
			var libraryIndex = LibraryManager.Instance.LibraryCollection.IndexOf(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Remove(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formColumns.Library);
			MainController.Instance.RequestUpdateLibrary(_formColumns.Library);
		}

		private void buttonItemSettingsAutoWidgets_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			if (!MainController.Instance.SaveLibraryWarning()) return;
			if (_formAutoWidgets == null) _formAutoWidgets = new FormAutoWidgets();
			_formAutoWidgets.Library = new Library(MainController.Instance.ActiveDecorator.Library.Name, MainController.Instance.ActiveDecorator.Library.Folder, MainController.Instance.ActiveDecorator.Library.UseDirectAccess, SettingsManager.Instance.DirectAccessFileAgeLimit);
			if (_formAutoWidgets.ShowDialog() != DialogResult.OK) return;
			var libraryIndex = LibraryManager.Instance.LibraryCollection.IndexOf(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Remove(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formAutoWidgets.Library);
			MainController.Instance.RequestUpdateLibrary(_formAutoWidgets.Library);
		}

		private void buttonItemSettingsDeadLinks_Click(object sender, EventArgs e)
		{
			if (!MainController.Instance.SaveLibraryWarning()) return;
			if (_formDeadLinks == null) _formDeadLinks = new FormDeadLinks();
			_formDeadLinks.Library = new Library(MainController.Instance.ActiveDecorator.Library.Name, MainController.Instance.ActiveDecorator.Library.Folder, MainController.Instance.ActiveDecorator.Library.UseDirectAccess, SettingsManager.Instance.DirectAccessFileAgeLimit);
			if (_formDeadLinks.ShowDialog() != DialogResult.OK) return;
			var libraryIndex = LibraryManager.Instance.LibraryCollection.IndexOf(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Remove(MainController.Instance.ActiveDecorator.Library);
			LibraryManager.Instance.LibraryCollection.Insert(libraryIndex, _formDeadLinks.Library);
			MainController.Instance.RequestUpdateLibrary(_formDeadLinks.Library);
		}

		private void buttonItemSettingsEmailList_Click(object sender, EventArgs e)
		{
			if (_formEmailList == null) _formEmailList = new FormEmailList();
			if (MainController.Instance.ActiveDecorator != null)
				_formEmailList.ShowDialog();
		}

		private void buttonItemSettingsAutoSync_Click(object sender, EventArgs e)
		{
			if (_formAutoSync == null) _formAutoSync = new FormAutoSync();
			if (MainController.Instance.ActiveDecorator != null)
				_formAutoSync.ShowDialog();
		}

		private void buttonItemSettingsMultitab_CheckedChanged(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null || _initialization) return;
			SettingsManager.Instance.MultitabView = _tabPage.barCheckItemTabs.Checked;
			SettingsManager.Instance.Save();
			MainController.Instance.RequestChangeLibrary();
		}
		#endregion

		#region Tags
		private void ButtonItemTagsCheckedChanged(object sender, EventArgs e)
		{
			if (_initialization) return;
			var button = sender as ButtonItem;
			if (button == null) return;
			if (!button.Checked) return;
			SettingsManager.Instance.ShowTagsCategories = FormMain.Instance.buttonItemTagsCategories.Checked;
			SettingsManager.Instance.ShowTagsSuperFilters = FormMain.Instance.buttonItemTagsSuperFilters.Checked;
			SettingsManager.Instance.ShowTagsKeywords = FormMain.Instance.buttonItemTagsKeywords.Checked;
			SettingsManager.Instance.ShowTagsCleaner = FormMain.Instance.buttonItemTagsClear.Checked;
			_wallBinOptions.ShowCategoryTags = SettingsManager.Instance.ShowTagsCategories;
			_wallBinOptions.ShowSuperFilterTags = SettingsManager.Instance.ShowTagsSuperFilters;
			_wallBinOptions.ShowKeywordTags = SettingsManager.Instance.ShowTagsKeywords;
			_wallBinOptions.ShowSecurityTags = false;
			_tabPage.ApplyWallBinOptions(_wallBinOptions);
			ApplyWallBinOptions();
			_tabPage.SwitchTagsEditor();
		}

		private void ButtonItemTagsClick(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button == null) return;
			if (button.Checked) return;
			if (MainController.Instance.ActiveDecorator == null) return;
			if (_tabPage.ActiveTagsEditor == null) return;
			if (MainController.Instance.ActiveDecorator.StateChanged && AppManager.Instance.ShowQuestion("Before you leave, do you want to save the changes you made?") == DialogResult.Yes)
			{
				_tabPage.ActiveTagsEditor.ApplyData();
				MainController.Instance.ActiveDecorator.Save();
			}
			else
				MainController.Instance.ActiveDecorator.StateChanged = false;
			FormMain.Instance.buttonItemTagsCategories.Checked = false;
			FormMain.Instance.buttonItemTagsSuperFilters.Checked = false;
			FormMain.Instance.buttonItemTagsKeywords.Checked = false;
			FormMain.Instance.buttonItemTagsClear.Checked = false;
			button.Checked = true;
		}

		private void btTagsSave_Click(object sender, EventArgs e)
		{
			if (_tabPage.ActiveTagsEditor == null) return;
			if (MainController.Instance.ActiveDecorator == null) return;
			_tabPage.ActiveTagsEditor.ApplyData();
			MainController.Instance.ActiveDecorator.Save();
			AppManager.Instance.ShowInfo("All Links are saved!");
		}
		#endregion

		#region Security
		public void ResetLinksData()
		{
			_tabPage.ActiveTagsEditor.ResetData();
		}

		private void SecuritySelect_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator == null) return;
			if (MainController.Instance.ActiveDecorator.ActivePage == null) return;
			MainController.Instance.ActiveDecorator.ActivePage.SelectAllLinks();
		}

		private void SecurityReset_Click(object sender, EventArgs e)
		{
			if (_tabPage.ActiveTagsEditor == null) return;
			if (MainController.Instance.ActiveDecorator == null) return;
			if (MainController.Instance.ActiveDecorator.ActivePage == null) return;
			MainController.Instance.ActiveDecorator.ActivePage.SelectAllLinks();
			ResetLinksData();
		}

		private void SecuritySave_Click(object sender, EventArgs e)
		{
			if (_tabPage.ActiveTagsEditor == null) return;
			if (MainController.Instance.ActiveDecorator == null) return;
			_tabPage.ActiveTagsEditor.ApplyData();
			MainController.Instance.ActiveDecorator.Save();
			AppManager.Instance.ShowInfo("All Links are saved!");
		}
		#endregion

		#endregion

		#region Button's States
		public bool AddLinkButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeAddUrl.Enabled = value;
				FormMain.Instance.buttonItemHomeAddNetworkShare.Enabled = value;
			}
		}

		public bool LineBreakButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeAddLineBreak.Enabled = value;
			}
		}

		public bool UpLinkButton
		{
			set
			{
				_tabPage.barButtonItemLinkUp.Enabled = value;
			}
		}

		public bool DownLinkButton
		{
			set
			{
				_tabPage.barButtonItemLinkDown.Enabled = value;
			}
		}

		public bool LinkPropertiesNotesButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeLinkPropertiesNotes.Enabled = value;
			}
		}

		public bool LinkPropertiesTagsButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeLinkPropertiesTags.Enabled = value;
			}
		}

		public bool LinkPropertiesExpirationDateButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeLinkPropertiesExpirationDate.Enabled = value;
			}
		}

		public bool LinkPropertiesSecurityButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeLinkPropertiesSecurity.Enabled = value;
			}
		}

		public bool LinkPropertiesWidgetButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeLinkPropertiesWidget.Enabled = value;
			}
		}

		public bool LinkPropertiesBannerButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeLinkPropertiesBanner.Enabled = value;
			}
		}

		public bool OpenLinkButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeOpen.Enabled = value;
			}
		}

		public bool DeleteLinkButton
		{
			set
			{
				FormMain.Instance.buttonItemHomeDelete.Enabled = value;
			}
		}
		#endregion
	}
}