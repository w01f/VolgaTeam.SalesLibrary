using System;
using System.Windows.Forms;

namespace FileManager.TabPages
{
	[System.ComponentModel.ToolboxItem(false)]
	public partial class TabIPadUsersControl : UserControl
	{
		public TabIPadUsersControl()
		{
			InitializeComponent();
			this.Dock = DockStyle.Fill;
		}

		public void RefreshData()
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null && !PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadPermissionsManager.HasConnection)
			{
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.Save();
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadPermissionsManager.RefreshData(true);
			}
		}

		public void buttonItemIPadUsersAdd_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadPermissionsManager.AddObject();
		}

		public void buttonItemIPadUsersEdit_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadPermissionsManager.EditObject();
		}

		public void buttonItemIPadUsersDelete_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadPermissionsManager.DeleteObject();
		}

		public void buttonItemIPadUsersRefresh_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadPermissionsManager.RefreshData(true);
		}

	}
}
