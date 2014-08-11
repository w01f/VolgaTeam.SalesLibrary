using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ToolForms.Settings
{
	public partial class FormDeadLinks : MetroForm
	{
		public FormDeadLinks()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laAdminCaption.Font = new Font(laAdminCaption.Font.FontFamily, laAdminCaption.Font.Size - 2, laAdminCaption.Font.Style);
				laMarkupAsBoldDescription.Font = new Font(laMarkupAsBoldDescription.Font.FontFamily, laMarkupAsBoldDescription.Font.Size - 1, laMarkupAsBoldDescription.Font.Style);
				laMarkupAsLineBreakDescription.Font = new Font(laMarkupAsLineBreakDescription.Font.FontFamily, laMarkupAsLineBreakDescription.Font.Size - 1, laMarkupAsLineBreakDescription.Font.Style);
				ckProcessDeadLinks.Font = new Font(ckProcessDeadLinks.Font.FontFamily, ckProcessDeadLinks.Font.Size - 2, ckProcessDeadLinks.Font.Style);
				ckShowDeadLinksWarningDialog.Font = new Font(ckShowDeadLinksWarningDialog.Font.FontFamily, ckShowDeadLinksWarningDialog.Font.Size - 2, ckShowDeadLinksWarningDialog.Font.Style);
				rbMarkupAsBold.Font = new Font(rbMarkupAsBold.Font.FontFamily, rbMarkupAsBold.Font.Size - 2, rbMarkupAsBold.Font.Style);
				rbMarkupAsLineBreak.Font = new Font(rbMarkupAsLineBreak.Font.FontFamily, rbMarkupAsLineBreak.Font.Size - 2, rbMarkupAsLineBreak.Font.Style);
			}
		}

		public Library Library { get; set; }

		private void Form_Load(object sender, EventArgs e)
		{
			ckProcessDeadLinks.Checked = Library.EnableInactiveLinks;
			rbMarkupAsBold.Checked = Library.InactiveLinksBoldWarning;
			rbMarkupAsLineBreak.Checked = Library.ReplaceInactiveLinksWithLineBreak;
			ckShowDeadLinksWarningDialog.Checked = Library.InactiveLinksMessageAtStartup;
		}

		private void Form_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				Library.EnableInactiveLinks = ckProcessDeadLinks.Checked;
				Library.InactiveLinksBoldWarning = rbMarkupAsBold.Checked;
				Library.ReplaceInactiveLinksWithLineBreak = rbMarkupAsLineBreak.Checked;
				Library.InactiveLinksMessageAtStartup = ckShowDeadLinksWarningDialog.Checked;
				Library.Save();
			}
		}

		private void ckProcessDeadLinks_CheckedChanged(object sender, EventArgs e)
		{
			gbDeadLinksMarkup.Enabled = ckProcessDeadLinks.Checked;
			ckShowDeadLinksWarningDialog.Enabled = ckProcessDeadLinks.Checked;
		}
	}
}