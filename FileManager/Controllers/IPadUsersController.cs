using System;
using FileManager.BusinessClasses;
using FileManager.PresentationClasses.TabPages;
using FileManager.PresentationClasses.WallBin.Decorators;

namespace FileManager.Controllers
{
	public class IPadUsersController : IPageController
	{
		private TabIPadUsersControl _tabPage;
		private bool _activated;

		public IPadUsersController()
		{
			MainController.Instance.LibraryChanged += (sender, args) =>
			{
				if (_activated)
					ApplyPermissionsManager();
			};
			FormMain.Instance.buttonItemIPadUsersAdd.Click += buttonItemIPadUsersAdd_Click;
			FormMain.Instance.buttonItemIPadUsersEdit.Click += buttonItemIPadUsersEdit_Click;
			FormMain.Instance.buttonItemIPadUsersDelete.Click += buttonItemIPadUsersDelete_Click;
			FormMain.Instance.buttonItemIPadUsersRefresh.Click += buttonItemIPadUsersRefresh_Click;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabIPadUsersControl();
			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);
		}

		public void PrepareTab(TabPageEnum tabPage) { }

		public void ShowTab()
		{
			_tabPage.BringToFront();
			if (!_activated)
				ApplyPermissionsManager();
			_activated = true;
		}
		#endregion

		private void ApplyPermissionsManager()
		{
			var activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator == null || !activeDecorator.Library.IsConfigured || !SettingsManager.Instance.EnableIPadUsersTab || !ServiceConnector.Instance.Connected) return;
			activeDecorator.IPadPermissionsManager.RefreshData(true);
			if (!_tabPage.Controls.Contains(activeDecorator.IPadPermissionsManager))
				_tabPage.Controls.Add(activeDecorator.IPadPermissionsManager);
			activeDecorator.IPadContentManager.BringToFront();
		}

		public void RefreshData()
		{
			LibraryDecorator activeDecorator = MainController.Instance.ActiveDecorator;
			if (activeDecorator == null || !activeDecorator.Library.IsConfigured) return;
			if (MainController.Instance.ActiveDecorator != null && !MainController.Instance.ActiveDecorator.IPadPermissionsManager.HasConnection)
			{
				MainController.Instance.ActiveDecorator.Save();
				MainController.Instance.ActiveDecorator.IPadPermissionsManager.RefreshData(true);
			}
		}

		private void buttonItemIPadUsersAdd_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.IPadPermissionsManager.AddObject();
		}

		private void buttonItemIPadUsersEdit_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.IPadPermissionsManager.EditObject();
		}

		private void buttonItemIPadUsersDelete_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.IPadPermissionsManager.DeleteObject();
		}

		private void buttonItemIPadUsersRefresh_Click(object sender, EventArgs e)
		{
			if (MainController.Instance.ActiveDecorator != null)
				MainController.Instance.ActiveDecorator.IPadPermissionsManager.RefreshData(true);
		}
	}
}