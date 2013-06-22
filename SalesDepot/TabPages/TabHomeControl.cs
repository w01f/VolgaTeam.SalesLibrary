using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;
using SalesDepot.InteropClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using SalesDepot.ToolForms;
using SalesDepot.ToolForms.Settings;
using SalesDepot.ToolForms.WallBin;

namespace SalesDepot.TabPages
{
	[ToolboxItem(false)]
	public partial class TabHomeControl : UserControl, IController
	{
		private readonly SuperTooltipInfo _accordionToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Accordion View", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _classicToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Column View", null, null, eTooltipColor.Gray);
		private readonly string _emailBinFileName = string.Empty;
		private readonly Dictionary<LibraryLink, string> _emailLinks = new Dictionary<LibraryLink, string>();

		private readonly SuperTooltipInfo _emailToolTip = new SuperTooltipInfo("HELP", "", "Learn more about how to EMAIL files from this Sales Library", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _listToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library List View", null, null, eTooltipColor.Gray);

		public AppManager.SingleParamDelegate PageChanged;
		public AppManager.SingleParamDelegate StationChanged;
		private bool _allowToSave;

		public TabHomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			NeedToUpdate = true;

			_emailBinFileName = string.Format(@"{0}\newlocaldirect.com\xml\Sales Depot\Settings\EmailBin.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			if ((CreateGraphics()).DpiX > 96)
			{
				buttonXPDF.Font = new Font(buttonXPDF.Font.FontFamily, buttonXPDF.Font.Size - 2, buttonXPDF.Font.Style);
				buttonXZip.Font = new Font(buttonXZip.Font.FontFamily, buttonXZip.Font.Size - 2, buttonXZip.Font.Style);
				buttonXEmptyEmailBin.Font = new Font(buttonXEmptyEmailBin.Font.FontFamily, buttonXEmptyEmailBin.Font.Size - 2, buttonXEmptyEmailBin.Font.Style);
				buttonXCreateEmail.Font = new Font(buttonXCreateEmail.Font.FontFamily, buttonXCreateEmail.Font.Size - 2, buttonXCreateEmail.Font.Style);
			}
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.comboBoxItemPackages.SelectedIndexChanged += comboBoxItemPackages_SelectedIndexChanged;
			FormMain.Instance.comboBoxItemStations.SelectedIndexChanged += comboBoxItemStations_SelectedIndexChanged;
			FormMain.Instance.comboBoxItemPages.SelectedIndexChanged += comboBoxItemPages_SelectedIndexChanged;
			barButtonItemFontUp.ItemClick += buttonItemLargerText_Click;
			barButtonItemFontDown.ItemClick += buttonItemSmallerText_Click;
			barButtonItemRowSpaceUp.ItemClick += buttonItemLargerRowSpace_Click;
			barButtonItemRowSpaceDown.ItemClick += buttonItemSmallerRowSpace_Click;
			FormMain.Instance.buttonItemEmailBin.CheckedChanged += buttonItemEmailBin_CheckedChanged;
			FormMain.Instance.buttonItemHomeHelp.Click += buttonItemHomeHelp_Click;

			FormMain.Instance.buttonItemSettingsLaunchPowerPoint.CheckedChanged += buttonItemSettingsLaunchPowerPoint_CheckedChanged;
			barCheckItemViewTabs.CheckedChanged += buttonItemSettingsMultitab_CheckedChanged;
			FormMain.Instance.buttonItemSettingsPowerPointLaunch.Click += buttonItemSettingsPowerPointSettings_Click;
			FormMain.Instance.buttonItemSettingsPowerPointMenu.Click += buttonItemSettingsPowerPointSettings_Click;
			FormMain.Instance.buttonItemSettingsPowerPointViewer.Click += buttonItemSettingsPowerPointSettings_Click;
			FormMain.Instance.buttonItemSettingsPowerPointLaunch.CheckedChanged += buttonItemSettingsPowerPointSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsPowerPointMenu.CheckedChanged += buttonItemSettingsPowerPointSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsPowerPointViewer.CheckedChanged += buttonItemSettingsPowerPointSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsPDFLaunch.Click += buttonItemSettingsPDFSettings_Click;
			FormMain.Instance.buttonItemSettingsPDFMenu.Click += buttonItemSettingsPDFSettings_Click;
			FormMain.Instance.buttonItemSettingsPDFViewer.Click += buttonItemSettingsPDFSettings_Click;
			FormMain.Instance.buttonItemSettingsPDFLaunch.CheckedChanged += buttonItemSettingsPDFSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsPDFMenu.CheckedChanged += buttonItemSettingsPDFSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsPDFViewer.CheckedChanged += buttonItemSettingsPDFSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsWordLaunch.Click += buttonItemSettingsWordSettings_Click;
			FormMain.Instance.buttonItemSettingsWordMenu.Click += buttonItemSettingsWordSettings_Click;
			FormMain.Instance.buttonItemSettingsWordViewer.Click += buttonItemSettingsWordSettings_Click;
			FormMain.Instance.buttonItemSettingsWordLaunch.CheckedChanged += buttonItemSettingsWordSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsWordMenu.CheckedChanged += buttonItemSettingsWordSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsWordViewer.CheckedChanged += buttonItemSettingsWordSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsExcelLaunch.Click += buttonItemSettingsExcelSettings_Click;
			FormMain.Instance.buttonItemSettingsExcelMenu.Click += buttonItemSettingsExcelSettings_Click;
			FormMain.Instance.buttonItemSettingsExcelViewer.Click += buttonItemSettingsExcelSettings_Click;
			FormMain.Instance.buttonItemSettingsExcelLaunch.CheckedChanged += buttonItemSettingsExcelSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsExcelMenu.CheckedChanged += buttonItemSettingsExcelSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsExcelViewer.CheckedChanged += buttonItemSettingsExcelSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsVideoLaunch.Click += buttonItemSettingsVideoSettings_Click;
			FormMain.Instance.buttonItemSettingsVideoMenu.Click += buttonItemSettingsVideoSettings_Click;
			FormMain.Instance.buttonItemSettingsVideoViewer.Click += buttonItemSettingsVideoSettings_Click;
			FormMain.Instance.buttonItemSettingsVideoLaunch.CheckedChanged += buttonItemSettingsVideoSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsVideoMenu.CheckedChanged += buttonItemSettingsVideoSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsVideoViewer.CheckedChanged += buttonItemSettingsVideoSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsFolderLaunch.Click += buttonItemSettingsFolderSettings_Click;
			FormMain.Instance.buttonItemSettingsFolderMenu.Click += buttonItemSettingsFolderSettings_Click;
			FormMain.Instance.buttonItemSettingsFolderViewer.Click += buttonItemSettingsFolderSettings_Click;
			FormMain.Instance.buttonItemSettingsFolderLaunch.CheckedChanged += buttonItemSettingsFolderSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsFolderMenu.CheckedChanged += buttonItemSettingsFolderSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsFolderViewer.CheckedChanged += buttonItemSettingsFolderSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsQuickViewImages.Click += buttonItemSettingsQuickView_Click;
			FormMain.Instance.buttonItemSettingsQuickViewSlides.Click += buttonItemSettingsQuickView_Click;
			FormMain.Instance.buttonItemSettingsQuickViewImages.CheckedChanged += buttonItemSettingsQuickViewSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsQuickViewSlides.CheckedChanged += buttonItemSettingsQuickViewSettings_CheckedChanged;
			FormMain.Instance.buttonItemSettingsEmail.Click += buttonItemSettingsEmail_Click;
			FormMain.Instance.buttonItemSettingsHelp.Click += buttonItemSettingsHelp_Click;

			LoadWallBinSettings();
			UpdateFontButtonStatus();
			UpdateRowSpaceButtonStatus();
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
			AppManager.Instance.ActivityManager.AddUserActivity("Wall Bin selected");
			SettingsManager.Instance.HomeView = true;
			SettingsManager.Instance.SaveSettings();
		}
		#endregion

		#region Methods
		public void LoadTab()
		{
			_allowToSave = false;
			LoadPackages();
			ApplySelectedDecorator();
			InitEmailBin();
			ChangeView();
			_allowToSave = true;
		}

		private void LoadWallBinSettings()
		{
			barCheckItemViewClassic.Caption = !string.IsNullOrEmpty(SettingsManager.Instance.ClassicTitle) ? SettingsManager.Instance.ClassicTitle : barCheckItemViewClassic.Caption;
			barCheckItemViewList.Caption = !string.IsNullOrEmpty(SettingsManager.Instance.ListTitle) ? SettingsManager.Instance.ListTitle : barCheckItemViewList.Caption;
			barCheckItemViewAccordion.Caption = !string.IsNullOrEmpty(SettingsManager.Instance.AccordionTitle) ? SettingsManager.Instance.AccordionTitle : barCheckItemViewAccordion.Caption;

			#region Wall Bin Last Saved State
			barCheckItemViewClassic.Checked = SettingsManager.Instance.ClassicView;
			barCheckItemViewList.Checked = SettingsManager.Instance.ListView;
			barCheckItemViewAccordion.Checked = SettingsManager.Instance.AccordionView;

			barCheckItemViewClassic.CheckedChanged += ChangeView_CheckedChanged;
			barCheckItemViewList.CheckedChanged += ChangeView_CheckedChanged;
			barCheckItemViewAccordion.CheckedChanged += ChangeView_CheckedChanged;
			#endregion

			#region Wall Bin Configuration
			FormMain.Instance.buttonItemSettingsLaunchPowerPoint.Checked = SettingsManager.Instance.LaunchPPT;
			barCheckItemViewTabs.Checked = SettingsManager.Instance.MultitabView;
			switch (SettingsManager.Instance.PowerPointLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.PDFLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsPDFViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsPDFMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsPDFLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.WordLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsWordViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsWordMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsWordLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.ExcelLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsExcelViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsExcelMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsExcelLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.VideoLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsVideoViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsVideoMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsVideoLaunch.Checked = true;
					break;
			}
			switch (SettingsManager.Instance.FolderLaunchOptions)
			{
				case LinkLaunchOptions.Viewer:
					FormMain.Instance.buttonItemSettingsFolderViewer.Checked = true;
					break;
				case LinkLaunchOptions.Menu:
					FormMain.Instance.buttonItemSettingsFolderMenu.Checked = true;
					break;
				case LinkLaunchOptions.Launch:
					FormMain.Instance.buttonItemSettingsFolderLaunch.Checked = true;
					break;
			}
			FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = !SettingsManager.Instance.OldStyleQuickView;
			FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = SettingsManager.Instance.OldStyleQuickView;
			#endregion
		}

		private void LoadPackages()
		{
			FormMain.Instance.comboBoxItemPackages.Items.Clear();
			foreach (LibraryPackage salesDepot in LibraryManager.Instance.LibraryPackageCollection)
			{
				FormMain.Instance.comboBoxItemPackages.Items.Add(salesDepot.Name);
				var packageLable = new LabelItem();
				packageLable.Text = salesDepot.Name;
				packageLable.TextAlignment = StringAlignment.Near;
				while (packageLable.Text.Length < 20)
					packageLable.Text += " ";
			}

			FormMain.Instance.comboBoxItemPackages.Enabled = true;
			FormMain.Instance.comboBoxItemStations.Enabled = true;
			FormMain.Instance.comboBoxItemPages.Enabled = !SettingsManager.Instance.MultitabView;

			if (FormMain.Instance.comboBoxItemPackages.Items.Count > 0)
			{
				int previousSelectedPackageIndex = FormMain.Instance.comboBoxItemPackages.Items.IndexOf(SettingsManager.Instance.SelectedPackage);
				if (previousSelectedPackageIndex >= 0)
					FormMain.Instance.comboBoxItemPackages.SelectedIndex = previousSelectedPackageIndex;
				else
					FormMain.Instance.comboBoxItemPackages.SelectedIndex = 0;
				FormMain.Instance.comboBoxItemPackages.Enabled = FormMain.Instance.comboBoxItemPackages.Items.Count > 1;
				DecoratorManager.Instance.ActivePackageViewer = DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];
			}
			else
			{
				FormMain.Instance.comboBoxItemPackages.Enabled = false;
				FormMain.Instance.comboBoxItemStations.Enabled = false;
				FormMain.Instance.comboBoxItemPages.Enabled = false;
			}
		}

