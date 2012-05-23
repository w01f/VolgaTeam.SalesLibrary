using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace SalesDepot.PresentationClasses.WallBin
{
    public partial class ClassicViewControl : UserControl, IWallBinView
    {
        private string _emailBinFileName = string.Empty;
        private Dictionary<BusinessClasses.LibraryFile, string> _emailLinks = new Dictionary<BusinessClasses.LibraryFile, string>();

        private DevComponents.DotNetBar.SuperTooltipInfo _classicToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Sales Library Column View", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _listToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Sales Library List View", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _emailToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about how to EMAIL files from this Sales Library", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        public ClassicViewControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            _emailBinFileName = string.Format(@"{0}\newlocaldirect.com\xml\Sales Depot\Settings\EmailBin.xml", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));

            if ((base.CreateGraphics()).DpiX > 96)
            {
                buttonXPDF.Font = new System.Drawing.Font(buttonXPDF.Font.FontFamily, buttonXPDF.Font.Size - 2, buttonXPDF.Font.Style);
                buttonXZip.Font = new System.Drawing.Font(buttonXZip.Font.FontFamily, buttonXZip.Font.Size - 2, buttonXZip.Font.Style);
                buttonXEmptyEmailBin.Font = new System.Drawing.Font(buttonXEmptyEmailBin.Font.FontFamily, buttonXEmptyEmailBin.Font.Size - 2, buttonXEmptyEmailBin.Font.Style);
                buttonXCreateEmail.Font = new System.Drawing.Font(buttonXCreateEmail.Font.FontFamily, buttonXCreateEmail.Font.Size - 2, buttonXCreateEmail.Font.Style);
            }
        }

        public void ApplyView()
        {
            FormMain.Instance.ribbonBarEmailBin.Visible = (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin;
            FormMain.Instance.ribbonBarEmailBin.BringToFront();
            FormMain.Instance.buttonItemEmailBin.Checked = (ConfigurationClasses.SettingsManager.Instance.EmailButtons & ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin) == ConfigurationClasses.EmailButtonsDisplayOptions.DisplayEmailBin ? ConfigurationClasses.SettingsManager.Instance.ShowEmailBin : false;
            FormMain.Instance.ribbonBarViewSettings.Visible = true;
            FormMain.Instance.ribbonBarViewSettings.BringToFront();

            FormMain.Instance.ribbonBarHomeSearchMode.Visible = false;
            FormMain.Instance.ribbonBarHomeAddSlide.Visible = false;

            FormMain.Instance.comboBoxItemStations.Visible = FormMain.Instance.comboBoxItemStations.Items.Count > 1;
            FormMain.Instance.comboBoxItemPages.Visible = true;
            FormMain.Instance.ribbonBarStations.RecalcLayout();

            FormMain.Instance.ribbonBarHomeHelp.Visible = true;
            FormMain.Instance.ribbonBarHomeHelp.BringToFront();
            FormMain.Instance.ribbonBarExit.Visible = true;
            FormMain.Instance.ribbonBarExit.BringToFront();

            if (ConfigurationClasses.SettingsManager.Instance.ClassicView)
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _classicToolTip);
            else if (ConfigurationClasses.SettingsManager.Instance.ListView)
                FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : _listToolTip);

            PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.ActivePackageViewer.UpdateView();
        }

        private void ClassicViewControl_Load(object sender, System.EventArgs e)
        {
            LoadEmailBin();
            gridControlFiles.DataSource = new BindingList<BusinessClasses.LibraryFile>(_emailLinks.Keys.ToArray());
            LoadOptions();
        }

        #region Wall Bin Methods and Event Handlers
        public void UpdateFontButtonStatus()
        {
            FormMain.Instance.buttonItemLargerText.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize < 20;
            FormMain.Instance.buttonItemSmallerText.Enabled = ConfigurationClasses.SettingsManager.Instance.FontSize > 8;
        }

        public void buttonItemLargerText_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize += 2;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            UpdateFontButtonStatus();
            if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers.Count)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
        }

        public void buttonItemSmallerText_Click(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.FontSize -= 2;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
            UpdateFontButtonStatus();
            if (FormMain.Instance.comboBoxItemPackages.SelectedIndex >= 0 && FormMain.Instance.comboBoxItemPackages.SelectedIndex < PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers.Count)
                PresentationClasses.WallBin.Decorators.DecoratorManager.Instance.PackageViewers[FormMain.Instance.comboBoxItemPackages.SelectedIndex].FormatWallBin();
        }

        public void buttonItemEmailBin_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerControl.PanelVisibility = FormMain.Instance.buttonItemEmailBin.Checked ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemHomeHelp, FormMain.Instance.buttonItemEmailBin.Checked ? _emailToolTip : (FormMain.Instance.buttonItemHomeClassicView.Checked ? _classicToolTip : _listToolTip));
            ConfigurationClasses.SettingsManager.Instance.ShowEmailBin = FormMain.Instance.buttonItemEmailBin.Checked;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();

        }
        #endregion

        #region Email Bin Methods and Event Handlers
        private void LoadEmailBin()
        {
            _emailLinks.Clear();
            if (File.Exists(_emailBinFileName))
            {
                XmlDocument document = new XmlDocument();
                document.Load(_emailBinFileName);
                XmlNode node = document.SelectSingleNode(@"/EmailBin");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Name.Equals("EmailLink"))
                        {
                            string path = string.Empty;
                            BusinessClasses.LibraryFile link = new BusinessClasses.LibraryFile(null);
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
                                if (!string.IsNullOrEmpty(path))
                                    _emailLinks.Add(link, path);
                            }
                        }
                    }
                }
            }
        }

        public void LoadOptions()
        {
            buttonXPDF.Enabled = InteropClasses.PowerPointHelper.Instance.PowerPointObject != null ? ConfigurationClasses.SettingsManager.Instance.EnablePdfConverting : false;
            buttonXPDF.CheckedChanged -= new EventHandler(ckConvertPDF_CheckedChanged);
            buttonXPDF.Checked = ConfigurationClasses.SettingsManager.Instance.EnablePdfConverting ? ConfigurationClasses.SettingsManager.Instance.EmailBinSendAsPdf : false;
            buttonXPDF.CheckedChanged += new EventHandler(ckConvertPDF_CheckedChanged);
            buttonXZip.CheckedChanged -= new EventHandler(ckZip_CheckedChanged);
            buttonXZip.Checked = ConfigurationClasses.SettingsManager.Instance.EmailBinSendAsZip;
            buttonXZip.CheckedChanged += new EventHandler(ckZip_CheckedChanged);
        }

        private void SaveEmailBin()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendLine("<EmailBin>");
            foreach (var item in _emailLinks)
            {
                xml.AppendLine("<EmailLink>");
                xml.AppendLine(@"<Link>" + item.Key.Serialize() + @"</Link>");
                xml.AppendLine(@"<Path>" + item.Value.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Path>");
                xml.AppendLine(@"</EmailLink>");
            }
            xml.AppendLine(@"</EmailBin>");

            using (StreamWriter sw = new StreamWriter(_emailBinFileName, false))
            {
                sw.Write(xml.ToString());
                sw.Flush();
            }
        }

        private void ckConvertPDF_CheckedChanged(object sender, System.EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.EmailBinSendAsPdf = buttonXPDF.Checked;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
        }

        private void ckZip_CheckedChanged(object sender, EventArgs e)
        {
            ConfigurationClasses.SettingsManager.Instance.EmailBinSendAsZip = buttonXZip.Checked;
            ConfigurationClasses.SettingsManager.Instance.SaveSettings();
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to remove from email bin?") == DialogResult.Yes)
            {
                _emailLinks.Remove(_emailLinks.Keys.ElementAt(gridViewFiles.GetDataSourceRowIndex(gridViewFiles.FocusedRowHandle)));
                gridControlFiles.DataSource = new BindingList<BusinessClasses.LibraryFile>(_emailLinks.Keys.ToArray());
                SaveEmailBin();
            }
        }

        private void gridControlFiles_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Serializable, true))
            {
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data != null)
                {
                    BusinessClasses.LibraryFile link = data as BusinessClasses.LibraryFile;
                    if (link != null)
                    {
                        if (!_emailLinks.Keys.Contains(link))
                        {
                            if (File.Exists(link.FullPath))
                            {
                                _emailLinks.Add(link, link.FullPath);
                                SaveEmailBin();
                                gridControlFiles.DataSource = new BindingList<BusinessClasses.LibraryFile>(_emailLinks.Keys.ToArray());
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
                var data = e.Data.GetData(DataFormats.Serializable, true);
                if (data.GetType() == typeof(BusinessClasses.LibraryFile))
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private void buttonXEmptyEmailBin_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("Are you sure you want to remove ALL from email bin?") == DialogResult.Yes)
            {
                gridControlFiles.DataSource = null;
                _emailLinks.Clear();
                gridControlFiles.DataSource = new BindingList<BusinessClasses.LibraryFile>(_emailLinks.Keys.ToArray());
                SaveEmailBin();
            }
        }

        private void buttonXCreateEmail_Click(object sender, EventArgs e)
        {
            SaveEmailBin();

            bool closePowerPoint = false;
            if (ConfigurationClasses.SettingsManager.Instance.EnablePdfConverting && !InteropClasses.PowerPointHelper.Instance.IsLinkedWithApplication)
            {
                InteropClasses.PowerPointHelper.Instance.Connect(true);
                closePowerPoint = true;
            }

            List<string> emailFiles = new List<string>();
            if (buttonXPDF.Checked)
            {
                bool convertResult = true;
                foreach (var item in _emailLinks)
                {
                    Application.DoEvents();
                    switch (item.Key.Type)
                    {
                        case BusinessClasses.FileTypes.BuggyPresentation:
                        case BusinessClasses.FileTypes.FriendlyPresentation:
                        case BusinessClasses.FileTypes.OtherPresentation:
                            string pdfFileName = Path.Combine(AppManager.Instance.TempFolder.FullName, Path.GetFileNameWithoutExtension(item.Value) + ".pdf");
                            if (InteropClasses.PowerPointHelper.Instance.ConvertToPDF(item.Value, pdfFileName))
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

            if (emailFiles.Count > 0 && ConfigurationClasses.SettingsManager.Instance.EmailBinSendAsZip)
            {
                using (ToolForms.WallBin.FormZipFileName form = new ToolForms.WallBin.FormZipFileName())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        string compressedFilesPath = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, form.FileName + ".zip");
                        BusinessClasses.LinkManager.CompressFiles(emailFiles.ToArray(), compressedFilesPath);
                        emailFiles.Clear();
                        emailFiles.Add(compressedFilesPath);
                    }
                    else
                        emailFiles.Clear();
                }
            }
            if (emailFiles.Count > 0)
                BusinessClasses.LinkManager.EmailFile(emailFiles.ToArray());
            if (closePowerPoint)
                InteropClasses.PowerPointHelper.Instance.Disconnect();
        }
        #endregion
    }
}
