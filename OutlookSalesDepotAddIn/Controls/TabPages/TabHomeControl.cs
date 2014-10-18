using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using OutlookSalesDepotAddIn.BusinessClasses;
using OutlookSalesDepotAddIn.Forms;
using SalesDepot.CommonGUI.Forms;

namespace OutlookSalesDepotAddIn.Controls.TabPages
{
	[ToolboxItem(false)]
	public partial class TabHomeControl : UserControl
	{
		public Utils.SingleParamDelegate StationChanged;
		private bool _allowToSave;

		public TabHomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			NeedToUpdate = true;
		}

		#region IController Methods
		public bool IsActive { get; set; }
		public bool NeedToUpdate { get; set; }

		public void InitController()
		{
			FormMain.Instance.comboBoxItemPackages.SelectedIndexChanged -= comboBoxItemPackages_SelectedIndexChanged;
			FormMain.Instance.comboBoxItemPackages.SelectedIndexChanged += comboBoxItemPackages_SelectedIndexChanged;
			FormMain.Instance.comboBoxItemStations.SelectedIndexChanged -= comboBoxItemStations_SelectedIndexChanged;
			FormMain.Instance.comboBoxItemStations.SelectedIndexChanged += comboBoxItemStations_SelectedIndexChanged;
			FormMain.Instance.buttonItemHomeAttach.Click -= buttonItemHomeAttach_Click;
			FormMain.Instance.buttonItemHomeAttach.Click += buttonItemHomeAttach_Click;
		}

		public void ShowTab()
		{
			IsActive = true;
			BringToFront();
		}
		#endregion

		#region Methods
		public void LoadTab()
		{
			_allowToSave = false;
			LoadPackages();
			ApplySelectedDecorator();
			ChangeView();
			_allowToSave = true;
		}

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
				var previousSelectedPackageIndex = FormMain.Instance.comboBoxItemPackages.Items.IndexOf(SettingsManager.Instance.SelectedPackage);
				FormMain.Instance.comboBoxItemPackages.SelectedIndex = previousSelectedPackageIndex >= 0 ? previousSelectedPackageIndex : 0;
				FormMain.Instance.comboBoxItemPackages.Enabled = FormMain.Instance.comboBoxItemPackages.Items.Count > 1;
				DecoratorManager.Instance.ActivePackageViewer = DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex];
			}
			else
			{
				FormMain.Instance.comboBoxItemPackages.Enabled = false;
				FormMain.Instance.comboBoxItemStations.Enabled = false;
			}
		}

		private void ChangeView()
		{
			if (_allowToSave)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading Page...";
					form.TopMost = true;
					var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate
					{
						pnEmpty.BringToFront();
						Application.DoEvents();
						DecoratorManager.Instance.ActivePackageViewer.UpdateView();
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}));
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
				FormMain.Instance.ribbonBarHomeStations.Text = DecoratorManager.Instance.ActivePackageViewer.Name;
				FormMain.Instance.ribbonBarHomeStations.RecalcLayout();
				FormMain.Instance.ribbonPanelHome.PerformLayout();

				pnEmpty.BringToFront();
				Application.DoEvents();
				DecoratorManager.Instance.ActivePackageViewer.Apply();
				if (!pnMain.Controls.Contains(DecoratorManager.Instance.ActivePackageViewer.Container))
					pnMain.Controls.Add(DecoratorManager.Instance.ActivePackageViewer.Container);
				DecoratorManager.Instance.ActivePackageViewer.Container.BringToFront();
				pnMain.BringToFront();
				Application.DoEvents();
				FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
				FormMain.Instance.ribbonBarHomeStations.RecalcLayout();
			}
			Application.DoEvents();
		}
		#endregion

		#region Ribbon Button's Click Event Handlers
		private void buttonItemHomeAttach_Click(object sender, EventArgs e)
		{
			if (DecoratorManager.Instance.ActivePackageViewer == null) return;
			var selectedPage = DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.SelectedPage;
			LinkManager.Instance.AttachFiles(selectedPage.GetSelectedLinks());
			selectedPage.ClearSelectedLinks();
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
						var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate()
						{
							ApplySelectedDecorator();
							Application.DoEvents();
						}));
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
					var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate()
					{
						pnEmpty.BringToFront();
						Application.DoEvents();
						if (StationChanged!=null)
							StationChanged(sender);
						Application.DoEvents();
						pnMain.BringToFront();
						Application.DoEvents();
					}));
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
	}
}