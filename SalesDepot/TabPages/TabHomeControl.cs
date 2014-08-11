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
using SalesDepot.ToolForms.WallBin;

namespace SalesDepot.TabPages
{
	[ToolboxItem(false)]
	public partial class TabHomeControl : UserControl, IController
	{
		private readonly SuperTooltipInfo _accordionToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Accordion View", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _classicToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Column View", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _listToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library List View", null, null, eTooltipColor.Gray);
		private readonly string _emailBinFileName = string.Empty;
		private readonly Dictionary<LibraryLink, string> _emailLinks = new Dictionary<LibraryLink, string>();

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

			barCheckItemViewTabs.CheckedChanged += buttonItemSettingsMultitab_CheckedChanged;

			LoadWallBinSettings();
			UpdateFontButtonStatus();
			UpdateRowSpaceButtonStatus();
		}

		public void ShowTab()
		{
			LoadEmailBinOptions();
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
			barCheckItemViewTabs.Checked = SettingsManager.Instance.MultitabView;
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
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _classicToolTip);
			else if (SettingsManager.Instance.ListView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _listToolTip);
			else if (SettingsManager.Instance.AccordionView)
				FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _accordionToolTip);
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
					FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
					FormMain.Instance.comboBoxItemPages.Visible = true;
					FormMain.Instance.ribbonBarStations.RecalcLayout();
				}
				else
				{
					FormMain.Instance.TabHome.barCheckItemViewTabs.Enabled = false;
					FormMain.Instance.TabHome.barCheckItemViewTabs.Enabled = false;
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
			SettingsManager.Instance.ShowEmailBin = FormMain.Instance.buttonItemEmailBin.Checked;
			SettingsManager.Instance.SaveSettings();
		}


		public void buttonItemHomeHelp_Click(object sender, EventArgs e)
		{
			if (SettingsManager.Instance.ClassicView)
			{
				HelpManager.Instance.OpenHelpLink("classic");
			}
			else if (SettingsManager.Instance.ListView)
			{
				HelpManager.Instance.OpenHelpLink("list");
			}
			else if (SettingsManager.Instance.AccordionView)
			{
				HelpManager.Instance.OpenHelpLink("accord");
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
		public void buttonItemSettingsMultitab_CheckedChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			SettingsManager.Instance.MultitabView = barCheckItemViewTabs.Checked;
			SettingsManager.Instance.SaveSettings();
			pnEmpty.BringToFront();
			StationChanged(FormMain.Instance.comboBoxItemStations);
			pnMain.BringToFront();
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
			UpdateEmailBinButtons();
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
			FormMain.Instance.buttonItemEmailBin.Checked = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayEmailBin) == EmailButtonsDisplayOptions.DisplayEmailBin && SettingsManager.Instance.ShowEmailBin;
			buttonXPDF.Enabled = PowerPointHelper.Instance.PowerPointObject != null && SettingsManager.Instance.EnablePdfConverting;
			buttonXPDF.CheckedChanged -= ckConvertPDF_CheckedChanged;
			buttonXPDF.Checked = SettingsManager.Instance.EnablePdfConverting && SettingsManager.Instance.EmailBinSendAsPdf;
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
						UpdateEmailBinButtons();
						gridControlFiles.DataSource = new BindingList<LibraryLink>(_emailLinks.Keys.ToArray());
					}
					else
						AppManager.Instance.ShowWarning("File is not existed and cannot be added into Email Bin.\n Contact your system administrator");
				}
			}
		}

		private void UpdateEmailBinButtons()
		{
			buttonXCreateEmail.Enabled = _emailLinks.Count > 0;
			buttonXEmptyEmailBin.Enabled = _emailLinks.Count > 0;
			buttonXPDF.Enabled = _emailLinks.Count > 0;
			buttonXZip.Enabled = _emailLinks.Count > 0;
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
				UpdateEmailBinButtons();
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
				UpdateEmailBinButtons();
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

		private void pictureBoxHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("email");
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
							FormMain.Instance.TabSearch.ClearSolutionControl();
							Application.DoEvents();
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

		public void comboBoxItemPages_SelectedIndexChanged(object sender, EventArgs e)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Loading Page...";
				form.TopMost = true;
				var thread = new Thread(() => FormMain.Instance.Invoke((MethodInvoker)delegate()
				{
					pnEmpty.BringToFront();
					Application.DoEvents();
					PageChanged(sender);
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
		#endregion

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}