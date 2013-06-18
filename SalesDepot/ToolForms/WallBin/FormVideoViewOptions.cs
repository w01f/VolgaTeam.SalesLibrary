using System;
using System.Drawing;
using System.Windows.Forms;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.ToolForms.WallBin
{
	public partial class FormVideoViewOptions : Form
	{
		#region VideoViewOptions enum
		public enum VideoViewOptions
		{
			Add = 0,
			Open,
			QuickSiteEmail,
			QuickSiteAdd
		}
		#endregion

		public FormVideoViewOptions()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				buttonXAddToPresentation.Font = new Font(buttonXAddToPresentation.Font.FontFamily, buttonXAddToPresentation.Font.Size - 3, buttonXAddToPresentation.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
				buttonXReview.Font = new Font(buttonXReview.Font.FontFamily, buttonXReview.Font.Size - 3, buttonXReview.Font.Style);
				buttonXQuickSiteAdd.Font = new Font(buttonXQuickSiteAdd.Font.FontFamily, buttonXQuickSiteAdd.Font.Size - 3, buttonXQuickSiteAdd.Font.Style);
				buttonXQuickSiteEmail.Font = new Font(buttonXQuickSiteEmail.Font.FontFamily, buttonXQuickSiteEmail.Font.Size - 3, buttonXQuickSiteEmail.Font.Style);
			}
		}

		public bool DoNotAdd { get; set; }
		public VideoViewOptions SelectedOption { get; private set; }

		private void VideoViewOptionsForm_Load(object sender, EventArgs e)
		{
			if (DoNotAdd)
				buttonXAddToPresentation.Enabled = false;
			buttonXQuickSiteAdd.Enabled = buttonXQuickSiteEmail.Enabled = SettingsManager.Instance.QBuilderSettings.AvailableHosts.Count > 0;
		}

		private void buttonXAddToPresentation_Click(object sender, EventArgs e)
		{
			SelectedOption = VideoViewOptions.Add;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXReview_Click(object sender, EventArgs e)
		{
			SelectedOption = VideoViewOptions.Open;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXQuickSiteEmail_Click(object sender, EventArgs e)
		{
			SelectedOption = VideoViewOptions.QuickSiteEmail;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXQuickSiteAdd_Click(object sender, EventArgs e)
		{
			SelectedOption = VideoViewOptions.QuickSiteAdd;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}