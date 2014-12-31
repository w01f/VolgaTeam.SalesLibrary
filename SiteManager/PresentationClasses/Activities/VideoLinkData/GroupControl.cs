using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;
using SalesDepot.Services.StatisticService;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.VideoLinkData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl
	public partial class GroupControl : XtraTabPage
	{
		public List<VideoLinkInfo> Records { get; private set; }

		private string _groupName;
		public string GroupName
		{
			get { return _groupName; }
			set
			{
				_groupName = String.IsNullOrEmpty(value) ? "No Group" : value;
				Text = _groupName;
			}
		}

		public GroupControl(IEnumerable<VideoLinkInfo> records)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<VideoLinkInfo>();
			Records.AddRange(records);
			gridControlData.DataSource = Records;
		}

		private void repositoryItemHyperLinkEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (advBandedGridViewData.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			if (e.Button.Index != 0) return;
			var videoLinkInfo = advBandedGridViewData.GetFocusedRow() as VideoLinkInfo;
			if (videoLinkInfo == null) return;
			Clipboard.SetText(advBandedGridViewData.FocusedColumn == gridColumnMp4Url ? videoLinkInfo.mp4Url : videoLinkInfo.thumbUrl);
		}
	}
}