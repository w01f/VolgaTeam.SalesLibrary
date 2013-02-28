using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.SiteManager;
using SalesDepot.SiteManager.BusinessClasses;
using SalesDepot.SiteManager.PresentationClasses.Activities;
using SalesDepot.SiteManager.TabPages;

namespace FileManager.Controllers
{
	public class ActivitiesController : IPageController
	{
		private TabActivitiesControl _tabPage;

		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public ActivitiesManagerControl ActivitiesManagerControl { get; private set; }

		public ActivitiesController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			_tabPage = new TabActivitiesControl();

			ActivitiesManagerControl = new ActivitiesManagerControl();
			if (!_tabPage.Controls.Contains(ActivitiesManagerControl))
				_tabPage.Controls.Add(ActivitiesManagerControl);
			ActivitiesManagerControl.BringToFront();

			FormMain.Instance.comboBoxEditActivitiesSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditActivitiesSite.Properties.Items.AddRange(SiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditActivitiesSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditActivitiesSite.EditValue as SiteClient);
			};
			MainController.Instance.SiteChanged += (sender, args) =>
													   {
														   if (IsActive)
															   ActivitiesManagerControl.ClearData();
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
				ActivitiesManagerControl.ClearData();
				FormMain.Instance.comboBoxEditActivitiesSite.EditValue = SiteManager.Instance.SelectedSite;
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion
	}
}