using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools.Ribbon;
using OutlookSalesDepotAddIn.Forms;

namespace OutlookSalesDepotAddIn
{
	public partial class RibbonMail
	{
		private void RibbonMail_Load(object sender, RibbonUIEventArgs e) { }

		private void buttonSelectFile_Click(object sender, RibbonControlEventArgs e)
		{
			FormMain.Instance.ShowDialog();
		}
	}
}
