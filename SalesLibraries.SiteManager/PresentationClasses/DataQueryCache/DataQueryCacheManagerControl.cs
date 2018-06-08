using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SalesLibraries.ServiceConnector.ShortcutsDataQueryCacheService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.DataQueryCache
{
	[ToolboxItem(false)]
	public partial class DataQueryCacheManagerControl : UserControl
	{
		private const string DefaultProfileId = "default";

		public DataQueryCacheManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void RefreshData(bool showMessages)
		{
			var records = new List<SoapShortcutModel>();
			var profiles = new List<ShortcutDataQueryCacheServiceProfile>(new[] { new ShortcutDataQueryCacheServiceProfile() { id = DefaultProfileId, name = "All Shortcuts", shortcutIds = new string[] { } } });
			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(() =>
					{
						records.AddRange(WebSiteManager.Instance.SelectedSite.GetLandingPages(out message));
						profiles.AddRange(WebSiteManager.Instance.SelectedSite.GetShortcutDataQueryCacheProfiles(out message));
					});
					form.Show();
					thread.Start();
					while (thread.IsAlive)
					{
						Thread.Sleep(100);
						Application.DoEvents();
					}
					form.Close();
					Enabled = true;
					FormMain.Instance.ribbonControl.Enabled = true;
				}
				if (!string.IsNullOrEmpty(message))
					AppManager.Instance.PopupMessages.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(() =>
				{
					records.AddRange(WebSiteManager.Instance.SelectedSite.GetLandingPages(out message));
					profiles.AddRange(WebSiteManager.Instance.SelectedSite.GetShortcutDataQueryCacheProfiles(out message));
				});
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}

			gridControlRecords.DataSource = records;

			gridControlProfiles.DataSource = profiles;
			ApplyProfile();
		}

		public void AddProfile()
		{
			using (var form = new FormEditProfile(true))
			{
				if (form.ShowDialog(FormMain.Instance) != DialogResult.OK) return;

				var profileModel = new ShortcutDataQueryCacheServiceProfile();
				profileModel.id = Guid.NewGuid().ToString();
				profileModel.name = form.ProfileName;

				var profiles = (IList<ShortcutDataQueryCacheServiceProfile>)gridControlProfiles.DataSource;
				profiles.Add(profileModel);

				SaveProfileInternal(profileModel);

				gridControlProfiles.RefreshDataSource();
				gridViewProfiles.RefreshData();

				gridViewProfiles.FocusedRowHandle = gridViewProfiles.RowCount - 1;
			}
		}

		private void EditProfileTitle()
		{
			if (!(gridViewProfiles.GetFocusedRow() is ShortcutDataQueryCacheServiceProfile selectedProfile) || selectedProfile.id == DefaultProfileId) return;
			using (var form = new FormEditProfile(false))
			{
				form.ProfileName = selectedProfile.name;
				if (form.ShowDialog(FormMain.Instance) != DialogResult.OK) return;
				selectedProfile.name = form.ProfileName;

				SaveProfileInternal(selectedProfile);

				gridControlProfiles.RefreshDataSource();
				gridViewProfiles.RefreshData();
			}
		}

		public void DeleteProfile()
		{
			if (!(gridViewProfiles.GetFocusedRow() is ShortcutDataQueryCacheServiceProfile selectedProfile) || selectedProfile.id == DefaultProfileId) return;
			if (AppManager.Instance.PopupMessages.ShowWarningQuestion(String.Format("Are you sure want to delete profile {0}?", selectedProfile.name)) != DialogResult.Yes) return;

			var message = string.Empty;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Deleting Profile...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.DeleteShortcutDataQueryCacheProfile(selectedProfile, out message));
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}

			var profiles = (IList<ShortcutDataQueryCacheServiceProfile>)gridControlProfiles.DataSource;
			profiles.Remove(selectedProfile);

			gridControlProfiles.RefreshDataSource();
			gridViewProfiles.RefreshData();

			if (!String.IsNullOrEmpty(message))
				AppManager.Instance.PopupMessages.ShowWarning(message);
		}

		public void SaveProfile()
		{
			if (!(gridViewProfiles.GetFocusedRow() is ShortcutDataQueryCacheServiceProfile selectedProfile) || selectedProfile.id == DefaultProfileId) return;

			SaveProfileInternal(selectedProfile);
		}

		public void ResetDataQueryCache()
		{
			var message = string.Empty;

			var shortcutModels = gridViewRecords.GetSelectedRows()
				.Select(rowIndex => gridViewRecords.GetRow(rowIndex))
				.OfType<SoapShortcutModel>()
				.ToList();

			if (!shortcutModels.Any()) return;

			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Refreshing Snapshots...";
				form.TopMost = true;

				var thread = new Thread(() =>
				{
					foreach (var shortcutModel in shortcutModels)
					{
						Console.WriteLine(shortcutModel.id);
						WebSiteManager.Instance.SelectedSite.ResetDataQueryCache(shortcutModel.id, out message);
						if (!String.IsNullOrEmpty(message))
							break;
						Console.WriteLine(String.Format("Success {0:MM/dd/yyyy hh:mm}", DateTime.Now));
						Console.WriteLine();
					}
				});
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}

			if (!String.IsNullOrEmpty(message))
				AppManager.Instance.PopupMessages.ShowWarning(message);
			else
				AppManager.Instance.PopupMessages.ShowInfo("Snapshots Updated");
		}

		private void ApplyProfile()
		{
			gridViewRecords.ClearSelection();

			if (!(gridViewProfiles.GetFocusedRow() is ShortcutDataQueryCacheServiceProfile selectedProfile)) return;

			FormMain.Instance.buttonItemDataQueryCacheProfileDelete.Enabled =
				FormMain.Instance.buttonItemDataQueryCacheProfileSave.Enabled = selectedProfile.id != DefaultProfileId;

			if (selectedProfile.id == DefaultProfileId)
				return;
			var shortcutIds = selectedProfile.shortcutIds;
			if (shortcutIds.Any())
				for (var i = 0; i < gridViewRecords.RowCount; i++)
				{
					if (!(gridViewRecords.GetRow(i) is SoapShortcutModel shortcutRecord)) continue;
					if (shortcutIds.Contains(shortcutRecord.id))
						gridViewRecords.SelectRow(i);
				}
		}

		private void SaveProfileInternal(ShortcutDataQueryCacheServiceProfile profile)
		{
			profile.shortcutIds = gridViewRecords.GetSelectedRows()
				.Select(rowIndex => gridViewRecords.GetRow(rowIndex))
				.OfType<SoapShortcutModel>()
				.Select(shortcut => shortcut.id)
				.ToArray();

			var message = string.Empty;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Saving Profile...";
				form.TopMost = true;

				var thread = new Thread(() =>
				{
					WebSiteManager.Instance.SelectedSite.SaveShortcutDataQueryCacheProfile(profile, out message);
				});
				form.Show();
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
				form.Close();
				Enabled = true;
				FormMain.Instance.ribbonControl.Enabled = true;
			}

			if (!String.IsNullOrEmpty(message))
				AppManager.Instance.PopupMessages.ShowWarning(message);
			else
				AppManager.Instance.PopupMessages.ShowInfo("Profile Saved");
		}

		private void OnProfilesFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			ApplyProfile();
		}

		private void OnProfilesCustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			if (e.Column == gridColumnProfileActions)
			{
				if (gridViewProfiles.GetRow(e.RowHandle) is ShortcutDataQueryCacheServiceProfile profile)
					e.RepositoryItem = profile.id == DefaultProfileId ? repositoryItemButtonEditActionsHidden : repositoryItemButtonEditActions;
			}
		}

		private void OnProfilesActionsButtonClick(object sender, ButtonPressedEventArgs e)
		{
			switch (e.Button.Index)
			{
				case 0:
					EditProfileTitle();
					break;
				case 1:
					DeleteProfile();
					break;
			}
		}
	}
}