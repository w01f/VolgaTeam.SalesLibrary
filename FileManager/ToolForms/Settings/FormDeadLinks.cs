using System;
using System.Windows.Forms;

namespace FileManager.ToolForms.Settings
{
    public partial class FormDeadLinks : Form
    {
        public BusinessClasses.Library Library { get; set; }
        
        public FormDeadLinks()
        {
            InitializeComponent();
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laAdminCaption.Font = new System.Drawing.Font(laAdminCaption.Font.FontFamily, laAdminCaption.Font.Size - 2, laAdminCaption.Font.Style);
                laMarkupAsBoldDescription.Font = new System.Drawing.Font(laMarkupAsBoldDescription.Font.FontFamily, laMarkupAsBoldDescription.Font.Size - 1, laMarkupAsBoldDescription.Font.Style);
                laMarkupAsLineBreakDescription.Font = new System.Drawing.Font(laMarkupAsLineBreakDescription.Font.FontFamily, laMarkupAsLineBreakDescription.Font.Size - 1, laMarkupAsLineBreakDescription.Font.Style);
                ckProcessDeadLinks.Font = new System.Drawing.Font(ckProcessDeadLinks.Font.FontFamily, ckProcessDeadLinks.Font.Size - 2, ckProcessDeadLinks.Font.Style);
                ckShowDeadLinksWarningDialog.Font = new System.Drawing.Font(ckShowDeadLinksWarningDialog.Font.FontFamily, ckShowDeadLinksWarningDialog.Font.Size - 2, ckShowDeadLinksWarningDialog.Font.Style);
                rbMarkupAsBold.Font = new System.Drawing.Font(rbMarkupAsBold.Font.FontFamily, rbMarkupAsBold.Font.Size - 2, rbMarkupAsBold.Font.Style);
                rbMarkupAsLineBreak.Font = new System.Drawing.Font(rbMarkupAsLineBreak.Font.FontFamily, rbMarkupAsLineBreak.Font.Size - 2, rbMarkupAsLineBreak.Font.Style);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            ckProcessDeadLinks.Checked = this.Library.EnableInactiveLinks;
            rbMarkupAsBold.Checked = this.Library.InactiveLinksBoldWarning;
            rbMarkupAsLineBreak.Checked = this.Library.ReplaceInactiveLinksWithLineBreak;
            ckShowDeadLinksWarningDialog.Checked = this.Library.InactiveLinksMessageAtStartup;
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.Library.EnableInactiveLinks = ckProcessDeadLinks.Checked;
                this.Library.InactiveLinksBoldWarning = rbMarkupAsBold.Checked;
                this.Library.ReplaceInactiveLinksWithLineBreak = rbMarkupAsLineBreak.Checked;
                this.Library.InactiveLinksMessageAtStartup = ckShowDeadLinksWarningDialog.Checked;
                this.Library.Save();
            }
        }

        private void ckProcessDeadLinks_CheckedChanged(object sender, EventArgs e)
        {
            gbDeadLinksMarkup.Enabled = ckProcessDeadLinks.Checked;
            ckShowDeadLinksWarningDialog.Enabled = ckProcessDeadLinks.Checked;
        }
    }
}
