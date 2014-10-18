using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using OvernightsCalendarViewer.BusinessClasses;
using OvernightsCalendarViewer.ConfigurationClasses;
using OvernightsCalendarViewer.PresentationClasses.Decorators;
using SalesDepot.CommonGUI.Forms;

namespace OvernightsCalendarViewer.TabPages
{
	[ToolboxItem(false)]
	public partial class TabOvernightsCalendarControl : UserControl, IController
	{
		private bool _allowToSave;

		public AppManager.SingleParamDelegate StationChanged;

		public TabOvernightsCalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.comboBoxItemPackages.SelectedIndexChanged += comboBoxItemPackages_SelectedIndexChanged;
			FormMain.Instance.comboBoxItemStations.SelectedIndexChanged += comboBoxItemStations_SelectedIndexChanged;
			FormMain.Instance.labelItemCalendarDisclaimerLogo.Click += buttonItemCalendarDisclaimer_Click;
			FormMain.Instance.buttonItemCalendarFontSizeLarger.Click += buttonItemCalendarFontLarger_Click;
			FormMain.Instance.buttonItemCalendarFontSizeSmaler.Click += buttonItemCalendarFontSmaller_Click;
			FormMain.Instance.buttonItemCalendarHelp.Click += buttonItemHelp_Click;
		}

		public void LoadTab()
		{
			_allowToSave = false;
			LoadPackages();
			ApplySelectedDecorator();
			_allowToSave = true;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
		}
		#endregion


		#region Methods

		private void LoadPackages()
		{
			FormMain.Instance.comboBoxItemPackages.Items.Clear();
			foreach (var salesDepot in LibraryManager.Instance.LibraryPackageCollection)
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
			}
		}

		private void ApplySelectedDecorator()
		{
			if (DecoratorManager.Instance.ActivePackageViewer == null) return;
			FormMain.Instance.ribbonBarStations.Text = DecoratorManager.Instance.ActivePackageViewer.Name;
			FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
			DecoratorManager.Instance.ActivePackageViewer.Apply();
			DecoratorManager.Instance.ActivePackageViewer.Container.BringToFront();
			FormMain.Instance.ribbonBarStations.RecalcLayout();
			Application.DoEvents();
		}
		#endregion


		#region Comboboxes Event Handlers
		public void comboBoxItemPackages_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
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
					var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)ApplySelectedDecorator));
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

		public void comboBoxItemStations_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = string.Format("Loading {0}...", DecoratorManager.Instance.ActivePackageViewer != null ? DecoratorManager.Instance.ActivePackageViewer.Name : "Library");
					form.TopMost = true;
					var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)(() => StationChanged(sender))));
					form.Show();
					Application.DoEvents();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
			}
		}
		#endregion


		public void buttonItemCalendarDisclaimer_Click(object sender, EventArgs e)
		{
			if (File.Exists(SettingsManager.Instance.DisclaimerPath))
				Process.Start(SettingsManager.Instance.DisclaimerPath);
		}

		public void buttonItemCalendarFontLarger_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.CalendarFontSize++;
			SettingsManager.Instance.SaveSettings();
			if (DecoratorManager.Instance.ActivePackageViewer != null)
				DecoratorManager.Instance.ActivePackageViewer.FormatCalendar();
		}

		public void buttonItemCalendarFontSmaller_Click(object sender, EventArgs e)
		{
			SettingsManager.Instance.CalendarFontSize--;
			SettingsManager.Instance.SaveSettings();
			if (DecoratorManager.Instance.ActivePackageViewer != null)
				DecoratorManager.Instance.ActivePackageViewer.FormatCalendar();
		}

		public void buttonItemHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("overnights");
		}
	}
}