using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Skins;
using SalesLibraries.Common.Helpers;
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

			retractableBar.Width = RectangleHelper.ScaleHorizontal(retractableBar.Width, Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
			retractableBar.ContentSize = RectangleHelper.ScaleHorizontal(retractableBar.ContentSize, Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
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
