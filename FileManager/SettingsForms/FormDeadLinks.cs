using System;
using System.Windows.Forms;

namespace FileManager.SettingsForms
{
    public partial class FormDeadLinks : Form
    {
        public BusinessClasses.Library Library { get; set; }
        
        public FormDeadLinks()
        {
            InitializeComponent();
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
