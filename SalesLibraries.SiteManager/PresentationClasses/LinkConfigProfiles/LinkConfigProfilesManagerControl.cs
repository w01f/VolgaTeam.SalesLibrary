using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using SalesLibraries.ServiceConnector.LinkConfigProfileService;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.ToolForms;

namespace SalesLibraries.SiteManager.PresentationClasses.LinkConfigProfiles
{
	[ToolboxItem(false)]
	public partial class LinkConfigProfilesManagerControl : UserControl
	{
		private bool _loading;
		private readonly List<LinkConfigProfileModel> _profileModels = new List<LinkConfigProfileModel>();
		private readonly List<ProfileControl> _profileControls = new List<ProfileControl>();

		private ProfileControl _selectedProfileControl;

		public LinkConfigProfilesManagerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void RefreshData(bool showMessages)
		{
			_loading = true;

			ClearData();

			var loadAction = new ThreadStart(() =>
			{
				LinkConfigProfilesDataHelper.LoadReferences(WebSiteManager.Instance.SelectedSite);
				_profileModels.AddRange(WebSiteManager.Instance.SelectedSite.GetLinkConfigProfiles());
			});

			var message = string.Empty;
			if (showMessages)
			{
				using (var form = new FormProgress())
				{
					FormMain.Instance.ribbonControl.Enabled = false;
					Enabled = false;
					form.laProgress.Text = "Loading data...";
					form.TopMost = true;
					var thread = new Thread(loadAction);
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
					AppManager.Instance.ShowWarning(message);
			}
			else
			{
				var thread = new Thread(loadAction);
				thread.Start();
				while (thread.IsAlive)
				{
					Thread.Sleep(100);
					Application.DoEvents();
				}
			}
			ApplyData();

			_loading = false;
		}

		public void ClearData()
		{
			SaveData();
			pnProfileContentContainer.Controls.Clear();
			gridControlProfiles.DataSource = null;
			_profileControls.Clear();
			_profileModels.Clear();
		}

		public void SaveData()
		{
			if (_selectedProfileControl == null) return;
			if (!_selectedProfileControl.NeedToSave) return;
			_selectedProfileControl.SaveData();
		}

		public void AddProfile()
		{
			using (var form = new FormEditProfile(true))
			{
				if (form.ShowDialog(FormMain.Instance) != DialogResult.OK) return;
				var newProfileModel = new LinkConfigProfileModel();
				newProfileModel.name = form.ProfileName;
				newProfileModel.order = _profileModels.Any() ? _profileModels.Max(pm => pm.order) + 1 : 0;
				_profileModels.Add(newProfileModel);

				var newProfileControl = new ProfileControl(newProfileModel);
				_profileControls.Add(newProfileControl);

				newProfileControl.SaveData();

				RefreshGrid();
				gridViewProfiles.FocusedRowHandle = _profileControls.IndexOf(newProfileControl);
			}
		}

		public void DeleteProfile()
		{
			if (_selectedProfileControl == null) return;
			if (AppManager.Instance.ShowWarningQuestion(String.Format("Are you sure want to delete profile {0}?", _selectedProfileControl.ProfileName)) != DialogResult.Yes) return;
			using (var form = new FormProgress())
			{
				FormMain.Instance.ribbonControl.Enabled = false;
				Enabled = false;
				form.laProgress.Text = "Deleting Profile...";
				form.TopMost = true;
				var thread = new Thread(() => WebSiteManager.Instance.SelectedSite.DeleteLinkConfigProfile(_selectedProfileControl.Profile));
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
			_profileControls.Remove(_selectedProfileControl);
			pnProfileContentContainer.Controls.Remove(_selectedProfileControl);
			RefreshGrid();
		}

		public void ExportFiles()
		{
			if (_selectedProfileControl == null) return;
			_selectedProfileControl.ExportFiles();
		}

		private void LoadActiveProfile()
		{
			if (_selectedProfileControl == null) return;
			if (!pnProfileContentContainer.Controls.Contains(_selectedProfileControl))
			{
				pnProfileContentContainer.Controls.Add(_selectedProfileControl);
				_selectedProfileControl.LoadData();
			}
			_selectedProfileControl.BringToFront();
		}

		private void EditProfileTitle()
		{
			if (_selectedProfileControl == null) return;
			using (var form = new FormEditProfile(false))
			{
				form.ProfileName = _selectedProfileControl.ProfileName;
				if (form.ShowDialog(FormMain.Instance) != DialogResult.OK) return;
				_selectedProfileControl.ProfileName = form.ProfileName;
				_selectedProfileControl.SaveData();
				RefreshGrid();
			}
		}

		private void ApplyData()
		{
			foreach (var profileModel in _profileModels)
			{
				var profileControl = new ProfileControl(profileModel);
				_profileControls.Add(profileControl);
			}
			RefreshGrid();

			_selectedProfileControl = gridViewProfiles.GetFocusedRow() as ProfileControl;
			LoadActiveProfile();
		}

		private void RefreshGrid()
		{
			gridControlProfiles.DataSource = _profileControls;
			gridViewProfiles.RefreshData();
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

		private void OnSelectedProfileChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
			if(_loading) return;
			if(_selectedProfileControl!= null)
				_selectedProfileControl.SaveData();
			_selectedProfileControl = gridViewProfiles.GetFocusedRow() as ProfileControl;
			LoadActiveProfile();
		}
	}
}
