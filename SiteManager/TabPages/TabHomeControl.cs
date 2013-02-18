using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.SiteManager.PresentationClasses;

namespace SalesDepot.SiteManager.TabPages
{
	[ToolboxItem(false)]
	public partial class TabHomeControl : UserControl
	{
		public TabHomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public PermissionsManagerControl PermissionsManagerControl { get; private set; }

		public void InitControl()
		{
			PermissionsManagerControl = new PermissionsManagerControl();
			if (!Controls.Contains(PermissionsManagerControl))
				Controls.Add(PermissionsManagerControl);

			LoadSiteList();

			RefreshData();
		}

		private void LoadSiteList()
		{
			FormMain.Instance.comboBoxEditSite.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditSite.Properties.Items.AddRange(BusinessClasses.SiteManager.Instance.Sites.Select(x => x.Website).ToArray());
			if (BusinessClasses.SiteManager.Instance.SelectedSite != null)
				FormMain.Instance.comboBoxEditSite.EditValue = BusinessClasses.SiteManager.Instance.SelectedSite.Website;
			FormMain.Instance.comboBoxEditSite.EditValueChanged += (o, e) =>
																	   {
																		   BusinessClasses.SiteManager.Instance.SelectSite(FormMain.Instance.comboBoxEditSite.EditValue != null ? FormMain.Instance.comboBoxEditSite.EditValue.ToString() : string.Empty);
																		   RefreshData();
																	   };
		}

		public void RefreshData()
		{
			PermissionsManagerControl.RefreshData(true);
		}

		public void buttonItemIPadUsersAdd_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.AddObject();
		}

		public void buttonItemIPadUsersEdit_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.EditObject();
		}

		public void buttonItemIPadUsersDelete_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.DeleteObject();
		}

		public void buttonItemIPadUsersRefresh_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.RefreshData(true);
		}

		public void buttonItemIPadUsersImport_Click(object sender, EventArgs e)
		{
			PermissionsManagerControl.ImportUsers();
		}
	}
}