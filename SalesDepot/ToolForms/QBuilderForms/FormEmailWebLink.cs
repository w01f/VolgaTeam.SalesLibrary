using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraGrid.Views.Layout;
using SalesDepot.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormEmailWebLink : Form
	{
		private LibraryLink _sourceLink;
		private FormLogin _formLogin;

		public FormEmailWebLink()
		{
			InitializeComponent();
			_formLogin = new FormLogin(QBuilder.Instance.Login);
		}

		public void Init(LibraryLink link)
		{
			QBuilder.Instance.ConnectionChanged -= QBuilderConnectionChanged;
			QBuilder.Instance.ConnectionChanged += QBuilderConnectionChanged;
			_sourceLink = link;
			ClearControls();
			UpdateControls();
		}

		private void QBuilderConnectionChanged(object sender, ConnectionChangedArgs e)
		{
			UpdateControls();
		}

		private void ClearControls()
		{
			checkEditTitle.Checked = true;
			textEditTitle.EditValue = null;
			buttonXExpiresIn_Click(buttonXExpiresIn7, new EventArgs());
			checkEditRestricted.Checked = false;
			checkEditShowLinkToMainSite.Checked = false;
		}

		private void UpdateControls()
		{
			labelControlSiteValue.Text = QBuilder.Instance.Connected ? String.Format("Site: {0}", QBuilder.Instance.Connection.Client.Website) : "Not Selected";
			labelControlLinkName.Text = String.Format("Link: {0}", _sourceLink.Name);
			textEditTitle.EditValue = _sourceLink.Name;
			xtraTabControlOptions.Enabled = QBuilder.Instance.Connected;

			UpdateLogos();
		}

		private void UpdateLogos()
		{
			gridControlLogoGallery.DataSource = null;
			if (QBuilder.Instance.Connected && !QBuilder.Instance.Logos.Any())
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading Page Logos...";
					form.TopMost = true;
					var thread = new Thread(delegate()
					{
						QBuilder.Instance.LoadLogos();
					});
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
			gridControlLogoGallery.DataSource = QBuilder.Instance.Logos;
		}

		private void simpleButtonAddLink_Click(object sender, EventArgs e)
		{
			var result = String.Empty;
			if (!QBuilder.Instance.Connected)
			{
				AppManager.Instance.ShowWarning("You need to select site firts");
				return;
			}

			var title = textEditTitle.EditValue != null ? textEditTitle.EditValue.ToString() : null;
			var expiresInDays = 0;
			if (buttonXExpiresIn7.Checked)
				expiresInDays = 7;
			else if (buttonXExpiresIn14.Checked)
				expiresInDays = 14;
			else if (buttonXExpiresIn30.Checked)
				expiresInDays = 30;
			var restricted = checkEditRestricted.Checked;
			var showLinkToMainSite = checkEditShowLinkToMainSite.Checked;
			var selectedLogo = layoutViewLogoGallery.GetFocusedRow() as PageLogo;

			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Creating Web Link...";
				form.TopMost = true;
				form.Show();
				result = QBuilder.Instance.AddPageLite(
					_sourceLink.Identifier.ToString(),
					title,
					expiresInDays,
					restricted,
					showLinkToMainSite,
					selectedLogo != null ? selectedLogo.EncodedImage : null);
				form.Close();
			}
			if (!String.IsNullOrEmpty(result))
			{
				DialogResult = DialogResult.OK;
				Close();
				try
				{
					if (String.IsNullOrEmpty(title))
						Process.Start("mailto: ?body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + result);
					else
						Process.Start("mailto: ?subject=" + title + "&body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + result);
				}
				catch { }
			}
			else
				AppManager.Instance.ShowWarning("Link is not available on Selected Site.\nYou may need to select another Site");
		}

		private void simpleButtonLogin_Click(object sender, EventArgs e)
		{
			_formLogin.Init();
			_formLogin.ShowDialog();
		}

		private void buttonXExpiresIn_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button != null && !button.Checked)
			{
				buttonXExpiresIn7.Checked = false;
				buttonXExpiresIn14.Checked = false;
				buttonXExpiresIn30.Checked = false;
				buttonXExpiresInNever.Checked = false;
				button.Checked = true;
			}
		}

		private void checkEditTitle_CheckedChanged(object sender, EventArgs e)
		{
			if (checkEditTitle.Checked)
				textEditTitle.Enabled = true;
			else
			{
				textEditTitle.EditValue = null;
				textEditTitle.Enabled = false;
			}
		}

		private void layoutViewLogoGallery_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
		{
			var view = sender as LayoutView;
			if (view.FocusedRowHandle == e.RowHandle)
			{
				e.Appearance.BackColor = Color.Orange;
				e.Appearance.BackColor2 = Color.Orange;
			}
		}
	}
}
