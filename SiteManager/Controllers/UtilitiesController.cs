using SalesDepot.Services;
using SalesDepot.SiteManager.PresentationClasses.Utilities;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager.Controllers
{
	public class UtilitiesController : IPageController
	{
		private TabUtilitiesControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public UtilitiesManagerControl UtilitiesManagerControl { get; private set; }

		public UtilitiesController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabUtilitiesControl();

			UtilitiesManagerControl = new UtilitiesManagerControl();
			if (!_tabPage.Controls.Contains(UtilitiesManagerControl))
				_tabPage.Controls.Add(UtilitiesManagerControl);
			UtilitiesManagerControl.BringToFront();

			FormMain.Instance.comboBoxEditUtilitiesSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditUtilitiesSite.Properties.Items.AddRange(BusinessClasses.WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditUtilitiesSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditUtilitiesSite.EditValue as SiteClient);
			};
			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   UtilitiesManagerControl.ClearData();
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
				UtilitiesManagerControl.ClearData();
				FormMain.Instance.comboBoxEditUtilitiesSite.EditValue = BusinessClasses.WebSiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion
	}
}
