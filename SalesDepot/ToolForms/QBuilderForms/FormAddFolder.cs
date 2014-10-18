using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormAddFolder : MetroForm
	{
		private LibraryFolder _sourceFolder;
		private readonly FormLogin _formLogin;

		public FormAddFolder()
		{
			InitializeComponent();
			_formLogin = new FormLogin(QBuilder.Instance.Login);
		}

		public void Init(LibraryFolder link)
		{
			QBuilder.Instance.ConnectionChanged -= QBuilderConnectionChanged;
			QBuilder.Instance.ConnectionChanged += QBuilderConnectionChanged;
			_sourceFolder = link;
			UpdateControls();
		}

		private void QBuilderConnectionChanged(object sender, ConnectionChangedArgs e)
		{
			UpdateControls();
		}

		private void UpdateControls()
		{
			labelControlSiteValue.Text = QBuilder.Instance.Connected ? String.Format("Site: {0}", QBuilder.Instance.Connection.Client.Website) : "Not Selected";
			labelControlFolderName.Text = String.Format("Folder: {0}", _sourceFolder.Name);
		}

		private void simpleButtonAddFolder_Click(object sender, EventArgs e)
		{
			var result = false;
			if (!QBuilder.Instance.Connected)
			{
				AppManager.Instance.ShowWarning("You need to select site first");
				return;
			}
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Adding Links to Cart...";
				form.TopMost = true;
				form.Show();
				result = QBuilder.Instance.AddFolderToCart(_sourceFolder.Identifier.ToString());
				form.Close();
			}
			if (result)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				AppManager.Instance.ShowWarning("This Folder is not yet Available in the Sales Cloud…\nTry again later…");
		}

		private void simpleButtonLogin_Click(object sender, EventArgs e)
		{
			_formLogin.Init();
			_formLogin.ShowDialog();
		}
	}
}
