using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesDepot.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.PresentationClasses.QBuilderControls;
using SalesDepot.ToolForms.QBuilderForms;

namespace SalesDepot.TabPages
{
	[ToolboxItem(false)]
	public partial class TabQBuilder : UserControl, IController
	{
		private readonly PageListControl _pageList = new PageListControl();

		private bool _needToUpdateLinkCart;
		private bool _needToUpdatePageList;
		private readonly LinkCartControl _lincCart = new LinkCartControl();

		private readonly PageContentControl _pageContent = new PageContentControl();

		public TabQBuilder()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			NeedToUpdate = true;
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			splitContainerControlBottom.Panel1.Controls.Add(_pageList);
			splitContainerControlTop.Panel1.Controls.Add(_pageContent);
			splitContainerControlTop.Panel2.Controls.Add(_lincCart);

			QBuilder.Instance.ConnectionChanged += QBuilderConnectionChanged;
			QBuilder.Instance.LinkCartChanged += QBuilderLinkCartChanged;
			QBuilder.Instance.PageListChanged += QBuilderPageListChanged;
			QBuilder.Instance.PageChanged += QBuilderPageChanged;
			FormMain.Instance.buttonItemQBuilderLogin.Click += (o, e) => Login();
			FormMain.Instance.buttonItemQBuilderLogout.Click += (o, e) => Logout();
			FormMain.Instance.buttonItemQBuilderPageList.CheckedChanged += buttonItemQBuilderPageList_CheckedChanged;
			FormMain.Instance.buttonItemQBuilderLinkCart.CheckedChanged += buttonItemQBuilderLinkCart_CheckedChanged;
			FormMain.Instance.buttonItemQBuilderPagesAdd.Click += buttonItemQBuilderPagesAdd_Click;
			FormMain.Instance.buttonItemQBuilderPagesDelete.Click += buttonItemQBuilderPagesDelete_Click;
			FormMain.Instance.buttonItemQBuilderPagesSave.Click += buttonItemQBuilderPagesSave_Click;
			FormMain.Instance.buttonItemQBuilderPagesPreview.Click += buttonItemQBuilderPagesPreview_Click;
			FormMain.Instance.buttonItemQBuilderPagesEmail.Click += buttonItemQBuilderPagesEmail_Click;
			FormMain.Instance.buttonItemQBuilderHelp.Click += buttonItemHelp_Click;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			Focus();
			AppManager.Instance.ActivityManager.AddUserActivity("quickSITES Builder selected");
			if (NeedToUpdate)
			{
				if (!QBuilder.Instance.Connected)
					Login();
				else
					UpdateContent();

				_needToUpdateLinkCart = false;
				_needToUpdatePageList = false;
			}

			if (_needToUpdateLinkCart)
				_lincCart.UpdateContent();

			if (_needToUpdatePageList)
				_pageList.UpdateContent();

			NeedToUpdate = false;
			_needToUpdateLinkCart = false;
			_needToUpdatePageList = false;
		}
		#endregion

		private void QBuilderLinkCartChanged(object sender, EventArgs e)
		{
			if (IsActive)
				_lincCart.UpdateContent();
			else
				_needToUpdateLinkCart = true;
		}

		private void QBuilderPageListChanged(object sender, EventArgs e)
		{
			if (IsActive)
				_pageList.UpdateContent();
			else
				_needToUpdatePageList = true;
		}

		private void QBuilderPageChanged(object sender, EventArgs e)
		{
			_pageContent.UpdateContent();
		}

		#region Ribbon Buttons Clicks
		private void buttonItemQBuilderPageList_CheckedChanged(object sender, EventArgs e)
		{
			splitContainerControlBottom.PanelVisibility = FormMain.Instance.buttonItemQBuilderPageList.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
		}

		private void buttonItemQBuilderLinkCart_CheckedChanged(object sender, EventArgs e)
		{
			splitContainerControlTop.PanelVisibility = FormMain.Instance.buttonItemQBuilderLinkCart.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel1;
		}

		public void buttonItemQBuilderPagesAdd_Click(object sender, EventArgs e)
		{
			_pageList.AddPage();
		}

		public void buttonItemQBuilderPagesDelete_Click(object sender, EventArgs e)
		{
			_pageList.DeletePage();
		}

		public void buttonItemQBuilderPagesSave_Click(object sender, EventArgs e)
		{
			_pageContent.SavePage();
		}

		public void buttonItemQBuilderPagesPreview_Click(object sender, EventArgs e)
		{
			_pageContent.SavePage();
			_pageList.PreviewPage();
		}

		public void buttonItemQBuilderPagesEmail_Click(object sender, EventArgs e)
		{
			_pageContent.SavePage();
			_pageList.EmailPage();
		}

		public void buttonItemHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("quicksites");
		}
		#endregion

		#region Login/Logout Processing
		private void Login()
		{
			using (var form = new FormLogin(QBuilder.Instance.Login))
			{
				form.ShowDialog();
			}
		}

		private void Logout()
		{
			QBuilder.Instance.Logout();
		}

		private void QBuilderConnectionChanged(object sender, ConnectionChangedArgs e)
		{
			NeedToUpdate = true;
			if (QBuilder.Instance.Connected)
			{
				ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.Host = e.Connection.Client.Website;
				ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.User = e.Connection.Client.User;
				ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.Password = e.Connection.SavePassword ? e.Connection.Client.Password : String.Empty;
				ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.SavePassword = e.Connection.SavePassword;
				ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.SaveLocalSettings();
			}
			UpdateContent();
		}

		private void UpdateContent()
		{
			if (!IsActive) return;
			UpdateRibbon();
			_lincCart.UpdateContent();
			_pageList.UpdateContent();
			_pageContent.UpdateContent();
		}

		private void UpdateRibbon()
		{
			if (QBuilder.Instance.Connected)
			{
				FormMain.Instance.ribbonBarQBuilderLogin.Text = "Logout";
				FormMain.Instance.buttonItemQBuilderLogin.Visible = false;
				FormMain.Instance.buttonItemQBuilderLogout.Visible = true;
				FormMain.Instance.buttonItemQBuilderPagesAdd.Enabled = true;
				FormMain.Instance.buttonItemQBuilderPagesDelete.Enabled = true;
				FormMain.Instance.buttonItemQBuilderPagesSave.Enabled = true;
				FormMain.Instance.buttonItemQBuilderPagesPreview.Enabled = true;
				FormMain.Instance.buttonItemQBuilderPagesEmail.Enabled = true;
			}
			else
			{
				FormMain.Instance.ribbonBarQBuilderLogin.Text = "Login";
				FormMain.Instance.buttonItemQBuilderLogin.Visible = true;
				FormMain.Instance.buttonItemQBuilderLogout.Visible = false;
				FormMain.Instance.buttonItemQBuilderPagesAdd.Enabled = false;
				FormMain.Instance.buttonItemQBuilderPagesDelete.Enabled = false;
				FormMain.Instance.buttonItemQBuilderPagesSave.Enabled = false;
				FormMain.Instance.buttonItemQBuilderPagesPreview.Enabled = false;
				FormMain.Instance.buttonItemQBuilderPagesEmail.Enabled = false;
			}
			FormMain.Instance.ribbonBarQBuilderLogin.RecalcLayout();
			FormMain.Instance.ribbonPanelQBuilder.PerformLayout();
		}
		#endregion


	}
}