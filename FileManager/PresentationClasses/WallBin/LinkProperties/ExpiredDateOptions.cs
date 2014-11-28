using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.LinkProperties
{
	//public partial class ExpiredDateOptions : UserControl, ILinkProperties
	public partial class ExpiredDateOptions : XtraTabPage, ILinkProperties
	{
		private readonly LibraryLink _data;

		public event EventHandler OnForseClose;

		public ExpiredDateOptions(LibraryLink data)
		{
			InitializeComponent();
			_data = data;
			LoadData();

			dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laAddDateTitle.Font = new Font(laAddDateTitle.Font.FontFamily, laAddDateTitle.Font.Size - 2, laAddDateTitle.Font.Style);
				laAddDateValue.Font = new Font(laAddDateValue.Font.FontFamily, laAddDateValue.Font.Size - 2, laAddDateValue.Font.Style);
				laExpirationDateTitle.Font = new Font(laExpirationDateTitle.Font.FontFamily, laExpirationDateTitle.Font.Size - 2, laExpirationDateTitle.Font.Style);
				laExpireddateActions.Font = new Font(laExpireddateActions.Font.FontFamily, laExpireddateActions.Font.Size - 2, laExpireddateActions.Font.Style);
				checkBoxLabelLink.Font = new Font(checkBoxLabelLink.Font.FontFamily, checkBoxLabelLink.Font.Size - 2, checkBoxLabelLink.Font.Style);
				checkBoxEnableExpiredLinks.Font = new Font(checkBoxEnableExpiredLinks.Font.FontFamily, checkBoxEnableExpiredLinks.Font.Size - 2, checkBoxEnableExpiredLinks.Font.Style);
				checkBoxSendEmailWhenDelete.Font = new Font(checkBoxSendEmailWhenDelete.Font.FontFamily, checkBoxSendEmailWhenDelete.Font.Size - 2, checkBoxSendEmailWhenDelete.Font.Style);
			}
		}

		private void LoadData()
		{
			laAddDateValue.Text = _data.AddDate.ToString("M/dd/yyyy h:mm:ss tt");
			dateEditExpirationDate.DateTime = _data.ExpirationDateOptions.ExpirationDate;
			timeEditExpirationTime.Time = _data.ExpirationDateOptions.ExpirationDate;
			checkBoxSendEmailWhenDelete.Checked = _data.ExpirationDateOptions.SendEmailWhenSync;
			checkBoxLabelLink.Checked = _data.ExpirationDateOptions.LabelLinkWhenExpired;
			checkBoxEnableExpiredLinks.Checked = _data.ExpirationDateOptions.EnableExpirationDate;
		}

		public void SaveData()
		{
			_data.ExpirationDateOptions.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year, dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour, timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
			_data.ExpirationDateOptions.SendEmailWhenSync = checkBoxSendEmailWhenDelete.Checked;
			_data.ExpirationDateOptions.LabelLinkWhenExpired = checkBoxLabelLink.Checked;
			_data.ExpirationDateOptions.EnableExpirationDate = checkBoxEnableExpiredLinks.Checked;
		}

		private void checkBoxEnableExpiredLinks_CheckedChanged(object sender, EventArgs e)
		{
			gbExpiredLinks.Enabled = checkBoxEnableExpiredLinks.Checked;
			if (checkBoxEnableExpiredLinks.Checked)
			{
				dateEditExpirationDate.DateTime = _data.ExpirationDateOptions.ExpirationDate;
				timeEditExpirationTime.Time = _data.ExpirationDateOptions.ExpirationDate;
			}
			else
			{
				dateEditExpirationDate.EditValue = DateTime.MinValue;
				timeEditExpirationTime.Time = DateTime.MinValue;
			}
		}
	}
}
