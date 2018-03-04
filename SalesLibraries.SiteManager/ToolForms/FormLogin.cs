using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace SalesLibraries.SiteManager.ToolForms
{
	public partial class FormLogin : Form
	{
		private const string ErrorTextFormat = "<span align=\"center\"><font color=\"#ED1C24\">{0}</font></span>";

		public event EventHandler<LoginEventArgs> Logining;

		public FormLogin(string authUrl, IList<string> approvedUsers)
		{
			InitializeComponent();

			Text = String.Format(Text, authUrl);
			comboBoxExLogin.Items.AddRange(approvedUsers.ToArray());
			labelXSiteCheck.Text = String.Format(labelXSiteCheck.Text, authUrl);

			if ((CreateGraphics()).DpiX > 96)
			{
				labelXErrorText.Font = new Font(labelXErrorText.Font.FontFamily, labelXErrorText.Font.Size - 2, labelXErrorText.Font.Style);
				labelXMainTitle.Font = new Font(labelXMainTitle.Font.FontFamily, labelXMainTitle.Font.Size - 2, labelXMainTitle.Font.Style);
				labelXPasswordDescription.Font = new Font(labelXPasswordDescription.Font.FontFamily, labelXPasswordDescription.Font.Size - 2, labelXPasswordDescription.Font.Style);
				labelXPasswordTitle.Font = new Font(labelXPasswordTitle.Font.FontFamily, labelXPasswordTitle.Font.Size - 2, labelXPasswordTitle.Font.Style);
				labelXSiteCheck.Font = new Font(labelXSiteCheck.Font.FontFamily, labelXSiteCheck.Font.Size - 2, labelXSiteCheck.Font.Style);
				labelXUserDescription.Font = new Font(labelXUserDescription.Font.FontFamily, labelXUserDescription.Font.Size - 2, labelXUserDescription.Font.Style);
				labelXUserTitle.Font = new Font(labelXUserTitle.Font.FontFamily, labelXUserTitle.Font.Size - 2, labelXUserTitle.Font.Style);
			}
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			pnInfo.BackColor = Color.White;
		}

		private void OnUrlLabelClick(object sender, MarkupLinkClickEventArgs e)
		{
			Process.Start(e.HRef);
		}

		private void OnOKClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(comboBoxExLogin.Text) ||
				String.IsNullOrEmpty(textBoxXPassword.Text))
			{
				ShowError("Select user name and password");
				return;
			}
			ResetError();
			ShowProgress();

			var loginArgs = new LoginEventArgs(comboBoxExLogin.Text, textBoxXPassword.Text);
			Logining(this, loginArgs);

			HideProgress();

			if (loginArgs.Accepted)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				ShowError("User name, email or password are wrong");
			}
		}

		private void ShowProgress()
		{
			circularProgress.IsRunning = true;
			circularProgress.Visible = true;
		}

		private void HideProgress()
		{
			circularProgress.Visible = false;
			circularProgress.IsRunning = false;
		}

		private void ShowError(string text)
		{
			labelXErrorText.Text = String.Format(ErrorTextFormat, text);
			labelXErrorText.Visible = true;
		}

		private void ResetError()
		{
			circularProgress.IsRunning = true;
			labelXErrorText.Text = String.Empty;
		}

		private void OnEnterKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
				OnOKClick(sender, e);
		}
	}

	public class LoginEventArgs : EventArgs
	{
		public string Login { get; }
		public string Password { get; }
		public bool Accepted { get; set; }

		public LoginEventArgs(string login, string password)
		{
			Login = login;
			Password = password;
		}
	}
}
