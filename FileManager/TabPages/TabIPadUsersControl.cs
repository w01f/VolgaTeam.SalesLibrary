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

		public void buttonItemIPadUsersAdd_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadUsers.AddUser();
		}

		public void buttonItemIPadUsersEdit_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadUsers.EditUser();
		}

		public void buttonItemIPadUsersDelete_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadUsers.DeleteUser();
		}

		public void buttonItemIPadUsersRefresh_Click(object sender, EventArgs e)
		{
			if (PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator != null)
				PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActiveDecorator.IPadUsers.UpdateUsers(true);
		}

	}
}
