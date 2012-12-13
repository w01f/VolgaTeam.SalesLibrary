using System;
using System.Linq;
using System.Windows.Forms;
using SalesDepot.SiteManager.PresentationClasses;

namespace SalesDepot.SiteManager.TabPages
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class TabHomeControl : UserControl
	{
		public PermissionsManagerControl PermissionsManagerControl { get; private set; }

		public TabHomeControl()
		{
			InitializeComponent();
			this.Dock = DockStyle.Fill;
		}

		public void InitControl()
		{
			this.PermissionsManagerControl = new PermissionsManagerControl();
			if (!this.Controls.Contains(this.PermissionsManagerControl))
				this.Controls.Add(this.PermissionsManagerControl);

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
			this.PermissionsManagerControl.RefreshData(true);
		}

		public void buttonItemIPadUsersAdd_Click(object sender, EventArgs e)
		{
			this.PermissionsManagerControl.AddObject();
		}

		public void buttonItemIPadUsersEdit_Click(object sender, EventArgs e)
		{
			this.PermissionsManagerControl.EditObject();
		}

		public void buttonItemIPadUsersDelete_Click(object sender, EventArgs e)
		{
			this.PermissionsManagerControl.DeleteObject();
		}

		public void buttonItemIPadUsersRefresh_Click(object sender, EventArgs e)
		{
			this.PermissionsManagerControl.RefreshData(true);
		}

	}
}
