using System;
using SalesLibraries.ServiceConnector.Services.Soap;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.PresentationClasses.DataQueryCache;
using SalesLibraries.SiteManager.TabPages;

namespace SalesLibraries.SiteManager.Controllers
{
	public class DataQueryCacheController : IPageController
	{
		private TabDataQueryCacheControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public DataQueryCacheManagerControl DataQueryCacheManagerControl { get; private set; }

		public DataQueryCacheController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabDataQueryCacheControl();

			DataQueryCacheManagerControl = new DataQueryCacheManagerControl();
			if (!_tabPage.Controls.Contains(DataQueryCacheManagerControl))
				_tabPage.Controls.Add(DataQueryCacheManagerControl);
			DataQueryCacheManagerControl.BringToFront();

			FormMain.Instance.buttonItemDataQueryCacheReset.Click += OnDataQueryCacheResetClick;
			FormMain.Instance.comboBoxEditDataQueryCacheSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditDataQueryCacheSite.Properties.Items.AddRange(WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditDataQueryCacheSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditDataQueryCacheSite.EditValue as SoapServiceConnection);
			};
			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   DataQueryCacheManagerControl.RefreshData(true);
														   else
															   NeedToUpdate = true;
													   };

			if (!FormMain.Instance.pnMain.Controls.Contains(_tabPage))
				FormMain.Instance.pnMain.Controls.Add(_tabPage);
		}

		public void PrepareTab(TabPageEnum tabPage) { }

		public void ShowTab()
		{
			_tabPage.BringToFront();
			if (NeedToUpdate)
			{
				FormMain.Instance.comboBoxEditDataQueryCacheSite.EditValue = WebSiteManager.Instance.SelectedSite;
				DataQueryCacheManagerControl.RefreshData(true);
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void OnDataQueryCacheResetClick(object sender, EventArgs e)
		{
			DataQueryCacheManagerControl.ResetDataQueryCache();
		}
	}
}
