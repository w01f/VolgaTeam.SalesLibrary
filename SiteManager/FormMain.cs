using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager
{
	public partial class FormMain : Form
	{
		private static FormMain _instance = null;

		public TabHomeControl TabHome { get; private set; }

		private FormMain()
		{
			InitializeComponent();

			this.TabHome = new TabHomeControl();
		}

		public static FormMain Instance
		{
			get { return _instance ?? (_instance = new FormMain()); }
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (File.Exists(ConfigurationClasses.SettingsManager.Instance.IconPath))
				this.Icon = new Icon(ConfigurationClasses.SettingsManager.Instance.IconPath);
			if (File.Exists(ConfigurationClasses.SettingsManager.Instance.LogoPath))
				labelItemHomeLogo.Image = new Bitmap(ConfigurationClasses.SettingsManager.Instance.LogoPath);

			this.TabHome = new TabHomeControl();
			buttonItemHomeAdd.Click += this.TabHome.buttonItemIPadUsersAdd_Click;
			buttonItemHomeEdit.Click += this.TabHome.buttonItemIPadUsersEdit_Click;
			buttonItemHomeDelete.Click += this.TabHome.buttonItemIPadUsersDelete_Click;
			buttonItemHomeRefresh.Click += this.TabHome.buttonItemIPadUsersRefresh_Click;
			if (!pnMain.Controls.Contains(this.TabHome))
				pnMain.Controls.Add(this.TabHome);
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			TabHome.InitControl();
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#region Select All in Editor Handlers
		private bool _enter = false;
		private bool _needSelect = false;

		public void Editor_Enter(object sender, EventArgs e)
		{
			_enter = true;
			BeginInvoke(new MethodInvoker(ResetEnterFlag));
		}

		public void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			if (_needSelect)
			{
				(sender as DevExpress.XtraEditors.BaseEdit).SelectAll();
			}
		}

		public void Editor_MouseDown(object sender, MouseEventArgs e)
		{
			_needSelect = _enter;
		}

		private void ResetEnterFlag()
		{
			_enter = false;
		}
		#endregion
	}
}
