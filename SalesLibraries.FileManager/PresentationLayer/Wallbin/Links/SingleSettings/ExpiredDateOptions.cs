using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings
{
	[IntendForClass(typeof(LibraryObjectLink))]
	//public partial class ExpiredDateOptions : UserControl, ILinkSetSettingsEditControl
	public partial class ExpiredDateOptions : XtraTabPage, ILinkSetSettingsEditControl
	{
		private readonly List<LibraryObjectLink> _sourceLinks = new List<LibraryObjectLink>();

		private LibraryObjectLink DefaultLink => _sourceLinks.First();

		public LinkSettingsType[] SupportedSettingsTypes => new[] { LinkSettingsType.ExpirationDate };
		public int Order => 0;
		public bool AvailableForEmbedded => false;
		public SettingsEditorHeaderInfo HeaderInfo => null;

		public event EventHandler<EventArgs> ForceCloseRequested;

		public ExpiredDateOptions()
		{
			InitializeComponent();

			dateEditExpirationDate.Properties.NullDate = DateTime.MinValue;
		}

		public ExpiredDateOptions(FileTypes? defaultLinkType = null) : this() { }

		public ExpiredDateOptions(ILinksGroup linksGroup, FileTypes? defaultLinkType = null) : this() { }

		public void LoadData(BaseLibraryLink sourceLink)
		{
			_sourceLinks.Clear();
			_sourceLinks.Add((LibraryObjectLink)sourceLink);

			LoadData();
		}

		public void LoadData(IEnumerable<BaseLibraryLink> sourceLinks)
		{
			_sourceLinks.Clear();
			_sourceLinks.AddRange(sourceLinks.OfType<LibraryObjectLink>());

			LoadData();
		}

		private void LoadData()
		{
			labelControlDateAdd.Text = String.Format(labelControlDateAdd.Text, DefaultLink.AddDate.ToString("M/dd/yy"));
			dateEditExpirationDate.DateTime = DefaultLink.ExpirationSettings.ExpirationDate;
			timeEditExpirationTime.Time = DefaultLink.ExpirationSettings.ExpirationDate;
			checkEditSendEmailWhenDelete.Checked = DefaultLink.ExpirationSettings.SendEmailOnSync;
			checkEditLabelLink.Checked = DefaultLink.ExpirationSettings.MarkWhenExpired;
			checkEditEnableExpiration.Checked = DefaultLink.ExpirationSettings.Enable;
		}

		public void SaveData()
		{
			if (!_sourceLinks.Any()) return;
			foreach (var link in _sourceLinks)
			{
				link.ExpirationSettings.ExpirationDate = new DateTime(dateEditExpirationDate.DateTime.Year,
					dateEditExpirationDate.DateTime.Month, dateEditExpirationDate.DateTime.Day, timeEditExpirationTime.Time.Hour,
					timeEditExpirationTime.Time.Minute, timeEditExpirationTime.Time.Second);
				link.ExpirationSettings.SendEmailOnSync = checkEditSendEmailWhenDelete.Checked;
				link.ExpirationSettings.MarkWhenExpired = checkEditLabelLink.Checked;
				link.ExpirationSettings.Enable = checkEditEnableExpiration.Checked;
			}
		}

		private void checkBoxEnableExpiredLinks_CheckedChanged(object sender, EventArgs e)
		{
			layoutControlItemDateAddLabel.Enabled =
				layoutControlItemExpirationDate.Enabled =
					layoutControlItemExpirationTime.Enabled =
						layoutControlItemExpiredDateActions.Enabled =
							layoutControlItemLabelLink.Enabled =
								layoutControlItemSendEmailWhenDelete.Enabled = checkEditEnableExpiration.Checked;
			if (checkEditEnableExpiration.Checked)
			{
				checkEditEnableExpiration.ForeColor = Color.Black;
				dateEditExpirationDate.ForeColor = Color.Black;
				timeEditExpirationTime.ForeColor = Color.Black;
				dateEditExpirationDate.DateTime = DefaultLink.ExpirationSettings.ExpirationDate;
				timeEditExpirationTime.Time = DefaultLink.ExpirationSettings.ExpirationDate;
			}
			else
			{
				checkEditEnableExpiration.ForeColor = Color.Gray;
				dateEditExpirationDate.ForeColor = Color.Gray;
				timeEditExpirationTime.ForeColor = Color.Gray;
				dateEditExpirationDate.EditValue = DateTime.MinValue;
				timeEditExpirationTime.Time = DateTime.MinValue;
			}
		}
	}
}
