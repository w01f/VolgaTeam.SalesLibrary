using System;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.BusinessClasses;
using SalesDepot.CommonGUI.Forms;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormAddLink : MetroForm
	{
		private LibraryLink _sourceLink;
		private readonly FormLogin _formLogin;

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
			Text = _sourceLink.Type != FileTypes.LineBreak ?
				"Add Link to quickSITE" :
				"Add LinkBreak to quickSITE";
			labelControlSiteTitle.Text = _sourceLink.Type != FileTypes.LineBreak ?
				"You are about to add link to Link Cart on Selected site:" :
				"You are about to add this LineBreak to Link Cart on Selected site:";
			labelControlLinkName.Text = String.Format("Link: {0}", _sourceLink.Type != FileTypes.LineBreak ? _sourceLink.Name : String.Format("LineBreak{0}", !String.IsNullOrEmpty(_sourceLink.Name) ? String.Format(" ({0})", _sourceLink.Name) : String.Empty));
			UpdateControls();
		}

		private void QBuilderConnectionChanged(object sender, ConnectionChangedArgs e)
		{
			UpdateControls();
		}

		private void UpdateControls()
		{
			labelControlSiteValue.Text = QBuilder.Instance.Connected ? String.Format("Site: {0}", QBuilder.Instance.Connection.Client.Website) : "Not Selected";
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
				AppManager.Instance.ShowWarning(_sourceLink.Type != FileTypes.LineBreak ?
					"This Link is not yet Available in the Sales Cloud…\nTry again later…":
					"This LineBreak is not yet Available in the Sales Cloud…\nTry again later…");
		}

		private void simpleButtonLogin_Click(object sender, EventArgs e)
		{
			_formLogin.Init();
			_formLogin.ShowDialog();
		}
	}
}
