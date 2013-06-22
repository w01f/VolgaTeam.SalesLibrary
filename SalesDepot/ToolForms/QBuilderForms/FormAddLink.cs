using System;
using System.Windows.Forms;
using SalesDepot.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormAddLink : Form
	{
		private LibraryLink _sourceLink;
		private FormLogin _formLogin;

		public FormAddLink()
		{
			InitializeComponent();
			_formLogin = new FormLogin(QBuilder.Instance.Login);
		}

		public void Init(LibraryLink link)
		{
			QBuilder.Instance.ConnectionChanged -= QBuilderConnectionChanged;
			QBuilder.Instance.ConnectionChanged += QBuilderConnectionChanged;
			_sourceLink = link;
			UpdateControls();
		}

		private void QBuilderConnectionChanged(object sender, ConnectionChangedArgs e)
		{
			UpdateControls();
		}

		private void UpdateControls()
		{
			labelControlSiteValue.Text = QBuilder.Instance.Connected ? String.Format("Site: {0}", QBuilder.Instance.Connection.Client.Website) : "Not Selected";
			labelControlLinkName.Text = String.Format("Link: {0}", _sourceLink.Name);
		}

		private void simpleButtonAddLink_Click(object sender, EventArgs e)
		{
			var result = false;
			if (!QBuilder.Instance.Connected)
			{
				AppManager.Instance.ShowWarning("You need to select site first");
				return;
			}
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Adding Link to Cart...";
				form.TopMost = true;
				form.Show();
				result = QBuilder.Instance.AddLinkToCart(_sourceLink.Identifier.ToString());
				form.Close();
			}
			if (result)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				AppManager.Instance.ShowWarning("Link is not available on Selected Site.\nYou may need to select another Site");
		}

		private void simpleButtonLogin_Click(object sender, EventArgs e)
		{
			_formLogin.Init();
			_formLogin.ShowDialog();
		}
	}
}
