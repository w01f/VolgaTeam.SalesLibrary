using System;
using System.ComponentModel;
using System.Windows.Forms;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.RetractableBar;
using SalesLibraries.SalesDepot.Configuration;
using SalesLibraries.SalesDepot.Controllers;
using SalesLibraries.SalesDepot.Properties;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class WallbinContainer : UserControl
	{
		public bool NeedToUpdate { get; set; }

		public WallbinContainer()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			pnEmpty.Dock = DockStyle.Fill;
			pnContainer.Dock = DockStyle.Fill;
			pnEmpty.BringToFront();
		}

		public void InitControl()
		{
			emailBinControl.InitControl();
			pbEmailBinHelp.Buttonize();
			UpdateEmailBin();
			retractableBar.StateChanged += OnRetractableBarStateChanged;
			MainController.Instance.Settings.SettingsChanged += OnEmailSettingsChanged;
		}

		#region Retractable Bar
		protected void UpdateEmailBin()
		{
			if ((MainController.Instance.Settings.EmailButtons & EmailButtonsDisplayOptionsEnum.DisplayEmailBin) == EmailButtonsDisplayOptionsEnum.DisplayEmailBin)
			{
				retractableBar.Visible = true;
				retractableBar.AddButtons(new[]
				{
					new ButtonInfo
					{
						Logo = Resources.EmailBinBarButtonLogo,
						Tooltip = "Expand email attachments panel"
					}
				});

				if (MainController.Instance.Settings.EmailBinSettings.ShowEmailBin)
					retractableBar.Expand(true);
				else
					retractableBar.Collapse(true);
			}
			else
				retractableBar.Visible = false;
		}

		private void OnRetractableBarStateChanged(object sender, StateChangedEventArgs e)
		{
			MainController.Instance.Settings.EmailBinSettings.ShowEmailBin = e.Expaned;
			MainController.Instance.Settings.SaveSettings();
		}

		private void OnEmailSettingsChanged(object sender, EventArgs e)
		{
			UpdateEmailBin();
		}

		private void pbEmailBinHelp_Click(object sender, EventArgs e)
		{
			MainController.Instance.HelpManager.OpenHelpLink("email");
		}
		#endregion
	}
}
