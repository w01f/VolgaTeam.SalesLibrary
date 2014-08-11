using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.BusinessClasses;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormLogin : MetroForm
	{
		private readonly Func<string, string, string, bool, bool> _login;

		public FormLogin(Func<string, string, string, bool, bool> login)
		{
			InitializeComponent();
			_login = login;

			comboBoxEditHost.Properties.Items.Clear();
			comboBoxEditHost.Properties.Items.AddRange(ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.AvailableHosts);
			Init();
		}

		public void Init()
		{
			if (!String.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.Host))
				comboBoxEditHost.EditValue = ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.Host;
			else
				comboBoxEditHost.EditValue = ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.AvailableHosts.FirstOrDefault();
			if (!String.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.User))
				textEditUser.EditValue = ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.User;
			if (!String.IsNullOrEmpty(ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.Password))
				textEditPassword.EditValue = ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.Password;
			checkEditSave.Checked = ConfigurationClasses.SettingsManager.Instance.QBuilderSettings.SavePassword;
		}

		private void simpleButtonLogin_Click(object sender, EventArgs e)
		{
			labelControlError.Visible = false;

			var host = comboBoxEditHost.EditValue as String;
			var user = textEditUser.EditValue as String;
			var pass = textEditPassword.EditValue as String;
			var save = checkEditSave.Checked;

			if (String.IsNullOrEmpty(user) || String.IsNullOrEmpty(pass))
			{
				labelControlError.Text = "You need to fill User and Password fileds.";
				labelControlError.Visible = true;
				return;
			}

			var connected = false;
			Visible = false;
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Login...";
				form.TopMost = true;
				form.Show();
				Application.DoEvents();
				connected = _login(host, user, pass, save);
				form.Close();
			}
			if (!connected)
			{
				Visible = true;
				labelControlError.Text = "User or Password not correct for selected Site";
				labelControlError.Visible = true;
				return;
			}
			Close();
		}

		private void pictureBoxHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("qslogin");
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}