		private void ChangeView()
		{
			if (SettingsManager.Instance.ClassicView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _classicToolTip);
			else if (SettingsManager.Instance.ListView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _listToolTip);
			else if (SettingsManager.Instance.AccordionView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _accordionToolTip);
			else
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _classicToolTip);
			if (_allowToSave)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading Page...";
					form.TopMost = true;
					var thread = new Thread(delegate()
					{
						FormMain.Instance.Invoke((MethodInvoker)delegate()
						{
							pnEmpty.BringToFront();
							Application.DoEvents();
							Application.DoEvents();
							DecoratorManager.Instance.ActivePackageViewer.UpdateView();
							Application.DoEvents();
							pnMain.BringToFront();
							Application.DoEvents();
						});
					});
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
			else
			{
				DecoratorManager.Instance.ActivePackageViewer.UpdateView();
			}
		}

		private void ApplySelectedDecorator()
		{
			if (DecoratorManager.Instance.ActivePackageViewer != null)
			{
				FormMain.Instance.ribbonBarStations.Text = DecoratorManager.Instance.ActivePackageViewer.Name;
				FormMain.Instance.ribbonBarStations.RecalcLayout();
				FormMain.Instance.ribbonPanelHome.PerformLayout();
				FormMain.Instance.ribbonBarSearchLogo.Text = DecoratorManager.Instance.ActivePackageViewer.Name;

				pnEmpty.BringToFront();
				Application.DoEvents();
				DecoratorManager.Instance.ActivePackageViewer.Apply();
				if (!pnRemoteLibraryContainer.Controls.Contains(DecoratorManager.Instance.ActivePackageViewer.Container))
					pnRemoteLibraryContainer.Controls.Add(DecoratorManager.Instance.ActivePackageViewer.Container);
				DecoratorManager.Instance.ActivePackageViewer.Container.BringToFront();
				pnMain.BringToFront();
				Application.DoEvents();

				if (DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary != null && !DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.UseDirectAccess)
				{
					FormMain.Instance.TabHome.barCheckItemViewTabs.Enabled = true;
					FormMain.Instance.buttonItemSettingsQuickViewImages.Enabled = true;

					FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
					FormMain.Instance.comboBoxItemPages.Visible = true;
					FormMain.Instance.ribbonBarStations.RecalcLayout();
				}
				else
				{
					FormMain.Instance.TabHome.barCheckItemViewTabs.Enabled = false;
					FormMain.Instance.TabHome.barCheckItemViewTabs.Enabled = false;
					FormMain.Instance.buttonItemSettingsQuickViewImages.Enabled = false;
					FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = false;
					FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = true;
					SettingsManager.Instance.OldStyleQuickView = true;
					SettingsManager.Instance.SaveSettings();

					FormMain.Instance.ribbonBarEmailBin.Enabled = false;
					FormMain.Instance.buttonItemEmailBin.Checked = false;
					FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
					FormMain.Instance.comboBoxItemPages.Visible = false;
					FormMain.Instance.ribbonBarStations.RecalcLayout();
				}
			}

			Application.DoEvents();
		}

		public void UpdateFontButtonStatus()
		{
			FormMain.Instance.TabHome.barButtonItemFontUp.Enabled = SettingsManager.Instance.FontSize < 20;
			FormMain.Instance.TabHome.barButtonItemFontDown.Enabled = SettingsManager.Instance.FontSize > 8;
		}

		public void UpdateRowSpaceButtonStatus()
		{
			FormMain.Instance.TabHome.barButtonItemRowSpaceUp.Enabled = SettingsManager.Instance.RowSpace < 3;
			FormMain.Instance.TabHome.barButtonItemRowSpaceDown.Enabled = SettingsManager.Instance.RowSpace > 1;
		}
		#endregion

		#region Ribbon Button's Click Event Handlers
		#region Wall Bin Button's Click Event Handlers
		public void buttonItemEmailBin_CheckedChanged(object sender, EventArgs e)
		{
			splitContainerControl.PanelVisibility = FormMain.Instance.buttonItemEmailBin.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : (FormMain.Instance.TabHome.barCheckItemViewClassic.Checked ? _classicToolTip : _listToolTip));
			SettingsManager.Instance.ShowEmailBin = FormMain.Instance.buttonItemEmailBin.Checked;
			SettingsManager.Instance.SaveSettings();
		}


		public void buttonItemHomeHelp_Click(object sender, EventArgs e)
		{
			if (SettingsManager.Instance.ClassicView)
			{
				HelpManager.Instance.OpenHelpLink(FormMain.Instance.buttonItemEmailBin.Checked ? "email" : "classic");
			}
			else if (SettingsManager.Instance.ListView)
			{
				HelpManager.Instance.OpenHelpLink(FormMain.Instance.buttonItemEmailBin.Checked ? "email" : "list");
			}
			else if (SettingsManager.Instance.AccordionView)
			{
				HelpManager.Instance.OpenHelpLink(FormMain.Instance.buttonItemEmailBin.Checked ? "email" : "accordion");
			}
			else
			{
				if (SettingsManager.Instance.SolutionTitleView)
					HelpManager.Instance.OpenHelpLink("title");
				else if (SettingsManager.Instance.SolutionTagsView)
					HelpManager.Instance.OpenHelpLink("target");
				else if (SettingsManager.Instance.SolutionDateView)
					HelpManager.Instance.OpenHelpLink("date");
			}
		}
		#endregion

		#region Settings Button's Click Event Handlers
		public void buttonItemSettingsLaunchPowerPoint_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsManager.Instance.LaunchPPT = FormMain.Instance.buttonItemSettingsLaunchPowerPoint.Checked;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsMultitab_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsManager.Instance.MultitabView = barCheckItemViewTabs.Checked;
				SettingsManager.Instance.SaveSettings();
				pnEmpty.BringToFront();
				StationChanged(FormMain.Instance.comboBoxItemStations);
				pnMain.BringToFront();
			}
		}

		public void buttonItemSettingsPowerPointSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsPowerPointSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsPowerPointViewer.Checked)
					SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsPowerPointMenu.Checked)
					SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsPowerPointLaunch.Checked)
					SettingsManager.Instance.PowerPointLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsPDFSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsPDFViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsPDFMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsPDFLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsPDFSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsPDFViewer.Checked)
					SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsPDFMenu.Checked)
					SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsPDFLaunch.Checked)
					SettingsManager.Instance.PDFLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsWordSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsWordViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsWordMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsWordLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsWordSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsWordViewer.Checked)
					SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsWordMenu.Checked)
					SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsWordLaunch.Checked)
					SettingsManager.Instance.WordLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsExcelSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsExcelViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsExcelMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsExcelLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsExcelSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsExcelViewer.Checked)
					SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsExcelMenu.Checked)
					SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsExcelLaunch.Checked)
					SettingsManager.Instance.ExcelLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsVideoSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsVideoViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsVideoMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsVideoLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsVideoSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsVideoViewer.Checked)
					SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsVideoMenu.Checked)
					SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsVideoLaunch.Checked)
					SettingsManager.Instance.VideoLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsFolderSettings_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsFolderViewer.Checked = false;
			FormMain.Instance.buttonItemSettingsFolderMenu.Checked = false;
			FormMain.Instance.buttonItemSettingsFolderLaunch.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsFolderSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemSettingsFolderViewer.Checked)
					SettingsManager.Instance.FolderLaunchOptions = LinkLaunchOptions.Viewer;
				else if (FormMain.Instance.buttonItemSettingsFolderMenu.Checked)
					SettingsManager.Instance.FolderLaunchOptions = LinkLaunchOptions.Menu;
				else if (FormMain.Instance.buttonItemSettingsFolderLaunch.Checked)
					SettingsManager.Instance.FolderLaunchOptions = LinkLaunchOptions.Launch;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsQuickView_Click(object sender, EventArgs e)
		{
			_allowToSave = false;
			FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = false;
			FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = false;
			_allowToSave = true;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemSettingsQuickViewSettings_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				SettingsManager.Instance.OldStyleQuickView = FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked;
				SettingsManager.Instance.SaveSettings();
			}
		}

		public void buttonItemSettingsEmail_Click(object sender, EventArgs e)
		{
			using (var form = new FormEmailSettings())
			{
				form.ShowDialog();
				LoadEmailBinOptions();
			}
		}

		public void buttonItemSettingsHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("settings");
		}
		#endregion
		#endregion

		#region Minibar Event Handlers
		public void ChangeView_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as BarCheckItem).Checked)
			{
				SettingsManager.Instance.ClassicView = barCheckItemViewClassic.Checked;
				SettingsManager.Instance.ListView = barCheckItemViewList.Checked;
				SettingsManager.Instance.AccordionView = barCheckItemViewAccordion.Checked;
				SettingsManager.Instance.SaveSettings();
				ChangeView();
			}
		}

		public void buttonItemLargerText_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.FontSize += 2;
			SettingsManager.Instance.SaveSettings();
			UpdateFontButtonStatus();
			if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < DecoratorManager.Instance.PackageViewers.Count)
				DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
		}

		public void buttonItemSmallerText_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.FontSize -= 2;
			SettingsManager.Instance.SaveSettings();
			UpdateFontButtonStatus();
			if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < DecoratorManager.Instance.PackageViewers.Count)
				DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
		}

		public void buttonItemLargerRowSpace_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.RowSpace++;
			SettingsManager.Instance.SaveSettings();
			UpdateRowSpaceButtonStatus();
			if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < DecoratorManager.Instance.PackageViewers.Count)
				DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
		}

		public void buttonItemSmallerRowSpace_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.RowSpace--;
			SettingsManager.Instance.SaveSettings();
			UpdateRowSpaceButtonStatus();
			if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < DecoratorManager.Instance.PackageViewers.Count)
				DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
		}


		private void TabHomeControl_Resize(object sender, EventArgs e)
		{
			barMinibar.BeginUpdate();
			barMinibar.Offset = (Width - 260) / 2;
			barMinibar.ApplyDockRowCol();
			barMinibar.EndUpdate();
		}
		#endregion

		#region Email Bin Methods and Event Handlers
		private void InitEmailBin()
		{
			LoadEmailBin();
			gridControlFiles.DataSource = new BindingList<LibraryLink>(_emailLinks.Keys.ToArray());
			LoadEmailBinOptions();
		}

		private void LoadEmailBin()
		{
			_emailLinks.Clear();
			if (File.Exists(_emailBinFileName))
			{
				var document = new XmlDocument();
				document.Load(_emailBinFileName);
				var node = document.SelectSingleNode(@"/EmailBin");
				if (node != null)
				{
					foreach (XmlNode childNode in node.ChildNodes)
					{
						if (childNode.Name.Equals("EmailLink"))
						{
							string path = string.Empty;
							var link = new LibraryLink(new LibraryFolder(new LibraryPage(DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library)));
							foreach (XmlNode emailLinkNode in childNode.ChildNodes)
							{
								switch (emailLinkNode.Name)
								{
									case "Link":
										link.Deserialize(emailLinkNode);
										break;
									case "Path":
										path = emailLinkNode.InnerText;
										break;
								}
								if (!string.IsNullOrEmpty(path) && File.Exists(path))
									_emailLinks.Add(link, path);
							}
						}
					}
				}
			}
		}

		private void LoadEmailBinOptions()
		{
			FormMain.Instance.ribbonBarEmailBin.Enabled = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayEmailBin) == EmailButtonsDisplayOptions.DisplayEmailBin;
			FormMain.Instance.buttonItemEmailBin.Checked = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayEmailBin) == EmailButtonsDisplayOptions.DisplayEmailBin ? SettingsManager.Instance.ShowEmailBin : false;
			buttonXPDF.Enabled = PowerPointHelper.Instance.PowerPointObject != null ? SettingsManager.Instance.EnablePdfConverting : false;
			buttonXPDF.CheckedChanged -= ckConvertPDF_CheckedChanged;
			buttonXPDF.Checked = SettingsManager.Instance.EnablePdfConverting ? SettingsManager.Instance.EmailBinSendAsPdf : false;
			buttonXPDF.CheckedChanged += ckConvertPDF_CheckedChanged;
			buttonXZip.CheckedChanged -= ckZip_CheckedChanged;
			buttonXZip.Checked = SettingsManager.Instance.EmailBinSendAsZip;
			buttonXZip.CheckedChanged += ckZip_CheckedChanged;
		}

		private void SaveEmailBin()
		{
			var xml = new StringBuilder();
			xml.AppendLine("<EmailBin>");
			foreach (var item in _emailLinks)
			{
				xml.AppendLine("<EmailLink>");
				xml.AppendLine(@"<Link>" + item.Key.Serialize() + @"</Link>");
				xml.AppendLine(@"<Path>" + item.Value.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Path>");
				xml.AppendLine(@"</EmailLink>");
			}
			xml.AppendLine(@"</EmailBin>");

			using (var sw = new StreamWriter(_emailBinFileName, false))
			{
				sw.Write(xml.ToString());
				sw.Flush();
			}
		}

		public void AddToEmailBin(LibraryLink link)
		{
			if (link != null)
			{
				if (!_emailLinks.Keys.Contains(link))
				{
					if (link.LinkAvailable)
					{
						LinkManager.Instance.RequestFile(link);
						_emailLinks.Add(link, link.LocalPath);
						SaveEmailBin();
						gridControlFiles.DataSource = new BindingList<LibraryLink>(_emailLinks.Keys.ToArray());
					}
					else
						AppManager.Instance.ShowWarning("File is not existed and cannot be added into Email Bin.\n Contact your system administrator");
				}
			}
		}

		private void ckConvertPDF_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.EmailBinSendAsPdf = buttonXPDF.Checked;
			SettingsManager.Instance.SaveSettings();
		}

		private void ckZip_CheckedChanged(object sender, EventArgs e)
		{
			SettingsManager.Instance.EmailBinSendAsZip = buttonXZip.Checked;
			SettingsManager.Instance.SaveSettings();
		}

		private void repositoryItemButtonEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to remove from email bin?") == DialogResult.Yes)
			{
				_emailLinks.Remove(_emailLinks.Keys.ElementAt(gridViewFiles.GetDataSourceRowIndex(gridViewFiles.FocusedRowHandle)));
				gridControlFiles.DataSource = new BindingList<LibraryLink>(_emailLinks.Keys.ToArray());
				SaveEmailBin();
			}
		}

		private void buttonXEmptyEmailBin_Click(object sender, EventArgs e)
		{
			if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to remove ALL from email bin?") == DialogResult.Yes)
			{
				gridControlFiles.DataSource = null;
				_emailLinks.Clear();
				gridControlFiles.DataSource = new BindingList<LibraryLink>(_emailLinks.Keys.ToArray());
				SaveEmailBin();
			}
		}

		private void buttonXCreateEmail_Click(object sender, EventArgs e)
		{
			SaveEmailBin();

			bool closePowerPoint = false;
			if (SettingsManager.Instance.EnablePdfConverting && !PowerPointHelper.Instance.IsLinkedWithApplication)
			{
				PowerPointHelper.Instance.Connect(true);
				closePowerPoint = true;
			}

			var emailFiles = new List<string>();
			if (buttonXPDF.Checked)
			{
				bool convertResult = true;
				foreach (var item in _emailLinks)
				{
					Application.DoEvents();
					switch (item.Key.Type)
					{
						case FileTypes.BuggyPresentation:
						case FileTypes.FriendlyPresentation:
						case FileTypes.Presentation:
							string pdfFileName = Path.Combine(AppManager.Instance.TempFolder.FullName, Path.GetFileNameWithoutExtension(item.Value) + ".pdf");
							if (PowerPointHelper.Instance.ConvertToPDF(item.Value, pdfFileName))
							{
								if (File.Exists(pdfFileName))
									emailFiles.Add(pdfFileName);
							}
							else
							{
								convertResult = false;
								emailFiles.Add(item.Value);
							}
							break;
						default:
							emailFiles.Add(item.Value);
							break;
					}
				}
				if (!convertResult)
					if (AppManager.Instance.ShowWarningQuestion("Some Power Point files were not converted to PDF.\nDo you want to send them in original format?") != DialogResult.Yes)
						return;
			}
			else
				emailFiles.AddRange(_emailLinks.Values.ToArray());

			if (emailFiles.Count > 0 && SettingsManager.Instance.EmailBinSendAsZip)
			{
				using (var form = new FormZipFileName())
				{
					if (form.ShowDialog() == DialogResult.OK)
					{
						string compressedFilesPath = Path.Combine(SettingsManager.Instance.TempPath, form.FileName + ".zip");
						Utils.CompressFiles(emailFiles.ToArray(), compressedFilesPath);
						emailFiles.Clear();
						emailFiles.Add(compressedFilesPath);
					}
					else
						emailFiles.Clear();
				}
			}
			if (emailFiles.Count > 0)
				LinkManager.Instance.EmailFile(emailFiles.ToArray());
			if (closePowerPoint)
				PowerPointHelper.Instance.Disconnect();
		}
		#endregion

		#region Comboboxes Event Handlers
		public void comboBoxItemPackages_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < DecoratorManager.Instance.PackageViewers.Count)
				{
					_allowToSave = false;
					SettingsManager.Instance.SelectedPackage = FormMain.Instance.comboBoxItemPackages.SelectedItem.ToString();
					SettingsManager.Instance.SaveSettings();
					DecoratorManager.Instance.ActivePackageViewer = DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];

					using (var form = new FormProgress())
					{
						form.laProgress.Text = string.Format("Loading {0}...", DecoratorManager.Instance.ActivePackageViewer != null ? DecoratorManager.Instance.ActivePackageViewer.Name : "Library");
						form.TopMost = true;
						var thread = new Thread(delegate()
													{
														FormMain.Instance.Invoke((MethodInvoker)delegate()
																									{
																										FormMain.Instance.TabSearch.ClearSolutionControl();
																										Application.DoEvents();
																										ApplySelectedDecorator();
																										Application.DoEvents();
																									});
													});
						form.Show();
						Application.DoEvents();
						thread.Start();
						while (thread.IsAlive)
							Application.DoEvents();
						form.Close();
					}
					_allowToSave = true;
				}
				else
					DecoratorManager.Instance.ActivePackageViewer = null;
			}
		}

		public void comboBoxItemStations_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = string.Format("Loading {0}...", DecoratorManager.Instance.ActivePackageViewer != null ? DecoratorManager.Instance.ActivePackageViewer.Name : "Library");
					form.TopMost = true;
					var thread = new Thread(delegate()
												{
													FormMain.Instance.Invoke((MethodInvoker)delegate()
																								{
																									pnEmpty.BringToFront();
																									Application.DoEvents();
																									StationChanged(sender);
																									Application.DoEvents();
																									pnMain.BringToFront();
																									Application.DoEvents();
																								});
												});
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
		}

		public void comboBoxItemPages_SelectedIndexChanged(object sender, EventArgs e)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading Page...";
				form.TopMost = true;
				var thread = new Thread(delegate()
											{
												FormMain.Instance.Invoke((MethodInvoker)delegate()
																							{
																								pnEmpty.BringToFront();
																								Application.DoEvents();
																								PageChanged(sender);
																								Application.DoEvents();
																								pnMain.BringToFront();
																								Application.DoEvents();
																							});
											});
				form.Show();
				Application.DoEvents();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
		}
		#endregion
	}
}