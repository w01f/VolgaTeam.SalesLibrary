using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesDepot.SiteManager.ConfigurationClasses;
using SalesDepot.SiteManager.TabPages;

namespace SalesDepot.SiteManager
{
	public partial class FormMain : Form
	{
		private static FormMain _instance;

		private FormMain()
		{
			InitializeComponent();

			TabHome = new TabHomeControl();
		}

		public TabHomeControl TabHome { get; private set; }

		public static FormMain Instance
		{
			get { return _instance ?? (_instance = new FormMain()); }
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.IconPath))
				Icon = new Icon(SettingsManager.Instance.IconPath);
			if (File.Exists(SettingsManager.Instance.LogoPath))
				labelItemHomeLogo.Image = new Bitmap(SettingsManager.Instance.LogoPath);

			TabHome = new TabHomeControl();
			buttonItemHomeAdd.Click += TabHome.buttonItemIPadUsersAdd_Click;
			buttonItemHomeEdit.Click += TabHome.buttonItemIPadUsersEdit_Click;
			buttonItemHomeDelete.Click += TabHome.buttonItemIPadUsersDelete_Click;
			buttonItemHomeRefresh.Click += TabHome.buttonItemIPadUsersRefresh_Click;
			buttonItemHomeImport.Click += TabHome.buttonItemIPadUsersImport_Click;
			if (!pnMain.Controls.Contains(TabHome))
				pnMain.Controls.Add(TabHome);
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			TabHome.InitControl();
		}

		private void buttonItemHomeExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		#region Select All in Editor Handlers
		private bool _enter;
		private bool _needSelect;

		public void Editor_Enter(object sender, EventArgs e)
		{
			_enter = true;
			BeginInvoke(new MethodInvoker(ResetEnterFlag));
		}

		public void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			if (_needSelect)
			{
				(sender as BaseEdit).SelectAll();
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