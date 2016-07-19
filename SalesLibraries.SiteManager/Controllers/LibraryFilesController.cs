using System;
using SalesLibraries.ServiceConnector.Services.Soap;
using SalesLibraries.SiteManager.BusinessClasses;
using SalesLibraries.SiteManager.PresentationClasses.LibraryFiles;

namespace SalesLibraries.SiteManager.Controllers
{
	public class LibraryFilesController : IPageController
	{
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }
		public LibraryFilesManagerControl LibraryFilesManagerControl { get; private set; }

		public LibraryFilesController()
		{
			NeedToUpdate = true;
		}

		#region IPageController Members
		public void InitController()
		{
			LibraryFilesManagerControl = new LibraryFilesManagerControl();

			FormMain.Instance.buttonItemLibrariesExport.Click += OnExportClick;

			FormMain.Instance.comboBoxEditLibrariesSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditLibrariesSite.Properties.Items.AddRange(WebSiteManager.Instance.Sites);
			FormMain.Instance.comboBoxEditLibrariesSite.EditValueChanged += (o, e) =>
			{
				if (!NeedToUpdate)
					MainController.Instance.ChangeSite(FormMain.Instance.comboBoxEditLibrariesSite.EditValue as SoapServiceConnection);
			};
			MainController.Instance.SiteChanged += (sender, args) =>
			{
				if (IsActive)
					LibraryFilesManagerControl.RefreshData(true);
				else
					NeedToUpdate = true;
			};

			if (!FormMain.Instance.pnMain.Controls.Contains(LibraryFilesManagerControl))
				FormMain.Instance.pnMain.Controls.Add(LibraryFilesManagerControl);
		}

		public void PrepareTab(TabPageEnum tabPage) { }

		public void ShowTab()
		{
			LibraryFilesManagerControl.BringToFront();
			if (NeedToUpdate)
			{
				FormMain.Instance.comboBoxEditLibrariesSite.EditValue = WebSiteManager.Instance.SelectedSite;
				LibraryFilesManagerControl.RefreshData(true);
			}
			NeedToUpdate = false;
			IsActive = true;
		}
		#endregion

		private void OnExportClick(object sender, EventArgs e)
		{
			LibraryFilesManagerControl.ExportData();
		}
	}
}
