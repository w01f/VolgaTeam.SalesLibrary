using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SalesDepot.BusinessClasses;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.BusinessClasses;
using SalesDepot.CoreObjects.ToolClasses;
using SalesDepot.InteropClasses;
using SalesDepot.PresentationClasses.WallBin.Decorators;
using SalesDepot.ToolForms.WallBin;

namespace SalesDepot.PresentationClasses.WallBin
{
	public partial class ClassicViewControl : UserControl, IWallBinView
	{
		private readonly SuperTooltipInfo _accordionToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Accordion View", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _classicToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Column View", null, null, eTooltipColor.Gray);
		private readonly string _emailBinFileName = string.Empty;
		private readonly Dictionary<LibraryLink, string> _emailLinks = new Dictionary<LibraryLink, string>();

		private readonly SuperTooltipInfo _emailToolTip = new SuperTooltipInfo("HELP", "", "Learn more about how to EMAIL files from this Sales Library", null, null, eTooltipColor.Gray);
		private readonly SuperTooltipInfo _listToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Sales Library List View", null, null, eTooltipColor.Gray);

		public ClassicViewControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_emailBinFileName = string.Format(@"{0}\newlocaldirect.com\xml\Sales Depot\Settings\EmailBin.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXPDF.Font = new Font(buttonXPDF.Font.FontFamily, buttonXPDF.Font.Size - 2, buttonXPDF.Font.Style);
				buttonXZip.Font = new Font(buttonXZip.Font.FontFamily, buttonXZip.Font.Size - 2, buttonXZip.Font.Style);
				buttonXEmptyEmailBin.Font = new Font(buttonXEmptyEmailBin.Font.FontFamily, buttonXEmptyEmailBin.Font.Size - 2, buttonXEmptyEmailBin.Font.Style);
				buttonXCreateEmail.Font = new Font(buttonXCreateEmail.Font.FontFamily, buttonXCreateEmail.Font.Size - 2, buttonXCreateEmail.Font.Style);
			}
		}

		#region IWallBinView Members
		public void ApplyView()
		{
			if (DecoratorManager.Instance.ActivePackageViewer != null)
			{
				if (DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary != null && !DecoratorManager.Instance.ActivePackageViewer.SelectedLibrary.Library.UseDirectAccess)
				{
					FormMain.Instance.ribbonBarHomeView.Enabled = true;
					FormMain.Instance.buttonItemSettingsMultitab.Enabled = true;
					FormMain.Instance.buttonItemSettingsQuickViewImages.Enabled = true;

					FormMain.Instance.ribbonBarEmailBin.Enabled = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayEmailBin) == EmailButtonsDisplayOptions.DisplayEmailBin;
					FormMain.Instance.buttonItemEmailBin.Checked = (SettingsManager.Instance.EmailButtons & EmailButtonsDisplayOptions.DisplayEmailBin) == EmailButtonsDisplayOptions.DisplayEmailBin ? SettingsManager.Instance.ShowEmailBin : false;
					FormMain.Instance.ribbonBarViewSettings.Enabled = true;

					FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
					FormMain.Instance.comboBoxItemPages.Visible = true;
					FormMain.Instance.ribbonBarStations.RecalcLayout();
				}
				else
				{
					FormMain.Instance.ribbonBarHomeView.Enabled = false;
					FormMain.Instance.buttonItemSettingsMultitab.Enabled = false;
					FormMain.Instance.buttonItemSettingsMultitab.Checked = false;
					FormMain.Instance.buttonItemSettingsQuickViewImages.Enabled = false;
					FormMain.Instance.buttonItemSettingsQuickViewImages.Checked = false;
					FormMain.Instance.buttonItemSettingsQuickViewSlides.Checked = true;
					SettingsManager.Instance.OldStyleQuickView = true;
					SettingsManager.Instance.SaveSettings();

					FormMain.Instance.ribbonBarEmailBin.Enabled = false;
					FormMain.Instance.buttonItemEmailBin.Checked = false;
					FormMain.Instance.ribbonBarViewSettings.Enabled = false;
					FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
					FormMain.Instance.comboBoxItemPages.Visible = false;
					FormMain.Instance.ribbonBarStations.RecalcLayout();
				}
				if (SettingsManager.Instance.ClassicView)
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _classicToolTip);
				else if (SettingsManager.Instance.ListView)
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _listToolTip);
				else if (SettingsManager.Instance.AccordionView)
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _accordionToolTip);
				else
					FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, _classicToolTip);
			}
		}
		#endregion

		private void ClassicViewControl_Load(object sender, EventArgs e)
		{
			LoadEmailBin();
			gridControlFiles.DataSource = new BindingList<LibraryLink>(_emailLinks.Keys.ToArray());
			LoadOptions();
		}

		#region Wall Bin Methods and Event Handlers
		public void UpdateFontButtonStatus()
		{
			FormMain.Instance.buttonItemLargerText.Enabled = SettingsManager.Instance.FontSize < 20;
			FormMain.Instance.buttonItemSmallerText.Enabled = SettingsManager.Instance.FontSize > 8;
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

		public void buttonItemEmailBin_CheckedChanged(object sender, EventArgs e)
		{
			splitContainerControl.PanelVisibility = FormMain.Instance.buttonItemEmailBin.Checked ? SplitPanelVisibility.Both : SplitPanelVisibility.Panel2;
			FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : (FormMain.Instance.buttonItemHomeClassicView.Checked ? _classicToolTip : _listToolTip));
			SettingsManager.Instance.ShowEmailBin = FormMain.Instance.buttonItemEmailBin.Checked;
			SettingsManager.Instance.SaveSettings();
		}
		#endregion

		#region Email Bin Methods and Event Handlers
		private void LoadEmailBin()
		{
			_emailLinks.Clear();
			if (File.Exists(_emailBinFileName))
			{
				var document = new XmlDocument();
				document.Load(_emailBinFileName);
				XmlNode node = document.SelectSingleNode(@"/EmailBin");
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

		public void LoadOptions()
		{
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

		private void gridControlFiles_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Serializable, true))
			{
				object data = e.Data.GetData(DataFormats.Serializable, true);
				if (data != null)
				{
					var link = data as LibraryLink;
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
			}
		}

		private void gridControlFiles_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Serializable, true))
			{
				object data = e.Data.GetData(DataFormats.Serializable, true);
				if (data.GetType() == typeof(LibraryLink))
					e.Effect = DragDropEffects.Copy;
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
	}
}