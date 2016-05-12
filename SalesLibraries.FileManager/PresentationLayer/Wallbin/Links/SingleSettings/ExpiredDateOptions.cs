using System;
using System.Drawing;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	//public partial class ExpiredDateOptions : UserControl, ILinkProperties
	[IntendForClass(typeof(LibraryObjectLink))]
	public partial class ExpiredDateOptions : XtraTabPage, ILinkSettingsEditControl
	{
		private readonly LibraryObjectLink _data;

		public LinkSettingsType SettingsType => LinkSettingsType.ExpirationDate;
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public ExpiredDateOptions(LibraryObjectLink data)
		{
			InitializeComponent();
			_data = data;

			dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;
			if ((CreateGraphics()).DpiX > 96)
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

		public void LoadData()
		{
			laAddDateValue.Text = _data.AddDate.ToString("M/dd/yyyy h:mm:ss tt");
			dateEditExpirationDate.DateTime = _data.ExpirationSettings.ExpirationDate;
			timeEditExpirationTime.Time = _data.ExpirationSettings.ExpirationDate;
			checkBoxSendEmailWhenDelete.Checked = _data.ExpirationSettings.SendEmailOnSync;
			checkBoxLabelLink.Checked = _data.ExpirationSettings.MarkWhenExpired;
			checkBoxEnableExpiredLinks.Checked = _data.ExpirationSettings.Enable;
		}

		public void SaveData()
		{
			_data.ExpirationSettings.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year, dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour, timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
			_data.ExpirationSettings.SendEmailOnSync = checkBoxSendEmailWhenDelete.Checked;
			_data.ExpirationSettings.MarkWhenExpired = checkBoxLabelLink.Checked;
			_data.ExpirationSettings.Enable = checkBoxEnableExpiredLinks.Checked;
		}

		private void checkBoxEnableExpiredLinks_CheckedChanged(object sender, EventArgs e)
		{
			gbExpiredLinks.Enabled = checkBoxEnableExpiredLinks.Checked;
			if (checkBoxEnableExpiredLinks.Checked)
			{
				dateEditExpirationDate.DateTime = _data.ExpirationSettings.ExpirationDate;
				timeEditExpirationTime.Time = _data.ExpirationSettings.ExpirationDate;
			}
			else
			{
				dateEditExpirationDate.EditValue = DateTime.MinValue;
				timeEditExpirationTime.Time = DateTime.MinValue;
			}
		}
	}
}
